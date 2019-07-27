using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_cbtecble_Plantilla_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdPlantilla { get; set; }
        [Required(ErrorMessage = "El campo tipo de comprobante es obligatorio")]
        public int IdTipoCbte { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string cb_Observacion { get; set; }
        public string cb_Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }

        #region Campos que no estan en la tabla
        public bool EstadoBool { get; set; }
        public List<ct_cbtecble_Plantilla_det_Info> lst_cbtecble_plantilla_det { get; set; }
        #endregion
    }
}
