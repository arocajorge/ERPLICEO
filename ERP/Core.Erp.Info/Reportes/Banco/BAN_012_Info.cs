using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_012_Info
    {
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public int IdTipoFlujo { get; set; }
        public string IdUsuario { get; set; }
        public string ba_descripcion { get; set; }
        public string nom_tipo_flujo { get; set; }
        public double SaldoInicial { get; set; }
        public double Ingresos { get; set; }
        public double Egresos { get; set; }
        public double SaldoFinal { get; set; }
        public double SaldoFinalBanco { get; set; }
    }
}
