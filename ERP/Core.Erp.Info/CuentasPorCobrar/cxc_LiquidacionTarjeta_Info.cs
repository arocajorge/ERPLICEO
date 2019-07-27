using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_Info
    {
        

        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public string Lote { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo cuenta bancaria es obligatorio")]
        public int IdBanco { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public double Valor { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte_ct { get; set; }
        public Nullable<decimal> IdCbteCble_ct { get; set; }

        #region Campos auditoria
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }
        #endregion

        #region Campos que no existen en la tabla
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> ListaCobros { get; set; }
        public List<cxc_LiquidacionTarjetaDet_Info> ListaDet { get; set; }
        public string Su_Descripcion { get; set; }
        public string ba_descripcion { get; set; }
        #endregion
    }
}
