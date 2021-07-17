using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO.Models
{
    public class PaymentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Bu alan zorunludur")]
        public string CardUserName { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur")]
        public string SecurityNumber { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur")]
        public int CardExpiryMonth { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur")]
        public int CardExpiryYear { get; set; }

        public decimal ShoppingPrice { get; set; }
    }
}
