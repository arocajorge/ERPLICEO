using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.SeguridadAcceso
{
    public class seg_Usuario_x_Empresa_Info
    {
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string Observacion { get; set; }
        //Campos que no existen en la tabla
        public string em_nombre { get; set; }
        public bool seleccionado { get; set; }
    }
}
