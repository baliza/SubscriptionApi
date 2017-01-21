using Api.Controllers;
using Core.Services;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Core.Repositories;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Infrastructure.ExternalService.Email;
using Infrastructure.ExternalService.Event;
using Infrastructure.Repositories;
using Infrastructure.Services;
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
            container.RegisterType<INewsletterRepository, NewsletterRepository>();
            container.RegisterType<ISubscriptionValidator, SubscriptionValidator>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IEventService, EventService>();
            container.RegisterType<ISubscriptionService, SubscriptionService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}