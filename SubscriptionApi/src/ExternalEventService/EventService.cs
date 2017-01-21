using System;

namespace ExternalEventService
{
    public class EventService : IEventService
    {
        public NewSubscriptionCreatedResponse NewSubscriptionCreated(NewSubscriptionCreatedRequest subscriptionCreated)
        {
            return new NewSubscriptionCreatedResponse { Result = NewSubscriptionCreatedResult.Success };
        }
    }

    public class NewSubscriptionCreatedRequest
    {
        public Guid SubscriptionId { get; set; }
    }

    public interface IEventService
    {
        NewSubscriptionCreatedResponse NewSubscriptionCreated(NewSubscriptionCreatedRequest subscriptionCreated);
    }

    public class NewSubscriptionCreatedResponse
    {
        public NewSubscriptionCreatedResult Result { get; set; }
    }

    public enum NewSubscriptionCreatedResult
    {
        Success = 0,
        FailedCommands = 1,
        InvalidRequest = 2,
    }
}