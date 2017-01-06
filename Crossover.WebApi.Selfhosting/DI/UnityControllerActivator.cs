using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.Unity;

namespace Crossover.WebApi.Selfhosting
{
    public class UnityControllerActivator : IHttpControllerActivator
    {
        private readonly IHttpControllerActivator _baseActivator;
        private readonly IUnityContainer _unity;

        public UnityControllerActivator(IUnityContainer unity, IHttpControllerActivator baseActivator)
        {
            _unity = unity;
            _baseActivator = baseActivator;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                GlobalConfiguration.Configuration.DependencyResolver.GetService(controllerType) as IHttpController;
            if (controller == null)
            {
                controller = _baseActivator.Create(request, controllerDescriptor, controllerType);
                if (controller != null) _unity.BuildUp(controller.GetType(), controller);
            }
            return controller;
        }
    }
}