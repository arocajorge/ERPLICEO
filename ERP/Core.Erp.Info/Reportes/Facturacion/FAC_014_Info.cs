using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_014_Info
    {
        public int IdEmpresa { get; set; }
        public string IdComprobante { get; set; }
        public string IdTipoDocumento { get; set; }
        public string IdEstado_cbte { get; set; }
        public string Cedula_Ruc { get; set; }
        public string Numero_Autorizacion { get; set; }
        public Nullable<System.DateTime> FechaAutorizacion { get; set; }
        public string Nom_Contribuyente { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> Total { get; set; }
        public int Evento { get; set; }
        public int Factura { get; set; }
        public Nullable<System.DateTime> Fecha_Emi_Fact { get; set; }
        public Nullable<System.DateTime> Fecha_transaccion { get; set; }
        public string Nombre_Evento { get; set; }
        public string Observacion { get; set; }
    }
}
