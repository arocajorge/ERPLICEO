using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_region_Info
    {
        [Key]
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 4 caracteres y máximo 10")]
        public string Cod_Region { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 4 caracteres y máximo 100")]
        public string Nom_region { get; set; }
        public string codigo { get; set; }
        public bool estado { get; set; }
        public string IdPais { get; set; }
    }
}
