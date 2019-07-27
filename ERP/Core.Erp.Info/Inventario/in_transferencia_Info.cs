using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Inventario;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Inventario
{
  public  class in_transferencia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursalOrigen { get; set; }
        public int IdBodegaOrigen { get; set; }
        public decimal IdTransferencia { get; set; }
        public string Codigo { get; set; }
        public int IdSucursalDest { get; set; }
        public int IdBodegaDest { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = ("El campo descripción debe tener mínimo 1 caracter máximo 500"))]
        public string tr_Observacion { get; set; }
        public System.DateTime tr_fecha { get; set; }
        public Nullable<int> IdEmpresa_Ing_Egr_Inven_Origen { get; set; }
        public Nullable<int> IdSucursal_Ing_Egr_Inven_Origen { get; set; }
        public Nullable<int> IdMovi_inven_tipo_SucuOrig { get; set; }
        public Nullable<decimal> IdNumMovi_Ing_Egr_Inven_Origen { get; set; }
        public Nullable<int> IdEmpresa_Ing_Egr_Inven_Destino { get; set; }
        public Nullable<int> IdSucursal_Ing_Egr_Inven_Destino { get; set; }
        public Nullable<int> IdMovi_inven_tipo_SucuDest { get; set; }
        public Nullable<decimal> IdNumMovi_Ing_Egr_Inven_Destino { get; set; }
        public string IdUsuario { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string tr_userAnulo { get; set; }
        public Nullable<System.DateTime> tr_fechaAnulacion { get; set; }
        public Nullable<System.DateTime> tr_fecha_transaccion { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string motivo_anula { get; set; }
        public string IdEstadoAprobacion_cat { get; set; }
        
        public string SucuOrigen { get; set; }
        public string BodegaORIG { get; set; }
        public string SucuDEST { get; set; }
        public string BodegDest { get; set; }
        public int[] IntArray { get; set; }

        public List<in_transferencia_det_Info> list_detalle { get; set; }
        public in_Ing_Egr_Inven_Info info_ingreso { get; set; }
        public in_Ing_Egr_Inven_Info info_egreso { get; set; }
        public in_movi_inven_tipo_Info info_movimiento { get; set; }

        

        public in_transferencia_Info()
        {
            list_detalle = new List<in_transferencia_det_Info>();
            info_ingreso = new in_Ing_Egr_Inven_Info();
            info_egreso = new in_Ing_Egr_Inven_Info();
            info_movimiento = new in_movi_inven_tipo_Info();
        }

        #region Campos Auditoria Inventario
        public System.DateTime fecha_correccion_transferencia { get; set; }
        public System.DateTime fecha_recosteo_sucursal { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public System.DateTime fecha_inicio_mov { get; set; }
        public System.DateTime fecha_fin_mov { get; set; }
        public string TipoMovimiento { get; set; }
        #endregion
    }
}
