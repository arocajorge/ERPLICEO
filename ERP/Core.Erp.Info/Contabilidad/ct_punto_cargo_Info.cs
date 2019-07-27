using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
   public class ct_punto_cargo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPunto_cargo { get; set; }
        public string cod_punto_cargo { get; set; }
        public string nom_punto_cargo { get; set; }
        public bool Estado { get; set; }
        public int IdPunto_cargo_grupo { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
