using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_prestamo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdPrestamo { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo rubro es obligatorio")]
        public string IdRubro { get; set; }
        public bool descuento_mensual { get; set; }
        public bool descuento_quincena { get; set; }
        public bool descuento_men_quin { get; set; }
        public bool Estado { get; set; }
        public bool EstadoBool { get; set; }
        public System.DateTime Fecha { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Ingrese solo numeros")]
        [Required(ErrorMessage = "El campo monto es obligatorio")]
        public double MontoSol { get; set; }
        [Required(ErrorMessage = "El campo número cuota es obligatorio")]

        public int NumCuotas { get; set; }
        public System.DateTime Fecha_PriPago { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotiAnula { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }
        public Nullable<decimal> IdOrdenPago { get; set; }    
        public string IdUsuarioAprueba { get; set; }
        public string EstadoAprob { get; set; }
        public bool GeneraOP { get; set; }



        public double TotalCobrado { get; set; }
        public double Valor_pendiente { get; set; }
        public string pe_nombre_completo { get; set; }
        public string ru_descripcion { get; set; }
        public double Monto_aplicado { get; set; }
        public string IdCtaCble_Emplea { get; set; }
        public string IdCtaCble_x_pagar_empleado { get; set; }

        public string rub_ctacon { get; set; }
        public string EstadoCancelacion { get; set; }

        public List<ro_prestamo_detalle_Info> lst_detalle { get; set; }

        public void ro_prestamo_detalle_Info()
        {
            lst_detalle = new List<RRHH.ro_prestamo_detalle_Info>();
        }

    }
}
