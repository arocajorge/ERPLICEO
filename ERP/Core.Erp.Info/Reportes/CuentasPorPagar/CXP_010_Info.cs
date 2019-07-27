using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_010_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdProveedor { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string Referencia { get; set; }
        public string co_observacion { get; set; }
        public Nullable<double> Debito { get; set; }
        public double Credito { get; set; }
        public Nullable<double> Saldo { get; set; }
        public Nullable<double> SaldoModulo { get; set; }
        public int Secuencia { get; set; }
    }
}
