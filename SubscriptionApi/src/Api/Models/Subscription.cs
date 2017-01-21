using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Subscription
    {
        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string Gender { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public bool MarketingConsent { get; set; }

        [Required]
        public string NewsletterId { get; set; }
    }
}
