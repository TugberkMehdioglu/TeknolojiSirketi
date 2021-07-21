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

        public ActionResult ProfileDetail()
        {
            if (Session["member"] != null)
            {
                ProfileVM pvm = new ProfileVM
                {
                    User = Session["member"] as AppUser,
                    Profile = (Session["member"] as AppUser).Profile
                };

                return View(pvm);
            }
            else return RedirectToAction("Login");
        }

        public ActionResult EditProfile(int id)
        {
            if (id > 0)
            {
                ProfileVM pvm = new ProfileVM
                {
                    User = _auRep.Find(id),
                    Profile = _auRep.Find(id).Profile
                };

                return View(pvm);
            }
            else return RedirectToAction("ShoppingList");
        }

        [HttpPost]
        public ActionResult EditProfile(ProfileVM profileVM, HttpPostedFileBase photo)
        {
            AppUser au = _auRep.Find(profileVM.User.ID);

            //Resim yüklenmemiş ise önceden yüklenmiş fotoğrafı atıyoruz
            if (photo == null) profileVM.Profile.ImagePath = au.Profile.ImagePath;

            else profileVM.Profile.ImagePath = ImageUploader.UploadImage("/Pictures/", photo);

            profileVM.User.Password = profileVM.User.ConfirmPassword = DantexCryptex.Crypt(profileVM.User.Password);

            //Validation Email'i zorunlu tutuyor ve burada active'i atamazsak otomatik false'a çeker
            profileVM.User.Email = au.Email;
            profileVM.User.Active = au.Active;

            _auRep.Update(profileVM.User);
            _upRep.Update(profileVM.Profile);

            return RedirectToAction("ProfileDetail");
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
            AppUser user = _auRep.FirstOrDefault(x => x.Email == email);

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
            AppUser appUser = _auRep.Find(user.ID);

            appUser.Password = DantexCryptex.Crypt(user.Password);
            appUser.ConfirmPassword = DantexCryptex.Crypt(user.ConfirmPassword);

            _auRep.Update(appUser);
            return RedirectToAction("Login");
        }
    }
}