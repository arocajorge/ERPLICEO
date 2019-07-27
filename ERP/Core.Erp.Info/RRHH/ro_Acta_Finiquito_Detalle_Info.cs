using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_Acta_Finiquito_Detalle_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdActaFiniquito { get; set; }
        public int IdSecuencia { get; set; }
        public string IdRubro { get; set; }
        public decimal IdEmpleado { get; set; }
        public string Observacion { get; set; }
        public double Valor { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string ru_tipo{ get; set; }
    }
}
