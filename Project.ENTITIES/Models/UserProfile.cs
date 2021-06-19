using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class UserProfile : BaseEntity
    {
        [Required(ErrorMessage ="{0} zorunludur"), Display(Name ="İsim")]
        [MaxLength(100, ErrorMessage ="{0} en fazla {1} karakter alabilir")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="{0} zorunludur"), Display(Name ="Soyisim")]
        [MaxLength(100, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } } //Bu property DB'ye gitmicek
        public string ImagePath { get; set; }

        public UserProfile()
        {
            //Initilization ile DB oluşturulurken SaveChanges() demeden UserProfile'ın Addresses kısmına veri girişi yapabilmek için List instance'ı aldık
            Addresses = new List<Address>();
        }

        //Relational Properties
        public virtual AppUser User { get; set; }
        public virtual List<Address> Addresses { get; set; }//Adresi profile ekledik çünkü admin rolündeki kullanıcıların adresi ve profili olmasını istemiyoruz


    }
}
