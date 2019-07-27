using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Core.Erp.Info.RRHH
{
   public class ro_cargo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdCargo { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La descripción debe tener mínimo 4 caracteres y máximo 500")]
        public string ca_descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
