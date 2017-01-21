using Core.Services;
using ExternalEmailService;
using ExternalEventService;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Core.Repositories;
using Unity.WebApi;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterType<ISubscriptionRepository, SubscriptionRepository>();
            container.RegisterType<ISubscriptionValidator, SubscriptionValidator>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IEventService, EventService>();
            container.RegisterType<ISubscriptionService, InternalSubscriptionService>();            

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
         
        }
    }
}