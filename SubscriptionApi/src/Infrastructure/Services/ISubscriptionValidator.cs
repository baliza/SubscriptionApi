using Core.Models;

namespace Infrastructure.Services
{
    public interface ISubscriptionValidator
    {
        string Validate(Subscription subscription);
    }
}