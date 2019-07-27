using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
 public   class cp_orden_pago_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int Secuencia { get; set; }
        public Nullable<int> IdEmpresa_cxp { get; set; }
        public Nullable<decimal> IdCbteCble_cxp { get; set; }
        public Nullable<int> IdTipoCbte_cxp { get; set; }
        public double Valor_a_pagar { get; set; }
        public string Referencia { get; set; }
        public string IdFormaPago { get; set; }
        public System.DateTime Fecha_Pago { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public Nullable<int> IdBanco { get; set; }
        public string IdUsuario_Aprobacion { get; set; }
        public Nullable<System.DateTime> fecha_hora_Aproba { get; set; }
        public string Motivo_aproba { get; set; }



        public string IdTipo_Persona { get; set; }
        public string IdTipoPersona { get; set; }
        public decimal IdPersona { get; set; }
        public Nullable<decimal> IdEntidad { get; set; }
        public string IdTipo_op { get; set; }
        public double Total_cancelado_OP { get; set; }
        public double Valor_estimado_a_pagar_OP { get; set; }
        public double Saldo_x_Pagar_OP { get; set; }
        public string Nom_Beneficiario { get; set; }
        public bool seleccionado { get; set; }


    }
}
