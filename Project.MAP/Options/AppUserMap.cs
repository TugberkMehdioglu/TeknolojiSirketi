using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class AppUserMap : BaseMap<AppUser>
    {
        public AppUserMap()
        {
            HasOptional(x => x.Profile).WithRequired(x => x.User); //Bire - bir ilişki tamamlaması

            Ignore(x => x.ConfirmPassword);

            ToTable("Kullanıcılar");
            Property(x => x.UserName).HasColumnName("Kullanıcı Adı").HasMaxLength(15).IsRequired();
            Property(x => x.Password).HasColumnName("Şifre").HasMaxLength(20).IsRequired();
            Property(x => x.Role).HasColumnName("Rol");
            Property(x => x.ActivationCode).HasColumnName("Aktivasyon Kodu");
            Property(x => x.Active).HasColumnName("Aktif");
            Property(x => x.Email).IsRequired().HasMaxLength(60);
        }
    }
}
