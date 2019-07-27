using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_cbtecble_Plantilla_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPlantilla { get; set; }
        public int secuencia { get; set; }
        public string IdCtaCble { get; set; }
        [Required(ErrorMessage = "El campo valor es obligatorio")]
        public double dc_Valor { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string dc_Observacion { get; set; }
        public Nullable<int> IdPunto_cargo_grupo { get; set; }
        public Nullable<int> IdPunto_cargo { get; set; }
        public string IdCentroCosto { get; set; }


        #region Campos que no estan en la tabla
        public string pc_Cuenta { get; set; }
        public double dc_Valor_debe { get; set; }
        public double dc_Valor_haber { get; set; }
        #endregion
    }
}
