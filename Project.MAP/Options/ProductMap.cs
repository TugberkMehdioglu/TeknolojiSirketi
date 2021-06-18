using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class ProductMap : BaseMap<Product>
    {
        public ProductMap()
        {
            ToTable("Ürünler");

            Property(x => x.Name).HasColumnName("Ad").IsRequired().HasMaxLength(100);
            Property(x => x.UnitPrice).HasColumnName("Birim Fiyat").IsRequired();
            Property(x => x.UnitInStock).HasColumnName("Stok").IsRequired();
            Property(x => x.ImagePath).HasColumnName("Resim");
        }
    }
}
