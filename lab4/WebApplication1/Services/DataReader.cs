using System.Text.Json.Nodes;

namespace WebApplication1.Services
{
    public class DataReader: IDataReader
    {
        public readonly string fileName = "json.json";

        public JsonNode GetData(string key)
        {
            StreamReader sr = new StreamReader(fileName);
            var input = sr.ReadToEnd();
            var jsonObject = JsonNode.Parse(input)!.AsObject();
            return jsonObject["pages"][key];
        }

    }
}
