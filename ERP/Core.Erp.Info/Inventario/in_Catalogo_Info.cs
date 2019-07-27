using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Catalogo_Info
    {

        [Required(ErrorMessage = ("El campo código es obligatorio"))]
        [StringLength(15, MinimumLength = 1, ErrorMessage = ("El campo código debe tener mínimo 1 caracter máximo 15"))]
        public string IdCatalogo { get; set; }
        public int IdCatalogo_tipo { get; set; }
        [Required(ErrorMessage =("El campo descripción es obligatorio"))]
        [StringLength(100, MinimumLength =1, ErrorMessage =("El campo descripción debe tener mínimo 1 caracter máximo 100"))]
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<int> Orden { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
    }
}
