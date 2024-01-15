using System.Reflection;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RailWorkshop.Db.Utils
{
    public static class Extensions
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

        public static T UpdateFromJson<T>(this T target, JsonElement source)
        {
            JsonConvert.PopulateObject(source.ToString(), target);

            return target;
        }
    }
}