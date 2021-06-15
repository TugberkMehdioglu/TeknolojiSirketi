using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class UserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } } //Bu property DB'ye gitmicek
        public string ImagePath { get; set; }


        //Relational Properties
        public virtual AppUser User { get; set; }
        public virtual List<Address> Addresses { get; set; }//Adresi profile ekledik çünkü admin rolündeki kullanıcıların adresi ve profili olmasını istemiyoruz


    }
}
