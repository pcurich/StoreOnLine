using System.Configuration;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StoreOnLine.DataBase.Concrete;
using StoreOnLine.DataBase.Model.Configuration;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.Infrastructure.Abstract;
using StoreOnLine.Infrastructure.Concrete;

namespace StoreOnLine.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }



        private void AddBindings()
        {
            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            _kernel.Bind<IProductsRepository>().To<ProductsRepository>();
            _kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            _kernel.Bind<ICampaingRepository>().To<CampaingRepository>();
            _kernel.Bind<IUnitRepository>().To<UnitRepository>();
            _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            _kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}