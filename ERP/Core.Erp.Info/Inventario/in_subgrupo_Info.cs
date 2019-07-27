using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_subgrupo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public string IdCategoria { get; set; }
        public int IdLinea { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubgrupo { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = ("El campo código debe tener mínimo 1 caracter máximo 50"))]
        public string cod_subgrupo { get; set; }

        [Required(ErrorMessage = ("El campo descripción es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = ("El campo descripción debe tener mínimo 1 caracter máximo 150"))]
        public string nom_subgrupo { get; set; }
        public string observacion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string MotiAnula { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }

        #endregion

        #region Campos que no existn en la tabla
        public string NomCategoria { get; set; }
        public string NomLinea { get; set; }
        public string NomGrupo { get; set; }
        #endregion
    }
}
