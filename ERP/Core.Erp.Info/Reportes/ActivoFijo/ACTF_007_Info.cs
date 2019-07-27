using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.ActivoFijo
{
    public class ACTF_007_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public System.DateTime Af_fecha_compra { get; set; }
        public double Af_costo_compra { get; set; }
        public string Estado { get; set; }
        public string Af_observacion { get; set; }
        public string Af_Nombre { get; set; }
        public string Su_Descripcion { get; set; }
        public string NomTipo { get; set; }
        public string NomCategoria { get; set; }
        public string NomDepartamento { get; set; }
        public string NomEncargado { get; set; }
        public string NomCustodio { get; set; }
        public int Cantidad { get; set; }
    }
}
