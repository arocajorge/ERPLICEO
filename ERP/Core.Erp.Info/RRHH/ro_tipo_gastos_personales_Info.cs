using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_tipo_gastos_personales_Info
    {
        [Required(ErrorMessage = ("El codigo del gasto es obligatorio"))]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El campo codigo debe tener mínimo 4 caracteres y máximo 10")]

        public string IdTipoGasto { get; set; }
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El campo codigo debe tener mínimo 4 caracteres y máximo 50")]
        [Required(ErrorMessage = ("El nombre del gasto es obligatorio"))]
        public string nom_tipo_gasto { get; set; }
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
