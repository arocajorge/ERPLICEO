using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Presupuesto
{
    public class pre_PresupuestoDet_Data
    {
        public List<pre_PresupuestoDet_Info> GetList(int IdEmpresa, int IdPresupuesto)
        {
            try
            {
                List<pre_PresupuestoDet_Info> Lista = new List<pre_PresupuestoDet_Info>();

                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    Lista = db.vwpre_PresupuestoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdPresupuesto == IdPresupuesto).Select(q => new pre_PresupuestoDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPresupuesto = q.IdPresupuesto,
                        Secuencia = q.Secuencia,
                        IdRubro = q.IdRubro,
                        Descripcion = q.Descripcion,
                        IdCtaCble = q.IdCtaCble,
                        Monto = q.Monto
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
