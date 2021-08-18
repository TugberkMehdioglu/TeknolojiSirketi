using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication] //Sadece Admin yetkisi olanlar bu controller'a giriş sağlayabilir
    public class CategoryController : Controller
    {
        CategoryRep _cRep;
        public CategoryController()
        {
            _cRep = new CategoryRep();
        }
        
        //id'li hali için ayrı bir action açmak yerine nullable ile turnery if kullanarak tek action'da iki ayrı request'e cevap verdik
        public ActionResult CategoryList(int? id)
        {
            //CategoryList'e Get request'i olduğunda id parametresi null ise bütün kategorileri getir, null değilse istenen kategoriyi getir
            CategoryVM cvm = id == null ? new CategoryVM
            {
                Categories = _cRep.GetActives()
            } : new CategoryVM { Categories = _cRep.Where(x => x.ID == id && x.Status != ENTITIES.Enums.DataStatus.Deleted) };
            return View(cvm);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _cRep.Add(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult UpdateCategory(int id)
        {
            if (id > 0)
            {
                CategoryVM cvm = new CategoryVM
                {
                    Category = _cRep.Find(id)
                };
                return View(cvm);
            }
            else return RedirectToAction("CategoryList");

        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            _cRep.Update(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            if (id > 0)
            {
                _cRep.Delete(_cRep.Find(id));
                return RedirectToAction("CategoryList");
            }
            else return RedirectToAction("CategoryList");
        }
    }
}