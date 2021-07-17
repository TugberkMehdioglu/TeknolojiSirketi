using Project.DTO.Models;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public StockDTO StockDTO { get; set; }
        public PaymentDTO PaymentDTO { get; set; }
        public List<Address> Addresses { get; set; }


    }
}