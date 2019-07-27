using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_parametro_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = ("el campo impuesto es obligatorio"))]
        public string IdCod_Impuesto { get; set; }
        [Required(ErrorMessage = ("el campo porcentaje es obligatorio"))]
        public double Porcentaje { get; set; }
        public bool EsMultiSucursal { get; set; }
    }
}
