using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_015_Bus
    {
        INV_015_Data odata = new INV_015_Data();
        public List<INV_015_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, int IdCategoria, int IdLinea, int IdGrupo, int IdSubGrupo,DateTime fecha_ini,DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa,  IdSucursal,  IdBodega,  IdProducto,  IdCategoria,  IdLinea,  IdGrupo, IdSubGrupo, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
