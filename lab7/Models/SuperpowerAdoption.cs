namespace SuperHeroes.Models
{
    public class SuperpowerAdoption
    {
        public int Superpower_id { get; set; }
        public string Superpower_name { get; set; } = null!;

        public List<SuperheroNeedParExtradition> Superheroes { get; set; } = new();
    }
}
