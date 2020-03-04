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
        public bool Nuevo { get; set; }
        public bool Modificar { get; set; }
        public bool Anular { get; set; }

        //Campos que no existen en la tabla
        public string DescripcionMenu { get; set; }
        public seg_Menu_Info info_menu { get; set; }
        public bool seleccionado { get; set; }
        public int modificado { get; set; }
        public int? IdMenuPadre { get; set; }
    }
}
