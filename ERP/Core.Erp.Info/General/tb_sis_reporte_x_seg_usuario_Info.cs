using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
   public class tb_sis_reporte_x_seg_usuario_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string CodReporte { get; set; }
        public string nom_reporte { get; set; }
        public bool seleccionado { get; set; }
        public object observacion { get; set; }
        public string mvc_accion { get; set; }
        public string mvc_controlador { get; set; }
        public string mvc_area { get; set; }
    }
}
