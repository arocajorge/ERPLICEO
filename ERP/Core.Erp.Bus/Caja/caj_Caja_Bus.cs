using Core.Erp.Bus.General;
using Core.Erp.Data.Caja;
using Core.Erp.Info.Caja;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Caja
{
    public class caj_Caja_Bus
    {
        caj_Caja_Data odata = new caj_Caja_Data();
        
        public List<caj_Caja_Info> GetList(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<caj_Caja_Info> get_list(int IdEmpresa,  bool mostrar_anulados)
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

        public int GetIdCajaPorUsuario(int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.GetIdCajaPorUsuario(IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public caj_Caja_Info get_info(int IdEmpresa, int IdCaja)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCaja);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(caj_Caja_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "caj_Caja_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(caj_Caja_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "caj_Caja_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(caj_Caja_Info info)
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

        public string get_IdCtaCble(int IdEmpresa, int IdCaja)
        {
            try
            {
                return odata.get_IdCtaCble(IdEmpresa, IdCaja);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetIdCajaPorSucursal(int IdEmpresa, int IdSucursal)

        {
            try
            {
                return odata.GetIdCajaPorSucursal(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
