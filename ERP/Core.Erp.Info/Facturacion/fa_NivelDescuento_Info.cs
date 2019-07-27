using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_NivelDescuento_Info
    {
        public int IdEmpresa { get; set; }
        public int IdNivel { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(5000, ErrorMessage = "El campo descripción debe contener máximo 5000 caracteres")]
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        [Required(ErrorMessage = "El campo porcentaje es obligatorio")]
        public double Porcentaje { get; set; }
        public bool Estado { get; set; }
    }
}
