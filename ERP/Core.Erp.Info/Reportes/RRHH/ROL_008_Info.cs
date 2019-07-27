using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_008_Info
    {

        public string CedulaRuc { get; set; }
        public decimal IdEmpleado { get; set; }
        public decimal IdPrestamo { get; set; }
        public int IdEmpresa { get; set; }
        public System.DateTime Fecha { get; set; }
        public double MontoSol { get; set; }
        public int NumCuotas { get; set; }
        public string Observacion { get; set; }
        public int NumCuota { get; set; }
        public double SaldoInicial { get; set; }
        public double TotalCuota { get; set; }
        public double Saldo { get; set; }
        public System.DateTime FechaPago { get; set; }
        public string EstadoPago { get; set; }
        public string ObservacionCuota { get; set; }
        public string RubroDescripcion { get; set; }
        public string CodigoEmpleado { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public bool descuento_mensual { get; set; }
        public bool descuento_quincena { get; set; }
        public bool descuento_men_quin { get; set; }
        public System.DateTime Fecha_PriPago { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public Nullable<double> TotalCobrado { get; set; }
    }
}
