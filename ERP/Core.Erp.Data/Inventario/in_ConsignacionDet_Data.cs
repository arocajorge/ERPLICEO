using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_ConsignacionDet_Data
    {
        public List<in_ConsignacionDet_Info> GetList(int IdEmpresa, int IdConsignacion)
        {
            try
            {
                List<in_ConsignacionDet_Info> Lista = new List<in_ConsignacionDet_Info>();

                using (Entities_inventario db = new Entities_inventario())
                {
                    Lista = db.vwin_ConsignacionDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdConsignacion == IdConsignacion).Select(q => new in_ConsignacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdConsignacion = q.IdConsignacion,
                        Secuencia = q.Secuencia,
                        pr_descripcion = q.pr_descripcion,
                        IdProducto = q.IdProducto,
                        IdUnidadMedida = q.IdUnidadMedida,
                        Cantidad = q.Cantidad,
                        Costo = q.Costo,
                        Observacion = q.Observacion
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
