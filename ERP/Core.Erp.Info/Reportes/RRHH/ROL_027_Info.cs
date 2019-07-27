using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_027_Info
    {
        public int IdEmpresa { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string Nomina { get; set; }
        public string Area { get; set; }
        public string pe_nombre { get; set; }
        public string pe_apellido { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string em_codigo { get; set; }
        public decimal IdEmpleado { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<double> Valor { get; set; }
        public int Descuento { get; set; }
        public Nullable<double> Total { get; set; }
        public int IdDepartamento { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public Nullable<int> IdArea { get; set; }
        public string de_descripcion { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
