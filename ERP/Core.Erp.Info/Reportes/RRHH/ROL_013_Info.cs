using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_013_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRol { get; set; }
        public decimal IdEmpleado { get; set; }
        public Nullable<int> IdArea { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public string IdRubro { get; set; }
        public double? Provision { get; set; }
        public string Estado { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string em_codigo { get; set; }
        public string de_descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Division { get; set; }
        public string Area { get; set; }
        public string Mes { get; set; }
        public double Prestamo { get; set; }
        public decimal Sueldo { get; set; }
        public Nullable<int> pe_anio { get; set; }
        public Nullable<int> pe_mes { get; set; }
        public decimal ValorNeto { get; set; }
        
    }
}
