using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Areas.Shopping.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Shopping;

namespace StoreOnLine.Areas.Shopping.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsRepository _repository;

        public CartController(IProductsRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            ProductBase productBase = _repository.ProductBases.FirstOrDefault(p => p.Id == productId);
            if (productBase != null)
            {
                GetCart().AddItem(productBase, 1);
            }
            else
            {
                ProductComposite productComposite = _repository.ProductComposites.FirstOrDefault(p => p.Id == productId);
                GetCart().AddItem(productComposite, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            ProductBase productBase = _repository.ProductBases.FirstOrDefault(p => p.Id == productId);
            if (productBase != null)
            {
                GetCart().RemoveLine(productBase);
            }
            else
            {
                ProductComposite productComposite = _repository.ProductComposites.FirstOrDefault(p => p.Id == productId);
                GetCart().RemoveLine(productComposite);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}