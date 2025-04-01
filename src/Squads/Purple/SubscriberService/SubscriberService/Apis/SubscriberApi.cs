using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SubscriberService.Application.Services;
using SubscriberService.Domain.Dtos;
using SubscriberService.Domain.Exceptions;
using SubscriberService.Domain.Models;

namespace SubscriberService.Apis;

public static class SubscriberApi
{
    public static RouteGroupBuilder AddSubscriberApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/v1/subscribe");

        api.MapGet("/{subscriptionType}", GetSubscribers)
            .WithName("Get all subscribers for a subscription type");

        api.MapGet("/", GetSubscriptionsForUser)
            .WithName("Get subscriptions for a user by their email");

        api.MapPost("/", CreateSubscription)
            .WithName("Create subscription");

        api.MapDelete("/{subscriptionId:guid}", DeleteSubscription)
            .WithName("Delete subscription");

        return api;
    }

    private static async Task<Results<Ok<List<Subscriber>>, ProblemHttpResult>> GetSubscribers(
        [FromRoute] string subscriptionType, [FromServices] ISubscriberService subscriberService,
        CancellationToken cancellationToken)
    {
        try
        {
            var subscribers = await subscriberService.GetSubscribersForSubscriptionTypeAsync(subscriptionType);
            return TypedResults.Ok(subscribers);
        }
        catch (NotFoundException e)
        {
            Log.Error("Subscription type not found: {SubscriptionType}", subscriptionType);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception e)
        {
            Log.Error("An error occurred while getting subscribers: {Error}", e.Message);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<Results<Ok<List<GetSubscriptionDto>>, ProblemHttpResult>> GetSubscriptionsForUser(
        [FromQuery] string email, [FromServices] ISubscriberService subscriberService,
        CancellationToken cancellationToken)
    {
        var subscriptions = await subscriberService.GetSubscriptionsByEmailAsync(email);
        var dtos = subscriptions.Select(s => new GetSubscriptionDto
        {
            Id = s.Id,
            Email = s.Subscriber.Email,
            SubscriptionType = s.SubscriptionType.Type
        }).ToList();
        return TypedResults.Ok(dtos);
    }

    private static async Task<Results<Ok<GetSubscriptionDto>, ProblemHttpResult>> CreateSubscription(
        [FromBody] CreateSubscriptionDto request, [FromServices] ISubscriberService subscriberService,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await subscriberService.SubscribeAsync(request);
            var dto = new GetSubscriptionDto
            {
                Id = result.Id,
                Email = result.Subscriber.Email,
                SubscriptionType = result.SubscriptionType.Type
            };
            return TypedResults.Ok(dto);
        }
        catch (NotFoundException e)
        {
            Log.Error("Subscription type not found: {SubscriptionType}", request.SubscriptionType);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (BadRequestException e)
        {
            Log.Error("Bad request: {Error}", e.Message);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        } 
        catch (Exception e)
        {
            Log.Error("An error occurred while creating subscription: {Error}", e.Message);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<Results<Ok, ProblemHttpResult>> DeleteSubscription([FromRoute] Guid subscriptionId,
        [FromServices] ISubscriberService subscriberService, CancellationToken cancellationToken)
    {
        if (subscriptionId == Guid.Empty)
        {
            return TypedResults.Problem("Invalid subscriber ID");
        }

        try
        {
            await subscriberService.UnsubscribeAsync(subscriptionId);
            return TypedResults.Ok();
        }
        catch (NotFoundException e)
        {
            Log.Error("Subscription not found: {SubscriptionId}", subscriptionId);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception e)
        {
            Log.Error("An error occurred while deleting subscription: {Error}", e.Message);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}