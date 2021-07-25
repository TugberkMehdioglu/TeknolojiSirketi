using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.DTO.Models;
using Project.ENTITIES.Models;
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
    public class ProductController : Controller
    {
        ProductRep _pRep;
        CategoryRep _cRep;
        public ProductController()
        {
            _pRep = new ProductRep();
            _cRep = new CategoryRep();
        }

        // GET: Admin/Product
        public ActionResult ProductList(int? id)
        {
            ProductVM pvm = new ProductVM()
            {
                Products = id == null ? _pRep.GetActives() : _pRep.GetActives().Where(x => x.CategoryID == id).ToList()
            };
            return View(pvm);
        }

        public ActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM
            {
                Product = new Product(),
                Categories = _cRep.GetActives()
            };
            return View(pvm);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase image)
        {
            StockDTO stock = new StockDTO
            {
                ID = product.ID,
                ProductName = product.Name,
                UnitPrice = product.UnitPrice,
                UnitInStock = product.UnitInStock
            };

            //Depo API'da yeni bir action açmak yerine önceden kullanılan List<StockDTO> parametreli action'ı kullandık
            List<StockDTO> listStock = new List<StockDTO> { stock };

            using(HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Home/AddStocks", listStock);

                HttpResponseMessage result;

                try
                {
                    result = postTask.Result;
                }
                catch (Exception)
                {
                    TempData["hataCRUD"] = "Depo bağlantıyı reddetti";
                    return RedirectToAction("ProductList");
                }

                if (result.IsSuccessStatusCode)
                {
                    product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);
                    _pRep.Add(product);
                    return RedirectToAction("ProductList");
                }
                else
                {
                    TempData["hataCRUD"] = "Depo işlemi reddetti";
                    return RedirectToAction("ProductList");
                }
            }
        }

        public ActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Categories=_cRep.GetActives(),
                Product = _pRep.Find(id)
            };
            return View(pvm);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product, HttpPostedFileBase image)
        {
            StockDTO stock = new StockDTO
            {
                ID = product.ID,
                ProductName = product.Name,
                UnitPrice = product.UnitPrice,
                UnitInStock = product.UnitInStock
            };

            using(HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");
                Task<HttpResponseMessage> postTask = client.PutAsJsonAsync("Home/UpdateStock", stock);

                HttpResponseMessage result;

                try
                {
                    result = postTask.Result;
                }
                catch (Exception)
                {
                    TempData["hataCRUD"] = "Depo bağlantıyı reddetti";
                    return RedirectToAction("ProductList");
                }

                if (result.IsSuccessStatusCode)
                {
                    if (image != null)
                    product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);

                    _pRep.Update(product);
                    return RedirectToAction("ProductList");
                }
                else
                {
                    TempData["hataCRUD"] = "Depo işlemi reddetti";
                    return RedirectToAction("ProductList");
                }
            }


        }

        public ActionResult DeleteProduct(int id)
        {
            StockDTO stock = new StockDTO { ID = id };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");
                Task<HttpResponseMessage> postTask = client.PutAsJsonAsync("Home/DeleteStock", stock);
                
                HttpResponseMessage result;

                try
                {
                    result = postTask.Result;
                }
                catch (Exception)
                {
                    TempData["hataCRUD"] = "Depo bağlantıyı reddetti";
                    return RedirectToAction("ProductList");
                }

                if (result.IsSuccessStatusCode)
                {
                    _pRep.Delete(_pRep.Find(id));
                    return RedirectToAction("ProductList");
                }
                else
                {
                    TempData["hataCRUD"] = "Depo işlemi reddetti";
                    return RedirectToAction("ProductList");
                }
            }
        }
    }
}