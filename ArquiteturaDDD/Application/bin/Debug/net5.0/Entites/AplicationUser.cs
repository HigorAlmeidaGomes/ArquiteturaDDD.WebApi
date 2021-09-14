using Entities.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entites
{
    public class AplicationUser : IdentityUser
    {
        [Column("USER_AGE")]
        public int Age { get; set; }

        [Column("USER_CELLPHONE")]
        public string Cellphone { get; set; }
        
        [Column("USER_TYPE")]
        public UserType? Type { get; set; }
    }
}
