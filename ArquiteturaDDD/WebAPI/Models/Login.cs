using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Login
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string Cellphone { get; set; }
    }
}
