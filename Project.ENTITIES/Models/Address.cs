using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    //Kullanıcının birden fazla adres kaydedebilmesi ve DB'de en çok hani ülke/şehir/mahalle'ye satış yapılmış sorgusu için bu class'ı açtık
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
        public byte AptNo { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Kapı No")]
        public byte? Flat { get; set; }//Müstakil daireler için null geçilebilir yaptık

        //Bu property DB'ye gitmeyecek
        public string FullAddress 
        {
            get 
            {
                return $"{District} {Neighborhood} {Street} {AptNo}/{Flat} - {City.ToUpper()}/{Country.ToUpper()}";
            } 
        }
        public int UserProfileID { get; set; }


        //Relational Properties
        public virtual UserProfile UserProfile { get; set; }//Adresi profile ekledik çünkü admin rolündeki kullanıcıların adresi ve profili olmasını istemiyoruz
        public virtual List<Order> Orders { get; set; }//Her order'da adres gözükmesini ve her bir adrese hangi siparişlerin gittiğini görmek istiyoruz

    }
}
