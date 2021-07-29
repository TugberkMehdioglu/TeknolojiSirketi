using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class ProductVM
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Project.ENTITIES.Models.Attribute> Attributes { get; set; }

        public Project.ENTITIES.Models.Attribute Attribute0 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute1 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute2 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute3 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute4 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute5 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute6 { get; set; }
        public Project.ENTITIES.Models.Attribute Attribute7 { get; set; }

    }
}