namespace SubscriberService.Models;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid SubscriberId { get; set; }
    public Subscriber? Subscriber { get; set; }
    public Guid SubscriptionTypeId { get; set; }
    public SubscriptionType? SubscriptionType { get; set; }
}