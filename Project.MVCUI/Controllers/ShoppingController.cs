using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.DTO.Models;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        AddressRep _aRep;
        OrderDetailRep _odRep;
        OrderRep _oRep;
        ProductRep _pRep;
        CategoryRep _cRep;
        public ShoppingController()
        {
            _aRep = new AddressRep();
            _odRep = new OrderDetailRep();
            _oRep = new OrderRep();
            _pRep = new ProductRep();
            _cRep = new CategoryRep();
        }
        public ActionResult ShoppingList(int? page, int?categoryID)
        {
            PAVM pavm = new PAVM
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 6) : _pRep.GetActives().Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 6),
                Categories = _cRep.GetActives(),
                Products = _pRep.GetActives()
            };

            if (categoryID != null) ViewBag.categoryID = categoryID;

            return View(pavm);
        }

        public ActionResult ProductDetail(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _pRep.Find(id)
            };

            return View(pvm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] != null ? Session["scart"] as Cart : new Cart();

            Product UpcomingProduct = _pRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = UpcomingProduct.ID,
                Name = UpcomingProduct.Name,
                Price = UpcomingProduct.UnitPrice,
                ImagePath = UpcomingProduct.ImagePath
            };

            c.SepeteEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");           
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;

                c.SepettenSil(id);

                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CardPage");
            }
            return RedirectToAction("ShoppingList");
        }

        public ActionResult CardPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cpvm = new CartPageVM
                {
                    Cart = Session["scart"] as Cart
                };
                return View(cpvm);
            }
            return RedirectToAction("ShoppingList");
        }

        public ActionResult ConfirmOrder()
        {
            if (Session["member"] == null)
            {
                TempData["siparis"] = "Siparişi tamamlamak için lütfen giriş yapınız";
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                AppUser au = Session["member"] as AppUser;
                OrderVM ovm = new OrderVM
                {
                    Addresses = au.Profile.Addresses
                };

                return View(ovm);
            }
        }

        [HttpPost]
        public ActionResult ConfirmOrder(OrderVM ovm)
        {
            Cart c = Session["scart"] as Cart;

            ovm.PaymentDTO.ShoppingPrice = ovm.Order.TotalPrice = c.TotalPrice;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", ovm.PaymentDTO);

                HttpResponseMessage result;

                try
                {
                    result = postTask.Result;
                }
                catch (Exception)
                {
                    TempData["hata"] = "Banka bağlantıyı reddetti, lütfen bankanızla iletişime geçin";
                    return RedirectToAction("ShoppingList");
                }

                if (result.IsSuccessStatusCode)
                {
                    AppUser user = Session["member"] as AppUser;
                    Order order = new Order()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        AddressID = ovm.Order.AddressID,
                        AppUserID = user.ID,
                        TotalPrice = ovm.Order.TotalPrice
                    };
                    _oRep.Add(order);

                    List<StockDropDTO> listDrop = new List<StockDropDTO>();//DepoAPI için
                    CargoDTO cargo = new CargoDTO(); //KargoAPI için

                    foreach (CartItem item in c.Sepetim)
                    {
                        OrderDetail od = new OrderDetail
                        {
                            OrderID = order.ID,
                            ProductID = item.ID,
                            Quantity = item.Amount,
                            TotalPrice = item.SubTotal
                        };
                        _odRep.Add(od);

                        //DepoAPI'dan da stok düşürücez
                        StockDropDTO dropDTO = new StockDropDTO
                        {
                            ID = item.ID,
                            Quantity = item.Amount
                        };

                        listDrop.Add(dropDTO);
                    }

                    using (HttpClient client2 = new HttpClient())
                    {
                        client2.BaseAddress = new Uri("https://localhost:44339/api/");
                        Task<HttpResponseMessage> postTask2 = client2.PostAsJsonAsync("Home/StockDrop", listDrop);

                        HttpResponseMessage result2;

                        try
                        {
                            result2 = postTask2.Result;
                        }
                        catch (Exception)
                        {
                            TempData["hata"] = "Depo bağlantıyı reddetti, lütfen müşteri hizmetleri ile iletişime geçiniz";
                            return RedirectToAction("ShoppingList");
                        }

                        if (result2.IsSuccessStatusCode)
                        {
                            foreach (CartItem item in c.Sepetim)
                            {
                                Product toBeUpdated = _pRep.Find(item.ID);
                                toBeUpdated.UnitInStock -= item.Amount;
                                _pRep.Update(toBeUpdated);
                            }

                            cargo.FirstName = user.Profile.FirstName;
                            cargo.LastName = user.Profile.LastName;
                            cargo.Email = user.Email;
                            cargo.Phone = "111111"; //CargoAPI'da phone zorunlu ama bu projemizde olmadığı için bu şekilde yaptık

                            foreach (Address item in user.Profile.Addresses)
                            {
                                if (order.AddressID == item.ID) 
                                {
                                    cargo.Address = item.FullAddress;
                                    cargo.Country = item.Country;
                                    cargo.City = item.City;
                                } 
                            }

                            using (HttpClient client3 = new HttpClient())
                            {
                                client3.BaseAddress = new Uri("https://localhost:44351/api/");
                                Task<HttpResponseMessage> postTask3 = client3.PostAsJsonAsync("Home/CargoOrder", cargo);

                                HttpResponseMessage result3;

                                try
                                {
                                    result3 = postTask3.Result;
                                }
                                catch (Exception)
                                {
                                    TempData["hata"] = "Kargo şirketi bağlantıyı reddetti, lütfen müşteri hizmetleri ile iletişime geçiniz";
                                    return RedirectToAction("ShoppingList");
                                }

                                if (result3.IsSuccessStatusCode)
                                {
                                    TempData["odeme"] = "Siparişiniz alınmıştır, teşekkür ederiz";

                                    MailService.Send(user.Email, subject: "Sipariş", body: $"Siparişiniz başarıyla alınmıştır, sipariş tutarınız: {c.TotalPrice.ToString("C2")}");
                                    c.Sepetim.Clear();//Sepeti temizledik
                                    Session.Remove("scart");//Sepeti temizledik
                                    return RedirectToAction("ShoppingList");
                                }
                                else
                                {
                                    TempData["hata"] = "Kargo şirketi ile ilgili bir sorun oluştu, lütfen müşteri hizmetleri ile iletişime geçin";
                                    return RedirectToAction("ShoppingList");
                                }
                            }
                        }
                        else
                        {
                            TempData["hata"] = "Depo ile ilgili bir sorun oluştu, lütfen müşteri hizmetleri ile iletişime geçin";
                            return RedirectToAction("ShoppingList");
                        }
                    }
                }
                else
                {
                    TempData["hata"] = "Ödeme ile ilgili bir sorun oluştu, lütfen bankanızla iletişime geçin";
                    return RedirectToAction("ShoppingList");
                }
            }               
        }
    }
}