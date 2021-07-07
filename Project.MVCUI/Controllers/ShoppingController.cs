using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        AddressRep _aRep;
        OrderDetailRep _odRep;
        OrderRep _oRep;
        ProductRep _pRep;
        CategoryRep _cRep;
        public ShoppingController()
        {
            _aRep = new AddressRep();
            _odRep = new OrderDetailRep();
            _oRep = new OrderRep();
            _pRep = new ProductRep();
            _cRep = new CategoryRep();
        }
        public ActionResult ShoppingList(int? page, int?categoryID)
        {
            PAVM pavm = new PAVM
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 6) : _pRep.GetActives().Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 6),
                Categories = _cRep.GetActives(),             
            };

            if (categoryID != null) ViewBag.categoryID = categoryID;

            return View(pavm);
        }

        public ActionResult ProductDetail(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _pRep.Find(id)
            };

            return View(pvm);
        }
    }
}