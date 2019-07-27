using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Facturacion
{
    public class fa_CambioProducto_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage ="El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = "El campo bodega es obligatorio")]
        public int IdBodega { get; set; }
        public decimal IdCambio { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [StringLength(5000, ErrorMessage = "El campo motivo anulación debe contener máximo 5000 caracteres")]
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> IdMovi_inven_tipo { get; set; }
        public Nullable<decimal> IdNumMovi { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaTransac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> FechaUltAnu { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        [StringLength(5000,ErrorMessage ="El campo motivo anulación debe contener máximo 5000 caracteres")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public List<fa_CambioProductoDet_Info> LstDet { get; set; }
        public string bo_Descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<decimal> NumeroFactura { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public bool GenerarDevolucion { get; set; }
        #endregion
    }
}
