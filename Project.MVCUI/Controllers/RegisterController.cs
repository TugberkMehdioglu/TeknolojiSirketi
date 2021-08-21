using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRep _auRep;
        UserProfileRep _upRep;
        public RegisterController()
        {
            _auRep = new AppUserRep();
            _upRep = new UserProfileRep();
        }
        
        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(AppUserVM appUserVM, HttpPostedFileBase photo)
        {
            AppUser au = appUserVM.AppUser;
            UserProfile up = appUserVM.Profile;

            if (au == null || up == null)
            {
                ViewBag.ZatenVar = "Lütfen bütün alanları doldurunuz!";
                return View();
            }
            else if (_auRep.Any(x => x.UserName == au.UserName && x.Status != ENTITIES.Enums.DataStatus.Deleted))
            {
                ViewBag.ZatenVar = "Kullanıcı adı daha önce alınmış, lütfen farklı bir kullanıcı adı seçiniz";
                return View();
            }
            else if (_auRep.Any(x => x.Email == au.Email && x.Status != ENTITIES.Enums.DataStatus.Deleted))
            {
                ViewBag.ZatenVar = "Girmiş olduğunuz Email'e kayıtlı hesap bulunmaktadır";
                return View();
            }

            string mail = "Kayıt işlemiz gerçekleşmiştir, hesabınızı aktifleştirmek için lütfen https://localhost:44399/Register/Activation/" + au.ActivationCode + " linkine tıklayarak hesabınızı aktifleştiriniz";

            //Her kullanıcının şifresini kriptolu şekilde DB'de tutuyoruz
            //Hem Password hem de ConfirmPassword'e aynı kriptolu şifreyi atamazsak validation error alırız
            au.Password = au.ConfirmPassword = DantexCryptex.Crypt(au.Password);
            up.ImagePath = "/Pictures/anonim.png";//İlk kayıtta herkese anonim resmi atadık
            _auRep.Add(au);

            up.ID = au.ID;//Bire-bir ilişki tamamlaması
            _upRep.Add(up);

            MailService.Send(au.Email, subject: "TeknoCenter Aktivasyon", body: mail);

            return View("RegisterOk");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult Activation(Guid id)
        {
            AppUser au = _auRep.FirstOrDefault(x => x.ActivationCode == id);

            if (au != null)
            {
                au.Active = true;
                _auRep.Update(au);
                TempData["HesapAktif"] = "Hesabınız aktif hale getirilmiştir";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                TempData["HesapYok"] = "Hesabınız bulunamadı";
                return RedirectToAction("Login", "Home");
            }
        }
    }
}