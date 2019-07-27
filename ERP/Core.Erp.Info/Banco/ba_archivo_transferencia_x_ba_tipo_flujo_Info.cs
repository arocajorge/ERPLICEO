using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_archivo_transferencia_x_ba_tipo_flujo_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public decimal IdTipoFlujo { get; set; }
        public int Secuencia { get; set; }
        public double Porcentaje { get; set; }
        public double Valor { get; set; }
        public string Descricion { get; set; }
    }
}
