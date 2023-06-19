using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FilmOMDB.Models
{
    public class Film
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Poster { get; set; }
        public string? Year { get; set; }
        public string? Genre { get; set; }
        public string? Actors { get; set; }
        public string? Plot { get; set; }

        public ICollection<UserFilm>? UserFilms { get; set; }
    }

}
