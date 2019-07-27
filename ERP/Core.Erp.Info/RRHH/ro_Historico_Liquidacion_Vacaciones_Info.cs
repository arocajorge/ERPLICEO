using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Erp.Info.RRHH
{
   public class ro_Historico_Liquidacion_Vacaciones_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public int IdLiquidacion { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int IdEmpresa_OP { get; set; }
        public double ValorCancelado { get; set; }
        public System.DateTime FechaPago { get; set; }
        public string Observaciones { get; set; }
        public string IdUsuario { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaHoraAnul { get; set; }
        public string MotiAnula { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string EstadoContrato { get; set; }
        public Nullable<double> Iess { get; set; }
        public string IdTipo_op { get; set; }
        public string Gozadas_Pagadas { get; set; }
        public int IdSolicitud { get; set; }


        public string Periodo { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public int Dias_q_Corresponde { get; set; }
        public int Dias_a_disfrutar { get; set; }
        public int Dias_pendiente { get; set; }
        public DateTime Fecha_Desde { get; set; }
        public DateTime Fecha_Hasta { get; set; }
        public DateTime Fecha_Retorno { get; set; }
        public bool Gozadas_Pgadas { get; set; }
        public string empleado { get; set; }
        public System.DateTime Anio_Desde { get; set; }
        public System.DateTime Anio_Hasta { get; set; }
        public List< ro_Historico_Liquidacion_Vacaciones_Det_Info> detalle{ get; set; }
        public ro_Historico_Liquidacion_Vacaciones_Info()
        {
            detalle = new List<ro_Historico_Liquidacion_Vacaciones_Det_Info>();
        }
    }
}
