using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_EmpleadoAnticipoBeneficio_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAnticipo { get; set; }
        [Required(ErrorMessage ="El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo fecha desde es obligatorio")]
        public System.DateTime FechaDesde { get; set; }
        [Required(ErrorMessage = "El campo fecha hasta es obligatorio")]
        public System.DateTime FechaHasta { get; set; }
        [Required(ErrorMessage = "El campo rubro es obligatorio")]
        public string IdRubro { get; set; }
        [Required(ErrorMessage = "El campo valor es obligatorio")]
        public double Valor { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string MotivoAnulacion { get; set; }

        #region Propiedades que no existen en la tabla
        public string pe_nombreCompleto { get; set; }
        public string ru_descripcion { get; set; }
        public bool Estado { get; set; }
        #endregion
    }
}
