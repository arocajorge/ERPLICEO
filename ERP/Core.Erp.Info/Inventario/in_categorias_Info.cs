using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_categorias_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = ("El campo id es obligatorio"))]
        [StringLength(25, MinimumLength = 1, ErrorMessage = ("El campo id debe tener mínimo 1 caracter máximo 25"))]
        public string IdCategoria { get; set; }
        
        public string cod_categoria { get; set; }
        [Required(ErrorMessage = ("El campo descripción es obligatorio"))]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ("El campo descripción debe tener mínimo 1 caracter máximo 100"))]
        public string ca_Categoria { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdCtaCtble_Inve { get; set; }
        public string IdCtaCtble_Costo { get; set; }
        public string IdCtaCble_venta { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotiAnula { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        
        #endregion
    }
}
