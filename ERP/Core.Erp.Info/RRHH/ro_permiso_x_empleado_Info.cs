using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_permiso_x_empleado_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPermiso { get; set; }
        [Required(ErrorMessage = ("el campo empleado es obligatorio"))]

        public decimal IdEmpleado { get; set; }
        public Nullable<decimal> IdEmpleadoAprueba { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public Nullable<System.TimeSpan> HoraSalida { get; set; }
        public Nullable<System.TimeSpan> HoraRegreso { get; set; }
        public bool DescuentaVacaciones { get; set; }
        public bool Recuperable { get; set; }
        [Required(ErrorMessage = ("el campo asunto es obligatorio"))]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "el campo asunto debe tener mínimo 1 caracter y máximo 250")]
        public string Asunto { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 250")]
        public string Descripcion { get; set; }
        public string TipoPermiso { get; set; }
        public bool estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string ip { get; set; }
        public string MotiAnula { get; set; }
    }
}
