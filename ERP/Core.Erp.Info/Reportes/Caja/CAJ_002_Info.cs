using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Caja
{
   public class CAJ_002_Info
    {
        public long IdRow { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_OGiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public string co_factura { get; set; }
        public string pe_nombreCompleto { get; set; }
        public System.DateTime co_FechaFactura { get; set; }
        public double co_total { get; set; }
        public double valor_retencion { get; set; }
        public double valor_a_pagar { get; set; }
        public double Valor_a_aplicar { get; set; }
        public string co_observacion { get; set; }
        public double Saldo_cont_al_periodo { get; set; }
        public double Ingresos { get; set; }
        public Nullable<double> Total_fact_vale { get; set; }
        public double Dif_x_pagar_o_cobrar { get; set; }
        public string TIPO { get; set; }
        public Nullable<System.DateTime> Fecha_ini { get; set; }
        public Nullable<System.DateTime> Fecha_fin { get; set; }
        public Nullable<double> valor_a_reponer { get; set; }
        public string NombreCaja { get; set; }
        public string tm_descripcion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public string NombreUsuario { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<decimal> SecuenciaCaja { get; set; }
        public decimal? SecuenciaVale { get; set; }
    }
}
