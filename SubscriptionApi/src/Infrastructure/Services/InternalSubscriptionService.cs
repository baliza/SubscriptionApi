using System;
using Core.Repositories;
using Core.Services;
using ExternalEmailService;
using ExternalEventService;

namespace Infrastructure.Services
{
    public class InternalSubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionrepository;
        private readonly INewsletterRepository _newsletterRepository;
        private readonly ISubscriptionValidator _subscriptionValidator;
        private readonly IEventService _eventService;
        private readonly IEmailService _emailService;

        public InternalSubscriptionService(ISubscriptionRepository subscriptionrepository, INewsletterRepository newsletterRepository, ISubscriptionValidator subscriptionValidator, IEventService eventService, IEmailService emailService)
        {
            _subscriptionrepository = subscriptionrepository;
            _newsletterRepository = newsletterRepository;
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
                if (!result.Succeeded)
                    return new CreateSubscriptionResponse(CreateResults.BadRequest) { ErrorMessage = result.ErrorMessage };

                var newsletter = _newsletterRepository.Find(subscription.NewsletterId);
                if (newsletter == null)
                    return new CreateSubscriptionResponse(CreateResults.BadRequest) { ErrorMessage = "newsletter not found" };
                if (newsletter.End <= DateTime.Today)
                    return new CreateSubscriptionResponse(CreateResults.BadRequest) { ErrorMessage = "newsletter ended" };

                var item = _subscriptionrepository.Add(subscription);

                _eventService.NewSubscriptionCreated(new NewSubscriptionCreatedRequest
                {
                    SubscriptionId= Guid.Parse(item.Id),
                });
                _emailService.SendWelcomeEmail(new SendWelcomeEmailRequest()
                {
                    SubscriptionId = Guid.Parse(item.Id),
                });

                return new CreateSubscriptionResponse(item);
            }
            catch
            {
                return new CreateSubscriptionResponse(CreateResults.Failed);
            }
        }
    }
}