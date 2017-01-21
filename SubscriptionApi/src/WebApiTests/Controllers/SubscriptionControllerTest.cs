using System;
using System.Net;
using Core.Models;
using Core.Services;
using WebApi.Controllers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace WebApiTests.Controllers
{
    [TestFixture]
    public class SubscriptionControllerTest
    {
        private Mock<ISubscriptionService> _mockInternalService;
        private Guid _key;

        [SetUp]
        public void SetUp()
        {
            _key = Guid.NewGuid();

            _mockInternalService = new Mock<ISubscriptionService>();
        }

        [Test]
        public void Create_Responses_Ok()
        {
            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()))
                .Returns(new CreateSubscriptionResponse(_key.ToString()));

            var controller = new SubscriptionController(_mockInternalService.Object);
            var result = controller.Post(new Subscription());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<string>));
            var resultT = (OkNegotiatedContentResult<string>)result;
            Assert.AreEqual(resultT.Content, _key.ToString());
        }

        [Test]
        public void Create_Responses_NoContent()
        {
            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()))
                .Returns(new CreateSubscriptionResponse(CreateResults.Existing));
            var controller = new SubscriptionController(_mockInternalService.Object);
            var result = controller.Create(new Subscription());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var resultedCode = (StatusCodeResult)result;
            Assert.AreEqual(resultedCode.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public void Create_Responses_BadRequest_For_Null_request()
        {
            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()));
            var controller = new SubscriptionController(_mockInternalService.Object);

            var result = controller.Create(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockInternalService.Verify(s => s.Create(It.IsAny<CreateSubscriptionRequest>()), Times.Never);
        }

        [Test]
        public void Create_Responses_BadRequest()
        {
            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()))
                .Returns(new CreateSubscriptionResponse(CreateResults.Failed));

            var controller = new SubscriptionController(_mockInternalService.Object);

            var result = controller.Create(new Subscription());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockInternalService.Verify(s => s.Create(It.IsAny<CreateSubscriptionRequest>()), Times.Once);
        }
    }
}