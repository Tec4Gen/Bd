using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSU.University.MVC.Views
{
    public class User
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}