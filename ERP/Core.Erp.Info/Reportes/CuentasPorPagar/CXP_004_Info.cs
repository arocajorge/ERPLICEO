using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
   public class CXP_004_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public Nullable<double> dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string tc_TipoCbte { get; set; }
        public string dc_Observacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string IdTipo_op { get; set; }
        public double Valor_a_pagar { get; set; }
        public string co_factura { get; set; }
        public string Descripcion { get; set; }
        public string GeneraDiario { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public string estado_apro { get; set; }
        public string NombreUsuario { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
