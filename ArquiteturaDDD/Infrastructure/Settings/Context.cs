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
            string strcon = "Data Source=Higor-PC\\SQLEXPRESS;Initial Catalog=API-Arquitetura-DDD-DB;Integrated Security=False;User ID=sa;Password=123456;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

            return strcon;
        }
    }
}
