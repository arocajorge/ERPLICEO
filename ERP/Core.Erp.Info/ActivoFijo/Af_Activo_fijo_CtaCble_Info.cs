using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
   public class Af_Activo_fijo_CtaCble_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public int Secuencia { get; set; }
        public decimal IdDepartamento { get; set; }
        public string IdCtaCble { get; set; }
        public double Porcentaje { get; set; }
        //campos que no existen en la tabla
        public string pc_Cuenta { get; set; }

    }
}
