using Core.Erp.Bus.General;
using Core.Erp.Data.ActivoFijo;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Mej_Baj_Activo_Bus
    {
        Af_Mej_Baj_Activo_Data odata = new Af_Mej_Baj_Activo_Data();
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        Af_Parametros_Data odata_af_param = new Af_Parametros_Data();
        Af_Activo_fijo_Data odata_af = new Af_Activo_fijo_Data();
        public List<Af_Mej_Baj_Activo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public Af_Mej_Baj_Activo_Info get_info(int IdEmpresa, decimal Id_Mejora_Baja_Activo)
        {
            try
            {
                return odata.get_info(IdEmpresa, Id_Mejora_Baja_Activo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Mej_Baj_Activo_Info info)
        {
            try
            {
                //Obtengo info de parametro Activo fijo
                var param = odata_af_param.get_info(info.IdEmpresa);
                //Obtengo el tipo dependiendo si es mejora o baja
                int IdTipoCbte = info.Id_Tipo == "Mejo_Acti" ? param.IdTipoCbteMejora : param.IdTipoCbteBaja;
                //Armo un diario pasando los parametros que pida
                var af = odata_af.get_info(info.IdEmpresa, info.IdActivoFijo);
                var info_cbte = odata_ct.armar_info(info.lst_ct_cbtecble_det, info.IdEmpresa, af.IdSucursal, IdTipoCbte, 0, (info.Id_Tipo == "Mejo_Acti" ? "MEJORA - " : "BAJA - ") + info.Motivo, info.Fecha_MejBaj);
                //Guardo el diario
                if (odata_ct.guardarDB(info_cbte))
                {
                    //Actualizo PK de mejora baja
                    info.IdEmpresa_ct = info_cbte.IdEmpresa;
                    info.IdTipoCbte = info_cbte.IdTipoCbte;
                    info.IdCbteCble = info_cbte.IdCbteCble;
                    //Guardo mejora/baja
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Mej_Baj_Activo_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(Af_Mej_Baj_Activo_Info info)
        {
            try
            {
                var af = odata_af.get_info(info.IdEmpresa, info.IdActivoFijo);
                var info_cbte = odata_ct.armar_info(info.lst_ct_cbtecble_det, info.IdEmpresa, af.IdSucursal, Convert.ToInt32(info.IdTipoCbte), Convert.ToInt32(info.IdCbteCble), (info.Id_Tipo == "Mejo_Acti" ? "MEJORA - " : "BAJA - ") + info.Motivo, info.Fecha_MejBaj);
                //Modifico el diario
                if (odata_ct.modificarDB(info_cbte))
                {
                    //Guardo mejora/baja
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Mej_Baj_Activo_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(Af_Mej_Baj_Activo_Info info)
        {
            try
            {
                var info_cbte = odata_ct.get_info(info.IdEmpresa, Convert.ToInt32(info.IdTipoCbte), Convert.ToDecimal(info.IdCbteCble));
                if (info_cbte != null)
                {
                    if (odata_ct.anularDB(info_cbte))
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
