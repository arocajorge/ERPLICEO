using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Contabilidad;
namespace Core.Erp.Info.Contabilidad.ATS.ATS_Info
{
   public class ats_Info
    {
        public List<compras_Info> lst_compras { get; set; }
        public List<ventas_Info> lst_ventas { get; set; }
        public List<retenciones_Info> lst_retenciones { get; set; }
        public List<importaciones_info> lst_importaciones { get; set; }
        public List<comprobantesAnulados_info> lst_anulados { get; set; }
        public List<exportaciones_Info> lst_exportaciones { get; set; }
        public int IdSucursal { get; set; }

        public int[] IntArray { get; set; }
        public int IdPeriodo { get; set; }
        public ats_Info()
        {
            lst_compras = new List<compras_Info>();
            lst_ventas = new List<ventas_Info>();
            lst_retenciones = new List<retenciones_Info>();
            lst_importaciones = new List<importaciones_info>();
            lst_anulados = new List<comprobantesAnulados_info>();
            lst_exportaciones = new List<exportaciones_Info>();
        }

    }
}
