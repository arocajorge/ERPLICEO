using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_participacion_utilidad_empleado_Info
    {
        public int IdEmpresa { get; set; }
        public int IdUtilidad { get; set; }
        public decimal IdEmpleado { get; set; }
        public double DiasTrabajados { get; set; }
        public double CargasFamiliares { get; set; }
        public double ValorIndividual { get; set; }
        public double ValorCargaFamiliar { get; set; }
        public double ValorExedenteIESS { get; set; }
        public double ValorTotal { get; set; }
        public string Observacion { get; set; }

        // vista
        public double UtilidadDerechoIndividual { get; set; }
        public double UtilidadCargaFamiliar { get; set; }
        public double FactorA { get; set; }
        public double FactorB { get; set; }

        public string ca_descripcion { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string em_status { get; set; }
        public int IdNomina { get; set; }
        public int IdNominaTipo_liq { get; set; }
        public int IdPeriodo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> em_fechaIngaRol { get; set; }

        public Nullable<System.DateTime> em_fecha_ingreso { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> em_fechaSalida { get; set; }
        public Nullable<int> num_contratos { get; set; }
    }
}
