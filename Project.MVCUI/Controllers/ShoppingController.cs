using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
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
                    }

                    TempData["odeme"] = "Siparişiniz alınmıştır, teşekkür ederiz";

                    MailService.Send(user.Email, subject: "Sipariş", body: $"Siparişiniz başarıyla alınmıştır, sipariş tutarınız: {c.TotalPrice.ToString("C2")}");
                    return RedirectToAction("ShoppingList");

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