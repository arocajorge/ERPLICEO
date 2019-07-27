using Core.Erp.Data.Caja;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;

namespace Core.Erp.Bus.Caja
{
    public class caj_Caja_Movimiento_Bus
    {
        caj_Caja_Movimiento_Data odata = new caj_Caja_Movimiento_Data();
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        caj_Caja_Data odata_caja = new caj_Caja_Data();
        public List<caj_Caja_Movimiento_Info> get_list(int IdEmpresa, int IdCaja, string cm_signo, bool mostrar_anulados, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCaja, cm_signo, mostrar_anulados, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public caj_Caja_Movimiento_Info get_info(int IdEmpresa, int IdTipocbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(caj_Caja_Movimiento_Info info)
        {
            try
            {
                var caja = odata_caja.get_info(info.IdEmpresa, info.IdCaja);
                //Como necesito que exista un diario para que el movimiento herede sus PK, armo un diario en base a lo que ingresen en la pantalla
                info.info_ct_cbtecble = odata_ct.armar_info(info.lst_ct_cbtecble_det, info.IdEmpresa, caja.IdSucursal, info.IdTipocbte, info.IdCbteCble, info.cm_observacion, info.cm_fecha);
                info.info_ct_cbtecble.IdUsuario = info.IdUsuario;

                //Guardo el diario
                if (odata_ct.guardarDB(info.info_ct_cbtecble))
                {//Si el diario se guarda exitosamente entonces paso los PK al movimiento de caja
                    info.IdCbteCble = info.info_ct_cbtecble.IdCbteCble;
                    //Guardo el movimiento de caja                    
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "caj_Caja_Movimiento_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(caj_Caja_Movimiento_Info info)
        {
            try
            {
                var caja = odata_caja.get_info(info.IdEmpresa, info.IdCaja);
                var info_ct_cbtecble = odata_ct.armar_info(info.lst_ct_cbtecble_det, info.IdEmpresa, caja.IdSucursal, info.IdTipocbte, info.IdCbteCble, info.cm_observacion, info.cm_fecha);
                info_ct_cbtecble.IdUsuarioUltModi = info.IdUsuarioUltMod;

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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "caj_Caja_Movimiento_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;

            }
        }

        public bool ValidarMovimientoModificar(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble, string signo)
        {
            try
            {
                return odata.ValidarMovimientoModificar(IdEmpresa, IdTipoCbte, IdCbteCble, signo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(caj_Caja_Movimiento_Info info)
        {
            try
            {
                var info_ct_cbtecble = odata_ct.get_info(info.IdEmpresa, info.IdTipocbte, info.IdCbteCble);
                if(info_ct_cbtecble != null)
                {
                    if(odata_ct.anularDB(info_ct_cbtecble))
                    {
                        return odata.anularDB(info);
                    }
                }
                else
                {
                    return odata.anularDB(info);
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
