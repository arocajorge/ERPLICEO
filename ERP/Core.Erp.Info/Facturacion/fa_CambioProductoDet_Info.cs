using System;

namespace Core.Erp.Info.Facturacion
{
    public class fa_CambioProductoDet_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCambio { get; set; }
        public int Secuencia { get; set; }
        public decimal IdCbteVta { get; set; }
        public int SecuenciaFact { get; set; }
        public decimal IdProductoFact { get; set; }
        public decimal IdProductoCambio { get; set; }
        public double CantidadFact { get; set; }
        public double CantidadCambio { get; set; }

        #region Campos que no existen en la tabla
        public string pr_descripcionFact { get; set; }
        public string pr_descripcionCambio { get; set; }
        public string vt_NumFactura { get; set; }
        public DateTime vt_fecha { get; set; }
        public string IdSecuencial { get; set; }
        public string NombreCliente { get; set; }
        #endregion

    }
}
