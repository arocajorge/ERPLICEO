using Core.Erp.Bus.General;
using Core.Erp.Data.ActivoFijo;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Depreciacion_Bus
    {
        Af_Depreciacion_Data odata = new Af_Depreciacion_Data();
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        Af_Parametros_Data odata_param = new Af_Parametros_Data();

        public List<Af_Depreciacion_Info> get_list(int IdEmpresa, bool mostrar_anulados, DateTime Fecha_ini, DateTime Fecha_fin)

        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados, Fecha_ini, Fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Af_Depreciacion_Info get_info(int IdEmpresa, decimal IdDepreciacion)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdDepreciacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Depreciacion_Info info)
        {
            try
            {
                var i_param = odata_param.get_info(info.IdEmpresa);
                var info_ct_cbtecble = odata_ct.armar_info(info.lst_detalle_ct, info.IdEmpresa, 1, i_param.IdTipoCbte, 0, "Depreciación "+info.IdPeriodo.ToString() + info.Descripcion, info.Fecha_Depreciacion);
                if (odata_ct.guardarDB(info_ct_cbtecble))
                {
                    info.IdEmpresa_ct = info_ct_cbtecble.IdEmpresa;
                    info.IdTipoCbte = info_ct_cbtecble.IdTipoCbte;
                    info.IdCbteCble = info_ct_cbtecble.IdCbteCble;
                    if (odata.guardarDB(info))
                    {
                        return true;
                    }
                }                
                return false;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Depreciacion_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(Af_Depreciacion_Info info)
        {
            try
            {
                var info_ct_cbtecble = odata_ct.armar_info(info.lst_detalle_ct, info.IdEmpresa, 1, Convert.ToInt32(info.IdTipoCbte), Convert.ToDecimal(info.IdCbteCble), info.Descripcion, info.Fecha_Depreciacion);
                if (odata_ct.modificarDB(info_ct_cbtecble))
                {
                    if (odata.modificarDB(info))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Depreciacion_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(Af_Depreciacion_Info info)
        {
            try
            {
                var info_ct_cbtecble = odata_ct.get_info(info.IdEmpresa, Convert.ToInt32(info.IdTipoCbte), Convert.ToDecimal(info.IdCbteCble));
                if (odata_ct.anularDB(info_ct_cbtecble))
                {
                    if(odata.anularDB(info))
                        return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        

    }
}
