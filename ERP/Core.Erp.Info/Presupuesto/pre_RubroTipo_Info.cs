using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Presupuesto
{
    public class pre_RubroTipo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdRubroTipo { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "El campo motivo anulación  debe tener mínimo 4 caracteres y máximo 500")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo signo es obligatorio")]
        public string Signo { get; set; }
        [Required(ErrorMessage = "El campo orden es obligatorio")]
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }        
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "El campo motivo anulación  debe tener mínimo 4 caracteres y máximo 500")]
        public string MotivoAnulacion { get; set; }
    }
}
