using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Facturacion
{
    public class fa_PuntoVta_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public int IdPuntoVta { get; set; }
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 50")]
        public string cod_PuntoVta { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 150")]
        public string nom_PuntoVta { get; set; }
        public bool estado { get; set; }
        [Required(ErrorMessage = "El campo bodega es obligatorio")]
        public int IdBodega { get; set; }
        public int IdCaja { get; set; }
        public string IPImpresora { get; set; }
        public Nullable<int> NumCopias { get; set; }
        public bool CobroAutomatico { get; set; }
        public string codDocumentoTipo { get; set; }
        public bool EsElectronico { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string Su_Descripcion { get; set; }
        public string Su_CodigoEstablecimiento { get; set; }
        public string Descripcion { get; set; }
        #endregion
    }
}
