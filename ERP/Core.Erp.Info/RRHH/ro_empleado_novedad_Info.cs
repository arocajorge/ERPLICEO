using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_empleado_novedad_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdNovedad { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]

        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo nómina es obligatorio")]

        public int IdNomina_Tipo { get; set; }
        [Required(ErrorMessage = "El campo tipo de nómina es obligatorio")]

        public int IdNomina_TipoLiqui { get; set; }
        public System.DateTime Fecha { get; set; }
        public double TotalValor { get; set; }
        public Nullable<System.DateTime> Fecha_PrimerPago { get; set; }
        public Nullable<int> NumCoutas { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotiAnula { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdCentroCosto { get; set; }
        public string MotivoModiica { get; set; }
        public string IdCalendario { get; set; }
        public Nullable<int> IdPeriodo { get; set; }
        public string Observacion { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdJornada { get; set; }


        public List<ro_empleado_novedad_det_Info> lst_novedad_det { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Descripcion { get; set; }
        public string IdRubro { get; set; }
        public string DescripcionProcesoNomina { get; set; }
    }
}
