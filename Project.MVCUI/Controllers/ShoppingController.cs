using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
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

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] != null ? Session["scart"] as Cart : new Cart();

            Product UpcomingProduct = _pRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = UpcomingProduct.ID,
                Name = UpcomingProduct.Name,
                Price = UpcomingProduct.UnitPrice,
                ImagePath = UpcomingProduct.ImagePath
            };

            c.SepeteEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");           
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;

                c.SepettenSil(id);

                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("ShoppingList");
        }

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cpvm = new CartPageVM
                {
                    Cart = Session["scart"] as Cart
                };
                return View(cpvm);
            }
            return RedirectToAction("ShoppingList");
        }

        public ActionResult ConfirmOrder()
        {
            if (Session["member"] == null)
            {
                TempData["siparis"] = "Siparişi tamamlamak için lütfen giriş yapınız";
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                AppUser au = Session["member"] as AppUser;
                OrderVM ovm = new OrderVM
                {
                    Addresses = au.Profile.Addresses
                };

                return View(ovm);
            }
        }

        [HttpPost]
        public ActionResult ConfirmOrder(OrderVM ovm)
        {
            return View();
        }
    }
}