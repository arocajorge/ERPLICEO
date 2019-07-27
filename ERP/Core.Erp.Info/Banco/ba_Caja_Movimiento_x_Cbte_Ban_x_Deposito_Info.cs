using System;

namespace Core.Erp.Info.Banco
{
    public class ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info
    {
        public int mcj_IdEmpresa { get; set; }
        public decimal mcj_IdCbteCble { get; set; }
        public int mcj_IdTipocbte { get; set; }
        public int mba_IdEmpresa { get; set; }
        public decimal mba_IdCbteCble { get; set; }
        public int mba_IdTipocbte { get; set; }
        public int mcj_Secuencia { get; set; }
        public string Observacion { get; set; }
        #region Campos que no existen en la tabla
        public string tc_descripcion { get; set; }
        public double cr_Valor { get; set; }
        public DateTime cm_fecha { get; set; }
        public string cm_observacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string cr_NumDocumento { get; set; }
        public string IdCtaCble { get; set; }
        public string ca_Descripcion { get; set; }
        #endregion

    }
}
