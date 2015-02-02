using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social.Domain.DataBase;

namespace Social.WebUI.Infrastructure
{
    public class NinjectFactory :DefaultControllerFactory
    {
        private IKernel kernel;

        public NinjectFactory()
        {
            kernel = new StandardKernel();
            AddBinds();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                                ? null
                                : (IController)kernel.Get(controllerType);
        }

        private void AddBinds()
        {
            kernel.Bind<IUser_Db>().To<Users>();
        }
    }
}