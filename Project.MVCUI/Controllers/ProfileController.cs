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
    public class ProfileController : Controller
    {
        AppUserRep _auRep;
        UserProfileRep _upRep;
        public ProfileController()
        {
            _auRep = new AppUserRep();
            _upRep = new UserProfileRep();
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
            else return RedirectToAction("Login", "Home");
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
            else return RedirectToAction("ShoppingList", "Shopping");
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
    }
}