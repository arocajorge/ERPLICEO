using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_016_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<int> ANIO { get; set; }
        public Nullable<int> Semana { get; set; }
        public string DescripcionSemana { get; set; }
        public Nullable<double> vt_total { get; set; }
    }
}
