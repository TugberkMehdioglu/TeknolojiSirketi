using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class AttributeMap : BaseMap<ENTITIES.Models.Attribute>
    {
        public AttributeMap()
        {

            ToTable("Özellikler");

            Property(x => x.Name).HasColumnName("Ad").IsRequired().HasMaxLength(25);
            Property(x => x.Value).HasColumnName("Değer").IsRequired().HasMaxLength(50);
        }
    }
}
