using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SubscriberService.Application.Services;
using SubscriberService.Models;

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
        var subscribers = await subscriberService.GetSubscribersForSubscriptionTypeAsync(subscriptionType);
        if (subscribers.Count == 0)
        {
            return TypedResults.Problem("No subscribers found for the given subscription type");
        }

        return TypedResults.Ok(subscribers);
    }

    private static async Task<Results<Ok<List<Subscription>>, ProblemHttpResult>> GetSubscriptionsForUser(
        [FromQuery] string email, [FromServices] ISubscriberService subscriberService,
        CancellationToken cancellationToken)
    {
        var subscriptions = await subscriberService.GetSubscriptionsByEmailAsync(email);
        if (subscriptions.Count == 0)
        {
            return TypedResults.Problem("No subscriptions found for the given email");
        }

        return TypedResults.Ok(subscriptions);
    }

    private static async Task<Results<Ok, ProblemHttpResult>> CreateSubscription(
        [FromBody] CreateSubscriptionDto request, [FromServices] ISubscriberService subscriberService,
        CancellationToken cancellationToken)
    {
        await subscriberService.SubscribeAsync(request);
        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, ProblemHttpResult>> DeleteSubscription([FromRoute] Guid subscriptionId,
        [FromServices] ISubscriberService subscriberService, CancellationToken cancellationToken)
    {
        if (subscriptionId == Guid.Empty)
        {
            return TypedResults.Problem("Invalid subscriber ID");
        }

        await subscriberService.UnsubscribeAsync(subscriptionId);
        return TypedResults.Ok();
    }
}