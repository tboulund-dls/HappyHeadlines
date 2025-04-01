namespace SubscriberService.Models;

public class Subscriber
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
}