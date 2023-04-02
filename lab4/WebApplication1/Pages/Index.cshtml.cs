using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Pages;
using System.Text.Json.Nodes;
using static WebApplication1.Pages.defaultModel;

namespace WebApplication1.Pages
{
    public class IndexModel : DefaultModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string name = "Index";
        public JsonNode data;

        public IndexModel(ILogger<IndexModel> logger, IDataReader reader) : base(reader, "index")
        {
            _logger = logger;
        }

        public void OnGet()
        {
            data = _dataReader.GetData("index");

        }

    }
}