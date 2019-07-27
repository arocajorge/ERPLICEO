using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Caja
{
    public class caj_Caja_Movimiento_Info
    {
        public decimal IdTransaccionSession { get; set; }

        public int IdEmpresa { get; set; }
        public decimal IdCbteCble { get; set; }
        public int IdTipocbte { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 20")]
        public string CodMoviCaja { get; set; }
        public string cm_Signo { get; set; }

        [Required(ErrorMessage = "El campo valor es obligatorio")]
        public double cm_valor { get; set; }

        [Required(ErrorMessage = "El campo tipo movimiento es obligatorio")]
        public int IdTipoMovi { get; set; }
        public string cm_observacion { get; set; }

        [Required(ErrorMessage = "El campo caja es obligatorio")]
        public int IdCaja { get; set; }
        public int IdPeriodo { get; set; }
        public System.DateTime cm_fecha { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuario_Anu { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string MotivoAnulacion { get; set; }
        public Nullable<decimal> IdTipoFlujo { get; set; }
        public string IdTipo_Persona { get; set; }
        public decimal IdEntidad { get; set; }
        public decimal IdPersona { get; set; }
        public Nullable<decimal> SecuenciaCaja { get; set; }


        #region Campos que no existen en la tabla
        public string ca_Descripcion { get; set; }
        public string tm_descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        #endregion

        public caj_Caja_Movimiento_det_Info info_caj_Caja_Movimiento_det { get; set; }
        public ct_cbtecble_Info info_ct_cbtecble { get; set; }
        public List<ct_cbtecble_det_Info> lst_ct_cbtecble_det { get; set; }
        public List<cp_orden_pago_cancelaciones_Info> lst_det_canc_op { get; set; }

    }
}
