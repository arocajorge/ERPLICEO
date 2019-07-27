using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_015_Data
    {
        public List<FAC_015_Info> GetList(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<FAC_015_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPFAC_015(IdEmpresa, IdSucursal, fecha_ini, fecha_fin).Select(q => new FAC_015_Info
                    {
                        IdEmpresa  = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        ANIO = q.ANIO,
                        MES = q.MES,
                        NomMes = q.NomMes,
                        Su_Descripcion = q.Su_Descripcion,
                        vt_total = q.vt_total
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
