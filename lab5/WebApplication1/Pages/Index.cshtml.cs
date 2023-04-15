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
        public IndexModel(IDataReader reader) : base(reader, "index")
        {
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];

        }

    }
}