using Newtonsoft.Json;
using System.Text.Json.Serialization;
using JsonConverterAttribute = System.Text.Json.Serialization.JsonConverterAttribute;


namespace BookGeneratorProject.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Region
    {
        USA,
        South_Korea,
        UAE
    }
}
