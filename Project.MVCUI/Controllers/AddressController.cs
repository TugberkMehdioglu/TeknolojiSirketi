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
    public class AddressController : Controller
    {
        AddressRep _aRep;
        AppUserRep _auRep;
        UserProfileRep _upRep;
        public AddressController()
        {
            _aRep = new AddressRep();
            _auRep = new AppUserRep();
            _upRep = new UserProfileRep();
        }
        
        public ActionResult Addresses()
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
            return RedirectToAction("Login", "Home");
        }

        public ActionResult AddAddress()
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
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AddAddress(ProfileVM pvm)
        {
            AppUser au = Session["member"] as AppUser;

            pvm.Address.UserProfileID = au.ID;

            _aRep.Add(pvm.Address);

            return RedirectToAction("Addresses");
        }

        public ActionResult UpdateAddress(int id)
        {
            ProfileVM pvm = new ProfileVM
            {
                Address = _aRep.Find(id),
                User = Session["member"] as AppUser,
                Profile = (Session["member"] as AppUser).Profile
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult UpdateAddress(Address address)
        {
            _aRep.Update(address);
            return RedirectToAction("Addresses");
        }

        public ActionResult DeleteAddress(int id)
        {
            _aRep.Destroy(_aRep.Find(id)); //Kullanıcının silmek istediği adres bilgisini DB'den destroy ettik
            return RedirectToAction("Addresses");
        }
    }
}