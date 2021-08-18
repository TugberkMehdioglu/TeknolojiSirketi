using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
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
        UserProfileRep _upRep;
        public HomeController()
        {
            _auRep = new AppUserRep();
            _upRep = new UserProfileRep();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser control = _auRep.FirstOrDefault(x => x.UserName == appUser.UserName);

            if (control == null)
            {
                ViewBag.Kullanici = "Kullanıcı Adı veya şifre hatalı";
                return View();
            }
            //Şifreyi decrypted etmeliyizki şifrenin sorgulamasını yapabilelim (DB'de kullanıcının şifresi kriptolu tutuluyor)
            string password = DantexCryptex.DeCrypt(control.Password);
            if (password != appUser.Password)
            {
                ViewBag.Kullanici = "Kullanıcı Adı veya şifre hatalı";
                return View();
            }
            else if (control.Role == UserRole.Admin)
            {
                if (!control.Active) return ActiveControl(); //Mail aktif değilse giriş yaptırtmıyoruz

                Session["admin"] = control;
                return RedirectToAction("CategoryList", new { @controller = "Category", area = "Admin" });
            }
            else
            {
                if (!control.Active) return ActiveControl(); //Mail aktif değilse giriş yaptırtmıyoruz

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

        public ActionResult CargoTracking()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            AppUser user = _auRep.FirstOrDefault(x => x.Email == email && x.Status != DataStatus.Deleted);

            if (user != null)
            {
                string mail = "Şifre değiştirme talibiniz alındı, lütfen http://localhost:44399/Home/ChangePassword/" + user.ActivationCode + " linkine tıklayarak şifrenizi değiştiriniz";
                MailService.Send(email, subject: "Şifre değiştirme talebi", body: mail);

                TempData["sifre"] = "Şifre değiştirme linkiniz e-postanıza yollandı, lütfen e-postanızı kontrol ediniz";
                return View();
            }
            else
            {
                TempData["red"] = "Bu email adresine ait hesap bulunmamaktadır";
                return View();
            }
        }

        public ActionResult ChangePassword(Guid id)
        {
            AppUser user = _auRep.FirstOrDefault(x => x.ActivationCode == id);

            if (user != null)
            {
                ProfileVM pvm = new ProfileVM
                {
                    User = user
                };

                return View(pvm);
            }
            else
            {
                TempData["red"] = "Hesabınız bulunamadı";
                return RedirectToAction("ForgetPassword");
            }

        }

        [HttpPost]
        public ActionResult ChangePassword(AppUser user)
        {
            if (user != null)
            {
                AppUser appUser = _auRep.Find(user.ID);

                appUser.Password = DantexCryptex.Crypt(user.Password);
                appUser.ConfirmPassword = DantexCryptex.Crypt(user.ConfirmPassword);

                _auRep.Update(appUser);
                return RedirectToAction("Login");
            }
            else return RedirectToAction("Login");
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(string fullName, string email, string message)
        {
            if (fullName != null && email != null && message != null)
            {
                string mail = $"Sayın yetkili {fullName} kişisinin E-postası: {email}, mesajı: {message}";

                //Burada receiver kısmına teknik destek mail hesabı yazılmalıdır
                MailService.Send(receiver: "tugberkmehdioglu@yandex.com", subject: "Bize ulaşın mesajı !", body: mail);

                ViewBag.destek = "Mesajınız müşteri hizmetlerine ulaşmıştır, en kısa zamanda size geri dönüş sağlanıcak";
                return View();
            }
            else return RedirectToAction("ContactUs");
        }
    }
}