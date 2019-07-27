using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_tipo_nota_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoNota { get; set; }
        [Required(ErrorMessage = ("el campo Tipo es obligatorio"))]
        public string Tipo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = ("el campo cuenta es obligatorio"))]
        public string IdCtaCble { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
    }
}
