using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_HorasProfesores_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }

        public decimal IdCarga { get; set; }
        public int Secuencia { get; set; }
        public string IdRubro { get; set; }
        public int IdEmpresa_nov { get; set; }
        public decimal IdNovedad { get; set; }
        public string Observacion { get; set; }
        public decimal IdEmpleado { get; set; }
        public double NumHoras { get; set; }
        public string rub_codigo { get; set; }
        public string ru_codRolGen { get; set; }
        public string ru_descripcion { get; set; }
        public string em_codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public double ValorHora { get; set; }
        public double Valor { get; set; }

        public string codigo { get; set; }
        public Nullable<int> IdJornada { get; set; }


    }
}
