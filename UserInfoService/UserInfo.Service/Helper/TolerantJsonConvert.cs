using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace UserInfo.Service.Helper
{
    public class TolerantJsonConvert
    {
        public static IEnumerable<T>? DeserializeCollectionObject<T>(string value, params JsonConverter[] converters)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(value, converters);
            }
            catch (Exception)
            {
                var collection = new List<T>();
                var rx = new Regex(@"\{[^{}]+\}");

                var matches = rx.Matches(value);
                foreach (var item in matches)
                {
                    try
                    {
                        var obj = JsonConvert.DeserializeObject<T>(item.ToString()!, converters);
                        if (obj != null) collection.Add(obj);
                    }
                    catch (Exception) { }
                }
                return collection;
            }
        }
    }
}
