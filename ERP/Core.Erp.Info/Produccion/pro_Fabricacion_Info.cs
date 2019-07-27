using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Produccion
{
    public class pro_Fabricacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdFabricacion { get; set; }
        [Required(ErrorMessage = ("el campo sucursal de egreso es obligatorio"))]
        public int egr_IdSucursal { get; set; }
        [Required(ErrorMessage = ("el campo bodega de egreso es obligatorio"))]

        public int egr_IdBodega { get; set; }
        public Nullable<int> egr_IdMovi_inven_tipo { get; set; }
        public Nullable<decimal> egr_IdNumMovi { get; set; }
        [Required(ErrorMessage = ("el campo sucursal de ingreso es obligatorio"))]
        public int ing_IdSucursal { get; set; }
        [Required(ErrorMessage = ("el campo bodega de ingreso es obligatorio"))]
        public int ing_IdBodega { get; set; }
        public Nullable<int> ing_IdMovi_inven_tipo { get; set; }
        public Nullable<decimal> ing_IdNumMovi { get; set; }
        [Required(ErrorMessage = ("el campo fecha es obligatorio"))]
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        //campos que no existen en la tabla
        public string pr_descripcion { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public List<pro_FabricacionDet_Info> LstDet { get; set; }
        public pro_FabricacionDet_Info info_det { get; set; }
        public bool Cerrar { get; set; }
        public string Su_Descripcion { get; set; }


    }
}
