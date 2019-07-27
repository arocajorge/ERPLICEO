using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Importacion
{
   public class imp_orden_compra_ext_recepcion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdRecepcion { get; set; }
        public System.DateTime or_fecha { get; set; }
        public string or_observacion { get; set; }
        public Nullable<int> IdEmpresa_oc { get; set; }
        public decimal IdOrdenCompraExt { get; set; }
        public List<imp_ordencompra_ext_det_Info> lst_detalle { get; set; }
        public bool estado { get; set; }
        public string IdUsuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public string IdUsuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public string IdUsuario_anulacion { get; set; }
        public Nullable<System.DateTime> fecha_anulacion { get; set; }


        #region MyRegion
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public int IdCatalogo_via { get; set; }
        public System.DateTime oe_fecha { get; set; }
        public decimal IdProveedor { get; set; }
        public Nullable<System.DateTime> oe_fecha_llegada { get; set; }
        public Nullable<System.DateTime> oe_fecha_embarque { get; set; }
        public string oe_observacion { get; set; }
        #endregion

    }
}
