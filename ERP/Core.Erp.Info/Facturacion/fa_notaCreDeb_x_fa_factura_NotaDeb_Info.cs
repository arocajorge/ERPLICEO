using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_notaCreDeb_x_fa_factura_NotaDeb_Info
    {
        public int IdEmpresa_nt { get; set; }
        public int IdSucursal_nt { get; set; }
        public int IdBodega_nt { get; set; }
        public decimal IdNota_nt { get; set; }
        public int secuencia { get; set; }
        public int IdEmpresa_fac_nd_doc_mod { get; set; }
        public int IdSucursal_fac_nd_doc_mod { get; set; }
        public int IdBodega_fac_nd_doc_mod { get; set; }
        public decimal IdCbteVta_fac_nd_doc_mod { get; set; }
        public string vt_tipoDoc { get; set; }
        public string NumDocumento { get; set; }
        public double Valor_Aplicado { get; set; }
        public System.DateTime fecha_cruce { get; set; }
        #region Campos que no existen en la tabla
        public string secuencial { get; set; }
        public string vt_NumDocumento { get; set; }
        public DateTime? vt_fecha { get; set; }
        public double? vt_total { get; set; }
        public double? Saldo { get; set; }
        public double? vt_Subtotal { get; set; }
        public double? vt_iva { get; set; }
        public string Observacion { get; set; }
        public bool seleccionado { get; set; }
        public double Saldo_final { get; set; }
        #endregion

    }
}
