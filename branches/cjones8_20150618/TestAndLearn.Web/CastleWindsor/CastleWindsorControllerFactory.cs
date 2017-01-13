using Castle.Core;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TestAndLearn.Web.CastleWindsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;

            //var controllerTypes = 
            //    from t in Assembly.GetExecutingAssembly().GetTypes()
            //    where 
            //        typeof(IController).IsAssignableFrom(t)
            //    select t;

            //foreach (Type t in controllerTypes)
            //{
            //    _container.AddComponentLifeStyle(t.FullName, t, LifestyleType.Transient);
            //}
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(
                    0x194, 
                    string.Format(CultureInfo.CurrentUICulture,
                    "Controller Not Found",
                    new object[] { requestContext.HttpContext.Request.Path }));
            }

            if (false == typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(
                        string.Format(
                                CultureInfo.CurrentUICulture
                                , "Type does not sub-class the controller base"
                                , new object[] { controllerType }), "controllerType");
            }

            return
                    (IController)_container.Resolve(controllerType);
        }
    }
}