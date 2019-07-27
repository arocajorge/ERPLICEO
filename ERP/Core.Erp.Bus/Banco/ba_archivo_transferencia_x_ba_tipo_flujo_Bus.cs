using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_archivo_transferencia_x_ba_tipo_flujo_Bus
    {
        ba_archivo_transferencia_x_ba_tipo_flujo_Data odata = new ba_archivo_transferencia_x_ba_tipo_flujo_Data();
        public List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> GetList(int IdEmpresa, decimal IdArchivo)
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
