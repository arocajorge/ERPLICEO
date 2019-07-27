using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_anio_fiscal_x_tb_sucursal_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo año fiscal es obligatorio")]
        public int IdanioFiscal { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }

        #region Campos que no existen en la tabla
        public string IdUsuario { get; set; }
        public string MotivoAnulacion { get; set; }
        public string Su_Descripcion { get; set; }
        public string Observacion { get; set; }
        public ct_cbtecble_det_Info info_cbtecble { get; set; }
        public List<ct_cbtecble_det_Info> info_cbtecble_det { get; set; }
        #endregion
    }
}
