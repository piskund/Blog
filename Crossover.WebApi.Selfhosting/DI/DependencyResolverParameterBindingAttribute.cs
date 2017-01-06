using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Crossover.WebApi.Selfhosting.DI
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class DependencyResolverParameterBindingAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new DependencyResolverParameterBinding(parameter);
        }
    }
}