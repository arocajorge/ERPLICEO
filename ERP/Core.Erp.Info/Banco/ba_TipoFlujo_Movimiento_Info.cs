using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_TipoFlujo_Movimiento_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMovimiento { get; set; }
        [Required(ErrorMessage = ("El campo tipo de flujo es obligatorio"))]
        public decimal IdTipoFlujo { get; set; }
        [Required(ErrorMessage = ("El campo sucursal es obligatorio"))]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = ("El campo banco es obligatorio"))]
        public int IdBanco { get; set; }
        [Required(ErrorMessage = ("El campo valor es obligatorio"))]
        public double Valor { get; set; }
        [Required(ErrorMessage = ("El campo fecha es obligatorio"))]
        public System.DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = ("El campo motivo de anulación es obligatorio"))]
        public string MotivoAnulacion { get; set; }

        #region Campos que no estan en la tabla
        public string Descricion { get; set; }
        public string Su_Descripcion { get; set; }
        public string ba_descripcion { get; set; }
        #endregion
    }
}
