using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_proveedor_clase_Info
    {
        public int IdEmpresa { get; set; }
        public int IdClaseProveedor { get; set; }
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 25")]
        public string cod_clase_proveedor { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 100")]
        public string descripcion_clas_prove { get; set; }
        public string IdCtaCble_Anticipo { get; set; }
        public string IdCtaCble_gasto { get; set; }
        [Required(ErrorMessage = "El campo cuenta contable es obligatoria")]
        public string IdCtaCble_CXP { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCentroCosto_sub_centro_costo { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioAnu { get; set; }
        public string MotivoAnu { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> FechaAnu { get; set; }
        public Nullable<System.DateTime> FechaTransac { get; set; }
        public Nullable<System.DateTime> FechaUltModi { get; set; }

    }
}
