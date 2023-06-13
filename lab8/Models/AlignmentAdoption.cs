namespace SuperHeroes.Models
{
    public class AlignmentAdoption
    {
        public int Alignment_id { get; set; }
        public string Alignment_name { get; set; } = null!;

        public ICollection<SuperheroNeedParExtradition> Superheroes { get; set; } = new List<SuperheroNeedParExtradition>();
    }
}
