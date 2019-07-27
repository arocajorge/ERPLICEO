using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.ActivoFijo
{
    public class ACTF_008_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public string Af_Nombre { get; set; }
        public string Af_observacion { get; set; }
        public string Estado { get; set; }
        public System.DateTime Af_fecha_compra { get; set; }
        public double Af_costo_compra { get; set; }
        public int Af_Vida_Util { get; set; }
        public decimal IdEmpleadoEncargado { get; set; }
        public decimal IdEmpleadoCustodio { get; set; }
        public string EmpleadoEncargado { get; set; }
        public string EmpleadoCustodio { get; set; }
        public decimal IdDepartamento { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public string NomDepartamento { get; set; }
        public int Cantidad { get; set; }
        public string NomCategoria { get; set; }
        public string NomTipo { get; set; }
    }
}
