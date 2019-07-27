using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
   public class FAC_011_Info
    {

        public int IdEmpresa { get; set; }
        public decimal IdCliente { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string Referencia { get; set; }
        public string Observacion { get; set; }
        public double Debitos { get; set; }
        public Nullable<double> Creditos { get; set; }
        public Nullable<double> Saldo { get; set; }
        public Nullable<double> SaldoModulo { get; set; }
        public Nullable<int> Secuencia { get; set; }
    }
}
