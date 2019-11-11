using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_004_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int IdBanco { get; set; }
        public int IdPeriodo { get; set; }
        public string nom_banco { get; set; }
        public string ba_Num_Cuenta { get; set; }
        public string IdCtaCble { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string CodTipoCbte { get; set; }
        public string Tipo_Cbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int IdTipoCbte { get; set; }
        public int SecuenciaCbte { get; set; }
        public Nullable<double> Valor { get; set; }
        public string Observacion { get; set; }
        public string Cheque { get; set; }
        public Nullable<double> SaldoInicial { get; set; }
        public Nullable<double> SaldoFinal { get; set; }
        public string Titulo_grupo { get; set; }
        public string referencia { get; set; }
        public string ruc_empresa { get; set; }
        public string nom_empresa { get; set; }
        public double SaldoBanco_EstCta { get; set; }
        public string Estado_Conciliacion { get; set; }
        public string GiradoA { get; set; }
        public Nullable<decimal> IdTipoFlujo { get; set; }
        public string nom_tipo_flujo { get; set; }
        public Nullable<double> Total_Conciliado { get; set; }
        public Nullable<System.DateTime> FechaIni { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public double TotalConciliadoNoContable { get; set; }
        public double co_SaldoBanco_anterior { get; set; }
        public double SaldoContableActual { get; set; }
    }
}
