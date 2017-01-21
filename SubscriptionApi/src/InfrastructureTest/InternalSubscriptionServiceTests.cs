using Core.Helpers;
using Core.Models;
using Core.Repositories;
using Core.Services;
using ExternalEmailService;
using ExternalEventService;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;

namespace InfrastructureTests
{
    [TestFixture]
    public class InternalSubscriptionServiceTests
    {
        private Mock<IEmailService> _mockEmailService;
        private Mock<IEventService> _mockEventService;
        private Mock<INewsletterRepository> _mockINewslettersRepository;
        private Mock<ISubscriptionRepository> _mockSubscriptionRepository;
        private Mock<ISubscriptionValidator> _mockSubscriptionValidator;
        private Subscription _subscription;

        [SetUp]
        public void SetUp()
        {
            _subscription = new Subscription
            {
                Id = Guid.NewGuid().ToString(),
                DateOfBirth = DateTime.UtcNow.AddYears(20),
                Email = "user@email.com",
                Gender = "F",
                FirstName = "MyName MySurname",
                MarketingConsent = true,
                NewsletterId = Guid.NewGuid().ToString()
            };

            _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            _mockSubscriptionRepository.Setup(x => x.Find(It.IsAny<string>())).Returns((Subscription)null);
            _mockSubscriptionRepository.Setup(x => x.Add(It.IsAny<Subscription>())).Returns(_subscription);

            _mockINewslettersRepository = new Mock<INewsletterRepository>();
            _mockINewslettersRepository.Setup(x => x.Find(It.IsAny<string>())).Returns(new Newsletter
            {
                Start = DateTime.UtcNow.AddDays(-5),
                End = DateTime.UtcNow.AddDays(5),
            });

            _mockEventService = new Mock<IEventService>();
            _mockEventService.Setup(x => x.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()))
                .Returns(new NewSubscriptionCreatedResponse());

            _mockEmailService = new Mock<IEmailService>();
            _mockEmailService.Setup(x => x.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()))
                .Returns(new SendWelcomeEmailResponse());

            _mockSubscriptionValidator = new Mock<ISubscriptionValidator>();
            _mockSubscriptionValidator.Setup(x => x.Validate(It.IsAny<Subscription>()))
                .Returns(new SimpleTrueFalseActionResult());
        }

        [Test]
        public void Returns_BadRequest_for_finnished_newsletter()
        {
            _mockINewslettersRepository.Setup(x => x.Find(It.IsAny<string>())).Returns(new Newsletter
            {
                Start = DateTime.UtcNow.AddDays(-5),
                End = DateTime.UtcNow.AddDays(-1),
            });

            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };

            var service = CreateInternalSubscriptionService();
            var response = service.Create(request);

            Assert.AreEqual(CreateResults.BadRequest, response.Result);
            Assert.AreEqual("newsletter ended", response.ErrorMessage);

            _mockINewslettersRepository.Verify(s => s.Find(It.IsAny<string>()), Times.Once);

            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Never);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()), Times.Never);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Never);
        }

        [Test]
        public void Returns_BadRequest_for_invalid_subscription()
        {
            _mockINewslettersRepository.Setup(x => x.Find(It.IsAny<string>())).Returns((Newsletter)null);

            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };
            var service = CreateInternalSubscriptionService();
            var response = service.Create(request);

            Assert.AreEqual(CreateResults.BadRequest, response.Result);
            Assert.AreEqual("newsletter not found", response.ErrorMessage);

            _mockINewslettersRepository.Verify(s => s.Find(It.IsAny<string>()), Times.Once);

            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Never);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()), Times.Never);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Never);
        }

        [Test]
        public void Returns_Failed_when_not_dependencies_setup()
        {
            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };

            var service = new InternalSubscriptionService(null, null, null, null, null);
            var response = service.Create(request);

            Assert.AreEqual(CreateResults.Failed, response.Result);
        }

        [Test]
        public void Returns_OK_for_valid_request()
        {
            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };
            var service = CreateInternalSubscriptionService();
            var response = service.Create(request);

            Assert.AreEqual(CreateResults.Ok, response.Result);
            Assert.AreEqual(_subscription.Id, response.Item);

            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Once);
            _mockINewslettersRepository.Verify(x => x.Find(It.IsAny<string>()), Times.Once);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()),
                Times.Once);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Once);
        }

        private InternalSubscriptionService CreateInternalSubscriptionService()
        {
            return new InternalSubscriptionService(_mockSubscriptionRepository.Object, _mockINewslettersRepository.Object,
                _mockSubscriptionValidator.Object, _mockEventService.Object, _mockEmailService.Object);
        }
    }
}