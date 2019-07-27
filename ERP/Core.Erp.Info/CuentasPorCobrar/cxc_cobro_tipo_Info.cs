using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorCobrar
{
    public class cxc_cobro_tipo_Info
    {
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 20")]
        public string IdCobro_tipo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string tc_descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = ("el campo abreviatura es obligatorio"))]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo abreviatura debe tener mínimo 1 caracter y máximo 20")]
        public string tc_abreviatura { get; set; }
        public string tc_Tomar_Cta_Cble_De { get; set; }
        public string ESRetenIVA { get; set; }
        public string ESRetenFTE { get; set; }
        public double PorcentajeRet { get; set; }
        public string IdMotivo_tipo_cobro { get; set; }
        public bool EsTarjetaCredito { get; set; }
        public bool SeDeposita { get; set; }
        public double PorcentajeDescuento { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        #endregion

        #region Campos que no existen en la tabla
        public List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> Lst_tipo_param_det { get; set; }
        public int IdSucursal { get; set; }
        public bool ESRetenIVA_bool { get; set; }
        public bool ESRetenFTE_bool { get; set; }
        public int IdEmpresa { get; set; }
        
        #endregion
    }
}
