using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_001_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public int secuencial { get; set; }
        public Nullable<int> IdBodega_Cbte { get; set; }
        public decimal IdCbte_vta_nota { get; set; }
        public string dc_TipoDocumento { get; set; }
        public double dc_ValorPago { get; set; }
        public string tc_descripcion { get; set; }
        public decimal? IdPersona { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string vt_NumFactura { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string ObservacionFact { get; set; }
        public string ObservacionCobro { get; set; }
        public System.DateTime? cr_fecha { get; set; }
        public string NombreContacto { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string cr_estado { get; set; }
        public string Su_Descripcion { get; set; }
        public string ba_descripcion { get; set; }
        public string cr_NumDocumento { get; set; }
        public double? cr_TotalCobro { get; set; }
    }
}
