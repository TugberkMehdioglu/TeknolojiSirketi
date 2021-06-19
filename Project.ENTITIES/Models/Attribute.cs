using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Attribute : BaseEntity
    {
        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Özellik Adı")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır")]
        [MaxLength(80, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Özellik Adı")]
        [MaxLength(80, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string Value { get; set; }

        //Relational Properties
        public virtual List<ProductAttribute> ProductAttributes { get; set; }

    }
}
