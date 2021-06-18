using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.ContextClasses;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            context.Profiles.Add(profile);
            context.SaveChanges();



            Category anakart = new Category
            {
                Name = "Anakart",
                Description = "Anakart modelleri, fiyatları ve anakart markaları en uygun özel taksit seçenekleriyle Vatan Bilgisayar'da!",
            };

            for (int i = 0; i < 10; i++)
            {
                Product urun = new Product
                {
                    Name = $"GIGABYTE Z590 AORUS MASTER Intel Z590 Soket 1200 DDR4 5400MHz (O.C) M.2 Anakart",
                    UnitPrice = 5.668m,
                    ImagePath = "/Pictures/Anakart.jpg",
                    UnitInStock = 100
                };
                anakart.Products.Add(urun);
            }
            context.Categories.Add(anakart);
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
        }
    }
}
