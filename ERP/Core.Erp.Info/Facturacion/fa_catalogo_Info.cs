using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_catalogo_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(15, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 15")]
        public string IdCatalogo { get; set; }
        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public int IdCatalogo_tipo { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo descripción debe tener máximo 50")]
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string Abrebiatura { get; set; }
        public string NombreIngles { get; set; }
        public Nullable<int> Orden { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
    }
}
