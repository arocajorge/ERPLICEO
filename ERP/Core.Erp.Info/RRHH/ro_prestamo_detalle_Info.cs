using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_prestamo_detalle_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPrestamo { get; set; }
        public int NumCuota { get; set; }
        public double SaldoInicial { get; set; }
        public double TotalCuota { get; set; }
        public double Saldo { get; set; }
        public System.DateTime FechaPago { get; set; }
        public string EstadoPago { get; set; }
        public string Estado { get; set; }
        public string Observacion_det { get; set; }
        public int IdNominaTipoLiqui { get; set; }


        #region campo vista
        public double ValorAplicado { get; set; }
        #endregion

    }
}
