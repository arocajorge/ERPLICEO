using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Ajuste_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdAjuste { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public Nullable<int> IdMovi_inven_tipo_ing { get; set; }
        public Nullable<int> IdMovi_inven_tipo_egr { get; set; }
        public Nullable<decimal> IdNumMovi_ing { get; set; }
        public Nullable<decimal> IdNumMovi_egr { get; set; }
        public string IdCatalogo_Estado { get; set; }
        public bool Estado { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }

        #region Campos que no estan en la tabla
        public string NombreEstado { get; set; }
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public List<in_AjusteDet_Info> lst_detalle { get; set; }
        public List<in_AjusteDet_Info> lst_movimiento { get; set; }
        #endregion
    }
}
