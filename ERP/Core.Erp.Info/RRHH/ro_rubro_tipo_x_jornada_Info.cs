using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_rubro_tipo_x_jornada_Info
    {
        public int IdEmpresa { get; set; }
        public string IdRubro { get; set; }
        public int Secuencia { get; set; }
        public string IdRubroContabilizacion { get; set; }
        public int IdJornada { get; set; }

        public string ru_descripcion { get; set; }
        public string Descripcion { get; set; }
    }
}
