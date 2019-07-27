using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_Conciliacion_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public int IdTipocbte { get; set; }
        public string tipo_IngEgr { get; set; }
        public string Referencia { get; set; }
        public bool Seleccionado { get; set; }
        public string Observacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Valor { get; set; }

    }
}
