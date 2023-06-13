using Microsoft.EntityFrameworkCore;

namespace SuperHeroes.Models
{
    public class AuthenticationContex : DbContext
    {
        public AuthenticationContex(DbContextOptions<AuthenticationContex> options)
           : base(options)
        {
        }

        public DbSet<AuthenticationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationUser>().ToTable("UserInfo");
            modelBuilder.Entity<AuthenticationUser>().HasKey(e => e.Login);
        }
    }
}
