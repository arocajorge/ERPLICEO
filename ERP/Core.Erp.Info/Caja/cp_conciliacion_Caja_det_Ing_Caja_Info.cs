using System;

namespace Core.Erp.Info.Caja
{
    public class cp_conciliacion_Caja_det_Ing_Caja_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int secuencia { get; set; }
        public int IdEmpresa_movcaj { get; set; }
        public decimal IdCbteCble_movcaj { get; set; }
        public int IdTipocbte_movcaj { get; set; }
        public double valor_aplicado { get; set; }
        public double valor_disponible { get; set; }
        #region Propiedades que no existen en la tabla
        public string cm_observacion { get; set; }
        public DateTime cm_fecha { get; set; }
        public double Total_movi { get; set; }
        public double Total_aplicado { get; set; }
        #endregion

    }
}
