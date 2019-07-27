using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_orden_pago_cancelaciones_Info
    {
        public int IdEmpresa { get; set; }
        public decimal Idcancelacion { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_op { get; set; }
        public decimal IdOrdenPago_op { get; set; }
        public int Secuencia_op { get; set; }
        public Nullable<int> IdEmpresa_op_padre { get; set; }
        public Nullable<decimal> IdOrdenPago_op_padre { get; set; }
        public Nullable<int> Secuencia_op_padre { get; set; }
        public Nullable<int> IdEmpresa_cxp { get; set; }
        public Nullable<int> IdTipoCbte_cxp { get; set; }
        public Nullable<decimal> IdCbteCble_cxp { get; set; }
        public int IdEmpresa_pago { get; set; }
        public int IdTipoCbte_pago { get; set; }
        public decimal IdCbteCble_pago { get; set; }
        public double MontoAplicado { get; set; }
        public double SaldoAnterior { get; set; }
        public double SaldoActual { get; set; }
        public string Observacion { get; set; }
        public System.DateTime fechaTransaccion { get; set; }

        #region Campos que no existen en la tabla
        public string IdTipo_op { get; set; }
        public string Referencia { get; set; }
        public string IdTipo_Persona { get; set; }
        public decimal? IdEntidad { get; set; }
        public decimal IdPersona { get; set; }
        public DateTime Fecha_Fa_Prov { get; set; }
        public DateTime Fecha_Venc_Fac_Prov { get; set; }
        public string IdCtaCble { get; set; }
        public string pe_nombreCompleto { get; set; }
        #endregion

    }
}
