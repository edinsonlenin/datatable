using Newtonsoft.Json;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Models
{
    public class WcbCuentaModel
    {
        [JsonProperty("CODCUENTABANCARIACLIENTE")]
        public int CodCuentaBancaria { get; set; }

        [JsonProperty("CODTIPOCUENTABANCARIA")]
        public int CodTipoCuentaBancaria { get; set; }

        [JsonProperty("NOCUENTA")]
        public string NroCuenta { get; set; }

        [JsonProperty("CODMONEDA")]
        public int CodMoneda { get; set; }
    }
}