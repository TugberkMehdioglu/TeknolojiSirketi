using Bogus.DataSets;
using Microsoft.Build.Utilities;
using Project.COMMON.Tools;
using Project.DAL.ContextClasses;
using Project.DTO.Models;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Address = Project.ENTITIES.Models.Address;
using Attribute = Project.ENTITIES.Models.Attribute;

namespace Project.DAL.StrategyPattern
{
    public class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            AppUser admin = new AppUser
            {
                UserName = "admin",
                Password = DantexCryptex.Crypt("123"),
                ConfirmPassword = DantexCryptex.Crypt("123"),
                Role = ENTITIES.Enums.UserRole.Admin,
                Active = true, //Mail aktivasyonu kabul edilmiş şekilde oluşturduk
                Email = "tugberkmehdioglu@yandex.com"
            };

            context.AppUsers.Add(admin);

            AppUser member = new AppUser
            {
                UserName = "member",
                Password = DantexCryptex.Crypt("123"),
                ConfirmPassword = DantexCryptex.Crypt("123"),
                Role = ENTITIES.Enums.UserRole.Member,
                Active = true, //Mail aktivasyonu kabul edilmiş şekilde oluşturduk
                Email = "deneme2@gmail.com"
            };

            context.AppUsers.Add(member);
            context.SaveChanges(); //Profile'a ID verebilmek için saveChanges yapmalıyızki user'ın ID'si oluşsun

            UserProfile profile = new UserProfile
            {
                ID = 2,
                FirstName = "Hakkı",
                LastName = "Ünal",
            };

            Address adres = new ENTITIES.Models.Address
            {
                Name = "Ev",
                Country = "Türkiye",
                City = "İstanbul",
                District = "Beşiktaş",
                Neighborhood = "4.Levent",
                Street = "Meşeli sok.",
                AptNo = 12,
                Flat = 8,
            };
            profile.Addresses.Add(adres); //Bire-çok ilişki tamamlaması
            context.Profiles.Add(profile);
            context.SaveChanges();


            List<StockDTO> dto = new List<StockDTO>();


            List<Project.ENTITIES.Models.Attribute> anakartList = new List<Attribute>
            {
                new Attribute{Name="Soket Tipi", Value="Soket 1200"},
                new Attribute{Name="Anakart Markası", Value="GIGABYTE"},
                new Attribute{Name="Anakart Yapı", Value="ATX"},
                new Attribute{Name="Maks. Ram Desteği", Value="128GB"},
                new Attribute{Name="Ram Tipi", Value="DDR4"},
                new Attribute{Name="Anakart Chipseti", Value="Intel® Z590"},
                new Attribute{Name="Ram Slot Sayısı", Value="4"},
                new Attribute{Name="Desteklenen Ram Hızı", Value="2133 MHz - 2400 MHz"}
            };
            context.Attributes.AddRange(anakartList);
            context.SaveChanges();


            Category anakart = new Category
            {
                Name = "Anakart",
                Description = "Anakart modelleri, fiyatları ve anakart markaları en uygun özel taksit seçenekleriyle Vatan Bilgisayar'da!",
            };
            context.Categories.Add(anakart);
            context.SaveChanges();

