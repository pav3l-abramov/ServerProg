using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Nodes;
using WebApplication1.Services;
namespace WebApplication1.Pages
{
    public class defaultModel : PageModel
    {
        public class DefaultModel : PageModel
        {
            protected IDataReader _dataReader;
            public string _pageName;
            public JsonNode title;
            public JsonNode testimonials;
            public string[] activeState;

            public DefaultModel(IDataReader dataService, string name)
            {
                _dataReader = dataService;
                _pageName = name;
                testimonials = dataService.GetData("testimonials")["items"];
            }

        }
    }
}
