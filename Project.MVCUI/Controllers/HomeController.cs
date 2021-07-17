using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
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

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser control = _auRep.FirstOrDefault(x => x.UserName == appUser.UserName);
            
            if(control == null)
            {
                ViewBag.Kullanici = "Kullanıcı Adı veya şifre hatalı";
                return View();
            }

            string password = DantexCryptex.DeCrypt(control.Password);
            if (password != appUser.Password)
            {
                ViewBag.Kullanici = "Kullanıcı Adı veya şifre hatalı";
                return View();
            }
            else if (control.Role == UserRole.Admin)
            {
                if (!control.Active) return ActiveControl();

                Session["admin"] = control;
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                if (!control.Active) return ActiveControl();

                Session["member"] = control;
                return RedirectToAction("ShoppingList", "Shopping");
            }
        }
        
        public ActionResult ActiveControl()
        {
            ViewBag.Active = "Lütfen Mail'inize yolladığımız link'e tıklayarak hesabınızı aktif hale getiriniz";
            return View("Login");
        }


        public ActionResult LogOut()
        {
            if (Session["member"] != null || Session["admin"] != null)
            {
                Session.RemoveAll();
                return RedirectToAction("ShoppingList", "Shopping");
            }
            else return RedirectToAction("ShoppingList", "Shopping");
        }
    }
}