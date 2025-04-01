using Microsoft.AspNetCore.Http.HttpResults;

namespace SubscriberService.Apis;

public static class SubscriberApi
{
    public static RouteGroupBuilder AddSubscriberApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("Subscriber");

        api.MapGet("/", GetSubscribers)
            .WithName("Get Tenants");

        return api;
    }

    public static async Task<Results<Ok<List<dynamic>>, ProblemHttpResult>> GetSubscribers(CancellationToken cancellationToken)
    {
        var subscribers = new List<dynamic>();

        return TypedResults.Ok(subscribers);
    }
}