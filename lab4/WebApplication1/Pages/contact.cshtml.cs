using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using static WebApplication1.Pages.defaultModel;

namespace WebApplication1.Pages
{
    public class contactModel : DefaultModel
    {
        public contactModel(IDataReader reader) : base(reader, "contact")
        {
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];
        }
    }
}
