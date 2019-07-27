using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
   public  class ROL_009_Info
    {

        public string CedulaRuc { get; set; }
        public string NombreCompleto { get; set; }
        public string IdRubro { get; set; }
        public System.DateTime FechaPago { get; set; }
        public double Valor { get; set; }
        public string EstadoCobro { get; set; }
        public string RubroDescripcion { get; set; }
        public string Division { get; set; }
        public string Departamento { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public string CodigoEmpleado { get; set; }
        public int IdDepartamento { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public Nullable<double> Num_Horas { get; set; }
        public string ca_descripcion { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public int IdNomina_Tipo { get; set; }
        public string Descripcion_Nomina_Tipo { get; set; }


    }
}
