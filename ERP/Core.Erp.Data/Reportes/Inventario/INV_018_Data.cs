using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
  public class INV_018_Data
    {
        public List<INV_018_Info> GetList(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                List<INV_018_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWINV_018.Where(q => q.IdEmpresa == IdEmpresa && q.IdAjuste == IdAjuste).Select(q => new INV_018_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAjuste = q.IdAjuste,
                        Ajuste = q.Ajuste,
                        bo_Descripcion = q.bo_Descripcion,
                        ca_Categoria = q.ca_Categoria,
                        Costo = q.Costo,
                        Estado = q.Estado,
                        Fecha = q.Fecha,
                        IdBodega = q.IdBodega,
                        IdCatalogo_Estado = q.IdCatalogo_Estado,
                        IdMovi_inven_tipo_egr = q.IdMovi_inven_tipo_egr,
                        IdMovi_inven_tipo_ing = q.IdMovi_inven_tipo_ing,
                        IdNumMovi_egr = q.IdNumMovi_egr,
                        IdNumMovi_ing = q.IdNumMovi_ing,
                        IdProducto = q.IdProducto,
                        IdSucursal = q.IdSucursal,
                        IdUnidadMedida = q.IdUnidadMedida,
                        NomUnidadMedida = q.NomUnidadMedida,
                        nom_linea = q.nom_linea,
                        Observacion = q.Observacion,
                        pr_descripcion = q.pr_descripcion,
                        Secuencia = q.Secuencia,
                        StockFisico = q.StockFisico,
                        StockSistema = q.StockSistema,
                        Su_Descripcion = q.Su_Descripcion,
                        tm_descripcion_egr = q.tm_descripcion_egr,
                        tm_descripcion_ing = q.tm_descripcion_ing,
                        Total = q.Total,
                        NombreEstado = q.NombreEstado

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
