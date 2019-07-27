using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Contabilidad
{
    public class CONTA_003_balances_Info
    {
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public string IdCtaCblePadre { get; set; }
        public bool EsCtaUtilidad { get; set; }
        public int IdNivelCta { get; set; }
        public string IdGrupoCble { get; set; }
        public string gc_GrupoCble { get; set; }
        public string gc_estado_financiero { get; set; }
        public int gc_Orden { get; set; }
        public double DebitosSaldoInicial { get; set; }
        public double CreditosSaldoInicial { get; set; }
        public double SaldoInicial { get; set; }
        public double Debitos { get; set; }
        public double Creditos { get; set; }
        public double SaldoDebitosCreditos { get; set; }
        public double SaldoDebitos { get; set; }
        public double SaldoCreditos { get; set; }
        public double SaldoFinal { get; set; }
        public bool EsCuentaMovimiento { get; set; }
        public string Naturaleza { get; set; }
        public double SaldoInicialNaturaleza { get; set; }
        public double SaldoDebitosCreditosNaturaleza { get; set; }
        public double SaldoDebitosNaturaleza { get; set; }
        public double SaldoCreditosNaturaleza { get; set; }
        public double SaldoFinalNaturaleza { get; set; }

        public string Su_Descripcion { get; set; }
    }
}
