using Core.Erp.Bus.General;
using Core.Erp.Data.ActivoFijo;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Venta_Activo_Bus
    {
        Af_Venta_Activo_Data odata = new Af_Venta_Activo_Data();
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        Af_Parametros_Data odata_af_param = new Af_Parametros_Data();
        Af_Activo_fijo_Data odata_af = new Af_Activo_fijo_Data();
        public List<Af_Venta_Activo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public Af_Venta_Activo_Info get_info(int IdEmpresa, decimal IdVtaActivo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdVtaActivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Venta_Activo_Info info)
        {
            try
            {
                //obtengo info de param AF
                var param = odata_af_param.get_info(info.IdEmpresa);
                //armar un diario pasando los parametros que pida
                var af = odata_af.get_info(info.IdEmpresa, info.IdActivoFijo);
                var info_cbte = odata_ct.armar_info(info.lst_ct_cbtecble_det, info.IdEmpresa, af.IdSucursal, param.IdTipoCbteVenta, 0, info.Concepto_Vta, info.Fecha_Venta);
                //guardo en el diario
                if (odata_ct.guardarDB(info_cbte))
                {
                    //Actualizo PK de mejora baja
                    info.IdEmpresa_ct = info_cbte.IdEmpresa;
                    info.IdTipoCbte = info_cbte.IdTipoCbte;
                    info.IdCbteCble = info_cbte.IdCbteCble;

                    //guarda mejora/baja
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Venta_Activo_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(Af_Venta_Activo_Info info)
        {
            try
            {
                var af = odata_af.get_info(info.IdEmpresa, info.IdActivoFijo);
                var info_cbte = odata_ct.armar_info(info.lst_ct_cbtecble_det, info.IdEmpresa, af.IdSucursal, Convert.ToInt32(info.IdTipoCbte), Convert.ToDecimal(info.IdCbteCble), info.Concepto_Vta, info.Fecha_Venta);
                //modifico el diario
                if (odata_ct.modificarDB(info_cbte))
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Venta_Activo_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(Af_Venta_Activo_Info info)
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
