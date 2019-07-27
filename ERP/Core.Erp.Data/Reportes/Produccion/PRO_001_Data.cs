using Core.Erp.Info.Reportes.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Produccion
{
   public class PRO_001_Data
    {
        public List<PRO_001_Info> GetList(int IdEmpresa, decimal IdFabricacion)
        {
            try
            {
                List<PRO_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWPRO_001.Where(q => q.IdEmpresa == IdEmpresa && q.IdFabricacion == IdFabricacion).Select(q => new PRO_001_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdFabricacion = q.IdFabricacion,
                        Cantidad = q.Cantidad,
                        Costo = q.Costo,
                        egr_IdNumMovi = q.egr_IdNumMovi,
                        eg_bo_Descripcion = q.eg_bo_Descripcion,
                        eg_NombreTipo = q.eg_NombreTipo,
                        eg_Su_Descripcion = q.eg_Su_Descripcion,
                        Estado = q.Estado,
                        Fecha = q.Fecha,
                        IdProducto = q.IdProducto,
                        IdUnidadMedida = q.IdUnidadMedida,
                        ing_IdNumMovi = q.ing_IdNumMovi,
                        in_bo_Descripcion = q.in_bo_Descripcion,
                        in_NombreTipo = q.in_NombreTipo,
                        in_Su_Descripcion = q.in_Su_Descripcion,
                        NombreUnidad = q.NombreUnidad,
                        Observacion = q.Observacion,
                        pr_descripcion = q.pr_descripcion,
                        RealizaMovimiento = q.RealizaMovimiento,
                        Secuencia = q.Secuencia,
                        Signo = q.Signo,
                        Movimiento = q.Movimiento,
                        Orden = q.Orden,
                        Tipo = q.Tipo
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
