using Core.Helpers;
using Core.Services;

namespace Core.Models
{
    public class CreateSubscriptionResponse : ActionResult<CreateResults, string>
    {
        public CreateSubscriptionResponse(Subscription subscription) : base(CreateResults.Ok, subscription.Key)
        {
        }

        public CreateSubscriptionResponse(CreateResults result) : base(result)
        {
        }

        public CreateSubscriptionResponse(string error) : base(CreateResults.Failed)
        {
            Error = error;
        }
    }
}