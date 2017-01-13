using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.Web.Http;

namespace TestAndLearn.Web.CastleWindsor
{
    internal sealed class CastleWindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public CastleWindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }
        public object GetService(Type t)
        {
            return _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new CastleWindsorDependencyScope(_container);
        }

        public void Dispose()
        {

        }
    }
}