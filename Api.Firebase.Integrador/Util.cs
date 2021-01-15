using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Firebase.Integrador
{
    public class JsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonCofigure.Settings());
        }

        public object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json, JsonCofigure.Settings());
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonCofigure.Settings());
        }
    }
    public static class JsonCofigure
    {
        public static JsonSerializerSettings Settings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DateFormatString = "yyyyMMdd HHmmss";
            return settings;

        }
    }
}
