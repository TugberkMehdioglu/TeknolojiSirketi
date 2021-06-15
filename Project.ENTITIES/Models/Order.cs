using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order : BaseEntity
    {
        //------ AppUser'dan bu bilgileri çektik çünkü Order tablosundan direk bu bilgilere ulaşılmasını istedik
        public string UserName { get; set; }
        public string Email { get; set; }
        //------

        public decimal TotalPrice { get; set; }
        public Address AddressID { get; set; }



        //Relational Properties
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Address Address { get; set; }//Her order'da adres gözükmesini ve her bir adrese hangi siparişlerin gittiğini görmek istiyoruz
    }
}
