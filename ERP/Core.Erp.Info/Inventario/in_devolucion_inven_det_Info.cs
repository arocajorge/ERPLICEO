using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_devolucion_inven_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdDev_Inven { get; set; }
        public int secuencia { get; set; }
        public int inv_IdEmpresa { get; set; }
        public int inv_IdSucursal { get; set; }
        public int inv_IdMovi_inven_tipo { get; set; }
        public decimal inv_IdNumMovi { get; set; }
        public int inv_Secuencia { get; set; }
        public double cant_devuelta { get; set; }
        #region Campos que no existen en la tabla
        public string IdUnidadMedida { get; set; }
        public double mv_costo { get; set; }
        public decimal IdProducto { get; set; }
        public int IdBodega { get; set; }
        public string NomUnidad { get; set; }
        public string lote_num_lote { get; set; }
        public string pr_descripcion { get; set; }
        public DateTime? lote_fecha_vcto { get; set; }
        public double dm_cantidad { get; set; }
        public string nom_presentacion { get; set; }
        #endregion
    }
}
