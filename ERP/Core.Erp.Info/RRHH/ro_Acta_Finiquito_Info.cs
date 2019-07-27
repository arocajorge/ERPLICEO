using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_Acta_Finiquito_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdActaFiniquito { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdCausaTerminacion { get; set; }
        public decimal IdContrato { get; set; }
        public Nullable<int> IdCargo { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public System.DateTime FechaSalida { get; set; }
        public double UltimaRemuneracion { get; set; }
        public string Observacion { get; set; }
        public double Ingresos { get; set; }
        public double Egresos { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string MotiAnula { get; set; }
        public Nullable<int> IdCodSectorial { get; set; }
        public bool EsMujerEmbarazada { get; set; }
        public bool EsDirigenteSindical { get; set; }
        public bool EsPorDiscapacidad { get; set; }
        public bool EsPorEnfermedadNoProfesional { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }
        public Nullable<decimal> IdOrdenPago { get; set; }
        public string Contrato_tipo { get; set; }

        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string em_codigo { get; set; }
        public string ca_descripcion { get; set; }
        public string Contrato { get; set; }
        public string pe_nombre_completo { get; set; }
        public string EstadoContrato { get; set; }

        public List<ro_Acta_Finiquito_Detalle_Info> lst_detalle { get; set; }

    }
}
