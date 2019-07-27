using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_011_Info
    {
        public int IdBanco { get; set; }
        public string IdUsuario { get; set; }
        public string IdCtaCble { get; set; }
        public string Descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        public double SaldoAnterior { get; set; }
        public double Ingreso { get; set; }
        public double Egreso { get; set; }
        public double SaldoFinal { get; set; }
    }
}
