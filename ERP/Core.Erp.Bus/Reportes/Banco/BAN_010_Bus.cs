using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
   public class BAN_010_Bus
    {
        BAN_010_Data odata = new BAN_010_Data();
        public List<BAN_010_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
