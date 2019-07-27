using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_016_Data
    {
        public List<FAC_016_Info> GetList(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<FAC_016_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPFAC_016(IdEmpresa, IdSucursal, fecha_ini, fecha_fin).Select(q => new FAC_016_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        ANIO = q.ANIO,
                        Su_Descripcion = q.Su_Descripcion,
                        vt_total = q.vt_total,
                        DescripcionSemana = q.DescripcionSemana,
                        Semana = q.Semana
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
