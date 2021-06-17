using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class ProductAttributeMap : BaseMap<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            //Çoka-çok ilişki tamamlaması
            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.ProductID,
                x.AttributeID
            });//Composite key

            ToTable("Ürün Özellikleri");
        }
    }
}
