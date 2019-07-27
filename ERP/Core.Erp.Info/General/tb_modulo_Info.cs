using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
   public class tb_modulo_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(20, MinimumLength =1, ErrorMessage =("El campo código debe tener mínimo 1 caracter máximo 20"))]
        public string CodModulo { get; set; }
       
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ("El campo descripción debe tener mínimo 1 caracter máximo 50"))]
        public string Descripcion { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = ("El campo carpeta debe tener mínimo 1 caracter máximo 50"))]
        public string Nom_Carpeta { get; set; }
        public bool Se_Cierra { get; set; }
    }
}
