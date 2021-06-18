using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Entities'deki data annotations ile uyumlu bir şekilde DB kuruyoruz.
    public class CategoryMap : BaseMap<Category>
    {
        public CategoryMap()
        {
            ToTable("Kategoriler");

            Property(x => x.Name).HasColumnName("Adı").IsRequired().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("Açıklaması");
        }
    }
}
