using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_conciliacionAnticipo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = "El campo proveedor es obligatorio")]
        public decimal IdProveedor { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public Nullable<decimal> Idcancelacion { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El motivo de anulación observación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string Su_Descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public List<cp_ConciliacionAnticipoDetAnt_Info> Lista_det_OP { get; set; }
        public List<cp_ConciliacionAnticipoDetCXP_Info> Lista_det_Fact { get; set; }
        public ct_cbtecble_Info InfoCbte { get; set; }
        public List<ct_cbtecble_det_Info> Lista_det_Cbte { get; set; }
        #endregion
    }
}
