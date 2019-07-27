namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_005_resumen_Info
    {
        public double TotalVentasLocales { get; set; }
        public double TotalExportaciones { get; set; }
        public double SaldoVentasLocales { get; set; }
        public double SaldoExportaciones { get; set; }
        public double TotalPorSucursal { get; set; }
        public double SaldoTotalSucursal { get; set; }
        public int CantidadVentasLocales { get; set; }
        public int CantidadPorSucursal { get; set; }
        public int CantidadExportaciones { get; set; }
        public string NomSucursal { get; set; }
        public decimal BaseImponible12 { get; set; }
        public decimal BaseImponible0 { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal ValorIva { get; set; }
    }
}
