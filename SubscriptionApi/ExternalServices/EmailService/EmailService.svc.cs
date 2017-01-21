using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmailService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmailService.svc or EmailService.svc.cs at the Solution Explorer and start debugging.
    public class EmailService : IEmailService
    {        
        public SendWelcomeEmailResponse SendWelcomeEmail(SendWelcomeEmailRequest composite)
        {
            return new SendWelcomeEmailResponse {Success = true};
        }
    }
}
