using System.Xml.Schema;
using Core.Models;
using Core.Services;
using ExternalEmailService;
using ExternalEventService;

namespace Infrastructure.Services
{
    public class InternalSubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;
        private readonly ISubscriptionValidator _subscriptionValidator;
        private readonly IEventService _eventService;
        private readonly IEmailService _emailService;

        public InternalSubscriptionService(ISubscriptionRepository repository, ISubscriptionValidator subscriptionValidator, IEventService eventService, IEmailService emailService)
        {
            _repository = repository;
            _subscriptionValidator = subscriptionValidator;
            _eventService = eventService;
            _emailService = emailService;
        }

        public CreateSubscriptionResponse Create(CreateSubscriptionRequest request)
        {
            try
            {
                var subscription = request.Subscription;
                var result = _subscriptionValidator.Validate(subscription);
                if (!string.IsNullOrEmpty(result))
                    return new CreateSubscriptionResponse(CreateResults.BadRequest) {Error = result};
                var item = _repository.Add(subscription);

                return new CreateSubscriptionResponse(item);
            }
            catch
            {
                return new CreateSubscriptionResponse(CreateResults.Failed);
            }
        }

        
    }
}