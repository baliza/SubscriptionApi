namespace Core.Models
{
    internal class CreateSubscriptionResponse
    {
        private Subscription _coreSubscription;

        public CreateSubscriptionResponse(Subscription _coreSubscription)
        {
            this._coreSubscription = _coreSubscription;
        }
    }
}