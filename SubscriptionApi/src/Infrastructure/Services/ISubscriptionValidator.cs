using Core.Helpers;
using Core.Models;

namespace Infrastructure.Services
{
    public interface ISubscriptionValidator
    {
        SimpleTrueFalseActionResult Validate(Subscription subscription);
    }
}