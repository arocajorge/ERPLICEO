using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_015_Info
    {
        public string IdUsuario { get; set; }
        public string Reporte { get; set; }
        public decimal Secuencia { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double SaldoE1 { get; set; }
        public double SaldoE2 { get; set; }
        public double SaldoE3 { get; set; }
        public double SaldoE4 { get; set; }
        public double Total { get; set; }
    }
}
