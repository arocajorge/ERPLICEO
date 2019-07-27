using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_bodega_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }        
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 50 caracteres")]
        public string cod_bodega { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 200")]
        public string bo_Descripcion { get; set; }
        [StringLength(3, MinimumLength = 0, ErrorMessage = "el campo código punto de emisión debe tener máximo 3 caracteres")]
        public string cod_punto_emision { get; set; }
        public string bo_manejaFacturacion { get; set; }
        public string bo_EsBodega { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdEstadoAproba_x_Ing_Egr_Inven { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCtaCtble_Inve { get; set; }
        public string IdCtaCtble_Costo { get; set; }

        #region Campos que no existen en la tabla
        public string IdString { get; set; }
        public bool bo_manejaFacturacion_bool { get; set; }
        public bool bo_EsBodega_bool { get; set; }
        #endregion

    }
}
