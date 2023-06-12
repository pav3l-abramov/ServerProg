using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace SuperHeroes.Models
{
    public class SuperHeroesContext : DbContext
    {
        public SuperHeroesContext(DbContextOptions<SuperHeroesContext> options)
    : base(options)
        {
        }

        public DbSet<AlignmentModel> Alignments { get; set; }
        public DbSet<SuperheroAllParModel> Superheroes { get; set; }
        public DbSet<SuperpowerModel> Superpowers { get; set; }
        public DbSet<HeroPowerModel> HeroPowers { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<SuperpowerModel>().ToTable("Superpower").HasKey(e => e.Superpower_id);

        

            modelBuilder.Entity<AlignmentModel>().ToTable("Alignment");
            modelBuilder.Entity<AlignmentModel>().HasKey(e => e.Alignment_id);
            modelBuilder.Entity<AlignmentModel>()
                .HasMany(e => e.Superheroes)
                .WithOne(e => e.Alignment)
                .HasForeignKey(e => e.Alignment_id)
                .IsRequired();

            modelBuilder.Entity<HeroPowerModel>().ToTable("HeroPower")
                .HasKey(e => new { e.Superhero_id, e.Superpower_id });

            modelBuilder.Entity<SuperheroAllParModel>().ToTable("Superhero");
            modelBuilder.Entity<SuperheroAllParModel>().HasKey(e => e.Superhero_id);
            modelBuilder.Entity<SuperheroAllParModel>()
                .HasMany(e => e.Superpowers)
                .WithMany(e => e.Superheroes)
            .UsingEntity<HeroPowerModel>();

        }
    }
}
