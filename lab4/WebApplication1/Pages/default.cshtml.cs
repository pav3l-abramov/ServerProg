using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
namespace WebApplication1.Pages
{
    public class defaultModel : PageModel
    {
        public class DefaultModel : PageModel
        {
            protected IDataReader _dataReader;
            public string _pageName;
            public DefaultModel(IDataReader dataService, string name)
            {
                _dataReader = dataService;
                _pageName = name;
            }

        }
    }
}
