using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_tabla_Impu_Renta_Info
    {
        [Required(ErrorMessage = ("El campo año fiscal es obligatorio"))]
        public int AnioFiscal { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = ("El campo fracción basica es obligatorio"))]
        public Nullable<double> FraccionBasica { get; set; }
        [Required(ErrorMessage = ("El campo exceso hasta es obligatorio"))]
        public Nullable<double> ExcesoHasta { get; set; }
        [Required(ErrorMessage = ("El campo impuesto fracción basica es obligatorio"))]
        public Nullable<double> ImpFraccionBasica { get; set; }
        [Required(ErrorMessage = ("El campo porcentaje fracción basica es obligatorio"))]
        public Nullable<double> Por_ImpFraccion_Exce { get; set; }
    }
}
