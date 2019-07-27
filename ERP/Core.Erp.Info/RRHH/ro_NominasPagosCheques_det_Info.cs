using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_NominasPagosCheques_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTransaccion { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public Nullable<decimal> IdEmpleado { get; set; }
        public string Observacion { get; set; }
        public double Valor { get; set; }
        public int IdEmpresa_op { get; set; }
        public decimal IdOrdenPago { get; set; }

        public int IdEmpresa_dc { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int Secuancia_op { get; set; }

        #region camp. vista
        public cp_orden_pago_Info info_orden_pago { get; set; }

        public double? ValorCancelado { get; set; }
        public string em_codigo { get; set; }
        public string em_tipoCta { get; set; }
        public string em_NumCta { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public double Saldo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public decimal IdPersona { get; set; }
        public string IdCtaCble_Emplea { get; set; }
        public string IdCtaCble_x_pagar_empleado { get; set; }


        #endregion

    }
}
