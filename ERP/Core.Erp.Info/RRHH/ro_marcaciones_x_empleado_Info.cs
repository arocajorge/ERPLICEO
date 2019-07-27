using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_marcaciones_x_empleado_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdRegistro { get; set; }
        public int IdCalendadrio { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo tipo marcación es obligatorio")]
        public string IdTipoMarcaciones { get; set; }
        public Nullable<int> IdNomina { get; set; }
        public Nullable<int> IdPeriodo { get; set; }
        [Required(ErrorMessage = "El campo hora es obligatorio es obligatorio")]
        public Nullable<System.TimeSpan> es_Hora { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime es_fechaRegistro { get; set; }

        public string IdTipoMarcaciones_Biometrico { get; set; }
        public string Observacion { get; set; }
        public string IdUsuario { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string Ip { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string Motivo_Anu { get; set; }
        public int IdSucursal { get; set; }
        public string ca_descripcion { get; set; }
        public string pe_NombreCompleato { get; set; }
        public string pe_cedula { get; set; }
        public string cargo { get; set; }
        public string em_codigo { get; set; }


        public List<ro_marcaciones_x_empleado_Info> detalle { get; set; }

    }
}
