using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using TestAndLearn.Web.CastleWindsor;
using TestAndLearn.Domain;
using TestAndLearn.Domain.Infrastructure;

namespace TestAndLearn.Web
{
    public class CastleWindsorConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Add Components from Config - move to here instead?
            //var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            var container = new WindsorContainer();

            container.Register(Component.For<IUserRepository>().ImplementedBy(typeof(UserRepository)));
            container.Register(Component.For<IClientRepository>().ImplementedBy(typeof(ClientRepository)));
            container.Register(Component.For<ITestRepository>().ImplementedBy(typeof(TestRepository)));

            //Register IHttpController for Web Api Controllers
            container.Register(Types.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient());

            //Register IController for regular Controllers
            container.Register(Types.FromThisAssembly().BasedOn<IController>().LifestyleTransient());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            var dependencyResolver = new CastleWindsorDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;
        }
    }
}