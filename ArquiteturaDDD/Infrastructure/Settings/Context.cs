using Entities.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Settings
{
    public class Context : IdentityDbContext<AplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }

        public DbSet<AplicationUser> AplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConectionDbBase());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        private string ConectionDbBase()
        {
            string strcon = "Data Source=HIGOR-PC\\SQLEXPRESS; Initial Catalog=ArquiteturaDDD-API; User Id=sa; Password=123456;";

            return strcon;
        }
    }
}
