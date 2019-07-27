using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
   public class tb_sis_reporte_Info
    {
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        public string CodReporte { get; set; }

        [Required(ErrorMessage = ("el campo nombre es obligatorio"))]
        public string nom_reporte { get; set; }

        [Required(ErrorMessage = ("el campo módulo es obligatorio"))]
        public string CodModulo { get; set; }

        [Required(ErrorMessage = ("el campo vista es obligatorio"))]
        public string rpt_vista { get; set; }
        public bool rpt_usa_store_procedure { get; set; }
        public string rpt_store_procedure { get; set; }

        [Required(ErrorMessage = ("el campo clase info es obligatorio"))]
        public string rpt_clase_info { get; set; }

        [Required(ErrorMessage = ("el campo clase data es obligatorio"))]
        public string rpt_clase_data { get; set; }

        [Required(ErrorMessage = ("el campo area es obligatorio"))]
        public string mvc_area { get; set; }

        [Required(ErrorMessage = ("el campo controlador es obligatorio"))]
        public string mvc_controlador { get; set; }

        [Required(ErrorMessage = ("el campo acción es obligatorio"))]
        public string mvc_accion { get; set; }
        [Required(ErrorMessage = ("el campo clase bus es obligatorio"))]
        public string rpt_clase_bus { get; set; }
        [Required(ErrorMessage = ("el campo clase rpt es obligatorio"))]
        public string rpt_clase_rpt { get; set; }
        public bool estado { get; set; }
        public bool se_muestra_administrador_reportes { get; set; }
        public int orden { get; set; }
        public string observacion { get; set; }
        public bool rpt_muestra_disenador_reporte { get; set; }
        public string Nom_Carpeta { get; set; }
    }
}
