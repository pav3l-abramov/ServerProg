using System.Text.Json.Nodes;

namespace WebApplication1.Services
{
    public class DataReader: IDataReader
    {
        public DataReader(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "json", "json.json"); }
        }

        public JsonNode GetData(string key)
        {
            StreamReader sr = new StreamReader(JsonFileName);
            var input = sr.ReadToEnd();
            var jsonObject = JsonNode.Parse(input)!.AsObject();
            return jsonObject["pages"][key];
        }

    }
}
