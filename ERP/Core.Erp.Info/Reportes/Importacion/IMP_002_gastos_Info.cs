using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Importacion
{
    public class IMP_002_gastos_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdOrdenCompra_ext { get; set; }
        public string gt_descripcion { get; set; }
        public double dc_Valor { get; set; }
        public string dc_Observacion { get; set; }
    }
}
