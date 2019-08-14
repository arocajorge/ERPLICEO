using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
   public class BAN_013_Bus
    {
        BAN_013_Data odata = new BAN_013_Data();
        public List<BAN_013_Info> GetList(int IdEmpresa, int IdBanco, decimal IdPersona, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdBanco, IdPersona, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
