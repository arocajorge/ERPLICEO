using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
   public class BAN_002_Info
    {
        public string CodTipoCbteBan { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble { get; set; }
        public int IdTipocbte { get; set; }
        public int IdBanco { get; set; }
        public string ba_descripcion { get; set; }
        public System.DateTime cb_Fecha { get; set; }
        public string cb_Observacion { get; set; }
        public string Estado { get; set; }
        public Nullable<int> IdTipoNota { get; set; }
        public string Descripcion_TipoNota { get; set; }
        public string NomBeneficiario { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public double dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string Nombre { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
