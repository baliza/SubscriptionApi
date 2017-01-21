using Core.Models;
using Core.Services;
using Infrastructure.Services;
using System.Net;
using System.Web.Http;

namespace WebApi.Controllers
{
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
        public IHttpActionResult Post([FromBody] Subscription item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var request = new CreateSubscriptionRequest()
            {
                Subscription = item
            };
            var key = _internalService.Create(request);
            if (key.Result == CreateResults.Ok)
                return Ok(key.Item);

            if (key.Result == CreateResults.Existing)
                return StatusCode(HttpStatusCode.NoContent);

            return BadRequest();
        }
    }
}