using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class UserProfileMap : BaseMap<UserProfile>
    {
        public UserProfileMap()
        {
            Ignore(x => x.FullName);

            ToTable("Profiller");

            Property(x => x.FirstName).HasColumnName("İsim").IsRequired().HasMaxLength(60);
            Property(x => x.LastName).HasColumnName("Soyisim").IsRequired().HasMaxLength(40);
            Property(x => x.ImagePath).HasColumnName("Resim");
        }
    }
}
