namespace SubscriberService.Domain.Dtos;

public class GetSubscriptionDto
{
    public required Guid Id { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required string SubscriptionType { get; set; } = string.Empty;
}