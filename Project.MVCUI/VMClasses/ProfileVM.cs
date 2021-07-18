using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class ProfileVM
    {
        public AppUser User { get; set; }
        public UserProfile Profile { get; set; }
        public Address Address { get; set; }

    }
}