using Api.Controllers;
using Core.Services;
using ExternalEmailService;
using ExternalEventService;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here

            //Products
            container.RegisterType<IProductsService, ProductsService>();
            //Subscriptions
            container.RegisterType<ISubscriptionRepository, SubscriptionRepository>();
            container.RegisterType<ISubscriptionValidator, SubscriptionValidator>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IEventService, EventService>();
            container.RegisterType<ISubscriptionService, InternalSubscriptionService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}