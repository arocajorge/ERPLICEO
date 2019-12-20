using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_AjusteImpuestoRentaDetOI_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdAjuste { get; set; }
        public decimal IdEmpleado { get; set; }
        public int Secuencia { get; set; }
        public string DescripcionOI { get; set; }
        public decimal Valor { get; set; }
    }
}
