using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_EmpleadoNovedadCargaMasiva_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdCarga { get; set; }
        [Required(ErrorMessage = "El campo nómina es obligatorio")]
        public int IdNomina { get; set; }
        [Required(ErrorMessage = "El campo proceso es obligatorio")]
        public int IdNominaTipo { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public System.DateTime FechaCarga { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        [Required(ErrorMessage = "El campo rubro es obligatorio")]
        public string IdRubro { get; set; }
        public string IdUsuario { get; set; }
        public bool Estado { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }

        public string Descripcion { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public string ru_descripcion { get; set; }
        public bool EstadoBool { get; set; }


        public List<ro_EmpleadoNovedadCargaMasiva_det_Info> detalle { get; set; }
    }
}
