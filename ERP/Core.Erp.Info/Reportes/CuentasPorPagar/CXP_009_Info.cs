using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_009_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRetencion { get; set; }
        public int Idsecuencia { get; set; }
        public Nullable<decimal> IdCbteCble_Ogiro { get; set; }
        public Nullable<int> IdTipoCbte_Ogiro { get; set; }
        public string IdOrden_giro_Tipo { get; set; }
        public Nullable<decimal> IdProveedor { get; set; }
        public string nom_proveedor { get; set; }
        public string ced_proveedor { get; set; }
        public string dir_proveedor { get; set; }
        public Nullable<System.DateTime> co_fechaOg { get; set; }
        public string co_serie { get; set; }
        public string num_factura { get; set; }
        public Nullable<System.DateTime> co_FechaFactura { get; set; }
        public string Estado { get; set; }
        public string TipoDocumento { get; set; }
        public System.DateTime fecha_retencion { get; set; }
        public Nullable<int> ejercicio_fiscal { get; set; }
        public string Impuesto { get; set; }
        public double base_retencion { get; set; }
        public int IdCodigo_SRI { get; set; }
        public string cod_Impuesto_SRI { get; set; }
        public double por_Retencion_SRI { get; set; }
        public double valor_Retenido { get; set; }
        public Nullable<int> IdEmpresa_Ogiro { get; set; }
        public string serie { get; set; }
        public string NumRetencion { get; set; }
        public string co_descripcion { get; set; }
        public string IdCtaCble { get; set; }
        public Nullable<decimal> IdCbteCbleRet { get; set; }
        public string co_observacion { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
