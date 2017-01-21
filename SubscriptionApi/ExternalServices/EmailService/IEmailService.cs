using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EmailService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmailService" in both code and config file together.
    [ServiceContract]
    public interface IEmailService
    {
        [OperationContract]
        SendWelcomeEmailResponse SendWelcomeEmail(SendWelcomeEmailRequest composite);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class SendWelcomeEmailResponse
    {
        [DataMember]
        public bool Success { get; set; } = true;
    }

    [DataContract]
    public class SendWelcomeEmailRequest
    {
        [DataMember]
        public Guid SubscriptionId
        {
            get; set;
        }
    }
}