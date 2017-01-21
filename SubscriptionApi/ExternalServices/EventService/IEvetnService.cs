using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EventService
{
    [ServiceContract]
    public interface IEvetnService
    {

        [OperationContract]
        NewSubscriptionCreatedResponse NewSubscriptionCreated(NewSubscriptionCreatedRequest composite);

        // TODO: Add your service operations here
    }


    [DataContract]
    public class NewSubscriptionCreatedRequest
    {
        [DataMember]
        public string SubcriptionId { get; set; } = "Hello ";
    }

    [DataContract]
    public class NewSubscriptionCreatedResponse
    {
        [DataMember]
        public bool Success { get; set; } = true;
    }
}
