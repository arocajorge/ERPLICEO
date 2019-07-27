using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_tipo_gastos_personales_maxim_x_anio_Info
    {
        public int IdGasto { get; set; }
        public string IdTipoGasto { get; set; }
        public int AnioFiscal { get; set; }
        public double Monto_max { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public bool EstadoBool { get; set; }
    }
}
