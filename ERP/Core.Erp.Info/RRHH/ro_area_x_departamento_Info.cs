using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_area_x_departamento_Info
    {
        public int IdEmpresa { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "El campo división es obligatorio")]
        public int IdDivision { get; set; }
        [Required(ErrorMessage = "El campo área es obligatorio")]
        public int IdArea { get; set; }
        [Required(ErrorMessage = "El campo departamento es obligatorio")]
        public int IdDepartamento { get; set; }

        public string area { get; set; }
        public string division { get; set; }
        public string departamento { get; set; }

    }
}
