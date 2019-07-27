using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Data;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Data.Facturacion;
using Core.Erp.Data.General;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_retencion_Bus
    {

        #region Variables
        cp_retencion_Data odata = new cp_retencion_Data();
        cp_orden_giro_Data o_data_orden_giro = new cp_orden_giro_Data();
        cp_retencion_Info info_retencion = new cp_retencion_Info();
        cp_orden_giro_Info info_orden_giro = new cp_orden_giro_Info();
        tb_sis_Documento_Tipo_Talonario_Data data_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
        tb_sis_Documento_Tipo_Talonario_Info info_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
        ct_cbtecble_Bus bus_comprobante = new ct_cbtecble_Bus();
        cp_retencion_x_ct_cbtecble_Info info_comp_x_retencion = new cp_retencion_x_ct_cbtecble_Info();
        cp_retencion_x_ct_cbtecble_Data data_comp_x_retencion = new cp_retencion_x_ct_cbtecble_Data();
        ct_cbtecble_det_Bus bus_comprobante_det = new ct_cbtecble_det_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        cp_retencion_det_Data data_retencion_der = new cp_retencion_det_Data();
        #endregion
        public List<cp_retencion_Info> get_list(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public cp_retencion_Info get_info(int IdEmpresa,decimal IdRetencion)
        {
            try
            {
                cp_retencion_Info info = new cp_retencion_Info();

                info= odata.get_info(IdEmpresa,IdRetencion);
                if (info == null)
                    info = new cp_retencion_Info();
                info.detalle = data_retencion_der.get_list(info.IdEmpresa, info.IdRetencion);
                if (info.ct_IdCbteCble != null)
                    info.info_comprobante = bus_comprobante.get_info(info.IdEmpresa, (int)info.ct_IdTipoCbte, (decimal)info.ct_IdCbteCble);
                else
                    info.info_comprobante = new Info.Contabilidad.ct_cbtecble_Info();

                if (info.ct_IdCbteCble != null)
                    info.info_comprobante.lst_ct_cbtecble_det = bus_comprobante_det.get_list(info.IdEmpresa, (int)info.ct_IdTipoCbte, (decimal)info.ct_IdCbteCble);
                else
                    info.info_comprobante.lst_ct_cbtecble_det = new List<Info.Contabilidad.ct_cbtecble_det_Info>();
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public cp_retencion_Info get_info(int IdEmpresa_Ogiro, decimal IdCbteCble_Ogiro, int IdTipoCbte_Ogiro)
        {
            try
            {
                cp_retencion_Info info = new cp_retencion_Info();

                info = odata.get_info(IdEmpresa_Ogiro,IdCbteCble_Ogiro, IdTipoCbte_Ogiro);
                if (info == null)
                    info = new cp_retencion_Info();
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public cp_retencion_Info get_info_factura(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {

                // ultima retencion no usada
                info_orden_giro = o_data_orden_giro.get_info_retencion(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
                tb_sucursal_Data data_sucursal = new tb_sucursal_Data();
                var sucursal = data_sucursal.get_info(info_orden_giro.IdEmpresa, info_orden_giro.IdSucursal);

                info_retencion.IdSucursal_cxp = info_orden_giro.IdSucursal_cxp;
                info_retencion.IdEmpresa = info_orden_giro.IdEmpresa;
                info_retencion.IdProveedor = info_orden_giro.IdProveedor;
                info_retencion.IdSucursal = info_orden_giro.IdSucursal;
                info_retencion.serie1 = sucursal.Su_CodigoEstablecimiento;
                info_retencion.serie2 = "001";
                info_retencion.NumRetencion = info_talonario.NumDocumento;
                info_retencion.IdTipoCbte_Ogiro = info_orden_giro.IdTipoCbte_Ogiro;
                info_retencion.IdCbteCble_Ogiro = info_orden_giro.IdCbteCble_Ogiro;
                info_retencion.co_baseImponible = info_orden_giro.co_total;
                info_retencion.co_serie = info_orden_giro.co_serie;
                info_retencion.co_factura = info_orden_giro.co_factura;
                info_retencion.co_subtotal_iva = info_orden_giro.co_subtotal_iva;
                info_retencion.co_valoriva = info_orden_giro.co_valoriva;
                info_retencion.co_subtotal_siniva = info_orden_giro.co_subtotal_siniva;
                info_retencion.Descripcion = info_orden_giro.Descripcion;
                info_retencion.pe_razonSocial = info_orden_giro.info_proveedor.info_persona.pe_razonSocial;
                info_retencion.observacion = info_orden_giro.co_observacion;
                info_retencion.fecha = info_orden_giro.co_FechaFactura;
                
                return info_retencion;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cp_retencion_Info info)
        {
            try
            {

                odata = new cp_retencion_Data();
                info.IdEmpresa_Ogiro = info.IdEmpresa;
                info.CodDocumentoTipo = cl_enumeradores.eTipoDocumento.RETEN.ToString();
                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Fecha = (DateTime)info.fecha;

                //REVISA CARLOS FALTA IDSUCURSAL
                info_orden_giro = o_data_orden_giro.get_info_retencion(info.IdEmpresa, Convert.ToInt32(info.IdTipoCbte_Ogiro), Convert.ToInt32(info.IdCbteCble_Ogiro));
                info.info_comprobante.cb_Estado = "A";
                info.info_comprobante.IdPeriodo = Convert.ToInt32(info.info_comprobante.cb_Fecha.Year.ToString() + info.info_comprobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Observacion = info.observacion;
                info.info_comprobante.IdSucursal = info.IdSucursal;
                if (odata.guardarDB(info))
                {
                    return true;
                }
                else
                    return false;


            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_retencion_Info_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(cp_retencion_Info info)
        {
            try
            {
                odata = new cp_retencion_Data();
                cp_orden_giro_Data odata_og = new cp_orden_giro_Data();
                info.IdEmpresa_Ogiro = info.IdEmpresa;
                info.CodDocumentoTipo = "RETEN";
                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Fecha = (DateTime)info.fecha;

                //REVISA CARLOS FALTA IDSUCURSAL

                info.info_comprobante.cb_Estado = "A";
                info.info_comprobante.IdPeriodo = Convert.ToInt32(info.info_comprobante.cb_Fecha.Year.ToString() + info.info_comprobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Observacion = info.observacion;
                info.info_comprobante.IdSucursal = info.IdSucursal;

                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_retencion_Info_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(cp_retencion_Info info)
        {

            try
            {
                odata = new cp_retencion_Data();
                info.IdEmpresa_Ogiro = info.IdEmpresa;
                info.CodDocumentoTipo = "RETEN";
                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Fecha = (DateTime)info.fecha;

                //REVISA CARLOS FALTA IDSUCURSAL

                info.info_comprobante.cb_Estado = "A";
                info.info_comprobante.IdPeriodo = Convert.ToInt32(info.info_comprobante.cb_Fecha.Year.ToString() + info.info_comprobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Observacion = info.observacion;
                if (bus_comprobante.anularDB(info.info_comprobante))
                {
                    if (odata.anularDB(info))
                    {

                        return true;
                    }
                    else
                        return false;

                }
                else
                    return false;

            }
            catch (Exception )
            {

                throw;
            }
        }
        public string validar(cp_retencion_Info info)
        {
            try
            {
                string mensaje = "";

                if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.fecha, cl_enumeradores.eModulo.CONTA, info.IdSucursal, ref mensaje))
                {
                    return mensaje;
                }

                if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.fecha, cl_enumeradores.eModulo.CXP, info.IdSucursal, ref mensaje))
                {
                    return mensaje;
                }

                if (info.co_serie == "" | info.co_serie == null)
                {
                    mensaje = "Ingrese la serie del documento";
                    return mensaje;
                }
                if (info.serie1 == "" | info.serie1 == null)
                {
                    mensaje = "Ingrese serie de la retención";
                    return mensaje;
                }
                if (info.serie2 == "" | info.serie2 == null)
                {
                    mensaje = "Ingrese serie de la retención";
                    return mensaje;
                }
                
                if (info.co_factura == "" | info.co_factura == null)
                {
                    mensaje = "Ingrese el número del documento";
                    return mensaje;
                }
               
               if(info.info_comprobante.lst_ct_cbtecble_det == null)
                {
                    mensaje = "No existe detalle";
                }
                else
                info.info_comprobante.lst_ct_cbtecble_det.ForEach(item =>
                {
                    if (item.IdCtaCble == null | item.IdCtaCble == "")
                        mensaje = "Falta cuenta contable " + item.dc_Observacion;
                });

                double valor = Convert.ToInt32(info.info_comprobante.lst_ct_cbtecble_det.Sum(v => v.dc_Valor));
                if (valor != 0)
                    mensaje = "El diario contable esta descuadrado ";

               
                return mensaje;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarEstadoAutorizacion(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)

        {
            try
            {
                return odata.ModificarEstadoAutorizacion(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
