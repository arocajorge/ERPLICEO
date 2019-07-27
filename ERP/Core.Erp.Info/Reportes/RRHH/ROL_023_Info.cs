using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_023_Info
    {

        public int IdEmpresa { get; set; }
        public decimal IdRol { get; set; }
        public decimal IdEmpleado { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public Nullable<int> IdArea { get; set; }
        public Nullable<int> IdDepartamento { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public int IdNominaTipo { get; set; }
        public string Descripcion { get; set; }
        public int IdNominaTipoLiqui { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public int IdPeriodo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NombreDivision { get; set; }
        public string NombreArea { get; set; }
        public string NombreDepartamento { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<double> SUELDO { get; set; }
        public Nullable<double> ANTICIPO { get; set; }
        public Nullable<double> DECIMOC { get; set; }
        public Nullable<double> DECIMOT { get; set; }
        public Nullable<double> FRESERVA { get; set; }
        public Nullable<double> IESS { get; set; }
        public Nullable<double> PRESTAMO { get; set; }
        public Nullable<double> SOBRET { get; set; }
        public Nullable<double> TOTALE { get; set; }
        public Nullable<double> TOTALI { get; set; }
        public Nullable<double> OTROEGR { get; set; }
        public Nullable<double> OTROING { get; set; }
        public Nullable<double> DIASTRABAJADOS { get; set; }
        public Nullable<double> NETO { get; set; }
        public string NombreCargo { get; set; }
        public string JORNADA { get; set; }
        public string UBUCACION { get; set; }

        public int CantidadEmpleados { get; set; }
        public Nullable<double> TotalResumen { get; set; }

        #region MyRegion

        public Nullable<double> FRESERVA_R { get; set; }
        public Nullable<double> IESS_R { get; set; }
        public double? IESS_TOTAL { get; set; }
        public double? FRESERVA_TOTAL { get; set; }
        public long? Fila { get; set; }
        public double? IESS_2 { get; set; }
        public double? FRESERVA_2 { get; set; }
        #endregion
    }
}
