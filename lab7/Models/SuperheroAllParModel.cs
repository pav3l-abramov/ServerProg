using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroes.Models
{
    public class SuperheroAllParModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Superhero_id { get; set; }
        public string Superhero_name { get; set; } = null!;
        public string Full_name { get; set; } = null!;
        // не забыть удалить
        public int Gender_id { get; set; }
        public int Eye_id { get; set; }
        public int Hair_id { get; set; }
        public int Skin_id { get; set; }
        public int Race_id { get; set; }
        public int Publisher_id { get; set; }
        //до сюда
        public int Alignment_id { get; set; }
        public int Height_cm { get; set; }
        public int Weigth_kg { get; set; }
        public AlignmentModel Alignment { get; set; } = null!;
        public List<SuperpowerModel> Superpowers { get; set; } = new();


    }
}
