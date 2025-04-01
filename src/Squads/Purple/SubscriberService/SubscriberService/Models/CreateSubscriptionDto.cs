namespace SubscriberService.Models;

public class CreateSubscriptionDto
{
    public required string Email { get; set; }
    public required Guid SubscriptionType { get; set; }
}