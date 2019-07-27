using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Caja
{
    public class cp_conciliacion_Caja_det_x_ValeCaja_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_movcaja { get; set; }
        public decimal IdCbteCble_movcaja { get; set; }
        public int IdTipocbte_movcaja { get; set; }
        public string IdCtaCble { get; set; }
        public Nullable<int> IdPunto_cargo { get; set; }
        public Nullable<int> IdPunto_cargo_grupo { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCentroCosto_sub_centro_costo { get; set; }

        #region Campos que no existen en la tabla
        [Required(ErrorMessage ="El campo fecha es obligatorio")]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage ="El campo valor es obligatorio")]
        [Range(0.01, 9999999.99, ErrorMessage = "El valor debe ser mayor a 0")]
        public double valor { get; set; }
        [Required(ErrorMessage = "El campo tipo de movimiento es obligatorio")]
        public int? idTipoMovi { get; set; }
        [Range(1,99999999,ErrorMessage = "Seleccione una persona")]
        public decimal IdPersona { get; set; }
        public string Observacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public bool se_modifico { get; set; }

        public string tm_descripcion { get; set; }
        public string IdTipo_Persona { get; set; }
        public decimal IdEntidad { get; set; }
        #endregion

    }
}
