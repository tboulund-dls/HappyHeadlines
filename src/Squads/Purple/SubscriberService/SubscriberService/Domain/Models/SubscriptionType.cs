namespace SubscriberService.Domain.Models;

public class SubscriptionType
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
}