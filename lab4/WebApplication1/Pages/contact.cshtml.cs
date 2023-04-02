using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebApplication1.Pages.defaultModel;
using Microsoft.Extensions.Logging;
using WebApplication1.Services;
using System.Security.Principal;
using System.Globalization;


namespace WebApplication1.Pages
{
    public class contactModel : PageModel
    {
        [IgnoreAntiforgeryToken]
        public class ContactModel : DefaultModel
        {
            public ContactModel(IDataReader reader) : base(reader, "contact")
            {
            }
            public void OnGet()
            {
                title = _dataReader.GetData(_pageName)["title"];
            }
        }
    }
}
