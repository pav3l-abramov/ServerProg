using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using static WebApplication1.Pages.defaultModel;

namespace WebApplication1.Pages
{
    public class testimonialsModel : DefaultModel
    {
        private readonly Context _context;
        public testimonialsModel(IDataReader reader, Context context) : base(reader, "testimonials")
        {
            _context = context;
        }
        public IList<Testimonial> Testimonials { get; set; } = default!;

        public async Task OnGetAsync()
        {
            title = _dataReader.GetData(_pageName)["title"];
            if (_context.Testimonials != null)
            {
                Testimonials = await _context.Testimonials.ToListAsync();
            }
        }
    }
}
