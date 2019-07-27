using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_devolucion_inven_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdDev_Inven { get; set; }
        public string cod_Dev_Inven { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdEmpresa_inv { get; set; }
        public int IdSucursal_inv { get; set; }
        public int IdMovi_inven_tipo_inv { get; set; }
        public decimal IdNumMovi_inv { get; set; }
        public int dev_IdEmpresa { get; set; }
        public int dev_IdSucursal { get; set; }
        public int dev_IdMovi_inven_tipo { get; set; }
        public decimal dev_IdNumMovi { get; set; }
        public string dev_signo { get; set; }
        public string observacion { get; set; }
        public bool Estado { get; set; }
        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdusuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        #endregion

        #region Campos que no existen en la tabla
        public List<in_devolucion_inven_det_Info> lst_det { get; set; }
        public System.DateTime Fecha_ini { get; set; }
        public System.DateTime Fecha_fin { get; set; }
        #endregion

    }
}
