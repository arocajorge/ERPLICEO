using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_003_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        public System.DateTime cn_fecha { get; set; }
        public System.DateTime cn_Fecha_vcto { get; set; }
        public string cb_Observacion { get; set; }
        public string Estado { get; set; }
        public double cn_subtotal_iva { get; set; }
        public double cn_subtotal_siniva { get; set; }
        public double cn_valoriva { get; set; }
        public decimal cn_total { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public Nullable<double> dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string tc_TipoCbte { get; set; }
        public string dc_Observacion { get; set; }
        public decimal IdProveedor { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string DebCre { get; set; }
        public string Tipo_doc { get; set; }
        public string num_documento { get; set; }
        public string Su_Descripcion { get; set; }
        public string NomTipoNota { get; set; }
    }
}
