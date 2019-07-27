using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Presupuesto
{
    public class pre_Presupuesto_Info
    {
        public decimal IdTransaccionSession { get; set; }

        public int IdEmpresa { get; set; }
        public decimal IdPresupuesto { get; set; }
        [Required(ErrorMessage = ("El campo sucursal es obligatorio"))]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = ("El campo periodo es obligatorio"))]
        public decimal IdPeriodo { get; set; }
        [Required(ErrorMessage = ("El campo grupo es obligatorio"))]
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public double MontoSolicitado { get; set; }
        [Required(ErrorMessage = ("El campo monto aprobado es obligatorio"))]
        public double MontoAprobado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = ("El campo motivo de anulación es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = ("El campo motivo de anulación debe tener mínimo 1 caracter máximo 500"))]
        public string MotivoAnulacion { get; set; }
        public string IdUsuarioAprobacion { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        [Required(ErrorMessage = ("El campo motivo de aprobación es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = ("El campo motivo de aprobación debe tener mínimo 1 caracter máximo 500"))]
        public string MotivoAprobacion { get; set; }
        [Required(ErrorMessage = ("El campo descripción de periodo es obligatorio"))]
        public string DescripcionPeriodo { get; set; }



        public List<pre_PresupuestoDet_Info> ListaPresupuestoDet { get; set; }
        public string Su_Descripcion { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public bool EstadoCierre { get; set; }      
    }
}
