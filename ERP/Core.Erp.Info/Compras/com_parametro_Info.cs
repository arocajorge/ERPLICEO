using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Compras
{
    public class com_parametro_Info
    {
        public int IdEmpresa { get; set; }
        public string IdEstadoAprobacion_OC { get; set; }
        public int IdMovi_inven_tipo_OC { get; set; }
        public string IdEstadoAnulacion_OC { get; set; }
        public string IdEstado_cierre { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
    }
}
