using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_rubro_tipo_x_jornada_Data
    {
        public List<ro_rubro_tipo_x_jornada_Info> get_list(int IdEmpresa, string IdRubro)
        {
            try
            {
                List<ro_rubro_tipo_x_jornada_Info> Lista = new List<ro_rubro_tipo_x_jornada_Info>();
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_rubro_tipo_x_jornada
                             where q.IdEmpresa == IdEmpresa
                             && q.IdRubro == IdRubro
                             select new ro_rubro_tipo_x_jornada_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRubro = q.IdRubro,
                                 Secuencia = q.Secuencia,
                                 IdJornada = q.IdJornada,
                                 IdRubroContabilizacion = q.IdRubroContabilizacion,
                                 ru_descripcion = q.ru_descripcion,
                                 Descripcion = q.Descripcion
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
