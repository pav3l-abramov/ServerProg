using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models
{
    public class Testimonial
    {
        public int Id { get; set; }


        [Column("Name")]
        public string Name { get; set; }

        [Column("ImgURL")]

        public string ImgURL { get; set; }

        public string Ocupation { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
