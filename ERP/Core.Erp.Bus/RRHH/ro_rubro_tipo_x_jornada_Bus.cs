using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_rubro_tipo_x_jornada_Bus
    {
        ro_rubro_tipo_x_jornada_Data odata = new ro_rubro_tipo_x_jornada_Data();

        public List<ro_rubro_tipo_x_jornada_Info> get_list(int IdEmpresa, string IdRubro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
