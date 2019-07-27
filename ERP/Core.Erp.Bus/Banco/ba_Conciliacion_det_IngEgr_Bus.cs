using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_Conciliacion_det_IngEgr_Bus
    {
        ba_Conciliacion_det_IngEgr_Data odata = new ba_Conciliacion_det_IngEgr_Data();
        public List<ba_Conciliacion_det_IngEgr_Info> get_list(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdConciliacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_Conciliacion_det_IngEgr_Info> get_list_x_conciliar(int IdEmpresa, int IdBanco, string IdCtaCble, DateTime F_fin)
        {
            try
            {
                return odata.get_list_x_conciliar(IdEmpresa, IdBanco,IdCtaCble,F_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
