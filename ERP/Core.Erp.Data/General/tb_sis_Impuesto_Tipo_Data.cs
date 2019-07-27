using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_sis_Impuesto_Tipo_Data
    {
        public List<tb_sis_Impuesto_Tipo_Info> get_list()
        {
            try
            {
                List<tb_sis_Impuesto_Tipo_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    Lista = (from q in Context.tb_sis_Impuesto_Tipo
                             select  new tb_sis_Impuesto_Tipo_Info
                             {
                                IdTipoImpuesto = q.IdTipoImpuesto,
                                nom_tipoImpuesto = q.nom_tipoImpuesto
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
