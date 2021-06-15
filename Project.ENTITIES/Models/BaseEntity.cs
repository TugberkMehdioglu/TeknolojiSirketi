using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    //Bu class'ın görevi sadece inheritance olduğu için abstract yaptım

    //Class'ımın amacı inharantance alan her class'da bulunmasını istediğim ortak özellekleri içermesidir
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }//Null geçilebilir çünkü her instance alındığında buraya veri girişi olma şartı yok
        public DateTime? DeleteDate { get; set; }//Null geçilebilir çünkü her instance alındığında buraya veri girişi olma şartı yok
        public DataStatus Status { get; set; }
        public BaseEntity()
        {
            //Bu class'tan instance alındığı gibi beliritilen değerler atanıcak
            CreatedDate = DateTime.Now; 
            Status = DataStatus.Inserted;
        }
    }
}
