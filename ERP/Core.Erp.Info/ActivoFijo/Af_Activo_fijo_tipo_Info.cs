using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Activo_fijo_tipo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        
      public int IdEmpresa { get; set; }
        public int IdActivoFijoTipo { get; set; }
        [StringLength(150, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 150")]
        public string CodActivoFijo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 150")]
        public string Af_Descripcion { get; set; }
        public double Af_Porcentaje_depre { get; set; }
        [Required(ErrorMessage = ("el campo año es obligatorio"))]
        public int Af_anio_depreciacion { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_Activo { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_Dep_Acum { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_Gastos_Depre { get; set; }
        public bool Se_Deprecia { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string Estado { get; set; }
        public string MotiAnula { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_CostoVenta { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_Mejora { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_Baja { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable es obligatorio"))]
        public string IdCtaCble_Retiro { get; set; }
        public bool EstadoBool { get; set; }

    }
}
