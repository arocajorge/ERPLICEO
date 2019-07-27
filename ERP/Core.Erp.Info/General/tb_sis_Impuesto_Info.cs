using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_sis_Impuesto_Info
    {
        [Key]
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 25")]
        public string IdCod_Impuesto { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 50")]
        public string nom_impuesto { get; set; }
        [Required(ErrorMessage = "El campo porcentaje es obligatorio")]
        public double porcentaje { get; set; }
        public bool Usado_en_Ventas { get; set; }
        public bool Usado_en_Compras { get; set; }
        public Nullable<int> IdCodigo_SRI { get; set; }
        public bool estado { get; set; }

        [Required(ErrorMessage = "El campo tipo impuesto es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo impuesto debe tener mínimo 1 caracter y máximo 50")]
        public string IdTipoImpuesto { get; set; }

        //no existe en la tabla

        public tb_sis_Impuesto_x_ctacble_Info info_impuesto_ctacble { get; set; }

        public string IdCtaCble { get; set; }
        public string IdCtaCble_vta { get; set; }
    }
}
