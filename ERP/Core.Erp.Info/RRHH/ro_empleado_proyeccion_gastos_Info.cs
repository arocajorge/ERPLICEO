using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_proyeccion_gastos_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdTransaccion { get; set; }
        public decimal IdEmpleado { get; set; }
        public int AnioFiscal { get; set; }
        public string Observacion { get; set; }
        public bool estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }


        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }

        public List<ro_empleado_proyeccion_gastos_det_Info> list_proyeciones { get; set; }

    }
}
