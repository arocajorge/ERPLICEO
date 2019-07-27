using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_proforma_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public decimal IdProforma { get; set; }
        [Required(ErrorMessage = "El campo ciente es obligatorio")]
        public decimal IdCliente { get; set; }
        [Required(ErrorMessage = "El campo termino pago es obligatorio")]
        public string IdTerminoPago { get; set; }
        [Required(ErrorMessage = "El campo plazo es obligatorio")]
        public int pf_plazo { get; set; }
        public string pf_codigo { get; set; }
        public string pf_observacion { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime pf_fecha { get; set; }
        [Required(ErrorMessage = "El campo fecha vencimiento es obligatorio")]
        public System.DateTime pf_fecha_vcto { get; set; }
        public bool estado { get; set; }
        [Required(ErrorMessage = "El campo bodega es obligatorio")]
        public int IdBodega { get; set; }
        [Required(ErrorMessage = "El campo vendedor es obligatorio")]
        public int IdVendedor { get; set; }
        [Required(ErrorMessage = "El campo días de entrega es obligatorio")]
        public int pr_dias_entrega { get; set; }
        public int IdNivel { get; set; }
        public string IdCatalogo_FormaPago { get; set; }

        #region Campos de auditoria
        public string IdUsuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public string IdUsuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public string IdUsuario_anulacion { get; set; }
        public Nullable<System.DateTime> fecha_anulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }
        #endregion

        #region Campos que no existen en la tabla
        public decimal IdEntidad { get; set; }
        public List<fa_proforma_det_Info> lst_det { get; set; }
        public string pe_nombreCompleto { get; set; }
        public double pd_total { get; set; }
        public double pd_iva { get; set; }
        public double pd_subtotal { get; set; }
        public decimal IdProductoSeleccionado { get; set; }
        public string EstadoCierre { get; set; }
        public string Su_Descripcion { get; set; }
        #endregion

        #region Campos de desbloqueo
        public string ContraseniaAdministrador { get; set; }
        #endregion

    }
}
