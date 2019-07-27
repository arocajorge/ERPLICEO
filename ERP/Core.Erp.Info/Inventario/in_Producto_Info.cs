using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Producto_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdProducto { get; set; }
        [StringLength(80,MinimumLength =0, ErrorMessage = "El campo código debe tener máximo 80 caracteres")]
        public string pr_codigo { get; set; }
        [StringLength(80, MinimumLength = 0, ErrorMessage = "El campo código 2 debe tener máximo 80 caracteres")]
        public string pr_codigo2 { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "El campo descripción debe tener mínimo 1 caracter y máximo 1000")]
        public string pr_descripcion { get; set; }
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "El campo descripción 2 debe tener mínimo 1 caracter y máximo 1000")]
        public string pr_descripcion_2 { get; set; }
        [Required(ErrorMessage = "El campo tipo producto es obligatorio")]
        public int IdProductoTipo { get; set; }
        [Required(ErrorMessage = "El campo marca es obligatorio")]
        public int IdMarca { get; set; }
        [Required(ErrorMessage = "El campo presentación es obligatorio")]
        public string IdPresentacion { get; set; }
        [Required(ErrorMessage = "El campo categoría es obligatorio")]
        public string IdCategoria { get; set; }
        [Required(ErrorMessage = "El campo línea es obligatorio")]
        public int IdLinea { get; set; }
        [Required(ErrorMessage = "El campo grupo es obligatorio")]
        public int IdGrupo { get; set; }
        [Required(ErrorMessage = "El campo subgrupo es obligatorio")]
        public int IdSubGrupo { get; set; }
        [Required(ErrorMessage = "El campo unidad de medida para ingreso es obligatorio")]
        public string IdUnidadMedida { get; set; }
        [Required(ErrorMessage = "El campo unidad de medida para kárdex es obligatorio")]
        public string IdUnidadMedida_Consumo { get; set; }
        [StringLength(400, MinimumLength = 0, ErrorMessage = "El campo código de barra debe tener máximo 400 caracteres")]
        public string pr_codigo_barra { get; set; }
        [StringLength(2000, MinimumLength = 0, ErrorMessage = "El campo observación debe tener máximo 2000 caracteres")]
        public string pr_observacion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = "El campo impuesto IVA es obligatorio")]
        public string IdCod_Impuesto_Iva { get; set; }
        public bool Aparece_modu_Ventas { get; set; }
        public bool Aparece_modu_Compras { get; set; }
        public bool Aparece_modu_Inventario { get; set; }
        public bool Aparece_modu_Activo_F { get; set; }
        public Nullable<decimal> IdProducto_padre { get; set; }
        public Nullable<System.DateTime> lote_fecha_fab { get; set; }
        public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "El campo código de lote debe tener máximo 50 caracteres")]
        public string lote_num_lote { get; set; }
        public double precio_1 { get; set; }
        public bool se_distribuye { get; set; }
        public byte[] pr_logo { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El campo motivo anulación debe tener máximo 100 caracteres")]
        [Required(ErrorMessage ="El campo motivo anulación es obligatorio")]
        public string pr_motivoAnulacion { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public byte[] pr_imagen { get; set; }
        public string contrasena_admin { get; set; }

        #endregion

        #region Campos que no existen en la tabla
        public List<in_Producto_Composicion_Info> lst_producto_composicion { get; set; }
        public List<in_producto_x_tb_bodega_Info> lst_producto_x_bodega { get; set; }
        public List<in_Producto_x_fa_NivelDescuento_Info> list_producto_x_fa_NivelDescuento { get; set; }

        public string nom_presentacion { get; set; }
        public string nom_categoria { get; set; }

        public string tp_descripcion { get; set; }
        public string ca_descripcion { get; set; }
        public string ma_descripcion { get; set; }
        public string pr_descripcion_combo { get; set; }
        public double stock { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public string IdUsuarioAut { get; set; }
        public int OrdenVcto { get; set; }
        public string IdCtaCtble_Inve { get; set; }
        public string tp_ManejaInven { get; set; }
        #endregion
    }

    public class in_Producto_Stock_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public double Cantidad { get; set; }
        public string tp_manejaInven { get; set; }
        public double CantidadAnterior { get; set; }
        public bool SeDestribuye { get; set; }
    }
}
