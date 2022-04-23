using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Ürün Adı")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır")]
        [MaxLength(100, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Birim Fiyatı")]
        [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "{0} 0'dan büyük olmalıdır")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Stok")]
        [Range(minimum: 0, maximum: double.MaxValue, ErrorMessage = "{0} 0'dan büyük olmalıdır")]
        public short UnitInStock { get; set; }
        public string ImagePath { get; set; }
        public int CategoryID { get; set; }
        public Product()
        {
            //Initilization ile DB oluşturulurken SaveChanges() demeden Product'ın Attributes kısmına veri girişi yapabilmek için List instance'ı aldık
            Attributes = new List<ProductAttribute>();
        }


        //Relational Properties
        public virtual List<ProductAttribute> Attributes { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }


    }
}
