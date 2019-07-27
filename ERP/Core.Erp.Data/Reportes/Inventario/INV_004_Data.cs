using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
   public class INV_004_Data
    {
        public List<INV_004_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, int IdMarca)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdBodegaIni = IdBodega;
                int IdBodegaFin = IdBodega == 0 ? 9999 : IdBodega;

                decimal IdProductoIni = IdProducto;
                decimal IdProductoFin = IdProducto == 0 ? 9999 : IdProducto;

                int IdMarcaIni = IdMarca;
                int IdMarcaFin = IdMarca == 0 ? 9999 : IdMarca;
                List<INV_004_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPINV_004(IdEmpresa, IdSucursalIni, IdSucursalFin, IdBodegaIni, IdBodegaFin, IdProductoIni, IdProductoFin, IdMarcaIni, IdMarcaFin)
                             select new INV_004_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdProducto = q.IdProducto,
                                 IdMarca = q.IdMarca,
                                 NomBodega = q.NomBodega,
                                 NomMarca = q.NomMarca,
                                 NomPresentacion =q.NomPresentacion,
                                 NomProducto = q.NomProducto,
                                 NomSucursal = q.NomSucursal,
                                 NomTipo = q.NomTipo,
                                 StockActual = q.StockActual,
                                 Stock_minimo = q.Stock_minimo
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
