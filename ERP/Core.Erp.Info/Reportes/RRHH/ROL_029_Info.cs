using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_029_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAjuste { get; set; }
        public int IdAnio { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime FechaCorte { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public int Secuencia { get; set; }
        public decimal IdEmpleado { get; set; }
        public decimal SueldoFechaCorte { get; set; }
        public decimal SueldoProyectado { get; set; }
        public decimal OtrosIngresos { get; set; }
        public decimal IngresosLiquidos { get; set; }
        public decimal GastosPersonales { get; set; }
        public decimal AporteFechaCorte { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal FraccionBasica { get; set; }
        public decimal Excedente { get; set; }
        public decimal ImpuestoFraccionBasica { get; set; }
        public decimal ImpuestoRentaCausado { get; set; }
        public decimal DescontadoFechaCorte { get; set; }
        public decimal LiquidacionFinal { get; set; }
        public decimal IdPersona { get; set; }
        public string pe_nombreCompleto { get; set; }

        public string DescripcionOI { get; set; }
        public decimal Valor { get; set; }
    }
}
