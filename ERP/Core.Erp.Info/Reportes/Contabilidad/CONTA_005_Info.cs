using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Contabilidad
{
    public class CONTA_005_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPunto_cargo { get; set; }
        public string IdUsuario { get; set; }
        public int IdPunto_cargo_grupo { get; set; }
        public string nom_punto_cargo { get; set; }
        public string nom_punto_cargo_grupo { get; set; }
        public double SaldoAnterior { get; set; }
        public double Debitos { get; set; }
        public double Creditos { get; set; }
        public double SaldoFinal { get; set; }
    }
}
