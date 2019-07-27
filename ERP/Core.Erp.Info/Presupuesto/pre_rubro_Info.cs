using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Presupuesto
{
    public class pre_rubro_Info
    {
        [Key]
        public int IdRubro { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo tipo de rubro es obligatorio")]
        public int IdRubroTipo { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "El campo descripción debe tener mínimo 4 caracteres y máximo 500")]

        public string Descripcion { get; set; }
        public string IdCtaCble { get; set; }

        public bool Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "El campo descripción debe tener mínimo 4 caracteres y máximo 500")]
        public string MotivoAnulacion { get; set; }


        public string pc_Cuenta { get; set; }
        public string Descripcion_RubroTipo { get; set; }

        public bool AsignaCuentaRubro { get; set; }
    }
}
