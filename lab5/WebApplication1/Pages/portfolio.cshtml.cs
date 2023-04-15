using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebApplication1.Pages.defaultModel;
using WebApplication1.Services;
namespace WebApplication1.Pages
{
    public class portfolioModel : DefaultModel
    {
        public portfolioModel(IDataReader reader) : base(reader, "portfolio")
        {
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];

        }
    }
}
