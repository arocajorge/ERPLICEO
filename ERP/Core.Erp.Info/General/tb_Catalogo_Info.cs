using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_Catalogo_Info
    {

        [Key]
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 25")]
        public string CodCatalogo { get; set; }
        [Required(ErrorMessage = "El campo tipo catálogo es obligatorio")]
        public int IdTipoCatalogo { get; set; }
        public int IdCatalogo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 250")]
        public string ca_descripcion { get; set; }
        public string ca_estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = ("el campo orden es obligatorio"))]
        public int ca_orden { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }

    }
}
