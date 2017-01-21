//using System;
//using System.Net;
//using NUnit.Framework;

//namespace ApiTests.Controllers
//{
//    [TestFixture]
//    public class SubscriptionControllerTest
//    {
//        private Mock<ISubscriptionService> _mockInternalService;
//        private Guid _key;
//        private Subscription _subscription;

//        [SetUp]
//        public void SetUp()
//        {
//            _key = Guid.NewGuid();

//            _subscription = new Subscription
//            {
//                Key = _key.ToString(),
//                Email = "user@email.com",
//                DateOfBirth = DateTime.UtcNow.AddYears(-20),
//                Gender = "f",
//                FirstName = "Username UserSurname",
//                MarketingConsent = true,
//                NewsletterId = Guid.NewGuid().ToString()
//            };
//            _mockInternalService = new Mock<ISubscriptionService>();

//        }

//        [Test]
//        public void Create_Responses_Ok()
//        {
//            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()))
//                .Returns(new CreateSubscriptionResponse(_subscription));

//            var controller = new SubscriptionController(_mockInternalService.Object);
//            var result = controller.Post(_subscription);

//            Assert.IsNotNull(result);
//            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<string>), result);
//            var resultT = (OkNegotiatedContentResult<string>)result;
//            Assert.AreEqual(resultT.Content, _key.ToString());
//        }

//        [Test]
//        public void Create_Responses_NoContent()
//        {
//            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()))
//                .Returns(new CreateSubscriptionResponse(CreateResults.Existing));
//            var controller = new SubscriptionController(_mockInternalService.Object);

//            var result = controller.Post(_subscription);

//            Assert.IsNotNull(result);
//            Assert.IsInstanceOf(typeof(StatusCodeResult), result);
//            var resultedCode = (StatusCodeResult)result;
//            Assert.AreEqual(resultedCode.StatusCode, HttpStatusCode.NoContent);
//        }

//        [Test]
//        public void Create_Responses_BadRequest_For_Null_request()
//        {
//            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()));
//            var controller = new SubscriptionController(_mockInternalService.Object);

//            var result = controller.Post(null);
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOf(typeof(BadRequestResult), result);
//            _mockInternalService.Verify(s => s.Create(It.IsAny<CreateSubscriptionRequest>()), Times.Never);
//        }

//        [Test]
//        public void Create_Responses_BadRequest()
//        {
//            _mockInternalService.Setup(x => x.Create(It.IsAny<CreateSubscriptionRequest>()))
//                .Returns(new CreateSubscriptionResponse(CreateResults.Failed));

//            var controller = new SubscriptionController(_mockInternalService.Object);

//            var result = controller.Post(_subscription);

//            Assert.IsNotNull(result);
//            Assert.IsInstanceOf(typeof(BadRequestResult), result);
//            _mockInternalService.Verify(s => s.Create(It.IsAny<CreateSubscriptionRequest>()), Times.Once);
//        }
//    }
//}