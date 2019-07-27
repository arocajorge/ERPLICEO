using Core.Erp.Bus.General;
using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
  public  class ba_Archivo_Transferencia_Bus
    {
        ba_Archivo_Transferencia_Data odata = new ba_Archivo_Transferencia_Data();
        public List<ba_Archivo_Transferencia_Info> GetList(int IdEmpresa, int IdSucursal, DateTime fechaini, DateTime fechafin, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa,IdSucursal, fechaini, fechafin, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_Archivo_Transferencia_Info GetInfo(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Archivo_Transferencia_Bus", Metodo = "GuardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool ModificarDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Archivo_Transferencia_Bus", Metodo = "ModificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool AnularDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ContabilizarDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                return odata.ContabilizarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
