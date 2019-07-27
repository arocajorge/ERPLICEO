using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_016_Bus
    {
        INV_016_Data odata = new INV_016_Data();
        public List<INV_016_Info> GetList(int IdEmpresa, int IdSucursal, int IdCategoria, int IdLinea, int IdGrupo, int IdSubGrupo, DateTime fecha_ini, DateTime fecha_fin, bool noMostrarSinVenta, string IdUsuario)
        {
            try
            {
                return odata.GetList(IdEmpresa,  IdSucursal,  IdCategoria,  IdLinea,  IdGrupo,  IdSubGrupo,  fecha_ini,  fecha_fin,  noMostrarSinVenta,  IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
