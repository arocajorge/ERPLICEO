using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Data;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_nota_DebCre_Bus
    {
        cp_nota_DebCre_Data data = new cp_nota_DebCre_Data();
        ct_cbtecble_Bus bus_contabilidad = new ct_cbtecble_Bus();
        ct_cbtecble_Bus bus_comprobante = new ct_cbtecble_Bus();
        ct_cbtecble_det_Bus bus_comprobante_det = new ct_cbtecble_det_Bus();
        cp_parametros_Bus bus_cp_parametro = new cp_parametros_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        tb_sis_Documento_Tipo_Talonario_Info info_talonario=new tb_sis_Documento_Tipo_Talonario_Info();
        cp_orden_pago_cancelaciones_Data data_cancelacion = new cp_orden_pago_cancelaciones_Data();
        cp_orden_pago_cancelaciones_Info info_cancelacion = new cp_orden_pago_cancelaciones_Info();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        public List<cp_nota_DebCre_Info> get_lst(int IdEmpresa, int IdSucursal, string DebCre, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return data.get_lst(IdEmpresa, IdSucursal, DebCre, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
      

        public bool guardarDB(cp_nota_DebCre_Info info)
        {
            try
            {
                cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
                var prov = bus_proveedor.get_info(info.IdEmpresa, info.IdProveedor);

                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.IdSucursal = info.IdSucursal;
                info.info_comrobante.cb_Fecha = (DateTime)info.Fecha_contable;
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                if (prov != null)
                {
                    if (info.cn_observacion == null)
                        info.cn_observacion = "";
                    info.info_comrobante.cb_Observacion = "Prov: " + prov.info_persona.pe_nombreCompleto + " "+ info.cn_observacion;

                }
                else
                    info.info_comrobante.cb_Observacion = info.cn_observacion;
                if (bus_contabilidad.guardarDB(info.info_comrobante))
                {
                    data = new cp_nota_DebCre_Data();
                    info.IdTipoCbte_Nota = info.info_comrobante.IdTipoCbte;
                    info.IdCbteCble_Nota = info.info_comrobante.IdCbteCble;
                    info.Estado = "A";
                    info.cn_vaCoa = "N";
                    info.IdTipoNota = info.IdTipoNota;
                   if( data.guardarDB(info))
                    {
                        if (info.lst_det_canc_op == null)
                            return true;
                        data_cancelacion = new cp_orden_pago_cancelaciones_Data();
                        foreach (var item in info.lst_det_canc_op)
                         {
                            info_cancelacion.IdEmpresa = info.IdEmpresa;
                            info_cancelacion.Idcancelacion = 0;
                            info_cancelacion.Secuencia = 1;

                            info_cancelacion.IdEmpresa_op = info.IdEmpresa;
                            info_cancelacion.IdOrdenPago_op = item.IdOrdenPago_op;
                            info_cancelacion.Secuencia_op = item.Secuencia_op;

                            info_cancelacion.IdEmpresa_cxp = info.IdEmpresa;
                            info_cancelacion.IdTipoCbte_cxp = item.IdTipoCbte_cxp;
                            info_cancelacion.IdCbteCble_cxp = item.IdCbteCble_cxp;
                            info_cancelacion.IdEmpresa_pago = info.IdEmpresa;
                            info_cancelacion.IdTipoCbte_pago = info.IdTipoCbte_Nota;
                            info_cancelacion.IdCbteCble_pago = info.IdCbteCble_Nota;
                            info_cancelacion.Observacion = info.cn_observacion;
                            info_cancelacion.MontoAplicado = item.MontoAplicado;
                            data_cancelacion.guardarDB(info_cancelacion);
                        }
                    }
                }

              
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_nota_DebCre_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(cp_nota_DebCre_Info info)
        {
            try
            {
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.IdTipoCbte = info.IdTipoCbte_Nota;
                info.info_comrobante.IdCbteCble = info.IdCbteCble_Nota;
                info.info_comrobante.cb_Fecha = (DateTime)info.Fecha_contable;
                info.info_comrobante.IdSucursal = info.IdSucursal;
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Observacion = info.cn_observacion;

                info.cn_baseImponible = info.cn_subtotal_iva + info.cn_subtotal_siniva;
                if (bus_contabilidad.modificarDB(info.info_comrobante))
                {
                    data = new cp_nota_DebCre_Data();
                    if (data.modificarDB(info))
                    {
                        data_cancelacion = new cp_orden_pago_cancelaciones_Data();
                        data_cancelacion.ElimarDB(info.IdEmpresa, info.IdTipoCbte_Nota, info.IdCbteCble_Nota);
                        if (info.lst_det_canc_op == null)
                            return true;
                        foreach (var item in info.lst_det_canc_op)
                        {
                            info_cancelacion.IdEmpresa = info.IdEmpresa;
                            info_cancelacion.Idcancelacion = 0;
                            info_cancelacion.Secuencia = 1;

                            info_cancelacion.IdEmpresa_op = info.IdEmpresa;
                            info_cancelacion.IdOrdenPago_op = item.IdOrdenPago_op;
                            info_cancelacion.Secuencia_op = item.Secuencia_op;

                            info_cancelacion.IdEmpresa_cxp = info.IdEmpresa;
                            info_cancelacion.IdTipoCbte_cxp = item.IdTipoCbte_cxp;
                            info_cancelacion.IdCbteCble_cxp = item.IdCbteCble_cxp;
                            info_cancelacion.IdEmpresa_pago = info.IdEmpresa;
                            info_cancelacion.IdTipoCbte_pago = info.IdTipoCbte_Nota;
                            info_cancelacion.IdCbteCble_pago = info.IdCbteCble_Nota;
                            info_cancelacion.Observacion = info.cn_observacion;
                            info_cancelacion.MontoAplicado = item.MontoAplicado;
                            data_cancelacion.guardarDB(info_cancelacion);
                        }
                    }
                    }


                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_nota_DebCre_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(cp_nota_DebCre_Info info)
        {
            try
            {
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.IdTipoCbte = info.IdTipoCbte_Nota;
                info.info_comrobante.IdCbteCble = info.IdCbteCble_Nota;
                info.info_comrobante.cb_Fecha = (DateTime)info.Fecha_contable;
                info.info_comrobante.IdSucursal = info.IdSucursal;
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Observacion = info.cn_observacion;


                if (bus_contabilidad.anularDB(info.info_comrobante))
                {
                    data = new cp_nota_DebCre_Data();
                    info.IdTipoCbte_Nota = info.IdTipoCbte_Nota;
                    info.IdTipoCbte_Nota = info.IdTipoCbte_Nota;
                    data.anularDB(info);
                    data_cancelacion.ElimarDB(info.IdEmpresa, info.IdTipoCbte_Nota, info.IdCbteCble_Nota);
                }


                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public cp_nota_DebCre_Info get_info(int IdEmpresa, int IdTipoCbte_Nota, decimal IdCbteCble_Nota)
        {
            try
            {
                cp_nota_DebCre_Info info = new cp_nota_DebCre_Info();
                info = data.get_info(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
                info.info_comrobante = bus_contabilidad.get_info(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
                info.info_comrobante.lst_ct_cbtecble_det = bus_comprobante_det.get_list(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
             
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string validar(cp_nota_DebCre_Info info)
        {
            try
            {
                string mensaje = "";
                if (mensaje != "")
                {
                    return mensaje;

                }
                if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.cn_fecha, cl_enumeradores.eModulo.CONTA, info.IdSucursal, ref mensaje))
                {
                    return mensaje;
                }

                if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.cn_fecha, cl_enumeradores.eModulo.CXP, info.IdSucursal, ref mensaje))
                {
                    return mensaje;
                }


                

                if (info.IdSucursal == 0 )
                {
                    mensaje = "Selecciona la sucursal";
                    return mensaje;
                }
                if (info.IdProveedor == 0)
                {
                    mensaje = "Selecciona el proveedor";
                    return mensaje;
                }
                if (info.IdTipoServicio == "")
                {
                    mensaje = "Selecciona el tipo de servicio";
                    return mensaje;
                }
                if (info.IdIden_credito == 0)
                {
                    mensaje = "Selecciona el tipo de sustento tributario";
                    return mensaje;
                }

                if ( info.IdTipoNota == "")
                {
                    mensaje = "Selecciona el tipo de documento";
                    return mensaje;
                }
                if (info.IdTipoNota == cl_enumeradores.eTipoNotaCXP.T_TIP_NOTA_SRI.ToString())
                {
                    if (info.cn_serie1 == "" | info.cn_Nota == null)
                    {
                        mensaje = "Ingrese serie del documento";
                        return mensaje;
                    }
                    if (info.cn_Nota == "" | info.cn_Nota == null)
                    {
                        mensaje = "Ingrese el número del documento";
                        return mensaje;
                    }
                    if (info.cn_Autorizacion == "" | info.cn_Autorizacion == null)
                    {
                        mensaje = "Ingrese el número de autorización";
                        return mensaje;
                    }
                }
                
                if (info.cn_observacion == "" | info.cn_observacion == null)
                {
                    mensaje = "Ingrese la observación";
                    return mensaje;
                }
                if (info.PagoLocExt == "EXT")
                {
                    if (info.PaisPago == "" | info.PaisPago == null)
                    {
                        mensaje = "Seleccion el país donde se realiza el pago";
                        return mensaje;
                    }
                }
                if (info.cn_total == 0)
                {
                    mensaje = "El total no puede ser cero";
                    return mensaje;
                }
                info.info_comrobante.lst_ct_cbtecble_det.ForEach(item =>
                {
                    if (item.IdCtaCble == null | item.IdCtaCble == "")
                        mensaje = "Falta cuenta contable " + item.dc_Observacion;
                });

                if (Math.Round(Convert.ToDouble(info.info_comrobante.lst_ct_cbtecble_det.Sum(v => v.dc_Valor)),2,MidpointRounding.AwayFromZero) != 0)
                    mensaje = "El diario contable esta descuadrado";

               
                return mensaje;

            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public bool guardar_importacionDB(cp_nota_DebCre_Info info)
        {
            try
            {
                cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
                var prov = bus_proveedor.get_info(info.IdEmpresa, info.IdProveedor);
                var cp_parametros = bus_cp_parametro.get_info(info.IdEmpresa);
                info.info_comrobante = new ct_cbtecble_Info();

                ct_cbtecble_Info diario = new ct_cbtecble_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = info.IdTipoCbte_Nota,
                    IdCbteCble = info.IdCbteCble_Nota,
                    cb_Fecha = info.cn_fecha,
                    cb_Observacion = info.cn_observacion,
                    IdPeriodo = Convert.ToInt32(info.cn_fecha.Year.ToString() + info.cn_fecha.Month.ToString().PadLeft(2, '0')),
                    IdSucursal = info.IdSucursal,
                    cb_FechaTransac = DateTime.Now,
                    
                    cb_Estado = "A"
                };

                if (prov != null)
                {
                    if (info.cn_observacion == null)
                        info.cn_observacion = "";
                    diario.cb_Observacion = "Prov: " + prov.info_persona.pe_nombreCompleto + " " + info.cn_observacion;

                }
                else
                    diario.cb_Observacion = info.cn_observacion;

                info.info_comrobante = diario;
                info.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();

                ct_cbtecble_det_Info diario_det = new ct_cbtecble_det_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = diario.IdTipoCbte,
                    IdCbteCble = diario.IdCbteCble,
                    secuencia = 1,
                    IdCtaCble = prov.IdCtaCble_CXP,
                    dc_Valor = Math.Round(Convert.ToDouble(info.cn_total), 2, MidpointRounding.AwayFromZero),
                };
                info.info_comrobante.lst_ct_cbtecble_det.Add(diario_det);

                ct_cbtecble_det_Info diario_det_ = new ct_cbtecble_det_Info
                {
                    IdEmpresa = diario.IdEmpresa,
                    IdTipoCbte = diario.IdTipoCbte,
                    IdCbteCble = diario.IdCbteCble,
                    secuencia = 2,
                    IdCtaCble = prov.IdCtaCble_CXP,
                    dc_Valor = Math.Round(Convert.ToDouble(info.cn_total), 2, MidpointRounding.AwayFromZero) * -1
                };
                info.info_comrobante.lst_ct_cbtecble_det.Add(diario_det_);


                if (bus_contabilidad.guardarDB(info.info_comrobante))
                {
                    info.IdTipoCbte_Nota = info.info_comrobante.IdTipoCbte;
                    info.IdCbteCble_Nota = info.info_comrobante.IdCbteCble;
                    info.Estado = "A";
                    info.cn_vaCoa = "N";
                    info.IdTipoNota = info.IdTipoNota;
                    if (data.guardarDB(info))
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
