using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using static WebApplication1.Pages.defaultModel;

namespace WebApplication1.Pages
{
    public class servicesModel : DefaultModel
    {
        public servicesModel(IDataReader reader) : base(reader, "services")
        {
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];
        }
    }
}
