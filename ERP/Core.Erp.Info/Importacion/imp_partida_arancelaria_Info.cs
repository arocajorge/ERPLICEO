using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Importacion
{
   public class imp_partida_arancelaria_Info
    {
        public decimal IdArancel { get; set; }
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 200")]
        public string CodigoPartidaArancelaria { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = ("el campo tarifa es obligatorio"))]
        public decimal TarifaArancelaria { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
    }
}
