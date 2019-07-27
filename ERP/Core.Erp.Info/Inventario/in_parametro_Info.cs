using System;

namespace Core.Erp.Info.Inventario
{
    public class in_parametro_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMovi_inven_tipo_egresoBodegaOrigen { get; set; }
        public int IdMovi_inven_tipo_ingresoBodegaDestino { get; set; }
        public int IdMovi_Inven_tipo_x_Dev_Inv_x_Ing { get; set; }
        public int IdMovi_Inven_tipo_x_Dev_Inv_x_Erg { get; set; }
        public string P_IdCtaCble_transitoria_transf_inven { get; set; }
        public int P_IdMovi_inven_tipo_default_ing { get; set; }
        public int P_IdMovi_inven_tipo_default_egr { get; set; }
        public int P_IdMovi_inven_tipo_ingreso_x_compra { get; set; }
        public int P_Dias_menores_alerta_desde_fecha_actual_rojo { get; set; }
        public int P_Dias_menores_alerta_desde_fecha_actual_amarillo { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public int IdMovi_inven_tipo_Cambio { get; set; }
        public int IdMovi_inven_tipo_Consignacion { get; set; }
        public int IdMovi_inven_tipo_elaboracion_egr { get; set; }
        public int IdMovi_inven_tipo_elaboracion_ing { get; set; }
        public Nullable<int> IdMotivo_Inv_elaboracion_ing { get; set; }
        public Nullable<int> IdMotivo_Inv_elaboracion_egr { get; set; }
        public Nullable<int> IdMovi_inven_tipo_ajuste_ing { get; set; }
        public Nullable<int> IdMovi_inven_tipo_ajuste_egr { get; set; }
        public string IdCatalogoEstadoAjuste { get; set; }
        public Nullable<int> IdMotivo_Inv_ajuste_ing { get; set; }
        public Nullable<int> IdMotivo_Inv_ajuste_egr { get; set; }
    }
}
