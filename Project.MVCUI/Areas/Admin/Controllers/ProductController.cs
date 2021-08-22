using Newtonsoft.Json;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.DTO.Models;
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
    public class ProductController : Controller
    {
        ProductRep _pRep;
        CategoryRep _cRep;
        Project.BLL.DesignPatterns.GenericRepository.ConcRep.AttributeRep _aRep;
        ProductAttributeRep _paRep;
        public ProductController()
        {
            _pRep = new ProductRep();
            _cRep = new CategoryRep();
            _aRep = new AttributeRep();
            _paRep = new ProductAttributeRep();
        }

        //id'li hali için ayrı bir action açmak yerine nullable ile turnery if kullanarak tek action'da iki ayrı request'e cevap verdik
        public async Task<ActionResult> ProductList(int? id)
        {

            getAsyn();
            
            ProductVM pvm = new ProductVM()
            {
                Products = id == null ? _pRep.GetActives() : _pRep.GetActives().Where(x => x.CategoryID == id).ToList(),
                Categories = _cRep.GetActives()
            };
            return View(pvm);
        }

        //Bu metod admin her siteye girip ürünleri listelettiğinde DepoAPI'daki stok miktarlarını alıp mevcut projenin stok miktarlarını günceller.
        //ShoppingList'te bunu yapmamamızın nedeni Depo'daki stok bittiğinde ya da sipariş edilecek ürün miktarı kadar stok olmadığında Depo'nun alışverişe izin vermemesidir.
        public async void getAsyn()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");
                Task<HttpResponseMessage> getTask = client.GetAsync("Home/GetStock");

                HttpResponseMessage result = new HttpResponseMessage();

                try
                {
                    result = getTask.Result;
                }
                catch (Exception)
                {
                    TempData["hata"] = "DepoAPI bağlantıyı reddetti, stok bilgileri güncel olmayabilir";
                    return;
                }

                if (result.IsSuccessStatusCode)
                {
                    string contentString = await getTask.Result.Content.ReadAsStringAsync();

                    List<StockDTO> resultContent = JsonConvert.DeserializeObject<List<StockDTO>>(contentString);

                    List<Product> abc = resultContent.Select(x => new Product
                    {
                        ID = x.ID,
                        UnitInStock = x.UnitInStock
                    }).ToList();

                    _pRep.UpdateStockRange(abc);
                }
                else
                {
                    TempData["hata"] = "API ile ilgili bir sorun oluştu, stok bilgileri güncel olmayabilir";
                }
            }
        }

        //Burada kategorisi belirtilmiş olan eklenecek ürünün attribute'larının name prop'ları belirtilmiş olarak gelmesini istedik
        public ActionResult AddProduct(string category)
        {
            if (category != null)
            {
                //Bu kısım sayesinde kategorisine göre attribute name'leri seçilmiş şekilde gelicek, kullanıcı sadece attribute value'suna değer ataması yapıcak

                string[] anakart = { "Soket Tipi", "Anakart Markası", "Anakart Yapı", "Maks. Ram Desteği", "Ram Tipi", "Anakart Chipseti", "Ram Slot Sayısı", "Desteklenen Ram Hızı" };
                string[] islemci = { "İşlemci Markası", "İşlemci Hızı", "İşlemci Çekirdek", "Entegre Grafik Kartı", "Soket Tipi", "Maks. Turbo Hızı", "Top. İş Parçacığı", "Grafik Kartı Chipseti" };
                string[] ekranKarti = { "Ekran Kartı Chipseti", "Çekirdek Hücre Sayısı", "Bellek Kapasitesi", "Bellek Arayüzü", "Grafik İşlemci", "Bellek Tipi", "Bellek Hızı", "HDMI" };
                string[] ram = { "Ram Tipi", "Ram Kapasitesi", "Modül Sayısı", "Kullanım Alanı", "Hafıza Bus Hızı", "Kit", "Gecikme Süresi", "Menşei" };
                string[] kasa = { "Boyut", "PSU", "Ön Çıkışlar", "Fan", "Anakart Desteği", "Malzeme" };
                string[] sogutucu = { "Fan RPM", "Fan Boyutu", "Radyatör Boyutu", "Radyatör Malzeme", "Gürültü Seviyesi", "Uygunluk" };
                string[] ssd = { "Kapasite", "Bağlantı", "Okuma Hızı", "Yazma Hızı", "IOPS Okuma", "IOPS Yazma", };


                List<Project.ENTITIES.Models.Attribute> listAttribute = new List<ENTITIES.Models.Attribute>();

                if (category == "Anakart")
                {
                    for (int i = 0; i < anakart.Length; i++)
                    {
                        listAttribute.Add(new Project.ENTITIES.Models.Attribute { Name = anakart[i] });
                    }
                }
                else if (category == "İşlemci")
                {
                    for (int i = 0; i < islemci.Length; i++)
                    {
                        listAttribute.Add(new Project.ENTITIES.Models.Attribute { Name = islemci[i] });
                    }
                }
                else if (category == "Ekran Kartı")
                {
                    for (int i = 0; i < ekranKarti.Length; i++)
                    {
                        listAttribute.Add(new Project.ENTITIES.Models.Attribute { Name = ekranKarti[i] });
                    }
                }
                else if (category == "Ram")
                {
                    for (int i = 0; i < ram.Length; i++)
                    {
                        listAttribute.Add(new Project.ENTITIES.Models.Attribute { Name = ram[i] });
                    }
                }
                else if (category == "Kasa")
                {
                    for (int i = 0; i < kasa.Length; i++)
                    {
                        listAttribute.Add(new ENTITIES.Models.Attribute { Name = kasa[i] });
                    }
                }
                else if (category == "Soğutucu")
                {
                    for (int i = 0; i < sogutucu.Length; i++)
                    {
                        listAttribute.Add(new ENTITIES.Models.Attribute { Name = sogutucu[i] });
                    }
                }
                else if (category == "SSD")
                {
                    for (int i = 0; i < ssd.Length; i++)
                    {
                        listAttribute.Add(new ENTITIES.Models.Attribute { Name = ssd[i] });
                    }
                }

                ProductVM pvm = new ProductVM
                {
                    Product = new Product(),
                    Categories = _cRep.GetActives(),
                    Attributes = listAttribute,
                    Attribute0 = new ENTITIES.Models.Attribute(),
                    Attribute1 = new ENTITIES.Models.Attribute(),
                    Attribute2 = new ENTITIES.Models.Attribute(),
                    Attribute3 = new ENTITIES.Models.Attribute(),
                    Attribute4 = new ENTITIES.Models.Attribute(),
                    Attribute5 = new ENTITIES.Models.Attribute(),
                    Attribute6 = new ENTITIES.Models.Attribute(),
                    Attribute7 = new ENTITIES.Models.Attribute()
                };

                if (category == "Kasa" || category == "Soğutucu" || category == "SSD")
                {
                    pvm.Attribute0.Name = listAttribute[0].Name;
                    pvm.Attribute1.Name = listAttribute[1].Name;
                    pvm.Attribute2.Name = listAttribute[2].Name;
                    pvm.Attribute3.Name = listAttribute[3].Name;
                    pvm.Attribute4.Name = listAttribute[4].Name;
                    pvm.Attribute5.Name = listAttribute[5].Name;
                }
                else
                {
                    pvm.Attribute0.Name = listAttribute[0].Name;
                    pvm.Attribute1.Name = listAttribute[1].Name;
                    pvm.Attribute2.Name = listAttribute[2].Name;
                    pvm.Attribute3.Name = listAttribute[3].Name;
                    pvm.Attribute4.Name = listAttribute[4].Name;
                    pvm.Attribute5.Name = listAttribute[5].Name;
                    pvm.Attribute6.Name = listAttribute[6].Name;
                    pvm.Attribute7.Name = listAttribute[7].Name;
                }


                return View(pvm);
            }
            else
            {
                return RedirectToAction("ProductList");
            }

        }

        [HttpPost]
        public ActionResult AddProduct(ProductVM pvm, HttpPostedFileBase image)
        {
            if (pvm.Product.UnitPrice == 0) pvm.Product.UnitPrice = 1;

            pvm.Product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);
            _pRep.Add(pvm.Product); //API ile haberleşirken ID belirtmemiz gerektiği için DB'ye ürünü kaydetmeliyiz


            StockDTO stock = new StockDTO
            {
                ID = pvm.Product.ID,
                ProductName = pvm.Product.Name,
                UnitPrice = pvm.Product.UnitPrice,
                UnitInStock = pvm.Product.UnitInStock
            };

            //Depo API'da yeni bir action açmak yerine önceden kullanılan List<StockDTO> parametreli action'ı kullandık
            List<StockDTO> listStock = new List<StockDTO> { stock };

            using (HttpClient client = new HttpClient())
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
                    //Eğer API işlemi yapmazsa sitenin DB'sinden de ürünü siliyoruzki her iki DB'de ID uyuşmazlığı yaşanmasın
                    _pRep.Destroy(_pRep.Find(pvm.Product.ID));
                    TempData["hataCRUD"] = "Depo bağlantıyı reddetti";
                    return RedirectToAction("ProductList");
                }

                if (result.IsSuccessStatusCode)
                {
                    if (pvm.Product.Category.Name == "Kasa" || pvm.Product.Category.Name == "Soğutucu" || pvm.Product.Category.Name == "SSD")
                    {
                        List<Project.ENTITIES.Models.Attribute> listAttribute = new List<ENTITIES.Models.Attribute>
                    {
                        pvm.Attribute0,
                        pvm.Attribute1,
                        pvm.Attribute2,
                        pvm.Attribute3,
                        pvm.Attribute4,
                        pvm.Attribute5

                    };
                        _aRep.AddRange(listAttribute);

                        foreach (Project.ENTITIES.Models.Attribute item in listAttribute)
                        {
                            ProductAttribute pa = new ProductAttribute
                            {
                                AttributeID = item.ID,
                                ProductID = pvm.Product.ID
                            };

                            _paRep.Add(pa);//Çoka-çok ilişki tamamlaması
                        }

                        return RedirectToAction("ProductList");
                    }
                    else
                    {
                        List<Project.ENTITIES.Models.Attribute> listAttribute = new List<ENTITIES.Models.Attribute>
                    {
                        pvm.Attribute0,
                        pvm.Attribute1,
                        pvm.Attribute2,
                        pvm.Attribute3,
                        pvm.Attribute4,
                        pvm.Attribute5,
                        pvm.Attribute6,
                        pvm.Attribute7
                    };

                        _aRep.AddRange(listAttribute);

                        foreach (Project.ENTITIES.Models.Attribute item in listAttribute)
                        {
                            ProductAttribute pa = new ProductAttribute
                            {
                                AttributeID = item.ID,
                                ProductID = pvm.Product.ID
                            };

                            _paRep.Add(pa);//Çoka-çok ilişki tamamlaması
                        }

                        return RedirectToAction("ProductList");
                    }


                    /*_aRep.AddRange(listAttribute);*///Çoka-çok ilişkide ID kullanabilmek için DB'ye ekledik



                    return RedirectToAction("ProductList");
                }
                else
                {
                    //Eğer API işlemi yapmazsa sitenin DB'sinden de ürünü siliyoruzki her iki DB'de ID uyuşmazlığı yaşanmasın
                    _pRep.Destroy(_pRep.Find(pvm.Product.ID));
                    TempData["hataCRUD"] = "Depo işlemi reddetti";
                    return RedirectToAction("ProductList");
                }
            }
        }

        public ActionResult UpdateProduct(int id)
        {
            if (id > 0)
            {
                List<Project.ENTITIES.Models.Attribute> listAttribute = new List<ENTITIES.Models.Attribute>();

                List<ProductAttribute> listProductAttribute = new List<ProductAttribute>();

                listProductAttribute.AddRange(_paRep.Where(x => x.ProductID == id));//Ürünün özelliklerini yakaladık


                foreach (ProductAttribute item in listProductAttribute)
                {
                    listAttribute.Add(_aRep.FirstOrDefault(x => x.ID == item.AttributeID));//Ürünün özelliklerini attribute list'ine atadık
                }


                ProductVM pvm = new ProductVM
                {
                    Categories = _cRep.GetActives(),
                    Product = _pRep.Find(id)
                };

                if (pvm.Product.Category.Name == "Kasa" || pvm.Product.Category.Name == "Soğutucu" || pvm.Product.Category.Name == "SSD")
                {
                    pvm.Attribute0 = listAttribute[0];
                    pvm.Attribute1 = listAttribute[1];
                    pvm.Attribute2 = listAttribute[2];
                    pvm.Attribute3 = listAttribute[3];
                    pvm.Attribute4 = listAttribute[4];
                    pvm.Attribute5 = listAttribute[5];
                }
                else
                {
                    pvm.Attribute0 = listAttribute[0];
                    pvm.Attribute1 = listAttribute[1];
                    pvm.Attribute2 = listAttribute[2];
                    pvm.Attribute3 = listAttribute[3];
                    pvm.Attribute4 = listAttribute[4];
                    pvm.Attribute5 = listAttribute[5];
                    pvm.Attribute6 = listAttribute[6];
                    pvm.Attribute7 = listAttribute[7];
                }


                return View(pvm);
            }
            else return RedirectToAction("ProductList");

        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductVM pvm, HttpPostedFileBase image)
        {
            if (pvm.Product.UnitPrice == 0) pvm.Product.UnitPrice = 1;

            StockDTO stock = new StockDTO
            {
                ID = pvm.Product.ID,
                ProductName = pvm.Product.Name,
                UnitPrice = pvm.Product.UnitPrice,
                UnitInStock = pvm.Product.UnitInStock
            };

            using (HttpClient client = new HttpClient())
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
                        pvm.Product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);

                    if (pvm.Product.CategoryID == 1 || pvm.Product.CategoryID == 2 || pvm.Product.CategoryID == 3 || pvm.Product.CategoryID == 4)
                    {
                        List<Project.ENTITIES.Models.Attribute> listAttribute = new List<ENTITIES.Models.Attribute>
                    {
                        pvm.Attribute0,
                        pvm.Attribute1,
                        pvm.Attribute2,
                        pvm.Attribute3,
                        pvm.Attribute4,
                        pvm.Attribute5,
                        pvm.Attribute6,
                        pvm.Attribute7
                    };
                        _aRep.UpdateRange(listAttribute);
                    }
                    else
                    {
                        List<Project.ENTITIES.Models.Attribute> listAttribute = new List<ENTITIES.Models.Attribute>
                    {
                        pvm.Attribute0,
                        pvm.Attribute1,
                        pvm.Attribute2,
                        pvm.Attribute3,
                        pvm.Attribute4,
                        pvm.Attribute5
                        };
                        _aRep.UpdateRange(listAttribute);
                    }



                    _pRep.Update(pvm.Product);
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
            if (id > 0)
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
            else return RedirectToAction("ProductList");

        }
    }
}