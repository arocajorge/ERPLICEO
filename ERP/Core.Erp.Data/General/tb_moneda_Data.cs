using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.General;
namespace Core.Erp.Data.General
{
   public class tb_moneda_Data
    {
        public List<tb_moneda_Info> get_list()
        {
            try
            {
                List<tb_moneda_Info> lista;
                using (Entities_general Context = new Entities_general())
                {
                    lista = (from q in Context.tb_moneda
                             select
                             new tb_moneda_Info
                             {
                                IdMoneda=q.IdMoneda,
                                im_simbolo=q.im_simbolo,
                                im_descripcion=q.im_descripcion,
                                 Estado=q.Estado,
                                 im_nemonico=q.im_nemonico
                             }).ToList();
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
