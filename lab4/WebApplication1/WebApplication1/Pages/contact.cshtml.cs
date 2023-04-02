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


            private readonly ILogger<IndexModel> _logger;
            public ContactModel(ILogger<IndexModel> logger, IDataReader reader) : base(reader, "contact")
            {
                _logger = logger;
            }

            public string Email { get; set; }
            public string Body { get; set; }
            public string name
            {
                get { return _pageName; }
            }
        }
    }
}
