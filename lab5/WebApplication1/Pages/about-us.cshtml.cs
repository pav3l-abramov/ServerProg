using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebApplication1.Pages.defaultModel;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class about_usModel : DefaultModel
    {
        public about_usModel(IDataReader reader) : base(reader, "about-us")
        {
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];
            
        }
    }
}