            Product urun = new Product
            {
                Name = $"GIGABYTE Z590 AORUS MASTER Intel Z590 Soket 1200 DDR4 5400MHz (O.C) M.2 Anakart",
                UnitPrice = 5.668m,
                ImagePath = "/Pictures/anakartUrun.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urun);
            context.SaveChanges();

            Product urun1 = new Product
            {
                Name = $"GIGABYTE B550M S2H 5000MHz(OC) DDR4 Soket AM4 M.2 HDMI DVI VGA mATX Anakart",
                UnitPrice = 799,
                ImagePath = "/Pictures/anakartUrun1.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urun1);
            context.SaveChanges();

            Product urun2 = new Product
            {
                Name = $"MSI MPG B560I GAMING EDGE WIFI 5200MHz(OC) DDR4 Soket M.2 HDMI DP mITX Anakart",
                UnitPrice = 1.988m,
                ImagePath = "/Pictures/anakartUrun2.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urun2);
            context.SaveChanges();

            Product urun3 = new Product
            {
                Name = $"ASUS TUF GAMING A520M-PLUS WIFI 4800MHz(OC) DDR4 Soket AM4 M.2 HDMI DP VGA mATX Anakart",
                UnitPrice = 1.134m,
                ImagePath = "/Pictures/anakartUrun3.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urun3);
            context.SaveChanges();

            Product urun4 = new Product
            {
                Name = $"ASUS ROG MAXIMUS XIII EXTREME Z590 5333MHz(OC) DDR4 Soket1200 M.2 Wi-Fi HDMI EATX Anakart",
                UnitPrice = 10.234m,
                ImagePath = "/Pictures/anakartUrun4.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urun4);
            context.SaveChanges();



            //Çoka-çok ilişki tamamlaması
            foreach (Product item in anakart.Products)
            {
                foreach (Project.ENTITIES.Models.Attribute element in anakartList)
                {
                    ProductAttribute pa = new ProductAttribute
                    {
                        ProductID = item.ID,
                        AttributeID = element.ID
                    };
                    context.ProductAttributes.Add(pa);
                }
            }
            context.SaveChanges();


            List<Project.ENTITIES.Models.Attribute> islemciList = new List<Attribute>
            {
                new Attribute{Name="İşlemci Markası", Value="Intel"},
                new Attribute{Name="İşlemci Hızı", Value="3.6 GHz"},
                new Attribute{Name="İşlemci Çekirdek", Value="8 Çekirdek"},
                new Attribute{Name="Entegre Grafik Kartı", Value="Var"},
                new Attribute{Name="Soket Tipi", Value="Soket 1151-V.2"},
                new Attribute{Name="Maks. Turbo Hızı", Value="5,0 GHz"},
                new Attribute{Name="Top.İş Parçacığı", Value="16"},
                new Attribute{Name="Grafik Kartı Chipseti", Value="Intel UHD 630 Serisi"}
            };
            context.Attributes.AddRange(islemciList);
            context.SaveChanges();


            Category İslemci = new Category
            {
                Name = "İşlemci",
                Description = "İşlemci fiyatları amd intel işlemci markaları ve modelleri en uygun taksit seçenekleri sadece Vatan Bilgisayar'da!",
            };

            for (int i = 0; i < 10; i++)
            {
                Product urun = new Product
                {
                    Name = "Intel Core i9 9900K Soket 1151 - 9. Nesil 3.6GHz 16MB Önbellek 14nm İşlemci",
                    UnitPrice = 5.906m,
                    UnitInStock = 100,
                    ImagePath = "/Pictures/İşlemci.jpg",
                };
                İslemci.Products.Add(urun);
            }
            context.Categories.Add(İslemci);
            context.SaveChanges();

            //Çoka-çok ilişki tamamlaması
            foreach (Product item in İslemci.Products)
            {
                foreach (Project.ENTITIES.Models.Attribute element in islemciList)
                {
                    ProductAttribute pa = new ProductAttribute
                    {
                        ProductID = item.ID,
                        AttributeID = element.ID
                    };
                    context.ProductAttributes.Add(pa);
                }
            }
            context.SaveChanges();


            List<Project.ENTITIES.Models.Attribute> ekrankartiList = new List<Attribute>
            {
                new Attribute{Name="Ekran Kartı Chipset", Value="NVIDIA®"},
                new Attribute{Name="Çekirdek Hücre Sayısı", Value="10496 Cuda"},
                new Attribute{Name="Bellek Kapasitesi", Value="24GB"},
                new Attribute{Name="Bellek Arayüzü", Value="384 Bit"},
                new Attribute{Name="Grafik İşlemci", Value="GeForce RTX 3090"},
                new Attribute{Name="Bellek Tipi", Value="GDDR6X"},
                new Attribute{Name="Bellek Hızı", Value="19500 MHz"},
                new Attribute{Name="HDMI", Value="1"}
            };
            context.Attributes.AddRange(ekrankartiList);
            context.SaveChanges();


            Category ekranKarti = new Category
            {
                Name = "Ekran Kartı",
                Description = "Nvidia ve amd gibi markalardan oluşan ekran kartı modellerini en uygun fiyatlarla ekran kartları ve özel taksit seçenekleriyle Vatan Bilgisayar'da"
            };

            for (int i = 0; i < 10; i++)
            {
                Product urun = new Product
                {
                    Name = "MSI GEFORCE RTX 3090 GAMING X TRIO 24G 24GB GDDR6X 384Bit",
                    UnitPrice = 40.732m,
                    UnitInStock = 100,
                    ImagePath = "/Pictures/EkranKarti.jpg"
                };
                ekranKarti.Products.Add(urun);
            }
            context.Categories.Add(ekranKarti);
            context.SaveChanges();

            //Çoka-çok ilişki tamamlaması
            foreach (Product item in ekranKarti.Products)
            {
                foreach (Project.ENTITIES.Models.Attribute element in ekrankartiList)
                {
                    ProductAttribute pa = new ProductAttribute
                    {
                        ProductID = item.ID,
                        AttributeID = element.ID
                    };
                    context.ProductAttributes.Add(pa);
                }
            }
            context.SaveChanges();


            List<Project.ENTITIES.Models.Attribute> ramList = new List<Attribute>
            {
                new Attribute{Name="Ram Tipi", Value="DDR4"},
                new Attribute{Name="Ram Kapasitesi", Value="128GB"},
                new Attribute{Name="Modül Sayısı", Value="4 x 32GB"},
                new Attribute{Name="Kullanım Alanı", Value="PC"},
                new Attribute{Name="Hafıza Bus Hızı", Value="3200 MHz"},
                new Attribute{Name="Kit", Value="Quad Kit"},
                new Attribute{Name="Gecikme Süresi", Value="CL16"},
                new Attribute{Name="Menşei", Value="Çin"}
            };
            context.Attributes.AddRange(ramList);
            context.SaveChanges();


            Category ram = new Category
            {
                Name = "Ram",
                Description = "Ram modelleri ve bellekler en uygun fiyat seçenekleri, farklı taksit imkanları ve kampanyalarla her zaman Vatan Bilgisayar'da. Hemen satın al."
            };

            for (int i = 0; i < 10; i++)
            {
                Product urun = new Product
                {
                    Name = "GSKILL 128GB (4X32GB) Trident Z Neo DDR4 3200MHz CL16 AMD Ryzen RGB LED Ram",
                    UnitPrice = 10.049m,
                    UnitInStock = 100,
                    ImagePath = "/Pictures/Ram.jpg"
                };
                ram.Products.Add(urun);
            }
            context.Categories.Add(ram);
            context.SaveChanges();

            //Çoka-çok ilişki tamamlaması
            foreach (Product item in ram.Products)
            {
                foreach (Project.ENTITIES.Models.Attribute element in ramList)
                {
                    ProductAttribute pa = new ProductAttribute
                    {
                        ProductID = item.ID,
                        AttributeID = element.ID
                    };
                    context.ProductAttributes.Add(pa);
                }
            }
            context.SaveChanges();


            dto.AddRange(anakart.Products.Select(x => new StockDTO
            {
                ID = x.ID,
                ProductName = x.Name,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock
            }).ToList());

            dto.AddRange(İslemci.Products.Select(x => new StockDTO
            {
                ID = x.ID,
                ProductName = x.Name,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock
            }).ToList());

            dto.AddRange(ekranKarti.Products.Select(x => new StockDTO
            {
                ID = x.ID,
                ProductName = x.Name,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock
            }).ToList());

            dto.AddRange(ram.Products.Select(x => new StockDTO
            {
                ID = x.ID,
                ProductName = x.Name,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock
            }).ToList());

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Home/AddStocks", dto);

                HttpResponseMessage result;

                try
                {
                    result = postTask.Result;
                }
                catch (Exception)
                {

                    throw new Exception("DepoAPI çalışmayı durdurdu");
                }
            }

        }
    }
}
