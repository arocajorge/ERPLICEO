using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_009_Bus
    {
        BAN_009_Data odata = new BAN_009_Data();
        public List<BAN_009_Info> GetList(int IdEmpresa, int IdBanco, DateTime fecha_fin, bool AgruparPorFlujo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdBanco, fecha_fin, AgruparPorFlujo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
