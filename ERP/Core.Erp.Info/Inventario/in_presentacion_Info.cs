using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_presentacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Key]
        [Required(ErrorMessage = ("El campo código es obligatorio"))]
        [StringLength(25, MinimumLength = 1, ErrorMessage = ("El campo código debe tener mínimo 1 caracter máximo 25"))]
        public string IdPresentacion { get; set; }

        [Required(ErrorMessage = ("El campo descripción es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = ("El campo descripción debe tener mínimo 1 caracter máximo 150"))]
        public string nom_presentacion { get; set; }
        public string estado { get; set; }
        public bool EstadoBool { get; set; }
    }
}
