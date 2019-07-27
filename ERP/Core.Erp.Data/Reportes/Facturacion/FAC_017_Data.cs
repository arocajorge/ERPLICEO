using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_017_Data
    {
        public List<FAC_017_Info> GetList(int IdEmpresa, int IdMarca, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<FAC_017_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPFAC_017(IdEmpresa, IdMarca, fecha_ini, fecha_fin).Select(q => new FAC_017_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        MES = q.MES,
                        NomMes = q.NomMes,
                        MarcaDescripcion = q.MarcaDescripcion,
                        ANIO = q.ANIO,
                        vt_total = q.vt_total,
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
