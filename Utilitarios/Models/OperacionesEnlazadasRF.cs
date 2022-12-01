using System.Collections.Generic;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Models
{
    public class OperacionesEnlazadasRF
    {
        public string CODOPERACIONEXTRABURSATILRF { get; set; }
        public string FECHAOPERACION { get; set; }
        public string NUMEROOPERACION { get; set; }
        public string TIPOOPERACION { get; set; }
        public string CANTIDADOPERACION { get; set; }
        public string MNEMONICOVALOR { get; set; }
        public string PRECIOPORCENTAJEOPERACION { get; set; }
        public string TIROPERACION { get; set; }
        public string FECHALIQUIDACION { get; set; }
        public string SABCONTRAPARTE { get; set; }
        public string POLIZAGENERADA { get; set; }
        public List<DetalleOperacionesEnlazadasRF> DetOperacionesEnlazadas { get; set; }
    }

    public class DetalleOperacionesEnlazadasRF
    {
        public int IDOPERACIONES { get; set; }
        public string FECHAORDEN { get; set; }
        public string NUMEROORDEN { get; set; }
        public string CANTIDADASIGNADA { get; set; }
        public string DESCRIPCION { get; set; }
        public string CODOPERACIONEXTRABURSATILRF { get; set; }
    }
}