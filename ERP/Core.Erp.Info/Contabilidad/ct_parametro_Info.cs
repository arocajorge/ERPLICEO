namespace Core.Erp.Info.Contabilidad
{
    public class ct_parametro_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte_SaldoInicial { get; set; }
        public int IdTipoCbte_AsientoCierre_Anual { get; set; }
        public bool P_Se_Muestra_Todas_las_ctas_en_combos { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }

    }
}
