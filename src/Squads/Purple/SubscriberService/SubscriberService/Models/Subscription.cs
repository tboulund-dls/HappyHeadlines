namespace SubscriberService.Models;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid SubscriberId { get; set; }
    public Subscriber? Subscriber { get; set; }
    public Guid TypeId { get; set; }
    public SubscriberType? Type { get; set; }
}