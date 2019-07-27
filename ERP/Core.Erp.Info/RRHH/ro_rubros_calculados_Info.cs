using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_rubros_calculados_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo días trabajados es obligatorio")]
        public string IdRubro_dias_trabajados { get; set; }
        [Required(ErrorMessage = "El campo total ingreso es obligatorio")]
        public string IdRubro_tot_ing { get; set; }
        [Required(ErrorMessage = "El campo total egreso es obligatorio")]
        public string IdRubro_tot_egr { get; set; }
        [Required(ErrorMessage = "El campo aporte personal es obligatorio")]
        public string IdRubro_iess_perso { get; set; }
        [Required(ErrorMessage = "El campo sueldo es obligatorio")]
        public string IdRubro_sueldo { get; set; }
        [Required(ErrorMessage = "El campo total a pagar es obligatorio")]
        public string IdRubro_tot_pagar { get; set; }
        public string IdRubro_aporte_patronal { get; set; }
        public string IdRubro_fondo_reserva { get; set; }
        public string IdRubro_prov_vac { get; set; }
        public string IdRubro_prov_DIII { get; set; }
        public string IdRubro_prov_DIV { get; set; }
        public string IdRubro_prov_FR { get; set; }
        public string IdRubro_DIII { get; set; }
        public string IdRubro_DIV { get; set; }
        public string IdRubro_IR { get; set; }
        public string IdRubro_horas_matutina { get; set; }
        public string IdRubro_horas_vespertina { get; set; }
        public string IdRubro_horas_brigadas { get; set; }
        public string IdRubro_horas_adicionales { get; set; }
        public string IdRubro_horas_control_salida { get; set; }
        public string IdRubro_horas_recargo { get; set; }
        public string IdRubro_bono_x_antiguedad { get; set; }
        public string IdRubro_anticipo { get; set; }
        public string IdRubro_novedad_proceso { get; set; }
        public string IdRubro_primaria_vespertina { get; set; }

    }
}
