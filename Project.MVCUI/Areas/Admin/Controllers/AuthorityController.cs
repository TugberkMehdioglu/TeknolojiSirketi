using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class AuthorityController : Controller
    {
        AppUserRep _auRep;
        UserProfileRep _upRep;
        public AuthorityController()
        {
            _auRep = new AppUserRep();
            _upRep = new UserProfileRep();
        }
        public ActionResult UserList()
        {
            AppUserVM auvm = new AppUserVM
            {
                AppUsers = _auRep.GetActives(),
                Profiles = _upRep.GetActives()
            };

            return View(auvm);
        }

        public ActionResult AdminAuthorization(int id)
        {
            if (id > 0)
            {
                AppUser au = _auRep.Find(id);

                if (au.Role == ENTITIES.Enums.UserRole.Admin)
                {
                    TempData["yetkiHata"] = "Kullanıcı zaten admin yetkisine sahip";
                    return RedirectToAction("UserList");
                }
                else
                {
                    au.Role = ENTITIES.Enums.UserRole.Admin;
                    _auRep.Update(au);

                    TempData["yetki"] = "Yetkilendirme işlemi tamamlandı";
                    return RedirectToAction("UserList");
                }
            }
            else return RedirectToAction("UserList");
        }

        public ActionResult MemberAuthorization(int id)
        {
            if (id > 0)
            {
                AppUser au = _auRep.Find(id);

                if (au.Role == ENTITIES.Enums.UserRole.Member)
                {
                    TempData["yetkiHata"] = "Kullanıcı zaten member yetkisine sahip";
                    return RedirectToAction("UserList");
                }
                else
                {
                    au.Role = ENTITIES.Enums.UserRole.Member;
                    _auRep.Update(au);

                    TempData["yetki"] = "Yetkilendirme işlemi tamamlandı";
                    return RedirectToAction("UserList");
                }
            }
            else return RedirectToAction("UserList");
        }

        public ActionResult DeleteUser(int id)
        {
            if (id > 0)
            {
                AppUser au = _auRep.Find(id);

                if (au.Profile != null)
                {
                    _upRep.Delete(au.Profile);
                    _auRep.Delete(au);

                    TempData["yetki"] = "Silme işlemi tamamlandı";
                    return RedirectToAction("UserList");
                }
                else
                {
                    _auRep.Delete(au);
                    TempData["yetki"] = "Silme işlemi tamamlandı";
                    return RedirectToAction("UserList");
                }
            }
            else return RedirectToAction("UserList");
        }
    }
}