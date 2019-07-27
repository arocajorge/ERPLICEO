using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_tipo_prestamo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoPrestamo { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La descripción debe tener mínimo 4 caracteres y máximo 50")]
        public string tp_Descripcion { get; set; }
        [Range(1, 99999, ErrorMessage = "El campo monto debe ser mayor a cero")]
        [Required(ErrorMessage = "El campo monto es obligatorio")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Ingrese solo numeros")]
        public Nullable<int> tp_Monto { get; set; }
        public string tp_Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }

        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaHoraAnul { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
