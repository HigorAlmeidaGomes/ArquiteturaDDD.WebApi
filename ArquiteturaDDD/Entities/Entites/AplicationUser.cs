using Entities.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entites
{
    public class AplicationUser : IdentityUser
    {
        [Column("USER_AGE")]
        public int Age { get; set; }

        [Column("USER_CELLPHONE")]
        public int Cellphone { get; set; }
        
        [Column("USER_TYPE")]
        public UserType? Type { get; set; }
    }
}
