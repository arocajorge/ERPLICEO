using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El campo empresa es obligatorio")]
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string IdUsuario { get; set; }        
        public string Contrasena { get; set; }
        public string new_Contrasena { get; set; }
    }
}
