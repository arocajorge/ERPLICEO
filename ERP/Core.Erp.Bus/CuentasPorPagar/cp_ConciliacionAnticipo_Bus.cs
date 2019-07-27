using Core.Erp.Bus.General;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_ConciliacionAnticipo_Bus
    {
        cp_ConciliacionAnticipo_Data oData = new cp_ConciliacionAnticipo_Data();
        cp_ConciliacionAnticipoDetAnt_Data oData_det_OP = new cp_ConciliacionAnticipoDetAnt_Data();
        cp_ConciliacionAnticipoDetCXP_Data oData_det_Fact = new cp_ConciliacionAnticipoDetCXP_Data();

        public List<cp_conciliacionAnticipo_Info> GetList(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
        {
            try
            {
                return oData.getlist(IdEmpresa, IdSucursal, fecha_ini, fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cp_conciliacionAnticipo_Info GetInfo(int IdEmpresa, int IdConciliacion)
        {
            try
            {
                cp_conciliacionAnticipo_Info info = new cp_conciliacionAnticipo_Info();

                info = oData.get_info(IdEmpresa, IdConciliacion);
                if (info == null)
                    info = new cp_conciliacionAnticipo_Info();

                info.Lista_det_OP = oData_det_OP.getlist(IdEmpresa, IdConciliacion);
                if (info.Lista_det_OP == null)
                {
                    info.Lista_det_OP = new List<cp_ConciliacionAnticipoDetAnt_Info>();
                }

                info.Lista_det_Fact = oData_det_Fact.getlist(IdEmpresa, IdConciliacion);
                if (info.Lista_det_Fact == null)
                {
                    info.Lista_det_Fact = new List<cp_ConciliacionAnticipoDetCXP_Info>();
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(cp_conciliacionAnticipo_Info info)
        {
            try
            {
                return oData.GuardarBD(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacionAnticipo_Bus", Metodo = "GuardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool ModificarBD(cp_conciliacionAnticipo_Info info)
        {
            try
            {
                return oData.ModificarBD(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacionAnticipo_Bus", Metodo = "ModificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool AnularBD(cp_conciliacionAnticipo_Info info)
        {
            try
            {
                return oData.AnularBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
