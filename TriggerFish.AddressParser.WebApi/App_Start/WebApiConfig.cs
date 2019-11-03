using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using FluentValidation;
using FluentValidation.WebApi;
using TriggerFish.AddressParser.WebApi.Configuration;
using TriggerFish.AddressParser.WebApi.ExceptionHandling;
using TriggerFish.AddressParser.WebApi.ModelValidation;

namespace TriggerFish.AddressParser.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var builder = new ContainerBuilder();

            builder.RegisterInstance(AppSettings.LoadFromConfiguration())
                .AsSelf()
                .SingleInstance();

            var executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterApiControllers(executingAssembly);
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var container = builder.Build();

            FluentValidationModelValidatorProvider.Configure(config, provider =>
            {
                provider.ValidatorFactory = new AutofactValidationFactory(container);
            });

            config.Services.Replace(typeof(IExceptionHandler), new UnexpectedExceptionHandler());

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
