using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_pais_Info
    {
        [Key]
        public string IdPais { get; set; }
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string CodPais { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracteres y máximo 50")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo nacionalidad es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo nacionalidad debe tener mínimo 1 caracteres y máximo 50")]
        public string Nacionalidad { get; set; }
        public string estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }

    }
}
