using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Contabilidad
{
    public class CONTA_004_Info
    {
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public string IdCtaCblePadre { get; set; }
        public bool EsCtaUtilidad { get; set; }
        public int IdNivelCta { get; set; }
        public string IdGrupoCble { get; set; }
        public string gc_GrupoCble { get; set; }
        public string gc_estado_financiero { get; set; }
        public int gc_Orden { get; set; }
        public bool EsCuentaMovimiento { get; set; }
        public string Naturaleza { get; set; }
        public decimal Valor1 { get; set; }
        public decimal Valor2 { get; set; }
        public decimal Variacion { get; set; }
        public string Signo { get; set; }

        public string Su_Descripcion { get; set; }
    }
}
