using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Presupuesto
{
    public class pre_Periodo_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPeriodo { get; set; }
        [Required(ErrorMessage = ("El campo descripción periodo es obligatorio"))]
        public string DescripcionPeriodo { get; set; }
        public string Observacion { get; set; }
        [Required(ErrorMessage = ("El campo fecha de inicio es obligatorio"))]
        public System.DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = ("El campo fecha de fin es obligatorio"))]
        public System.DateTime FechaFin { get; set; }
        public bool EstadoCierre { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = ("El campo motivo de anulación es obligatorio"))]
        [StringLength(150, MinimumLength = 1, ErrorMessage = ("El campo motivo de anulación debe tener mínimo 1 caracter máximo 500"))]
        public string MotivoAnulacion { get; set; }



        public string Periodo { get; set; }
    }
}
