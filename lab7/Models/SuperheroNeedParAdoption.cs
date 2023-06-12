using SuperHeroes.Models;

namespace SuperHeroes.Models
{
    public class SuperheroNeedParAdoption
    {
        public int Superhero_id { get; set; }
        public string Superhero_name { get; set; } = null!;
        public string Full_name { get; set; } = null!;
        public int Height_cm { get; set; }
        public int Weigth_kg { get; set; }
        public AlignmentExtradition Alignment { get; set; } = null!;
        public List<SuperpowerExtradition> Superpowers { get; set; } = new();
    }
}
