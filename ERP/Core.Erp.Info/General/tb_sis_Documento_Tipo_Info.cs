using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_sis_Documento_Tipo_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        public string codDocumentoTipo { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string descripcion { get; set; }
        public string estado { get; set; }
        public Nullable<int> Posicion { get; set; }
    }
}
