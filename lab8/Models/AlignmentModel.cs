using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroes.Models
{
    public class AlignmentModel
    {
      
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Alignment_id { get; set; }
            public string Alignment_name { get; set; } = null!;

            public ICollection<SuperheroAllParModel> Superheroes { get; set; } = new List<SuperheroAllParModel>();
        
    }
}
