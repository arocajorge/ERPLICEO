using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
    public class INV_007_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursalOrigen { get; set; }
        public int IdBodegaOrigen { get; set; }
        public decimal IdTransferencia { get; set; }
        public int dt_secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_codigo { get; set; }
        public string pr_descripcion { get; set; }
        public double dt_cantidad { get; set; }
        public string IdUnidadMedida { get; set; }
        public string nom_unidad_medida { get; set; }
        public string cod_sucursal_origen { get; set; }
        public string nom_sucursal_origen { get; set; }
        public string cod_bodega_origen { get; set; }
        public string nom_bodega_origen { get; set; }
        public string cod_sucursal_destino { get; set; }
        public string nom_sucursal_destino { get; set; }
        public string cod_bodega_destino { get; set; }
        public string nom_bodega_destino { get; set; }
        public System.DateTime tr_fecha { get; set; }
        public string tr_Observacion { get; set; }
        public string Estado { get; set; }
        public string Codigo { get; set; }
    }
}
