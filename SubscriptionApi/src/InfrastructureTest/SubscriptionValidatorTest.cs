using System;
using Core.Models;
using Infraestructure.Services;
using NUnit.Framework;

namespace InfrastructureTests
{
    [TestFixture]
    public class SubscriptionValidatorTest
    {
        private Subscription _subscription;

        [SetUp]
        public void SetUp()
        {
            _subscription = new Subscription
            {
                Id = Guid.NewGuid().ToString(),
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "user@email.com",
                Gender = "F",
                FirstName = "MyName MySurname",
                MarketingConsent = true,
                NewsletterId = Guid.NewGuid().ToString()
            };
        }

        [Test]
        public void Responses_Ok_For_valid_Subscription()
        {
            var sv = new SubscriptionValidator();
            var r = sv.Validate(_subscription);
            Assert.IsTrue(r.Succeeded);
        }
        [Test]
        public void Responses_Ok_For_empty_gender()
        {
            _subscription.Gender = string.Empty;
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsTrue(r.Succeeded);
        }

        [Test]
        public void Responses_Ok_For_empty_firstName()
        {
            _subscription.FirstName = string.Empty;
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsTrue(r.Succeeded);
        }
        [Test]
        public void Responses_Fault_For_invalid_date()
        {
            _subscription.DateOfBirth = DateTime.Now.AddYears(-1);
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsFalse(r.Succeeded);
            Assert.AreEqual("not valid date of birth (<18)", r.ErrorMessage);
        }

        [Test]
        public void Responses_Fault_For_invalid_email()
        {
            _subscription.Email = "not an email";
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsFalse(r.Succeeded);
            Assert.AreEqual("not valid email", r.ErrorMessage);
        }
        [Test]
        public void Responses_Fault_For_empty_email()
        {
            _subscription.Email = string.Empty;
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsFalse(r.Succeeded);
            Assert.AreEqual("not valid email", r.ErrorMessage);
        }

        [Test]
        public void Responses_Fault_For_invalid_newsletter_id()
        {
            _subscription.NewsletterId = "not a valid guid";
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsFalse(r.Succeeded);
            Assert.AreEqual("not valid newsletter Id", r.ErrorMessage);
        }

        [Test]
        public void Responses_Fault_For_invalid_gender()
        {
            _subscription.Gender = "not a valid gender";
            var r = new SubscriptionValidator().Validate(_subscription);
            Assert.IsFalse(r.Succeeded);
            Assert.AreEqual("not valid gender F/M", r.ErrorMessage);
        }
    }
}