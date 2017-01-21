using Api.Filters;
using Core.Services;
using System;
using System.Net;
using System.Web.Http;
using C = Core.Models;
using Subscription = Api.Models.Subscription;

namespace Api.Controllers
{
    [IdentityBasicAuthentication]
    [Authorize]
    public class SubscriptionController : ApiController
    {
        public SubscriptionController()
        {
        }

        public SubscriptionController(ISubscriptionService internalService)
        {
            _internalService = internalService;
        }

        private readonly ISubscriptionService _internalService;

        [HttpPost]
        [ValidateSubscriptionModel]
        public IHttpActionResult Post([FromBody] Subscription subscription)
        {
            try
            {
                var request = new CreateSubscriptionRequest
                {
                    Subscription = MapSubscription(subscription)
                };
                var key = _internalService.Create(request);
                switch (key.Result)
                {
                    case CreateResults.Ok:
                        return Ok(key.Item);

                    case CreateResults.Existing:
                        return StatusCode(HttpStatusCode.NoContent);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        private static C.Subscription MapSubscription(Subscription subscription)
        {
            return new C.Subscription
            {
                DateOfBirth = DateTime.Parse(subscription.DateOfBirth),
                Email = subscription.Email,
                Gender = subscription.Gender,
                FirstName = subscription.FirstName,
                NewsletterId = subscription.NewsletterId,
                MarketingConsent = subscription.MarketingConsent,
            };
        }
    }
}