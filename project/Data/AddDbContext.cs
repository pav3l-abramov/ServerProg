using FilmOMDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmOMDB.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<UserFilm> UserFilms { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFilm>()
                .HasKey(ug => new { ug.UserId, ug.FilmId });

            modelBuilder.Entity<UserFilm>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserFilms)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserFilm>()
                .HasOne(ug => ug.Film)
                .WithMany(g => g.UserFilms)
                .HasForeignKey(ug => ug.FilmId);
        }
    }

}

