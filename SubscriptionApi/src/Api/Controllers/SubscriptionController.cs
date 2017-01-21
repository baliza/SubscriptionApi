using System;
using Api.Filters;
using Core.Services;
using System.Net;
using System.Web.Http;
using M = Core.Models;
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
            var request = new M.CreateSubscriptionRequest
            {
                Subscription = new M.Subscription
                {
                    DateOfBirth = DateTime.Parse(subscription.DateOfBirth),
                    Email = subscription.Email,
                    FirstName = subscription.FirstName,
                    NewsletterId = subscription.NewsletterId,
                    MarketingConsent = subscription.MarketingConsent,
                }
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
    }
}