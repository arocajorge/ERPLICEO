using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_orden_giro_det_ing_x_oc_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public int Secuencia { get; set; }
        public int inv_IdSucursal { get; set; }
        public int inv_IdMovi_inven_tipo { get; set; }
        public double do_precioCompra { get; set; }
        public decimal inv_IdNumMovi { get; set; }
        public int inv_Secuencia { get; set; }
        public Nullable<int> oc_IdSucursal { get; set; }
        public Nullable<decimal> oc_IdOrdenCompra { get; set; }
        public Nullable<int> oc_Secuencia { get; set; }
        public string IdCtaCble { get; set; }
        public double dm_cantidad { get; set; }
        public double do_porc_des { get; set; }
        public double do_descuento { get; set; }
        public double do_precioFinal { get; set; }
        public double do_subtotal { get; set; }
        public string IdCod_Impuesto { get; set; }
        public double do_iva { get; set; }
        public double Por_Iva { get; set; }
        public double do_total { get; set; }
        public string IdUnidadMedida { get; set; }
        public decimal IdProducto { get; set; }

        #region Campos que no existen en la tabla
        public string IdGenerado { get; set; }
        public string pr_descripcion { get; set; }
        public string NomUnidadMedida { get; set; }
        public decimal IdProveedor { get; set; }
        public string pc_Cuenta { get; set; }
        public string IdCtaCble_oc { get; set; }
        #endregion
    }
}
