using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
   public class tbl_TransaccionesAutorizadas_info
    {
        public int IdEmpresa { get; set; }
        public decimal IdTransaccion { get; set; }
        public string IdUsuarioLog { get; set; }
        public string IdUsuarioAut { get; set; }
        public string Observacion { get; set; }
        public System.DateTime FechaTransaccion { get; set; }
    }
}
