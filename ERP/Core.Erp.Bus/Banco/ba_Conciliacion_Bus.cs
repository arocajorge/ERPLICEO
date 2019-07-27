using Core.Erp.Bus.General;
using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Banco
{
    public class ba_Conciliacion_Bus
    {
        ba_Conciliacion_Data odata = new ba_Conciliacion_Data();
        public List<ba_Conciliacion_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, Fecha_ini, Fecha_fin);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public ba_Conciliacion_Info get_info(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdConciliacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ExisteConciliacion(int IdEmpresa, int IdPeriodo, int IdBanco)
        {
            try
            {
                return odata.ExisteConciliacion(IdEmpresa, IdPeriodo, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_Conciliacion_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Conciliacion_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(ba_Conciliacion_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Conciliacion_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(ba_Conciliacion_Info info)
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
    }
}
