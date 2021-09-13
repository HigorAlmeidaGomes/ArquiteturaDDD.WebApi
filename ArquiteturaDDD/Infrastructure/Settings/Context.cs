using Entities.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class Context : IdentityDbContext<AplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }

        public DbSet<AplicationUser> AplicationUser { get; set; }


        private string ConectionDbBase() 
        {
            return "";
        }
    }
}
