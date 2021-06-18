using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class TestController : Controller
    {
        ProductRep _pRep;
        public TestController()
        {
            _pRep = new ProductRep();
        }
        
        // GET: Test
        public ActionResult Index()
        {
            ProductVM pvm = new ProductVM
            {
                Products = _pRep.GetActives()
            };
            return View(pvm);
        }
    }
}