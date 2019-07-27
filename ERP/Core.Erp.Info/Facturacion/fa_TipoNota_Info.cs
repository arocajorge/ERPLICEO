using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_TipoNota_Info
    {
        public decimal IdTransaccionSession { get; set; }


        public int IdEmpresa { get; set; }
        public int IdTipoNota { get; set; }
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 20")]
        public string CodTipoNota { get; set; }
        public string Tipo { get; set; }

        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 150")]
        public string No_Descripcion { get; set; }
        public bool GeneraMoviInven { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string MotiAnula { get; set; }
        
    }
}
