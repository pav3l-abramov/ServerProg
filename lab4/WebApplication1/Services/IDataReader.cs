using System.Text.Json.Nodes;

namespace WebApplication1.Services
{
    public interface IDataReader
    {
        public JsonNode GetData(string key);
    }
}
