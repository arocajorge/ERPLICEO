using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_010_Bus
    {
        INV_010_Data odata = new INV_010_Data();
        public List<INV_010_Info> get_list(int IdEmpresa, decimal IdProducto, string IdCategoria, int IdLinea, int IdGrupo, int IdSubGrupo, int IdMarca, string IdUsuario, DateTime fechaIni, DateTime fechaFin, bool mostrarSinMovimiento)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdProducto, IdCategoria, IdLinea, IdGrupo, IdSubGrupo, IdMarca, IdUsuario, fechaIni, fechaFin, mostrarSinMovimiento);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
