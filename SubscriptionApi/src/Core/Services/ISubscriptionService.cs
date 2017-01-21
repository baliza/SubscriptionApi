namespace Core.Services
{
    public interface ISubscriptionService
    {
        CreateSubscriptionResponse Create(CreateSubscriptionRequest request);
    }
}