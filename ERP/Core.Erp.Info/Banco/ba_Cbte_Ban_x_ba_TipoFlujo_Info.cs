using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_Cbte_Ban_x_ba_TipoFlujo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipocbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int Secuencia { get; set; }
        public decimal IdTipoFlujo { get; set; }
        public double Porcentaje { get; set; }
        public double Valor { get; set; }

        public string Descricion { get; set; }
    }
}
