using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_proveedor_detalle_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdProveedor { get; set; }
        public int Secuencia { get; set; }
        public string NombreAdicional { get; set; }
        public string IdTipoCta_acreditacion_cat { get; set; }
        public string num_cta_acreditacion { get; set; }
        public Nullable<int> IdBanco_acreditacion { get; set; }
        public string pr_correo { get; set; }
        public bool Estado { get; set; }

    }
}
