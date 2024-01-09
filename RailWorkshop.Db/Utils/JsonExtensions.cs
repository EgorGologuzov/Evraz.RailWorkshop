using System.Text.Json;
using Newtonsoft.Json;

namespace RailWorkshop.Db.Utils
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public static T FromJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T FromJson<T>(this JsonElement value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
    }
}