using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser : BaseEntity
    {
        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Kullanıcı Adı")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır")]
        [MaxLength(30, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} zorunludur"), Display(Name = "Şifre")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır")]
        [MaxLength(30, ErrorMessage = "{0} en çok {1} karakter olmalıdır")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; } //Bu property DB'ye gitmicek
        public UserRole Role { get; set; }
        public Guid ActivationCode { get; set; }
        public bool Active { get; set; }

        [Required(ErrorMessage ="{0} zorunludur")]
        [MaxLength(80, ErrorMessage ="{0} en fazla {1} karakter olmalıdır")]
        public string Email { get; set; }
        public AppUser()
        {
            Role = UserRole.Member; //Instance alındığında ek bir müdehale olmazsa member rolü atanıcak
            ActivationCode = Guid.NewGuid(); //Instance alındığında guid oluşturduk
        }


        //Relational Properties
        public virtual UserProfile Profile { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
