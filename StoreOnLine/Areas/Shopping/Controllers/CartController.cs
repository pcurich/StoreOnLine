using System.Linq;
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
        private readonly IOrderProcessor _orderProcessor;

        public CartController(IProductsRepository repo, IOrderProcessor proc)
        {
            _repository = repo;
            _orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            ProductBase productBase = _repository.ProductBases.FirstOrDefault(p => p.Id == productId);
            if (productBase != null)
            {
                cart.AddItem(productBase, 1);
            }
            else
            {
                ProductComposite productComposite = _repository.ProductComposites.FirstOrDefault(p => p.Id == productId);
                cart.AddItem(productComposite, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            ProductBase productBase = _repository.ProductBases.FirstOrDefault(p => p.Id == productId);
            if (productBase != null)
            {
                cart.RemoveLine(productBase);
            }
            else
            {
                ProductComposite productComposite = _repository.ProductComposites.FirstOrDefault(p => p.Id == productId);
                cart.RemoveLine(productComposite);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}