﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class OrderDetail : BaseEntity
    {
        public Order OrderID { get; set; }
        public Product ProductID { get; set; }
        public short Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        //Relational Properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}