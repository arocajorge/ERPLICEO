using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_empleado_x_jornada_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public int Secuencia { get; set; }
        public int IdJornada { get; set; }
        public double ValorHora { get; set; }
        public int MaxNumHoras { get; set; }

        public string pe_nombre { get; set; }
    }
}
