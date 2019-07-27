using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_ConciliacionAnticipoDetAnt_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdOrdenPago { get; set; }
        [Required(ErrorMessage = "El campo monto es obligatorio")]
        public double MontoAplicado { get; set; }

        #region Campos que no existen en la tabla
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        #endregion
    }
}
