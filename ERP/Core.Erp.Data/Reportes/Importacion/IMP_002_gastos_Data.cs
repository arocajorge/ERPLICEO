
using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Importacion
{
   
        public class IMP_002_gastos_Data
    {
            public List<IMP_002_gastos_Info> get_list(int IdEmpresa, int IdOrdenCompra_ext)
            {
                try
                {
                    List<IMP_002_gastos_Info> Lista;
                    using (Entities_reportes Context = new Entities_reportes())
                    {
                        Lista = (from q in Context.VWIMP_002_gastos
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdOrdenCompra_ext == IdOrdenCompra_ext
                                 orderby q.gt_orden ascending
                                 select new IMP_002_gastos_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                     gt_descripcion=q.gt_descripcion,
                                     dc_Valor=q.dc_Valor
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
