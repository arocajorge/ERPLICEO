using Core.Erp.Data.Caja;
using Core.Erp.Info.Caja;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Caja
{
    public class caj_Caja_Movimiento_Tipo_Bus
    {
        caj_Caja_Movimiento_Tipo_Data odata = new caj_Caja_Movimiento_Tipo_Data();
    
        public List<caj_Caja_Movimiento_Tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public List<caj_Caja_Movimiento_Tipo_Info> get_list(int IdEmpresa,string signo, bool mostrar_anulados, bool mostrar_sin_ctaCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, signo, mostrar_anulados, mostrar_sin_ctaCble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public caj_Caja_Movimiento_Tipo_Info get_info(int IdEmpresa, int IdTipoMovi)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTipoMovi);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(caj_Caja_Movimiento_Tipo_Info info)
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

        public bool modificarDB(caj_Caja_Movimiento_Tipo_Info info)
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

        public bool anularDB(caj_Caja_Movimiento_Tipo_Info info)
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

        public List<caj_Caja_Movimiento_Tipo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string signo)
        {
            try
            {
                return odata.get_list_bajo_demanda(args, IdEmpresa, signo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public caj_Caja_Movimiento_Tipo_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            try
            {
                return odata.get_info_bajo_demanda(args, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
