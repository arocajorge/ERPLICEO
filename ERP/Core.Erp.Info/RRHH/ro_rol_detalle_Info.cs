using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_rol_detalle_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRol { get; set; }
        public int IdNominaTipo { get; set; }
        public int IdNominaTipoLiqui { get; set; }
        public int IdPeriodo { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdRubro { get; set; }
        public int Orden { get; set; }
        public double Valor { get; set; }
        public Nullable<bool> rub_visible_reporte { get; set; }
        public string Observacion { get; set; }
        public bool rub_ContPorEmpleado { get; set; }
        public string IdCtaCble_Emplea { get; set; }
        public string ru_descripcion { get; set; }

        #region campos de la vista
        public Nullable<int> IdArea { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public int IdDepartamento { get; set; }
        public Nullable<int> IdCargo { get; set; }
        public decimal IdPersona { get; set; }
        public decimal IdEntidad { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public string pe_nombreCompleato { get; set; }
        public string pe_cedulaRuc { get; set; }

        public string ru_tipo { get; set; }

        public decimal? Valor_ { get; set; }
        public bool check { get; set; }
        public string IdCtaCble_Debe { get; set; }
        public string pc_CuentaRubro { get; set; }
        public string pc_CuentaEmple { get; set; }
        public int? IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }

        #endregion



    }
}
