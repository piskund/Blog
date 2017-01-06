using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace Crossover.WebApi.Selfhosting.DI
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly LinkedList<IUnityContainer> _children = new LinkedList<IUnityContainer>();
        private readonly IUnityContainer _unity;

        public UnityDependencyResolver(IUnityContainer unity)
        {
            _unity = unity;
        }

        public IDependencyScope BeginScope()
        {
            var child = _unity.CreateChildContainer();
            _children.AddLast(child);
            return new UnityDependencyResolver(child);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                var svc = _unity.Resolve(serviceType);
                return svc;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                var svcs = _unity.ResolveAll(serviceType);
                return svcs;
            }
            catch
            {
                return Enumerable.Empty<object>();
            }
        }

        public void Dispose()
        {
            foreach (var child in _children) child.Dispose();
            _children.Clear();
        }
    }
}