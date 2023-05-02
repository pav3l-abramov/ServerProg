using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Testimonial> Testimonials { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Testimonial>().ToTable("Testimonial");
        }
    }
}