using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_CentroCostoNivel_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = ("el campo nivel es obligatorio"))]
        [Range(1, int.MaxValue, ErrorMessage = "El campo nivel es obligatorio")]
        public int IdNivel { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El campo nivel es obligatorio")]
        [Required(ErrorMessage = ("el campo numero de digitos es obligatorio"))]
        public int nv_NumDigitos { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        public string nv_Descripcion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
