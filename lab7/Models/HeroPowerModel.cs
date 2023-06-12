using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroes.Models
{
    public class HeroPowerModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Superhero_id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Superpower_id { get; set; }
    }
}
