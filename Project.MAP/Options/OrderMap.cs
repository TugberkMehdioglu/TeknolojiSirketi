using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class OrderMap : BaseMap<Order>
    {
        public OrderMap()
        {
            ToTable("Siparişler");

            Property(x => x.UserName).HasColumnName("Kullanıcı Adı");
            Property(x => x.Email);
            Property(x => x.TotalPrice).HasColumnName("Toplam Tutar");
        }
    }
}
