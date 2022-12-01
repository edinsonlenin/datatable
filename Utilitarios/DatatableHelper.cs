using Newtonsoft.Json;
using System;
using System.Data;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    public static class DatatableHelper
    {
        public static string SerializeDatatableForDrowpDownList(DataTable data, string idField, string textField)
        {
            var idColumn = new DataColumn("id", typeof(string))
            {
                Expression = idField
            };

            var textColumn = new DataColumn("text", typeof(string))
            {
                Expression = textField
            };

            data.Columns.Add(idColumn);
            data.Columns.Add(textColumn);

            return SanitizeResponseString(JsonConvert.SerializeObject(data));
        }

        public static string SanitizeResponseString(string jsonStringSerialized)
        {
            jsonStringSerialized = jsonStringSerialized.Replace("\\\"", "\\\\\"").Replace("'", "\\'").Replace(@"\t", "");

            return jsonStringSerialized;
        }
    }
}