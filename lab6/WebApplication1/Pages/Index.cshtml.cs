using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Pages;
using System.Text.Json.Nodes;
using static WebApplication1.Pages.defaultModel;
using WebApplication1.Data;

namespace WebApplication1.Pages
{
    public class IndexModel : DefaultModel
    {
        private readonly Context _db;
        public JsonNode json;
        public IndexModel(IDataReader reader, Context db) : base(reader, "index")
        {
            _db = db;
        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];
            ViewData["count"] = this._db.Contacts.ToList().Count;

        }

    }
}