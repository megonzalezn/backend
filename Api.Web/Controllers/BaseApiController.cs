using Api.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Api.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        public const string VERBO_GET = "GET";
        public const string VERBO_POST = "POST";
        public const string VERBO_PUT = "PUT";
        public const string VERBO_DELETE = "DELETE";

        //private IApiRepository _repo;
        private ModelFactory _modelFactory;  

        public BaseApiController() { }
        //public BaseApiController(IApiRepository repo)
        //{
        //    _repo = repo;
        //}
        //protected IApiRepository Datos
        //{
        //    get
        //    {
        //        if (_repo == null)
        //        {
        //            _repo = new ApiRepository(new ApiContext());
        //        }
        //        return _repo;
        //    }
        //}
        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request);
                }
                return _modelFactory;
            }
        }

        protected override OkNegotiatedContentResult<T> Ok<T>(T content)
        {
            return base.Ok(content);
        }

        #region Manejo de Parametros en consultas GET con Filtro
        protected static Dictionary<string, string> ParsearFiltro(string paramsFiltro, bool keepCase = false)
        {
            if (string.IsNullOrEmpty(paramsFiltro))
            {
                return new Dictionary<string, string>();
            }

            var splitParams = paramsFiltro.Split(';');
            var result = new Dictionary<string, string>();
            foreach (var p in splitParams)
                if (!string.IsNullOrEmpty(p))
                {
                    var leftRight = p.Split('=');
                    if (keepCase)
                        result[leftRight[0]] = leftRight[1];
                    else
                        result[leftRight[0].ToUpper(CultureInfo.InvariantCulture)] = leftRight[1];
                }
            return result;
        }

        protected Entidad.FiltroBoolEnum FiltroBoolEnum(string valor)
        {
            return (Entidad.FiltroBoolEnum)int.Parse(valor);
        }

        protected static bool ValorFiltroBool(string valor)
        {
            string[] list = new[] { "TRUE", "1" };
            return valor != null && list.Any(valor.ToUpper(CultureInfo.InvariantCulture).Contains);
        }

        protected string ValorFiltroString(string valor)
        {
            return valor;
        }

        protected static int ValorFiltroInt(string valor)
        {
            return int.Parse(valor);
        }

        protected static long ValorFiltroLong(string valor)
        {
            return long.Parse(valor);
        }

        protected static int[] ValorFiltroIntArray(string valor)
        {
            var items = valor.Split(',');
            var resultado = new List<int>();
            foreach (var item in items)
                resultado.Add(int.Parse(item));
            return resultado.ToArray();
        }

        protected static string[] ValorFiltroStringArray(string valor)
        {
            var items = valor.Split(',');
            var resultado = new List<string>();
            foreach (var item in items)
                resultado.Add(item.Trim());
            return resultado.ToArray();
        }

        protected static DateTime ValorFiltroDateTime(string valor)
        {
            return DateTime.ParseExact(valor, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static T ValorFiltroEnumerado<T>(string valor)
        {
            Type genericType = typeof(T);
            foreach (T obj in Enum.GetValues(genericType))
            {
                if (Convert.ToInt32(obj) == int.Parse(valor))
                    return obj;
            }
            return default(T);
        }

        public static Entidad.FiltroBoolEnum ValorFiltroFiltroBoolEnum(string valor)
        {
            string _valor = valor.ToUpper();
            if (_valor == "TRUE" || _valor == "1")
                return Entidad.FiltroBoolEnum.Si;
            if (_valor == "FALSE" || _valor == "2")
                return Entidad.FiltroBoolEnum.No;
            return Entidad.FiltroBoolEnum.Ninguno;
        }

        public static T[] ValorFiltroEnumerados<T>(string valor)
        {
            List<T> states = new List<T>();
            int[] ids = ValorFiltroIntArray(valor);
            Type genericType = typeof(T);
            foreach (T obj in Enum.GetValues(genericType))
            {
                if (ids.Any(n => n == Convert.ToInt32(obj)))
                    states.Add(obj);
            }
            return states.ToArray();
        }


        public List<dynamic> CopyPropertiesToDynamic(ICollection collection)
        {
            List<dynamic> dynamics = new List<dynamic>();

            foreach (object obj in collection)
                dynamics.Add(CopyPropertiesToDynamic(obj));

            return dynamics;
        }

        public dynamic CopyPropertiesToDynamic(object source)
        {
            var response = new ExpandoObject();
            foreach (var prop in source.GetType().GetProperties())
            {
                if (EsPropiedadExcluida(prop.Name))
                    continue;

                //if (prop.PropertyType.IsClass)
                //    ((IDictionary<string, object>)response)[prop.Name] = CopyPropertiesToDynamic(prop.GetValue(source));
                //else
                ((IDictionary<string, object>)response)[prop.Name] = prop.GetValue(source);
            }

            return response;
        }

        private bool EsPropiedadExcluida(string nombre)
        {
            switch (nombre)
            {
                case "IdLocal":
                case "TimeStampLocal":
                case "Tag":
                case "InnerTypes":
                case "Instancia":
                    return true;
                default:
                    return false;
            }
        }

        protected static T ParsearFiltroGenerico<T>(string valor)
        {
            var filtroParseado = ParsearFiltro(valor);
            object entidad = Activator.CreateInstance(typeof(T));
            Type oType = null;
            Type type = entidad.GetType();
            System.Reflection.PropertyInfo[] propertyInfo = type.GetProperties();

            foreach (var prop in propertyInfo)
            {
                string valorFiltro;
                filtroParseado.TryGetValue(prop.Name.ToUpper(), out valorFiltro);

                if (string.IsNullOrEmpty(valorFiltro))
                    continue;

                if (prop.PropertyType == typeof(int))
                {
                    prop.SetValue(entidad, ValorFiltroInt(valorFiltro));
                }
                else if (prop.PropertyType == typeof(int[]))
                {
                    prop.SetValue(entidad, ValorFiltroIntArray(valorFiltro));
                }
                else if (prop.PropertyType == typeof(long))
                {
                    prop.SetValue(entidad, ValorFiltroLong(valorFiltro));
                }
                else if (prop.PropertyType == typeof(Entidad.FiltroBoolEnum) && (prop.PropertyType.IsEnum || IsNullableEnum(prop.PropertyType)))
                {
                    prop.SetValue(entidad, ValorFiltroFiltroBoolEnum(valorFiltro));
                }
                else if (prop.PropertyType.IsEnum || IsNullableEnum(prop.PropertyType))
                {
                    Type _type = prop.PropertyType;
                    var method = typeof(BaseApiController).GetMethod("ValorFiltroEnumerado");
                    var genericMethod = method.MakeGenericMethod(_type);
                    prop.SetValue(entidad, genericMethod.Invoke(_type, new[] { valorFiltro }));
                }
                else if (prop.PropertyType.IsArray && IsNullableArrayEnum(prop.PropertyType, out oType))
                {
                    var method = typeof(BaseApiController).GetMethod("ValorFiltroEnumerados");
                    var genericMethod = method.MakeGenericMethod(oType);
                    prop.SetValue(entidad, genericMethod.Invoke(oType, new[] { valorFiltro }));
                }
                else if (prop.PropertyType.IsArray && prop.PropertyType == typeof(string[]))
                {
                    prop.SetValue(entidad, ValorFiltroStringArray(valorFiltro));
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    prop.SetValue(entidad, ValorFiltroDateTime(valorFiltro));
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    prop.SetValue(entidad, ValorFiltroBool(valorFiltro));
                }
                else if(prop.PropertyType.GenericTypeArguments.Count() > 0 && prop.PropertyType.GenericTypeArguments[0] == typeof(bool))
                {
                    prop.SetValue(entidad, ValorFiltroBool(valorFiltro));
                }
                else if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(entidad, valorFiltro);
                }

            }

            return (T)entidad;

        }

        public static bool IsNullableEnum(Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }

        public static bool IsNullableArrayEnum(Type t, out Type type)
        {
            type = t.GetElementType();
            return (type != null) && type.IsEnum;
        }

        public static bool IsNullableArrayString(Type t, out Type type)
        {
            type = t.GetElementType();
            return (type != null) && type.IsEnum;
        }
        #endregion
    }
}
