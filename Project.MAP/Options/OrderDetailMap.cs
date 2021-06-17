using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class OrderDetailMap : BaseMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            //Çoka-çok ilişki tamamlaması
            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.OrderID,
                x.ProductID
            }); //Composite key

            ToTable("Sipariş Detayları");

            Property(x => x.Quantity).HasColumnName("Adet");
            Property(x => x.TotalPrice).HasColumnName("Toplam Tutar");
        }
    }
}
