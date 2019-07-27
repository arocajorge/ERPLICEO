using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_UnidadMedida_Equiv_conversion_Info
    {
        public string IdUnidadMedida { get; set; }
        [Required(ErrorMessage = "El campo unidad equivalencia es obligatorio")]
        public string IdUnidadMedida_equiva { get; set; }
        [Required(ErrorMessage ="El campo valor equivalencia es obligatorio")]
        public double valor_equiv { get; set; }
        public string interpretacion { get; set; }

        //Campos que no existen en la tabla
        public int secuencia { get; set; }
        public string Descripcion { get; set; }
    }
}
