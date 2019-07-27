using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_marcaciones_tipo_Data
    {
        public List<ro_marcaciones_tipo_Info> get_list()
        {
            try
            {
                List<ro_marcaciones_tipo_Info> Lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_marcaciones_tipo
                             select new ro_marcaciones_tipo_Info
                             {
                             IdTipoMarcaciones = q.IdTipoMarcaciones,
                             ma_descripcion = q.ma_descripcion
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
