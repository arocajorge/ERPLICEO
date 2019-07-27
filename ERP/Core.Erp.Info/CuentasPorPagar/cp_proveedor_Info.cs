using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_proveedor_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdTransaccionSession { get; set; }
        public decimal IdProveedor { get; set; }
        public decimal IdPersona { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string pr_codigo { get; set; }
        public string pr_contribuyenteEspecial { get; set; }
        public int pr_plazo { get; set; }
        public string pr_estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = "El campo ciudad es obligatorio")]

        public string IdCiudad { get; set; }
        [Required(ErrorMessage = "El campo cuenta contable es obligatorio")]
        public string IdCtaCble_CXP { get; set; }
        public string IdCtaCble_Gasto { get; set; }
        [Required(ErrorMessage = "El campo clase proveedor es obligatorio")]
        public int IdClaseProveedor { get; set; }        
        public string IdTipoCta_acreditacion_cat { get; set; }
        public string num_cta_acreditacion { get; set; }
        public Nullable<int> IdBanco_acreditacion { get; set; }
        public bool es_empresa_relacionada { get; set; }
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo télefono debe tener mínimo 1 caracter y máximo 25")]
        public string pr_telefonos { get; set; }
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo célular debe tener mínimo 1 caracter y máximo 100")]
        public string pr_celular { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo dirección debe tener mínimo 1 caracter y máximo 500")]
        public string pr_direccion { get; set; }
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo correo debe tener mínimo 1 caracter y máximo 200")]
        public string pr_correo { get; set; }
        public string IdCtaCble_Anticipo { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        #endregion

        #region campos que no existen en la tabla
        public bool pr_contribuyenteEspecial_bool { get; set; }
        public tb_persona_Info info_persona { get; set; }
        public string descripcion_clas_prove { get; set; }
        public decimal IdEntidad { get; set; }
        #endregion

        public cp_proveedor_Info()
        {
            info_persona = new tb_persona_Info();
        }

    }
}
