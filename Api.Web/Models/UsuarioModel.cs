using Api.Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Web.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public bool EsAdmin { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; internal set; }
        public bool DebeCambiarClave { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] ImageData { get; set; }
        public List<UsuarioDistribuidora> UsuarioDistribuidora { get; set; }
        //public List<Api.Entities.Distribuidora> Distribuidoras { get; set; }
    }

    public class Base64FileJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert.FromBase64String(reader.Value as string);
        }

        //Because we are never writing out as Base64, we don't need this. 
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}