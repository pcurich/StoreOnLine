//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using StoreOnLine.Areas.Configuration.Controllers;
//using StoreOnLine.Areas.Configuration.Models;
//using StoreOnLine.DataBase.Abstract;
//using StoreOnLine.DataBase.Model.Products;
//using StoreOnLine.HtmlHelpers;

//namespace StoreOnLine.Tests.Area.Configuration
//{
//    [TestClass]
//    public class TestProductController
//    {
//        [TestMethod]
//        public void Can_Paginate()
//        {
//            //Arrange
//            var mock = new Mock<IProductsRepository>();
//            mock.Setup(m => m.ProductBases).Returns(new[]
//            {
//                new ProductBase{Id = 1, ProductName = "p1"},
//                new ProductBase{Id = 2, ProductName = "p2"},
//                new ProductBase{Id = 3, ProductName = "p3"},
//                new ProductBase{Id = 4, ProductName = "p4"},
//                new ProductBase{Id = 5, ProductName = "p5"},
//                new ProductBase{Id = 6, ProductName = "p6"}
//            });

//            var controller = new ProductBaseController(mock.Object) { PageSize = 3 };

//            //Act
//            var result = (IEnumerable<Product>)controller.List("1",2).Model;

//            //Asert
//            var prodArray = result.ToArray();
//            Assert.IsTrue(prodArray.Length == 3);
//            Assert.AreEqual(prodArray[0].ProductName, "p4");
//            Assert.AreEqual(prodArray[1].ProductName, "p5");
//        }

//        [TestMethod]
//        public void Can_Generate_Page_Link()
//        {
//            //Arrange - define an HTML helper - we need to do this in order to apply the extension method
//            HtmlHelper myHelpers = null;

//            //Arrange - Create Paggination Info
//            PagingInfo pagingInfo = new PagingInfo
//            {
//                CurrentPage = 2,
//                TotalItem = 28,
//                ItemsPerPage = 10
//            };

//            //Arrange - set up the delegate using a lamdba expression 
//            Func<int, string> pageUrlDelegate = i => "Page" + i;

//            //Act
//            MvcHtmlString result = myHelpers.PageLinks(pagingInfo, pageUrlDelegate);

//            //Assert
//            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
//                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
//                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
//                result.ToString());
//        }

//        [TestMethod]
//        public void Can_Send_Pagination_View_Model()
//        {
//            // Arrange
//            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
//            mock.Setup(m => m.ProductBases).Returns(new ProductBase[] {
//                new ProductBase {Id = 1, ProductName = "P1"},
//                new ProductBase {Id = 2, ProductName = "P2"},
//                new ProductBase {Id = 3, ProductName = "P3"},
//                new ProductBase {Id = 4, ProductName = "P4"},
//                new ProductBase {Id = 5, ProductName = "P5"}
//            });
//            // Arrange
//            ProductBaseController controller = new ProductBaseController(mock.Object);
//            controller.PageSize = 3;
//            // Act
//            ProductsListViewModel result= (ProductsListViewModel)controller.List("0", 2).Model;
//            // Assert
//            PagingInfo pageInfo = result.PagingInfo;
//            Assert.AreEqual(pageInfo.CurrentPage, 2);
//            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
//            Assert.AreEqual(pageInfo.TotalItem, 5);
//            Assert.AreEqual(pageInfo.TotalPages, 2);

//        }
//    }
//}
