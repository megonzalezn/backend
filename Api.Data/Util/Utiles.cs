using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Util
{
    public class Utiles
    {
        public static string ArregloDeStringComoParametro(string[] items)
        {
            if (items == null || items.Length == 0)
                throw new Exception("ArregloDeStringComoParametro mal usado");
            System.Text.StringBuilder b = new System.Text.StringBuilder();
            b.AppendFormat("'{0}'", items[0]);
            for (int i = 1; i < items.Length; i++)
                b.AppendFormat(",'{0}'", items[i]);
            return b.ToString();
        }

        public static string ArregloDeIDComoParametro(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return null;
            //throw new Exception("ArregloDeIDComoParametro mal usado");

            var _ids = ids.Distinct().ToArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(_ids[0].ToString());
            if (_ids.Length > 1)
                for (int i = 1; i < _ids.Length; i++)
                    sb.Append($",{ _ids[i].ToString() }");
            return sb.ToString();
        }
    }
}
