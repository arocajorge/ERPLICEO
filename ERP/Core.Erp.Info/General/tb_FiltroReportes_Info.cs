using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_FiltroReportes_Info
    {
        public int IdEmpresa { get; set; }
        public string IdUsuario { get; set; }
        public int IdSucursal { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}
