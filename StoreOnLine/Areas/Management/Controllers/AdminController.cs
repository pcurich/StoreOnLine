using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Const;
using System.Linq;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IImagenRepository _repository;

        public AdminController(IImagenRepository repo)
        {
            ViewBag.Big = "Administrar";
            ViewBag.Small = "Sistema de Configuracion";
            ViewBag.Area = "Management";
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";
            _repository = repo;
        }

        //
        // GET: /Management/Admin/
        public ActionResult Index()
        {
            return View();
        }
    }
}