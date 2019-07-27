using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_012_Info
    {
        public int IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public decimal IdEmpleado { get; set; }
        public decimal IdPrestamo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string EstadoPago { get; set; }
        public string de_descripcion { get; set; }
        public Nullable<double> Total_Prestamo { get; set; }
        public Nullable<double> Total_Cancelado { get; set; }
        public Nullable<double> Total_Pendiente_pago { get; set; }
        public string Observacion { get; set; }
        public System.DateTime Fecha_PriPago { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public int IdSucursal { get; set; }
        public string IdRubro { get; set; }
        public string ru_descripcion { get; set; }
    }
}
