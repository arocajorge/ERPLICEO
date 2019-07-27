using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.SeguridadAcceso
{
    public class seg_Menu_x_Empresa_x_Usuario_Info
    {
        public int IdEmpresa { get; set; }
        public string IdUsuario { get; set; }
        public int IdMenu { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public bool Eliminacion { get; set; }

        //Campos que no existen en la tabla
        public seg_Menu_Info info_menu { get; set; }
        public bool seleccionado { get; set; }
    }
}
