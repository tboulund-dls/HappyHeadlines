namespace SharedModels;

public class SubscriptionCreatedEvent
{
    public required string Email { get; set; }
    public required string SubscriptionType { get; set; } // DAILY, NEWSSTREAM
}