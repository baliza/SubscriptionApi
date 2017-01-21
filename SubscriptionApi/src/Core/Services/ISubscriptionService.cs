using Core.Helpers;
using Core.Models;

namespace Core.Services
{
    public interface ISubscriptionService
    {
        CreateSubscriptionResponse Create(CreateSubscriptionRequest request);
    }

  

   
}
