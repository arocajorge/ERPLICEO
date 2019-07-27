using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Caja
{
    public class caj_Caja_Movimiento_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble { get; set; }
        public int IdTipocbte { get; set; }
        public int Secuencia { get; set; }
        public string IdCobro_tipo { get; set; }
        public double cr_Valor { get; set; }
    }
}
