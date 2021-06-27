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
                Description = "Anakart modelleri, fiyatları ve anakart markaları en uygun özel taksit seçenekleriyle Tekno Center'da!",
            };
            context.Categories.Add(anakart);
            context.SaveChanges();

            Product urunAnakart = new Product
            {
                Name = $"GIGABYTE Z590 AORUS MASTER Intel Z590 Soket 1200 DDR4 5400MHz (O.C) M.2 Anakart",
                UnitPrice = 5668m,
                ImagePath = "/Pictures/anakartUrun.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urunAnakart);

            Product urunAnakart1 = new Product
            {
                Name = $"GIGABYTE B550M S2H 5000MHz(OC) DDR4 Soket AM4 M.2 HDMI DVI VGA mATX Anakart",
                UnitPrice = 799,
                ImagePath = "/Pictures/anakartUrun1.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urunAnakart1);

            Product urunAnakart2 = new Product
            {
                Name = $"MSI MPG B560I GAMING EDGE WIFI 5200MHz(OC) DDR4 Soket M.2 HDMI DP mITX Anakart",
                UnitPrice = 1988m,
                ImagePath = "/Pictures/anakartUrun2.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urunAnakart2);

            Product urunAnakart3 = new Product
            {
                Name = $"ASUS TUF GAMING A520M-PLUS WIFI 4800MHz(OC) DDR4 Soket AM4 M.2 HDMI DP VGA mATX Anakart",
                UnitPrice = 1134m,
                ImagePath = "/Pictures/anakartUrun3.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urunAnakart3);

            Product urunAnakart4 = new Product
            {
                Name = $"ASUS ROG MAXIMUS XIII EXTREME Z590 5333MHz(OC) DDR4 Soket1200 M.2 Wi-Fi HDMI EATX Anakart",
                UnitPrice = 10234m,
                ImagePath = "/Pictures/anakartUrun4.jpg",
                UnitInStock = 100
            };
            anakart.Products.Add(urunAnakart4);
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


            Category Islemci = new Category
            {
                Name = "İşlemci",
                Description = "İşlemci fiyatları amd intel işlemci markaları ve modelleri en uygun taksit seçenekleri sadece Tekno Center'da!",
            };

            context.Categories.Add(Islemci);
            context.SaveChanges();

            Product urunIslemci = new Product
            {
                Name = "Intel Core i9 9900K Soket 1151 - 9. Nesil 3.6GHz 16MB Önbellek 14nm İşlemci",
                UnitPrice = 5906m,
                UnitInStock = 100,
                ImagePath = "/Pictures/islemciUrun.jpg",
            };
            Islemci.Products.Add(urunIslemci);

            Product urunIslemci1 = new Product
            {
                Name = "INTEL Core i9 11900F 2.5GHz 16MB Önbellek 8 Çekirdek 1200 14nm İşlemci",
                UnitPrice = 4299m,
                UnitInStock = 100,
                ImagePath = "/Pictures/islemciUrun1.jpg",
            };
            Islemci.Products.Add(urunIslemci1);

            Product urunIslemci2 = new Product
            {
                Name = "AMD RYZEN 7 3700X 3.6GHz 32MB Önbellek 8 Çekirdek AM4 7nm İşlemci",
                UnitPrice = 2799m,
                UnitInStock = 100,
                ImagePath = "/Pictures/islemciUrun2.jpg",
            };
            Islemci.Products.Add(urunIslemci2);

            Product urunIslemci3 = new Product
            {
                Name = "AMD RYZEN 5 1600 (AF) 3.2GHz 19MB Önbellek 6 Çekirdek AM4 14nm İşlemci",
                UnitPrice = 1383m,
                UnitInStock = 100,
                ImagePath = "/Pictures/islemciUrun3.jpg",
            };
            Islemci.Products.Add(urunIslemci3);

            Product urunIslemci4 = new Product
            {
                Name = "AMD RYZEN 9 5950X 3.4GHz 64MB Önbellek 16 Çekirdek AM4 7nm İşlemci",
                UnitPrice = 9419m,
                UnitInStock = 100,
                ImagePath = "/Pictures/islemciUrun4.jpg",
            };
            Islemci.Products.Add(urunIslemci4);
            context.SaveChanges();


            //Çoka-çok ilişki tamamlaması
            foreach (Product item in Islemci.Products)
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
                Description = "Nvidia ve amd gibi markalardan oluşan ekran kartı modellerini en uygun fiyatlarla ekran kartları ve özel taksit seçenekleriyle Tekno Center'da"
            };

            context.Categories.Add(ekranKarti);
            context.SaveChanges();

            Product urunEkranKarti = new Product
            {
                Name = "MSI GEFORCE RTX 3090 GAMING X TRIO 24G 24GB GDDR6X 384Bit",
                UnitPrice = 40732m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ekranKartıUrun.jpg"
            };
            ekranKarti.Products.Add(urunEkranKarti);

            Product urunEkranKarti1 = new Product
            {
                Name = "GIGABYTE Radeon RX 6900 XT GAMING OC 16GB GDDR6 256 Bit Ekran Kartı",
                UnitPrice = 25428m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ekranKartıUrun1.jpg"
            };
            ekranKarti.Products.Add(urunEkranKarti1);

            Product urunEkranKarti2 = new Product
            {
                Name = "MSI GeForce GT 1030 2GH LP OC 2GB GDDR5 64 Bit Ekran Kartı",
                UnitPrice = 1031m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ekranKartıUrun2.jpg"
            };
            ekranKarti.Products.Add(urunEkranKarti2);

            Product urunEkranKarti3 = new Product
            {
                Name = "MSI GeForce RTX 3070 Ti VENTUS 3X 8GB GDDR6 256 Bit Ekran Kartı",
                UnitPrice = 6427m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ekranKartıUrun3.jpg"
            };
            ekranKarti.Products.Add(urunEkranKarti3);

            Product urunEkranKarti4 = new Product
            {
                Name = "SAPPHIRE RX 550 PULSE OC 4GB GDDR5 128Bit DX12 Ekran Kartı",
                UnitPrice = 2459m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ekranKartıUrun4.jpg"
            };
            ekranKarti.Products.Add(urunEkranKarti4);
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
                Description = "Ram modelleri ve bellekler en uygun fiyat seçenekleri, farklı taksit imkanları ve kampanyalarla her zaman Tekno Center'da. Hemen satın al."
            };
            context.Categories.Add(ram);
            context.SaveChanges();

            Product urunRAM = new Product
            {
                Name = "GSKILL 128GB (4X32GB) Trident Z Neo DDR4 3200MHz CL16 AMD Ryzen RGB LED Ram",
                UnitPrice = 10049m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun.jpg"
            };
            ram.Products.Add(urunRAM);

            Product urunRAM1 = new Product
            {
                Name = "CORSAIR 32GB (4x8GB) Vengeance RGB PRO Siyah 3600MHz CL16 DDR4 Quad Kit Ram",
                UnitPrice = 2499m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun1.jpg"
            };
            ram.Products.Add(urunRAM1);

            Product urunRAM2 = new Product
            {
                Name = "PATRIOT 8GB Viper Steel Siyah 3000MHz CL16 DDR4 Single Kit Ram",
                UnitPrice = 399m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun2.jpg"
            };
            ram.Products.Add(urunRAM2);

            Product urunRAM3 = new Product
            {
                Name = "SILICON POWER 32GB (2x16GB) XPOWER Turbine RGB Gri 3200MHz CL16 DDR4 Dual Kit Ram",
                UnitPrice = 2309m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun3.jpg"
            };
            ram.Products.Add(urunRAM3);

            Product urunRAM4 = new Product
            {
                Name = "HYPERX 16GB Fury 3200MHz CL16 DDR4 Single Kit Ram",
                UnitPrice = 908m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun4.jpg"
            };
            ram.Products.Add(urunRAM4);

            Product urunRAM5 = new Product
            {
                Name = "XPG 16GB (2x8GB) SPECTRIX D50 Beyaz RGB 4133MHz CL19 DDR4 Dual Kit Ram",
                UnitPrice = 1451m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun4.jpg"
            };
            ram.Products.Add(urunRAM5);

            Product urunRAM6 = new Product
            {
                Name = "PATRIOT 16GB (2x8GB) Viper Rgb Bla 3200MHz CL16 DDR4 Dual Kit Ram",
                UnitPrice = 1072m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun6.jpg"
            };
            ram.Products.Add(urunRAM6);

            Product urunRAM7 = new Product
            {
                Name = "XPG 8GB Gammix D30 Kırmızı 3200MHz CL16 DDR4 Single Kit Ram",
                UnitPrice = 501m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun6.jpg"
            };
            ram.Products.Add(urunRAM7);

            Product urunRAM8 = new Product
            {
                Name = "CORSAIR 8GB Vengeance LPX Siyah 3600MHz CL18 DDR4 Single Kit Ram",
                UnitPrice = 603m,
                UnitInStock = 100,
                ImagePath = "/Pictures/ramUrun8.jpg"
            };
            ram.Products.Add(urunRAM8);
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

            dto.AddRange(Islemci.Products.Select(x => new StockDTO
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
