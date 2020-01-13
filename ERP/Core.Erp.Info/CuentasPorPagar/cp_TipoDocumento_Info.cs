using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
  public  class cp_TipoDocumento_Info
    {
        public string CodTipoDocumento { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public string DeclaraSRI { get; set; }
        public string CodSRI { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string GeneraRetencion { get; set; }
        public string Codigo_Secuenciales_Transaccion { get; set; }
        public string Sustento_Tributario { get; set; }
        public bool ManejaTalonario { get; set; }
    }
}
