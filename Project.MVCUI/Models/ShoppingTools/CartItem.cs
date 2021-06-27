using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ShoppingTools
{
    public class CartItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Amount { get; set; }
        public string ImagePath { get; set; }
        public decimal SubTotal 
        {
            get
            {
                return (Amount * Price);
            }  
        }
        public CartItem()
        {
            //Struct type'lar default olarak 0 alır, sepete ürün attığımızda adedi 1 olması lazım o yüzden ctor'da 1 arttırdık
            ++Amount; 
        }
    }
}