using Core.Erp.Info.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Produccion
{
    public class pro_FabricacionDet_Data
    {
        public List<pro_FabricacionDet_Info> GetList(int IdEmpresa, decimal IdFabricacion)
        {
            try
            {
                List<pro_FabricacionDet_Info> Lista;
                using (Entities_produccion Context = new Entities_produccion())
                {
                    Lista = Context.vwpro_FabricacionDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdFabricacion == IdFabricacion).Select(q => new pro_FabricacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdFabricacion = q.IdProducto,
                        IdProducto = q.IdProducto,
                        Cantidad = q.Cantidad,
                        Costo = q.Costo,
                        IdUnidadMedida = q.IdUnidadMedida,
                        RealizaMovimiento = q.RealizaMovimiento,
                        Secuencia = q.Secuencia,
                        Signo = q.Signo,
                        pr_descripcion = q.pr_descripcion,

                        CantidadAnterior = q.Cantidad,
                        tp_ManejaInven = q.tp_ManejaInven,
                        se_distribuye = q.se_distribuye ?? false
                        
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<pro_FabricacionDet_Info> GetProductoFacturadosPorFecha(int IdEmpresa, int IdSucursal, int IdBodega , DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<pro_FabricacionDet_Info> Lista;
                using (Entities_produccion Context = new Entities_produccion())
                {
                    Lista = (from q in Context.sppro_GetProductoFacturadosPorFecha(IdEmpresa, IdSucursal, IdBodega,  FechaIni,  FechaFin)
                             select new pro_FabricacionDet_Info
                             {
                                  IdEmpresa = q.IdEmpresa,
                                  IdProducto = q.IdProducto,
                                  pr_descripcion = q.pr_descripcion,
                                  Cantidad = q.vt_cantidad,
                                  vt_fecha = q.vt_fecha,
                                  NombreUnidad = q.NombreUnidad,
                                  CantidadFabricada = q.CantidadFabricada,
                                  stock = q.stock,
                                  IdUnidadMedida = q.IdUnidadMedida,
                                  Signo = "+"
                             }).ToList();
                    int Secuencia = 1;
                    Lista.ForEach(q => q.Secuencia = Secuencia++);
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
