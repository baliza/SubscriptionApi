using Core.Helpers;
using Core.Models;
using System;
using System.Linq;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class SubscriptionValidator : ISubscriptionValidator
    {
        public SimpleTrueFalseActionResult Validate(Subscription subscription)
        {
            if (!IsOlderThan18(subscription.DateOfBirth))
                return new SimpleTrueFalseActionResult("not valid date of birth (<18)");
            if (!IsValidNewsletterId(subscription.NewsletterId))
                return new SimpleTrueFalseActionResult("not valid newsletter Id");
            if (!IsValidGender(subscription.Gender))
                return new SimpleTrueFalseActionResult("not valid gender F/M");
            if (!IsValidEmail(subscription.Email))
                return new SimpleTrueFalseActionResult("not valid email");
            return new SimpleTrueFalseActionResult();
        }

        private static bool IsValidEmail(string emailaddress)
        {
            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsOlderThan18(DateTime dob)
        {
            try
            {
                return (DateTime.UtcNow > dob.AddYears(18));
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidGender(string gender)
        {
            try
            {
                var validGenders = new[] { "f", "m", "" };
                var g = gender.ToLowerInvariant();
                return validGenders.Any(x => x == g);
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidNewsletterId(string newsletterId)
        {
            var g = new Guid();
            return Guid.TryParse(newsletterId, out g);
        }
    }
}