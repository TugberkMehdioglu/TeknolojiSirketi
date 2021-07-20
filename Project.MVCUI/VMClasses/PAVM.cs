using PagedList;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    //FrontEnd'de sayfalandırma yapabilmek için PagedList.MVC kütüphanesini kullandık
    public class PAVM
    {
        public IPagedList<Product> PagedProducts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

    }
}