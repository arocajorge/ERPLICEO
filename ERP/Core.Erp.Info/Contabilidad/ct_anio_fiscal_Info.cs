using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_anio_fiscal_Info
    {
        
        [Required(ErrorMessage = ("El campo año es obligatorio"))]
        public int IdanioFiscal { get; set; }
        public System.DateTime af_fechaIni { get; set; }
        public System.DateTime af_fechaFin { get; set; }
        public string af_estado { get; set; }
        public bool EstadoBool { get; set; }
        //no existe en la tabla
        public ct_anio_fiscal_x_cuenta_utilidad_Info info_anio_ctautil { get; set; }
    }
}
