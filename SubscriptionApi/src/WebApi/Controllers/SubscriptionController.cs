using Core.Models;
using Core.Services;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    //[RoutePrefix("api/Subscription")]
   
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

        [System.Web.Http.HttpPost]        
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