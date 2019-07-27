using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.SeguridadAcceso
{
    public class seg_Menu_x_Empresa_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMenu { get; set; }

        //Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        public seg_Menu_Info info_menu { get; set; }
    }
}
