namespace Core.Erp.Info.Facturacion
{
    public class fa_cuotas_x_doc_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public int secuencia { get; set; }
        public int num_cuota { get; set; }
        public System.DateTime fecha_vcto_cuota { get; set; }
        public double valor_a_cobrar { get; set; }
        public bool Estado { get; set; }
    }
}
