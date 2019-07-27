using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Compras
{
    public class com_ordencompra_local_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public string IdInventarioOrdenCompra { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = ("el campo sucursal es obligatorio"))]
        public int IdSucursal { get; set; }
        public decimal IdOrdenCompra { get; set; }
        public int SecuenciaTipo { get; set; }
        public string Tipo { get; set; }
        [Required(ErrorMessage = ("el campo proveedor es obligatorio"))]
        [Range(1,int.MaxValue,ErrorMessage ="El campo proveedor es obligatorio")]
        public decimal IdProveedor { get; set; }
        public string oc_NumDocumento { get; set; }
        [Required(ErrorMessage = ("el campo termino pago es obligatorio"))]
        public int IdTerminoPago { get; set; }
        [Required(ErrorMessage = ("el campo plazo es obligatorio"))]
        public int oc_plazo { get; set; }
        public System.DateTime oc_fecha { get; set; }
        public string oc_observacion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = ("el campo estado de aprobación es obligatorio"))]
        public string IdEstadoAprobacion_cat { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaHoraAnul { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public string MotivoReprobacion { get; set; }
        public decimal IdDepartamento { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime oc_fechaVencimiento { get; set; }
        public string IdEstado_cierre { get; set; }
        [Required(ErrorMessage = ("el campo comprador es obligatorio"))]
        public decimal IdComprador { get; set; }

        //campos que no existen en la tabla
        public string pr_codigo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Su_Descripcion { get; set; }
        public string IdUsuarioAprobacion { get; set; }
        public string MotivoAprobacion { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        public List<com_ordencompra_local_det_Info> lst_det { get; set; }
        public Nullable<double> Total { get; set; }
        public string TerminoPago { get; set; }

        #region Campos de vwcom_ordencompra_local_x_ingresar
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public double do_Cantidad_vw { get; set; }
        public double do_precioFinal { get; set; }
        public int CantidadIngresada { get; set; }
        public double Saldo { get; set; }
        public string IdUnidadMedida { get; set; }
        #endregion
    }
    public class com_orden_aprobacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public System.DateTime fecha_ini { get; set; }
        public System.DateTime fecha_fin { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioAprobacion { get; set; }
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "La observación debe tener máximo 500 caracteres")]
        public string MotivoAprobacion { get; set; }
        public string IdEstadoAprobacion_cat { get; set; }
        public DateTime FechaAprobacion { get; set; }

        public com_orden_aprobacion_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
    }


}
