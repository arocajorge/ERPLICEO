using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_plancta_nivel_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo nivel es obligatorio")]
        public int IdNivelCta { get; set; }
        [Required(ErrorMessage = "El campo cantidad de dígitos es obligatorio")]
        public int nv_NumDigitos { get; set; }
        [Required(ErrorMessage ="El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string nv_Descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        #endregion
    }
}
