using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_010_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public Nullable<int> IdArea { get; set; }
        public decimal IdEmpleado { get; set; }
        public Nullable<int> IdTipoNomina { get; set; }
        public string Su_Descripcion { get; set; }
        public string DescDivision { get; set; }
        public string DescArea { get; set; }
        public string de_descripcion { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string Empleado { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public Nullable<int> edad { get; set; }
        public Nullable<System.DateTime> em_fecha_ingreso { get; set; }
        public Nullable<System.DateTime> em_fechaSalida { get; set; }
        public string ca_descripcion { get; set; }
        public string EstadoEmpleado { get; set; }
        public string em_status { get; set; }
        public Nullable<System.DateTime> em_fechaIngaRol { get; set; }
        public string antiguedad_string { get; set; }
        public string CodCatalogo_Ubicacion { get; set; }
        public string UbicacionGeneral { get; set; }
        public string TipoNomina { get; set; }
        public string pe_correo { get; set; }
        public string pe_telfono_Contacto { get; set; }
        public string pe_direccion { get; set; }


        public int CantidadEmpleados { get; set; }
    }
}
