using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_x_ro_rubro_Info
    {
        public int IdEmpresa { get; set; }
        public int IdRubroFijo { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo nómina es obligatorio")]
        public int IdNomina_Tipo { get; set; }
        [Required(ErrorMessage = "El campo tipo de nómina es obligatorio")]
        public int IdNomina_TipoLiqui { get; set; }
        [Required(ErrorMessage = "El campo rubro es obligatorio")]
        public string IdRubro { get; set; }
        [Required(ErrorMessage = "El campo valor es obligatorio")]
        public double Valor { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public bool es_indifinido { get; set; }

        public string Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotiAnula { get; set; }


        public string pe_cedulaRuc { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string ru_descripcion { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionProcesoNomina { get; set; }
    }
}
