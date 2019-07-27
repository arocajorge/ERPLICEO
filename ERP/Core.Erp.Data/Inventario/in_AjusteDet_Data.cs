using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_AjusteDet_Data
    {
        public List<in_AjusteDet_Info> GetList(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                List<in_AjusteDet_Info> Lista = new List<in_AjusteDet_Info>();

                using (Entities_inventario db = new Entities_inventario())
                {
                    Lista = db.in_AjusteDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdAjuste == IdAjuste).Select(q => new in_AjusteDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAjuste = q.IdAjuste,
                        Secuencia = q.Secuencia,
                        IdProducto = q.IdProducto,
                        IdUnidadMedida = q.IdUnidadMedida,
                        StockSistema = q.StockSistema,
                        StockFisico = q.StockFisico,
                        Ajuste = q.Ajuste,
                        Costo = q.Costo
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<in_AjusteDet_Info> get_list_cargar_detalle(int IdEmpresa, int IdSucursal, int IdBodega, DateTime Fecha)
        {
            try
            {
                List<in_AjusteDet_Info> Lista = new List<in_AjusteDet_Info>();

                using (Entities_inventario contex = new Entities_inventario())
                {
                    contex.SetCommandTimeOut(5000);
                    int secuencia = 1;
                    Lista = (contex.SPINV_GetStock(IdEmpresa, IdSucursal, IdBodega, Fecha)).Select(q => new in_AjusteDet_Info
                    {
                        Secuencia = secuencia++,
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdBodega = q.IdBodega,
                        IdProducto = q.IdProducto,
                        IdUnidadMedida = q.IdUnidadMedida,
                        Stock = q.Stock,
                        UltimoCosto = q.UltimoCosto,
                        IdFecha = q.IdFecha,
                        pr_descripcion = q.pr_descripcion,
                        pr_codigo = q.pr_codigo

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
