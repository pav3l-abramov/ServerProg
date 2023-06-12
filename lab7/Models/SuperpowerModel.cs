using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroes.Models
{
    public class SuperpowerModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Superpower_id { get; set; }
        public string Superpower_name { get; set; } = null!;

        public List<SuperheroAllParModel> Superheroes { get; set; } = new();
    }
}

