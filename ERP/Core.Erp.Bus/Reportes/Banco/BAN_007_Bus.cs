using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_007_Bus
    {
        BAN_007_Data odata = new BAN_007_Data();
        public List<BAN_007_Info> get_list(int IdEmpresa, int IdBanco, decimal IdPersona, DateTime fecha_ini, DateTime fecha_fin, string Estado)

        {
            try
            {
                return odata.get_list(IdEmpresa, IdBanco, IdPersona, fecha_ini, fecha_fin, Estado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
