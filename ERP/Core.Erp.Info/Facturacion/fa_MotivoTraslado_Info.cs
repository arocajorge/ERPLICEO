using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_MotivoTraslado_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMotivoTraslado { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string tr_Descripcion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string MotivoAnulacion { get; set; }
    }
}
