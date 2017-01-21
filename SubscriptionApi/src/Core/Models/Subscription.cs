using System;

namespace Core.Models
{
    public class Subscription
    {
        public string Key { get; set; }
        public string Email { get; set; }        
        public string FirstName { get; set; }
        public string Gender { get; set; }        
        public DateTime DateOfBirth { get; set; }
        public bool MarketingConsent { get; set; }
        public string NewsletterId { get; set; }
    }
}
