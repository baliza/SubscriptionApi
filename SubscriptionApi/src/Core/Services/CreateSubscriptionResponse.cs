using Core.Helpers;
using Core.Models;

namespace Core.Services
{
    public class CreateSubscriptionResponse : ActionResult<CreateResults, string>
    {
        public CreateSubscriptionResponse(Subscription subscription) : base(CreateResults.Ok, subscription.Id)
        {
        }

        public CreateSubscriptionResponse(CreateResults result) : base(result)
        {
        }

        public CreateSubscriptionResponse(string errorMessage) : base(CreateResults.Failed)
        {
            ErrorMessage = errorMessage;
        }
    }
}