using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_Motivo_Inven_Bus
    {
        in_Motivo_Inven_Data odata = new in_Motivo_Inven_Data();
    
        public List<in_Motivo_Inven_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public List<in_Motivo_Inven_Info> get_list(int IdEmpresa, string tipo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa,tipo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_Motivo_Inven_Info get_info(int IdEmpresa, int IdMotivo_Inv)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdMotivo_Inv);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Motivo_Inven_Info info)
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

        public bool modificarDB(in_Motivo_Inven_Info info)
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
        public bool anularDB(in_Motivo_Inven_Info info)
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
        public int get_id_movimiento(int IdEmpresa, string signo)
        {
            try
            {
                return odata.get_id_movimiento(IdEmpresa, signo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
