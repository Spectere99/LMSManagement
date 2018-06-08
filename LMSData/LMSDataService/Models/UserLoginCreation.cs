using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSDataService.Models
{
    public class UserLoginCreation
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}