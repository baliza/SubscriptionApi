using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EventService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EventService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EventService.svc or EventService.svc.cs at the Solution Explorer and start debugging.
    public class EventService : IEvetnService
    {      
        public NewSubscriptionCreatedResponse NewSubscriptionCreated(NewSubscriptionCreatedRequest composite)
        {
            throw new NotImplementedException();
        }
    }
}
