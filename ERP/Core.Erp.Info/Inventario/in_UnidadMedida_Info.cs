using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_UnidadMedida_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "El campo código debe tener mínimo 1 caracter y máximo 25 caracteres")]
        public string IdUnidadMedida { get; set; }
        [StringLength(25, MinimumLength = 0, ErrorMessage = "El campo código alterno debe tener máximo 25 caracteres")]
        public string cod_alterno { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "El campo descripción debe tener mínimo 1 caracter y máximo 500 caracteres")]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        #region Campos de auditoria
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        #endregion

        //Campos que no existen en la tabla
        public List<in_UnidadMedida_Equiv_conversion_Info> lst_unidad_medida_equiv { get; set; }
    }
}
