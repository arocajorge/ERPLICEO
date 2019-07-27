using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_x_division_x_area_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public int Secuencia { get; set; }
        public int IDividion { get; set; }
        public int IdArea { get; set; }
        public double Porcentaje { get; set; }
        public string Observacion { get; set; }

        public Nullable<bool> CargaGasto { get; set; }

        public string Descripcion_Division { get; set; }
        public string Descripcion { get; set; }
        public int IDividion_det { get; set; }
        public int IdArea_det { get; set; }
    }
}
