using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.CreatedDate).HasColumnName("Yaratılma Tarihi");
            Property(x => x.DeleteDate).HasColumnName("Silinme Tarihi");
            Property(x => x.ModifiedDate).HasColumnName("Güncellenme Tarihi");
            Property(x => x.Status).HasColumnName("Durum");
        }
    }
}
