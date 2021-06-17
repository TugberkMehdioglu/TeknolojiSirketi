using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class AddressMap : BaseMap<Address>
    {
        //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
        public AddressMap()
        {
            Ignore(x => x.FullAddress);

            ToTable("Adresler");
            Property(x => x.Name).HasColumnName("Adres Adı").HasMaxLength(20).IsRequired();
            Property(x => x.Country).HasColumnName("Ülke").IsRequired();
            Property(x => x.City).HasColumnName("Şehir").IsRequired();
            Property(x => x.District).HasColumnName("İlçe").IsRequired();
            Property(x => x.Neighborhood).HasColumnName("Mahalle").IsRequired();
            Property(x => x.Street).HasColumnName("Sokak").IsRequired();
            Property(x => x.AptNo).HasColumnName("Apt. No").IsRequired();
            Property(x => x.Flat).HasColumnName("Kat No").IsOptional();
        }
    }
}
