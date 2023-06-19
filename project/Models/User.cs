using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FilmOMDB.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserFilm>? UserFilms { get; set; }
    }
}
