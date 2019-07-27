using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_sis_reporte_x_tb_empresa_Info
    {
        public int IdEmpresa { get; set; }
        public string CodReporte { get; set; }
        public byte[] ReporteDisenio { get; set; }
        public string Nom_Carpeta { get; set; }
        public string Reporte { get; set; }
    }
}
