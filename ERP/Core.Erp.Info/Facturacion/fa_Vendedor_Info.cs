using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_Vendedor_Info
    {
        public int IdEmpresa { get; set; }
        public int IdVendedor { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string Codigo { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre interno debe tener mínimo 1 caracter y máximo 400")]
        public string NomInterno { get; set; }
        [Required(ErrorMessage = ("el campo vendedor es obligatorio"))]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo vendedor debe tener mínimo 1 caracter y máximo 400")]
        public string Ve_Vendedor { get; set; }
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo cedula debe tener mínimo 1 caracter y máximo 20")]
        public string ve_cedula { get; set; }
        public double PorComision { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        
    }
}
