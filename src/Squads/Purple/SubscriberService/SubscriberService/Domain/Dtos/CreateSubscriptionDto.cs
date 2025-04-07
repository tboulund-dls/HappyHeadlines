namespace SubscriberService.Domain.Dtos;

public class CreateSubscriptionDto
{
    public required string Email { get; set; }
    public required string SubscriptionType { get; set; }
}