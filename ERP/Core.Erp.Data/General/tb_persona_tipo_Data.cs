using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.General;
namespace Core.Erp.Data.General
{
   public class tb_persona_tipo_Data
    {
        public List<tb_persona_tipo_Info> get_list()
        {
            try
            {
                List<tb_persona_tipo_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                        Lista = (from q in Context.tb_persona_tipo
                                 select new tb_persona_tipo_Info
                                 {
                                     IdTipo_Persona = q.IdTipo_Persona,
                                     Descricpion = q.Descricpion,
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
