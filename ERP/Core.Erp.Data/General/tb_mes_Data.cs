using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_mes_Data
    {
        public List<tb_mes_Info> get_list()
        {
            try
            {
                List<tb_mes_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    Lista = (from q in Context.tb_mes
                             select new tb_mes_Info
                             {
                                 idMes = q.idMes,
                                 smes = q.smes,
                                 Nemonico = q.Nemonico,
                                 smesIngles = q.smesIngles
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
