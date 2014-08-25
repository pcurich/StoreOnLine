using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StoreOnLine.DataBase.Concrete;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Abstract;

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
            //var mock = new Mock<IProductsRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product { ProductName = "Football", ProductBasePrice = 25M },
            //    new Product { ProductName = "Surf board", ProductBasePrice = 179M },
            //    new Product { ProductName = "Running shoes", ProductBasePrice = 95M }
            //    });

            //_kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
            _kernel.Bind<IProductsRepository>().To<ProductsRepository>();
            _kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            _kernel.Bind<ICampaingRepository>().To<CampaingRepository>();
            _kernel.Bind<IUnitRepository>().To<UnitRepository>();
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