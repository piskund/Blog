using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace Crossover.WebApi.Selfhosting.DI
{
    public sealed class DependencyResolverParameterBinding : HttpParameterBinding
    {
        public DependencyResolverParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (actionContext.ControllerContext.Configuration.DependencyResolver != null)
                actionContext.ActionArguments[Descriptor.ParameterName] =
                    actionContext.ControllerContext.Configuration.DependencyResolver.GetService(Descriptor.ParameterType);
            return Task.FromResult(0);
        }
    }
}