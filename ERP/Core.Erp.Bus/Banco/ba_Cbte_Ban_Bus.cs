using Core.Erp.Bus.General;
using Core.Erp.Data.Banco;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Banco
{
    public class ba_Cbte_Ban_Bus
    {
        ba_Cbte_Ban_Data odata = new ba_Cbte_Ban_Data();
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();

        public List<ba_Cbte_Ban_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal, string CodCbte, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, Fecha_ini, Fecha_fin, IdSucursal, CodCbte, mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ba_Cbte_Ban_Info get_info(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(ba_Cbte_Ban_Info info, cl_enumeradores.eTipoCbteBancario TipoCbteBanco)
        {
            try
            {
                return odata.guardarDB(info, TipoCbteBanco);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Cbte_Ban_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(ba_Cbte_Ban_Info info, cl_enumeradores.eTipoCbteBancario TipoCbteBanco)
        {
            try
            {
                return odata.modificarDB(info, TipoCbteBanco);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Cbte_Ban_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB_EstadoCheque(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble, string EstadoCheque)
        {
            try
            {
                return odata.modificarDB_EstadoCheque(IdEmpresa, IdTipoCbte, IdCbteCble, EstadoCheque);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(ba_Cbte_Ban_Info info)
        {
            try
            {
                if (odata_ct.anularDB(new Info.Contabilidad.ct_cbtecble_Info { IdEmpresa = info.IdEmpresa, IdTipoCbte = info.IdTipocbte, IdCbteCble = info.IdCbteCble, IdUsuarioAnu = info.IdUsuario_Anu, cb_MotivoAnu = info.MotivoAnulacion}))
                {
                    if (odata.anularDB(info))
                    {
                        return true;
                    }
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
