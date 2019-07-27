using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_ConsignacionDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConsignacion { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "El campo producto es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo producto es obligatorio")]
        public decimal IdProducto { get; set; }
        [Required(ErrorMessage = "El campo unidad de medida es obligatorio")]
        public string IdUnidadMedida { get; set; }
        [Required(ErrorMessage = "El campo cantidad es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "El campo cantidad es obligatorio")]
        public double Cantidad { get; set; }
        [Required(ErrorMessage = "El campo costo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo cantidad es obligatorio")]
        public double Costo { get; set; }
        public string Observacion { get; set; }

        #region Campos de la vista
        public string pr_descripcion { get; set; }
        #endregion
    }
}
