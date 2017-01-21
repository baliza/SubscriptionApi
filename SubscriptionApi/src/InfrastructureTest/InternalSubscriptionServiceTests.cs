using System;
using Core.Models;
using Core.Services;
using ExternalEmailService;
using ExternalEventService;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace InfrastructureTests
{
    [TestFixture]
    public class InternalSubscriptionServiceTests
    {
        private InternalSubscriptionService _service;
        private Subscription _subscription;

        private Mock<ISubscriptionRepository> _mockSubscriptionRepository;
        private Mock<IEventService> _mockEventService;
        private Mock<IEmailService> _mockEmailService;
        private Mock<ISubscriptionValidator> _mockSubscriptionValidator;

        [SetUp]
        public void SetUp()
        {
            _subscription = new Subscription
            {
                Key = Guid.NewGuid().ToString(),
                DateOfBirth = DateTime.UtcNow.AddYears(20),
                Email = "user@email.com",
                Gender = "F",
                FirstName = "MyName MySurname",
                MarketingConsent = true,
                NewsletterId = Guid.NewGuid().ToString()
            };
            Subscription nullSub = null;

            _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            _mockSubscriptionRepository.Setup(x => x.Find(It.IsAny<string>())).Returns(nullSub);
            _mockSubscriptionRepository.Setup(x => x.Add(It.IsAny<Subscription>())).Returns(_subscription);

            _mockEventService = new Mock<IEventService>();
            _mockEventService.Setup(x => x.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>())).Returns(new NewSubscriptionCreatedResponse());

            _mockEmailService = new Mock<IEmailService>();
            _mockEmailService.Setup(x => x.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>())).Returns(new SendWelcomeEmailResponse());

            _mockSubscriptionValidator = new Mock<ISubscriptionValidator>();
            _mockSubscriptionValidator.Setup(x => x.Validate(It.IsAny<Subscription>())).Returns(string.Empty);
        }

        [Test]
        public void Returns_OK_for_valid_request()
        {
            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };
            _service = new InternalSubscriptionService(_mockSubscriptionRepository.Object, _mockSubscriptionValidator.Object, _mockEventService.Object, _mockEmailService.Object);
            var response = _service.Create(request);

            Assert.AreEqual(response.Result, CreateResults.Ok);
            Assert.AreEqual(response.Item, _subscription.Key);
            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Once);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()), Times.Once);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Once);
        }        

        [Test]
        public void Returns_BadRequest_for_invalid_request_young_person()
        {
            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };
            _mockSubscriptionValidator.Setup(x => x.Validate(It.IsAny<Subscription>())).Returns("not valid dateOfBirth");
            _service = new InternalSubscriptionService(_mockSubscriptionRepository.Object, _mockSubscriptionValidator.Object, _mockEventService.Object, _mockEmailService.Object);
            var response = _service.Create(request);

            Assert.AreEqual(response.Result, CreateResults.BadRequest);
            Assert.AreEqual(response.Error, "not valid dateOfBirth");
            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Never);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()), Times.Never);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Never);
        }

        [Test]
        public void Returns_BadRequest_for_invalid_email()
        {
            _subscription.Email = string.Empty;
            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };
            _mockSubscriptionValidator.Setup(x => x.Validate(It.IsAny<Subscription>())).Returns("not valid email");
            _service = new InternalSubscriptionService(_mockSubscriptionRepository.Object, _mockSubscriptionValidator.Object, _mockEventService.Object, _mockEmailService.Object);
            var response = _service.Create(request);

            Assert.AreEqual(response.Result, CreateResults.BadRequest);
            Assert.AreEqual(response.Error, "not valid email");
            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Never);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()), Times.Never);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Never);
        }

        [Test]
        public void Returns_BadRequest_for_invalid_NewsletterId()
        {
            _subscription.NewsletterId = string.Empty;
            var request = new CreateSubscriptionRequest
            {
                Subscription = _subscription
            };
            _mockSubscriptionValidator.Setup(x => x.Validate(It.IsAny<Subscription>())).Returns("not valid newsletterId");
            _service = new InternalSubscriptionService(_mockSubscriptionRepository.Object, _mockSubscriptionValidator.Object, _mockEventService.Object, _mockEmailService.Object);
            var response = _service.Create(request);

            Assert.AreEqual(response.Result, CreateResults.BadRequest);
            Assert.AreEqual(response.Error, "not valid newsletterId");
            _mockSubscriptionRepository.Verify(s => s.Add(It.IsAny<Subscription>()), Times.Never);
            _mockEventService.Verify(s => s.NewSubscriptionCreated(It.IsAny<NewSubscriptionCreatedRequest>()), Times.Never);
            _mockEmailService.Verify(s => s.SendWelcomeEmail(It.IsAny<SendWelcomeEmailRequest>()), Times.Never);
        }
    }
}