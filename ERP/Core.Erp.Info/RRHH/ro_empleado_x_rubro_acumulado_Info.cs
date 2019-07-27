using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_x_rubro_acumulado_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo rubro es obligatorio")]
        public string IdRubro { get; set; }
        public System.DateTime FechaIngresa { get; set; }
        public string UsuarioIngresa { get; set; }
        public Nullable<System.DateTime> FechaModifica { get; set; }
        public string UsuarioModifica { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio acumulacion es obligatorio")]
        public Nullable<System.DateTime> Fec_Inicio_Acumulacion { get; set; }
        [Required(ErrorMessage = "El campo fecha fin acumulacion es obligatorio")]
        public Nullable<System.DateTime> Fec_Fin_Acumulacion { get; set; }



        public string ru_descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string em_codigo { get; set; }

        public List<ro_empleado_x_rubro_acumulado_detalle_Info> lst_empleado_x_rubro_acumulado_detalle { get; set; }

    }
}
