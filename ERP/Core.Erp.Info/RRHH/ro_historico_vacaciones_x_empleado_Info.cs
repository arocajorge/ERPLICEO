using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_historico_vacaciones_x_empleado_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public int IdVacacion { get; set; }
        public int IdPeriodo_Inicio { get; set; }
        public int IdPeriodo_Fin { get; set; }
        public System.DateTime FechaIni { get; set; }
        public System.DateTime FechaFin { get; set; }
        public int DiasGanado { get; set; }
        public int DiasTomados { get; set; }

        public int DiasPendientes { get; set; }


        public string Descripcion { get; set; }

    }
}
