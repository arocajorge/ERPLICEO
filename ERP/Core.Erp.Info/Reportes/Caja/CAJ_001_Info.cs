using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Caja
{
    public class CAJ_001_Info
    {

        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public double dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string tc_descripcion { get; set; }
        public double cr_Valor { get; set; }
        public string cm_Signo { get; set; }
        public int IdTipoMovi { get; set; }
        public string tm_descripcion { get; set; }
        public string cm_observacion { get; set; }
        public int IdCaja { get; set; }
        public string ca_Descripcion { get; set; }
        public System.DateTime cm_fecha { get; set; }
        public string Estado { get; set; }
        public string tc_TipoCbte { get; set; }
        public string IdCtaCble { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<decimal> SecuenciaCaja { get; set; }
    }
}
