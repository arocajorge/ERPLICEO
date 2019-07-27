using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_013_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRetencion { get; set; }
        public int Idsecuencia { get; set; }
        public string re_TipoRet { get; set; }
        public string co_factura { get; set; }
        public string NumRetencion { get; set; }
        public string TipoComprobante { get; set; }
        public System.DateTime FechaDeEmision { get; set; }
        public string EjercicioFiscal { get; set; }
        public double re_baseRetencion { get; set; }
        public double re_Porcen_retencion { get; set; }
        public double re_valor_retencion { get; set; }
        public string NombreProveedor { get; set; }
        public string pr_direccion { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pr_correo { get; set; }
        public string pr_telefonos { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizacion { get; set; }
        public string NAutorizacion { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
