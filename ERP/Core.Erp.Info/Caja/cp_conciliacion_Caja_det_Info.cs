using System;

namespace Core.Erp.Info.Caja
{
    public class cp_conciliacion_Caja_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_OGiro { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public Nullable<int> IdTipoMovi { get; set; }
        public string IdUsuario { get; set; }
        public decimal Valor_a_aplicar { get; set; }
        public string Tipo_documento { get; set; }
        public Nullable<int> IdEmpresa_OP { get; set; }
        public Nullable<decimal> IdOrdenPago_OP { get; set; }

        #region Campos que no existen en la tabla
        public decimal IdPersona { get; set; }
        public decimal idEntidad { get; set; }
        public string pe_nombreCompleto { get; set; }
        public int? idSucursal { get; set; }
        public double por_iva { get; set; }
        public string IdCtaCble_cxp { get; set; }
        public string Observacion { get; set; }
        public string co_factura { get; set; }
        public double co_baseImponible { get; set; }
        public System.DateTime co_FechaFactura { get; set; }
        public double co_valoriva { get; set; }
        public double co_total { get; set; }
        public double co_valorpagar { get; set; }
        public Nullable<double> Saldo_OG { get; set; }
        #endregion

    }
}
