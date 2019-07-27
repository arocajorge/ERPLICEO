using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Producto_Composicion_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdProductoPadre { get; set; }
        [Required(ErrorMessage = "El campo producto es obligatorio")]
        [Range(1, 9999999, ErrorMessage = "El campo producto es obligatorio")]
        public decimal IdProductoHijo { get; set; }
        public string IdUnidadMedida { get; set; }
        [Required(ErrorMessage = "El campo cantidad es obligatorio")]

        public double Cantidad { get; set; }

        //Campos que no existen en la tabla
        public int secuencia { get; set; }
        public string pr_descripcion { get; set; }
        public string ca_Categoria { get; set; }
        public DateTime? lote_fecha_fab { get; set; }
        public DateTime? lote_fecha_vcto { get; set; }
        public string nom_presentacion { get; set; }
        public string lote_num_lote { get; set; }
        public string tp_ManejaInven { get; set; }
        public bool? se_distribuye { get; set; }
    }
}
