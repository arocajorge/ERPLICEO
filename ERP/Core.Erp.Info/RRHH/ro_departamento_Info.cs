using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_departamento_Info
    {
        public int IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "El campo descripción debe tener mínimo 4 caracteres y máximo 200")]
        public string de_descripcion { get; set; }
        public string MotiAnula { get; set; }
        public string IdUsuario { get; set; }
        public DateTime? Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public DateTime? Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public DateTime? Fecha_UltAnu { get; set; }


        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string SEstado { get; set; }
    }
}
