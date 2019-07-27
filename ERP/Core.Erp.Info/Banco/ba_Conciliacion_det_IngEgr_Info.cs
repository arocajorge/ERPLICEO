using System;

namespace Core.Erp.Info.Banco
{
    public class ba_Conciliacion_det_IngEgr_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public string tipo_IngEgr { get; set; }
        public decimal IdCbteCble { get; set; }
        public int IdTipocbte { get; set; }
        public int SecuenciaCbteCble { get; set; }
        public bool seleccionado { get; set; }
        public string Estado { get; set; }
        
        #region Campos que no existen en la tabla
        public string IdPK { get; set; }
        public string IngEgr { get; set; }
        #endregion
        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public string IdUsuario_Anu { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string ip { get; set; }
        public string nom_pc { get; set; }
        public string MotivoAnulacion { get; set; }
        public string tc_TipoCbte { get; set; }
        public string cb_Cheque { get; set; }
        public string cb_Observacion { get; set; }
        public string ba_descripcion { get; set; }
        public double dc_Valor { get; set; }
        public string IdCtaCble { get; set; }
        public int IdBanco { get; set; }
        public DateTime cb_Fecha { get; set; }
        #endregion

    }
}
