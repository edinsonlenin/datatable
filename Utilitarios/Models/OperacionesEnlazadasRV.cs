using System.Collections.Generic;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Models
{
    public class OperacionesEnlazadasRV
    {
        public string CODOPERACIONEXTRABURSATIL { get; set; }
        public string FECHAOPERACION { get; set; }
        public string NUMEROOPERACION { get; set; }
        public string TIPOOPERACION { get; set; } 
        public string CANTIDADOPERACION { get; set; }
        public string MNEMONICOVALOR { get; set; }
        public string PRECIOOPERACION { get; set; }
        public string SABCONTRAPARTE { get; set; }
        public string POLIZAGENERADA { get; set; }
        public List<DetalleOperacionesEnlazadasRV> DetOperacionesEnlazadas { get; set; }
    }

    public class DetalleOperacionesEnlazadasRV
    {
        public int IDOPERACIONES { get; set; }
        public string FECHAORDEN { get; set; }
        public string NOMBRECLIENTE { get; set; }
        public string NUMEROORDEN { get; set; }
        public string CANTIDADACTUAL { get; set; }
        public string CANTIDADASIGNADA { get; set; }
        public string CODOPERACIONEXTRABURSATIL { get; set; }
    }
}