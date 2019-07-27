using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorCobrar
{
    public class cxc_cobro_det_Info
    {
        public string Observacion { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public int secuencial { get; set; }
        public string dc_TipoDocumento { get; set; }
        public Nullable<int> IdBodega_Cbte { get; set; }
        public decimal IdCbte_vta_nota { get; set; }
        public double dc_ValorPago { get; set; }
        public string estado { get; set; }
        public string IdCobro_tipo_det { get; set; }

        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        #endregion
                
        #region Campos que no existen en la tabla
        public string secuencia { get; set; }
        public DateTime? vt_fecha { get; set; }
        public double? vt_total { get; set; }
        public string vt_NumDocumento { get; set; }
        public double? Saldo { get; set; }
        public double Saldo_final { get; set; }
        public double? vt_Subtotal { get; set; }
        public double? vt_iva { get; set; }
        public DateTime? vt_fech_venc { get; set; }
        public double dc_ValorRetFu { get; set; }
        public double dc_ValorRetIva { get; set; }
        public string ESRetenIVA { get; set; }
        public string ESRetenFTE { get; set; }
        public string tc_descripcion { get; set; }
        public double PorcentajeRet { get; set; }
        public string cr_NumDocumento { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public string IdCtaCble { get; set; }
        public string NomCliente { get; set; }
        #endregion

    }
}
