namespace FilmOMDB.Models
{
    public class UserFilm
    {
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int FilmId { get; set; }
        public Film? Film { get; set; }
    }
}
