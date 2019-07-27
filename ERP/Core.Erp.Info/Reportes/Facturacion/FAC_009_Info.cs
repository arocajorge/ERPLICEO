using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_009_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdGuiaRemision { get; set; }
        public string vt_tipoDoc { get; set; }
        public string vt_NumFactura { get; set; }
        public string vt_autorizacion { get; set; }
        public System.DateTime gi_FechaInicioTraslado { get; set; }
        public System.DateTime gi_FechaFinTraslado { get; set; }
        public string MotivoTraslado { get; set; }
        public string Direccion_Origen { get; set; }
        public string Direccion_Destino { get; set; }
        public string CedulaCliente { get; set; }
        public string NomCliente { get; set; }
        public string CedulaTransportista { get; set; }
        public string NombreTransportista { get; set; }
        public string placa { get; set; }
        public string RucEmpresa { get; set; }
        public double gi_cantidad { get; set; }
        public string pr_descripcion { get; set; }
    }
}
