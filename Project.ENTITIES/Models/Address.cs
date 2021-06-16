using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    //Kullanıcının birden fazla adres kaydedebilmesi için bu class'ı açtık
    public class Address : BaseEntity
    {
        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Adres Adı")]
        [MaxLength(20, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Ülke")]
        public string Country { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Şehir")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "İlçe")]
        public string District { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Mahalle")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Sokak")]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Apartman No")]
        public byte ApartmentNo { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Kapı No")]
        public byte Flat { get; set; }


        //Bu property DB'ye gitmicek
        public string FullAddress 
        {
            get 
            {
                return $"{District} {Neighborhood} {Street} {ApartmentNo}/{Flat} - {City.ToUpper()}/{Country.ToUpper()}";
            } 
        }
        public bool Use { get; set; }
        public UserProfile UserProfileID { get; set; }
        public Address()
        {
            Use = false;//Kullanıcı siparişini verirken siparişin gideceği adresi, kendisinin seçmesini istediğimiz için false yaptık
        }


        //Relational Properties
        public virtual UserProfile UserProfile { get; set; }//Adresi profile ekledik çünkü admin rolündeki kullanıcıların adresi ve profili olmasını istemiyoruz
        public virtual List<Order> Orders { get; set; }//Her order'da adres gözükmesini ve her bir adrese hangi siparişlerin gittiğini görmek istiyoruz

    }
}
