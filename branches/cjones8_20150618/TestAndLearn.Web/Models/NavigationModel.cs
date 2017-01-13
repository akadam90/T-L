using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Web.Models
{
    public class NavigationModel
    {
        public string ActiveSection { get; set; }
        public UserModel CurrentUser { get; set; }
    }
}