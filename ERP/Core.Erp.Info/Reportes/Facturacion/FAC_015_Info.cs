using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_015_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<int> ANIO { get; set; }
        public Nullable<int> MES { get; set; }
        public string NomMes { get; set; }
        public Nullable<double> vt_total { get; set; }
    }
}
