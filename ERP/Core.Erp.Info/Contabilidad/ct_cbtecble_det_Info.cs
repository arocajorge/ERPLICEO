using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_cbtecble_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        [Required(ErrorMessage = "El campo cuenta contable es obligatorio")]
        public string IdCtaCble { get; set; }
        public double dc_Valor { get; set; }
        public string dc_Observacion { get; set; }
        public bool dc_para_conciliar { get; set; }
        public bool? dc_para_conciliar_null { get; set; }

        public Nullable<int> IdPunto_cargo_grupo { get; set; }
        public Nullable<int> IdPunto_cargo { get; set; }
        public string IdCentroCosto { get; set; }

        //campos que no existen en la tabla
        [Required(ErrorMessage = "El campo cuenta valor debe es obligatorio")]
        public double dc_Valor_debe { get; set; }
        [Required(ErrorMessage = "El campo cuenta valor haber es obligatorio")]
        public double dc_Valor_haber { get; set; }
        public string pc_Cuenta { get; set; }
        public int? IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
