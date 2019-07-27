using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_Comprobantes_Contables_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public Nullable<int> IdNomina { get; set; }
        public Nullable<int> IdNominaTipo { get; set; }
        public int IdPeriodo { get; set; }
        public string cb_Observacion { get; set; }
        public Nullable<int> IdEmpresa_rol { get; set; }
        public Nullable<decimal> IdRol { get; set; }
    }
}
