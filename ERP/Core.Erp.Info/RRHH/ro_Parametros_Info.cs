using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_Parametros_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo tipo de asiento es obligatorio es obligatorio")]

        public Nullable<int> IdTipoCbte_AsientoSueldoXPagar { get; set; }
        public bool Genera_op_por_liq_vacaciones { get; set; }
        public bool Genera_op_por_acta_finiquito { get; set; }
        public bool Genera_op_por_prestamos { get; set; }
        public string IdTipo_op_vacaciones { get; set; }
        public string IdTipo_op_prestamos { get; set; }
        public string IdTipo_op_acta_finiquito { get; set; }
        public string IdTipo_op_sueldo_por_pagar { get; set; }

        public string Descripcion { get; set; }
        [RegularExpression(@"\d+(\.\d{1,4})?", ErrorMessage = "Ingrese solo numeros con máximo 4 decimal")]
        [Required(ErrorMessage = "El campo salario básico unificado es obligatorio")]
        public double Sueldo_basico { get; set; }
        [RegularExpression(@"\d+(\.\d{1,4})?", ErrorMessage = "Ingrese solo numeros con máximo 4 decimal")]
        [Required(ErrorMessage = "El campo porcentaje aporte personal es obligatorio")]
        public double Porcentaje_aporte_pers { get; set; }
        [RegularExpression(@"\d+(\.\d{1,4})?", ErrorMessage = "Ingrese solo numeros con máximo 4 decimal")]
        [Required(ErrorMessage = "El campo porcentaje aporte patronal es obligatorio")]
        public double Porcentaje_aporte_patr { get; set; }
        [Required(ErrorMessage = "El campo rubro gasto acta finiquito es obligatorio")]
        public string IdRubro_acta_finiquito { get; set; }
        public bool genera_op_x_pago { get; set; }
        public bool Genera_op_x_pago_x_empleao { get; set; }
        public string EstadoCreacionPrestamos { get; set; }
        public double Porcentaje_anticipo { get; set; }
        public string pc_Cuenta { get; set; }
        public List<ro_Config_Param_contable_Info> lst_cta_x_rubros { get; set; }
        public List<ro_Config_Param_contable_Info> lst_cta_x_provisiones { get; set; }

        public List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info> lst_cta_x_sueldo_pagar { get; set; }

        public ro_Parametros_Info()
        {
            lst_cta_x_rubros = new List<ro_Config_Param_contable_Info>();
            lst_cta_x_sueldo_pagar = new List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info>();
            lst_cta_x_provisiones = new List<ro_Config_Param_contable_Info>();
        }

        public string IdString { get; set; }
    }

   
}
