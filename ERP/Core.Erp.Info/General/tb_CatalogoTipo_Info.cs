using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_CatalogoTipo_Info
    {
        [Key]
        [Required(ErrorMessage =" el campo ID es obligatorio")]
        public int IdTipoCatalogo { get; set; }

        [Required(ErrorMessage = " el campo código es obligatorio")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 10")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = " el campo descripción es obligatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 100")]
        public string tc_Descripcion { get; set; }

        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
