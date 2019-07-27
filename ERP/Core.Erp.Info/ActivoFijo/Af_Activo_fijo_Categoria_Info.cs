using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Activo_fijo_Categoria_Info
    {
        public decimal IdTransaccionSession { get; set; }

        public int IdEmpresa { get; set; }
        public int IdCategoriaAF { get; set; }
        public int IdActivoFijoTipo { get; set; }
        public string cod_tipo { get; set; }
        public string CodCategoriaAF { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 500")]
        public string Descripcion { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
    }
}
