using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
   public class ba_TipoFlujo_Bus
    {
        ba_TipoFlujo_Data odata = new ba_TipoFlujo_Data();
        public List<ba_TipoFlujo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_TipoFlujo_Info get_info(int IdEmpresa, decimal IdTipoFlujo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTipoFlujo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_TipoFlujo_Info info)

        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ba_TipoFlujo_Info info)

        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ba_TipoFlujo_Info info)

        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_TipoFlujo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa);

        }

        public ba_TipoFlujo_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            return odata.get_info_bajo_demanda(args, IdEmpresa);
        }

        public List<ba_TipoFlujo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string Tipo)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa, Tipo);

        }

    }
}
