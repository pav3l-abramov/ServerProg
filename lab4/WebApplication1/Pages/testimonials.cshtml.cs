using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using static WebApplication1.Pages.defaultModel;

namespace WebApplication1.Pages
{
    public class testimonialsModel : DefaultModel
    {
        public testimonialsModel(IDataReader reader) : base(reader, "testimonials")
        {
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];
        }
    }
}
