using FilmOMDB.Data;
using FilmOMDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FilmOMDB.Pages
{
    [Authorize]
    public class FilmsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public FilmsModel(HttpClient httpClient, AppDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _httpClient = httpClient;
            _dbContext = dbContext;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [BindProperty]
        public string? FilmName { get; set; }

        public Film? Film { get; set; }
        public List<Film>? Films { get; set; }

        public List<User>? Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userManager.GetUserId(User)!;

            Users = await _dbContext.Users
                .Include(u => u.UserFilms!)
                .ThenInclude(ug => ug.Film)
                .Where(u => u.Id == userId)
                .ToListAsync();

            Films = Users.SelectMany(u => u.UserFilms!)
                .Select(ug => ug.Film!)
                .ToList();

            return Page();
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Films");
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string apiKey = _configuration["ApiKey"];
            string apiUrl = $" http://www.omdbapi.com/?t={FilmName}&apikey={apiKey}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Film = JsonConvert.DeserializeObject<Film>(jsonResponse);
                string userId = _userManager.GetUserId(User)!;

                User? user = await _dbContext.Users
                    .Include(u => u.UserFilms)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    // Проверяем, существует ли игра в базе данных
                    Film? existingFilm = await _dbContext.Films
                        .FirstOrDefaultAsync(g => g.Title == Film!.Title);

                    if (existingFilm == null)
                    {
                        // Игра не существует, добавляем ее в базу данных
                        _dbContext.Films.Add(Film!);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // Игра уже существует, используем ее вместо новой
                        Film = existingFilm;
                    }

                    UserFilm userFilm = new UserFilm
                    {
                        User = user,
                        Film = Film
                    };

                    // Добавляем связь пользователя и игры в базу данных
                    _dbContext.UserFilms.Add(userFilm);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                Film = new Film();
            }

            return RedirectToPage("/Films");
        }

    }
}
