namespace Core.Erp.Info.Facturacion
{
    public class fa_factura_resumen_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public decimal SubtotalIVASinDscto { get; set; }
        public decimal SubtotalSinIVASinDscto { get; set; }
        public decimal SubtotalSinDscto { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubtotalIVAConDscto { get; set; }
        public decimal SubtotalSinIVAConDscto { get; set; }
        public decimal SubtotalConDscto { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
        public decimal ValorEfectivo { get; set; }
        public decimal Cambio { get; set; }
        public string mensaje { get; set; }
    }
}
