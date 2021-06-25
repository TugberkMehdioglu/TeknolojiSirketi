using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRep _auRep;
        public HomeController()
        {
            _auRep = new AppUserRep();
        }
        public ActionResult Login()
        {
            return View();
        }

    }
}