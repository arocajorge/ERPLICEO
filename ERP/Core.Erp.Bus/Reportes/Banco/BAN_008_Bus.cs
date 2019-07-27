using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_008_Bus
    {
        BAN_008_Data odata = new BAN_008_Data();
        public List<BAN_008_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, int IdBanco)
        {
            try
            {
                return odata.GetList(IdEmpresa, fecha_ini, fecha_fin, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
