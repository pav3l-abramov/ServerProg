namespace SuperHeroes.Models
{
    public class SuperheroNeedParExtradition
    {
        public int Superhero_id { get; set; }
        public string Superhero_name { get; set; } = null!;
        public string Full_name { get; set; } = null!;
        public int Alignment_id { get; set; }
        public int Height_cm { get; set; }
        public int Weigth_kg { get; set; }

    }
}
