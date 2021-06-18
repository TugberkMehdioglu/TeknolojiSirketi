using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    //Çoka-çok ilişki
    public class ProductAttribute : BaseEntity
    {
        public int AttributeID { get; set; }
        public int ProductID { get; set; }

        //Relational Properties
        public virtual Attribute Attribute { get; set; }
        public virtual Product Product { get; set; }

    }
}
