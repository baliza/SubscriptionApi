using System;

namespace Infrastructure.ExternalService.Email
{
    public class EmailService : IEmailService
    {
        public SendWelcomeEmailResponse SendWelcomeEmail(SendWelcomeEmailRequest sendWelcomeEmailRequest)
        {
            return new SendWelcomeEmailResponse {Result = SendWelcomeEmailResult.Success};
        }
    }

    public class SendWelcomeEmailRequest
    {
        public Guid SubscriptionId { get; set; }
    }

    public interface IEmailService
    {
        SendWelcomeEmailResponse SendWelcomeEmail(SendWelcomeEmailRequest sendWelcomeEmailRequest);
    }

    public class SendWelcomeEmailResponse
    {
        public SendWelcomeEmailResult Result { get; set; }
    }

    public enum SendWelcomeEmailResult
    {
        Success = 0,
        FailedCommands = 1,
        InvalidRequest = 2,
    }
}
