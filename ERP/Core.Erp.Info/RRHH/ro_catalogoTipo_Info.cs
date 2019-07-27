using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_catalogoTipo_Info
    {
        public int IdTipoCatalogo { get; set; }
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El campo código debe tener mínimo 4 caracteres y máximo 10")]
        [Required(ErrorMessage = "El campo Código obligatorio")]
        public string Codigo { get; set; }
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El campo descripción debe tener mínimo 4 caracteres y máximo 100")]
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string tc_Descripcion { get; set; }

        public string IdUsuario { get; set; }
        public string ca_estado { get; set; }
        public bool EstadoBool { get; set; }
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
