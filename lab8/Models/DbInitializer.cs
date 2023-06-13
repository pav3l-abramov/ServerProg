using Microsoft.EntityFrameworkCore;
namespace SuperHeroes.Models
{
    public class DbInitializer
    {
        public static void Initialize(DbContext DbContext)
        {
            if (DbContext.GetType() == typeof(SuperHeroesContext))
            {
                var context = (SuperHeroesContext)DbContext;
                string script;
                if (!context.Alignments.Any())
                {
                    script = File.ReadAllText(@"sql_scripts\01_alignment.sql");
                    context.Database.ExecuteSqlRaw(script);
                };
                if (!context.Superheroes.Any())
                {
                    script = File.ReadAllText(@"sql_scripts\02_superhero.sql");
                    context.Database.ExecuteSqlRaw(script);
                };
                if (!context.Superpowers.Any())
                {
                    script = File.ReadAllText(@"sql_scripts\04_superpower.sql");
                    context.Database.ExecuteSqlRaw(script);
                };
                if (!context.HeroPowers.Any())
                {
                    script = File.ReadAllText(@"sql_scripts\03_hero_power.sql");
                    context.Database.ExecuteSqlRaw(script);
                };
            }
            else if (DbContext.GetType() == typeof(AuthenticationContex))
            {
                var context = (AuthenticationContex)DbContext;
                if (!context.Users.Any())
                {
                    var users = new AuthenticationUser[]
                    {
                        new AuthenticationUser{ Login = "pavel", Password = "pavel", Role = "pavel"},
                        new AuthenticationUser{ Login = "user", Password = "user", Role = "user"},
                    };
                    context.Users.AddRange(users);
                    context.SaveChanges();

                }
            }
        }

    }
}
