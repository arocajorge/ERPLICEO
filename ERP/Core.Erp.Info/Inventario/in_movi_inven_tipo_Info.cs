using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
   public class in_movi_inven_tipo_Info
    {
        public int IdEmpresa { get; set; }        
        public int IdMovi_inven_tipo { get; set; }
        [StringLength(10, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 10")]
        public string Codigo { get; set; }
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 100")]
        [Required(ErrorMessage ="El campo descripción es obligatorio")]
        public string tm_descripcion { get; set; }
        public string cm_tipo_movi { get; set; }
        [StringLength(10, MinimumLength = 1, ErrorMessage = "el campo descripción corta debe tener mínimo 1 caracter y máximo 10")]
        [Required(ErrorMessage = "El campo descripción corta es obligatorio")]
        public string cm_descripcionCorta { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public bool Genera_Diario_Contable { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        #endregion

        //Campos que no existen en la tabla
        public List<in_movi_inven_tipo_x_tb_bodega_Info> lst_tipo_mov_x_bodega { get; set; }
        public string IdCatalogoAprobacion { get; set; }
    }
}
