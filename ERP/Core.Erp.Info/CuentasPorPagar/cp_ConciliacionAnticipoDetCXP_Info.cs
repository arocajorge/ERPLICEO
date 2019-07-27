using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_ConciliacionAnticipoDetCXP_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int IdEmpresa_cxp { get; set; }
        public int IdTipoCbte_cxp { get; set; }
        public decimal IdCbteCble_cxp { get; set; }
        [Required(ErrorMessage = "El campo monto es obligatorio")]
        public double MontoAplicado { get; set; }
        public DateTime Fecha_cxp { get; set; }
        public string Observacion_cxp { get; set; }

        #region Campos que no existen en la tabla
        public string tc_TipoCbte { get; set; }        
        #endregion
    }
}
