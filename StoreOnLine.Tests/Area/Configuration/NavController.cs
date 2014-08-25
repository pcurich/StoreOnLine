
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreOnLine.Areas.Configuration.Controllers;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.Tests.Area.Configuration
{
    [TestClass]
    public class TestNavController
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.ProductBases).Returns(new ProductBase[] {
            new ProductBase {Id = 1, ProductName = "P1", ProductCategory = new Category{CategoryName="Apples"}},
            new ProductBase {Id = 2, ProductName = "P2", ProductCategory = new Category{CategoryName="Apples"}},
            new ProductBase {Id = 3, ProductName = "P3", ProductCategory = new Category{CategoryName="Plums"}},
            new ProductBase {Id = 4, ProductName = "P4", ProductCategory = new Category{CategoryName="Oranges"}} 
            });

            //// Arrange - create the controller
            //NavController target = new NavController(mock.Object);
            //// Act = get the set of categories
            //string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();
            //// Assert
            //Assert.AreEqual(results.Length, 3);
            //Assert.AreEqual(results[0], "Apples");
            //Assert.AreEqual(results[1], "Oranges");
            //Assert.AreEqual(results[2], "Plums");
        }
    }
}
