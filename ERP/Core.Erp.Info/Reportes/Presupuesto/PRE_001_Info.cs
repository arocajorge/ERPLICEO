using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Presupuesto
{
    public class PRE_001_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPresupuesto { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public decimal IdPeriodo { get; set; }
        public string DescripciónPeriodo { get; set; }
        public int IdRubro { get; set; }
        public string DescripcionRubro { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public double Monto { get; set; }
        public bool Estado { get; set; }
        public double MontoSolicitado { get; set; }
        public double MontoAprobado { get; set; }
        public string Observacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public string IdUsuarioAprobacion { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
    }
}
