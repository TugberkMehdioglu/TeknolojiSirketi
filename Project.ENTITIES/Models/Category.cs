using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Ürün Adı")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır")]
        [MaxLength(30, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string Name { get; set; }
        public string Description { get; set; }

        public Category()
        {
            //FakeData ile DB oluşturulurken SaveChanges() demeden Category'nin Products kısmına veri girişi yapabilmek için List instance'ı aldık
            Products = new List<Product>();
        }

        //Relational Properties
        public virtual List<Product> Products { get; set; }
    }
}
