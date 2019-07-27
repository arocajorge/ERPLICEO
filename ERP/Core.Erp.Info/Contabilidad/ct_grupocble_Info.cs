using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_grupocble_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 5")]
        public string IdGrupoCble { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string gc_GrupoCble { get; set; }
        [Required(ErrorMessage = "El campo orden es obligatorio")]
        public byte gc_Orden { get; set; }
        [Required(ErrorMessage = "El campo estado financiero es obligatorio")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "el campo estado financiero debe tener mínimo 1 caracter y máximo 2")]
        public string gc_estado_financiero { get; set; }
        public Nullable<int> gc_signo_operacion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdGrupo_Mayor { get; set; }
    }
}
