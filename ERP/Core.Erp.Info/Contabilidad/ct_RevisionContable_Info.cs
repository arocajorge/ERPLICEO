using System;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_RevisionContableFacturas_Info
    {
        public decimal IdSecuencia { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public int ct_IdEmpresa { get; set; }
        public int ct_IdTipoCbte { get; set; }
        public decimal ct_IdCbteCble { get; set; }
        public string Nombres { get; set; }
        public string Referencia { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public decimal TotalModulo { get; set; }
        public double TotalContabilidad { get; set; }
        public Nullable<double> Diferencia { get; set; }
    }
}
