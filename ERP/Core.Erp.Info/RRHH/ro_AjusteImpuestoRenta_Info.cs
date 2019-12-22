using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_AjusteImpuestoRenta_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdAjuste { get; set; }
        [Required(ErrorMessage = "El campo año es obligatorio")]
        public int IdAnio { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo fecha corte es obligatorio")]
        public System.DateTime FechaCorte { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string IdUsuario { get; set; }
        public decimal IdEmpleado { get; set; }
        public List<ro_AjusteImpuestoRentaDet_Info> lst_det { get; set; }
        public List<ro_AjusteImpuestoRentaDetOI_Info> lst_det_oi { get; set; }
        #endregion
    }
}
