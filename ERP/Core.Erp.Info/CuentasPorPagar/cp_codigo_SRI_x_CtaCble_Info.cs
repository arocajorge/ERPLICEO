using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_codigo_SRI_x_CtaCble_Info
    {
        public int IdEmpresa { get; set; }
        public int idCodigo_SRI { get; set; }
        [Required(ErrorMessage = "El campo cuenta contable es obligatorio")]
        public string IdCtaCble { get; set; }
        public Nullable<System.DateTime> fecha_UltMod { get; set; }
        public string idUsuario { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
    }
}
