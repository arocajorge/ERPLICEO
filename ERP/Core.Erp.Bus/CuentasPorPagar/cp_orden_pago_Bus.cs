using Core.Erp.Bus.Contabilidad;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_orden_pago_Bus
    {
        cp_orden_pago_Data oData = new cp_orden_pago_Data();
        cp_orden_pago_det_Data odata_detalle = new cp_orden_pago_det_Data();

        ct_cbtecble_Bus bus_contabilidad = new ct_cbtecble_Bus();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        cp_proveedor_Info info_proveedor = new cp_proveedor_Info();
        ro_empleado_Info info_empleado = new ro_empleado_Info();
        fa_cliente_Info info_cliente = new fa_cliente_Info();
        tb_persona_Info info_persona = new tb_persona_Info();
        ct_cbtecble_det_Bus bus_contabilidad_det = new ct_cbtecble_det_Bus();

        public List<cp_orden_pago_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal)
        {
            try
            {
                return oData.get_list(IdEmpresa, Fecha_ini, Fecha_fin, IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<cp_orden_pago_Info> get_list_aprobacion(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal)
        {
            try
            {
                return oData.get_list_aprobacion(IdEmpresa, Fecha_ini, Fecha_fin, IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<cp_orden_pago_det_Info> Get_List_orden_pago_con_saldo(int IdEmpresa, string IdTipo_op, decimal IdProveedor, string IdEstado_Aprobacion, string IdUsuario, int IdSucursal)
        {
            try
            {
                return oData.Get_List_orden_pago_con_saldo(IdEmpresa,IdTipo_op, IdProveedor, IdEstado_Aprobacion, IdUsuario,  IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public cp_orden_pago_Info get_info(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                cp_orden_pago_Info info_ = new cp_orden_pago_Info();
                info_= oData.get_info(IdEmpresa, IdOrdenPago);
                if (info_ == null)
                    info_ = new cp_orden_pago_Info();
                info_.detalle = odata_detalle.Get_list_cuotas_x_doc_det(IdEmpresa, IdOrdenPago);
                if(info_.detalle==null)
                {
                    info_.detalle = new List<cp_orden_pago_det_Info>();
                }
                else
                {
                    if (info_.detalle.Count() > 0)
                        info_.Valor_a_pagar = info_.detalle.FirstOrDefault().Valor_a_pagar;
                }
                info_.info_comprobante = bus_contabilidad.get_info(info_.IdEmpresa, Convert.ToInt32(info_.detalle.FirstOrDefault().IdTipoCbte_cxp), Convert.ToInt32(info_.detalle.FirstOrDefault().IdCbteCble_cxp));
                if (info_.info_comprobante == null)
                    info_.info_comprobante = new ct_cbtecble_Info();
                info_.info_comprobante.lst_ct_cbtecble_det = bus_contabilidad_det.get_list(info_.IdEmpresa, info_.info_comprobante.IdTipoCbte,info_.info_comprobante.IdCbteCble);
                return info_;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean guardarDB(cp_orden_pago_Info info)
        {            
            var ObservacionComprobante = "";
            try
            {                
                if(cl_enumeradores.eTipoPersona.CLIENTE.ToString() == info.IdTipo_Persona)
                {
                    info_cliente = bus_cliente.get_info(info.IdEmpresa, Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_cliente.IdPersona;

                    ObservacionComprobante = "Orden pago a cliente: " + (info_cliente.info_persona.pe_nombreCompleto) == null ? "" : info_cliente.info_persona.pe_nombreCompleto + " " + info.Observacion;
                }
                if (cl_enumeradores.eTipoPersona.EMPLEA.ToString() == info.IdTipo_Persona)
                {
                    info_empleado = bus_empleado.get_info(info.IdEmpresa, Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_empleado.IdPersona;

                    ObservacionComprobante = "Orden pago a empleado: " + (info_empleado.info_persona.pe_nombreCompleto) == null ? "" : info_empleado.info_persona.pe_nombreCompleto + " " + info.Observacion;
                }
                if (cl_enumeradores.eTipoPersona.PERSONA.ToString() == info.IdTipo_Persona)
                {
                    info_persona = bus_persona.get_info(Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_persona.IdPersona;

                    ObservacionComprobante = "Orden pago a persona: " + (info_persona.pe_nombreCompleto) == null ? "" : info_persona.pe_nombreCompleto + " " + info.Observacion;
                }
                if (cl_enumeradores.eTipoPersona.PROVEE.ToString() == info.IdTipo_Persona)
                {
                    info_proveedor = bus_proveedor.get_info(info.IdEmpresa, Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_proveedor.IdPersona;

                    ObservacionComprobante = "Orden pago a proveedor: " + (info_proveedor.info_persona.pe_nombreCompleto) == null ? "" : info_proveedor.info_persona.pe_nombreCompleto + " " + info.Observacion;
                }

                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Fecha = info.Fecha;
                info.info_comprobante.IdSucursal = info.IdSucursal;
                info.info_comprobante.cb_Estado = "A";
                info.info_comprobante.IdPeriodo = Convert.ToInt32(info.info_comprobante.cb_Fecha.Year.ToString() + info.info_comprobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));

                if (info_cliente != null || info_empleado != null || info_persona != null || info_proveedor != null)
                {
                    if (info.Observacion == null)
                        info.Observacion = "";
                    info.info_comprobante.cb_Observacion = ObservacionComprobante;
                }
                else
                {
                    info.info_comprobante.cb_Observacion = info.Observacion;
                }                

                if (bus_contabilidad.guardarDB(info.info_comprobante))
                {                   
                    oData.guardarDB(info);
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_orden_pago_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public Boolean modificarDB(cp_orden_pago_Info info)
        {
            var ObservacionComprobante = "";

            try
            {
                if (cl_enumeradores.eTipoPersona.CLIENTE.ToString() == info.IdTipo_Persona)
                {
                    info_cliente = bus_cliente.get_info(info.IdEmpresa, Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_cliente.IdPersona;

                    ObservacionComprobante = "Orden pago a cliente: " + (info_cliente.info_persona.pe_nombreCompleto) == null ? "" : info_cliente.info_persona.pe_nombreCompleto + " " + info.Observacion;
                }
                if (cl_enumeradores.eTipoPersona.EMPLEA.ToString() == info.IdTipo_Persona)
                {
                    info_empleado = bus_empleado.get_info(info.IdEmpresa, Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_empleado.IdPersona;

                    ObservacionComprobante = "Orden pago a empleado: " + (info_empleado.info_persona.pe_nombreCompleto) == null ? "" : info_empleado.info_persona.pe_nombreCompleto + " " + info.Observacion;
                }
                if (cl_enumeradores.eTipoPersona.PERSONA.ToString() == info.IdTipo_Persona)
                {
                    info_persona = bus_persona.get_info(Convert.ToInt32(info.IdPersona));
                    info.IdPersona = info_persona.IdPersona;

                    ObservacionComprobante = "Orden pago a persona: " + (info_persona.pe_nombreCompleto) == null ? "" : info_persona.pe_nombreCompleto + " " + info.Observacion;
                }
                if (cl_enumeradores.eTipoPersona.PROVEE.ToString() == info.IdTipo_Persona)
                {
                    info_proveedor = bus_proveedor.get_info(info.IdEmpresa, Convert.ToInt32(info.IdEntidad));
                    info.IdPersona = info_proveedor.IdPersona;

                    ObservacionComprobante = "Orden pago a proveedor: " + (info_proveedor.info_persona.pe_nombreCompleto) == null ? "" : info_proveedor.info_persona.pe_nombreCompleto + " " + info.Observacion;
                }

                info.info_comprobante.IdEmpresa = info.IdEmpresa;
                info.info_comprobante.cb_Fecha = (DateTime)info.Fecha;
                info.info_comprobante.IdSucursal = info.IdSucursal;
                info.info_comprobante.cb_Estado = "A";
                info.info_comprobante.IdPeriodo = Convert.ToInt32(info.info_comprobante.cb_Fecha.Year.ToString() + info.info_comprobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comprobante.IdEmpresa = info.IdEmpresa;

                if (info_cliente != null || info_empleado != null || info_persona != null || info_proveedor != null)
                {
                    if (info.Observacion == null)
                        info.Observacion = "";
                    info.info_comprobante.cb_Observacion = ObservacionComprobante;

                }
                else
                    info.info_comprobante.cb_Observacion = info.Observacion;

                

                if (oData.modificarDB(info))
                {
                    bus_contabilidad.modificarDB(info.info_comprobante);

                    foreach (var item in info.detalle)
                    {
                        item.IdEmpresa = info.IdEmpresa;
                        item.IdOrdenPago = info.IdOrdenPago;
                        odata_detalle.modificarDB(item);

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_orden_pago_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public Boolean anularDB(cp_orden_pago_Info info)
        {
            try
            {
                oData = new cp_orden_pago_Data();
                if (oData.anularDB(info))
                {
                    info.info_comprobante.IdEmpresa = info.IdEmpresa;
                    if (info.IdTipo_op != "FACT_PROVEE")
                        bus_contabilidad.anularDB(info.info_comprobante);
                    
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string validar(cp_orden_pago_Info info)
        {
            try
            {
                string mensaje = "";

                if (info.detalle == null)
                    mensaje = "No existe detalle de pago";
                if (info.detalle.Count() == 0)
                    mensaje = "No existe detalle de pago";

                if (info.info_comprobante.lst_ct_cbtecble_det == null)
                    mensaje = "No existe diario contable";
                if (info.info_comprobante.lst_ct_cbtecble_det.Count() == 0)
                    mensaje = "No existe diario contable";

                if (Math.Round(info.info_comprobante.lst_ct_cbtecble_det.Sum(v=>v.dc_Valor), 2)!=0)                    
                    mensaje = "El diario contable esta descudrado";
                if (info.IdEstadoAprobacion == null)
                    mensaje = "Falta esta aprovación en tipo OP";
                if (info.detalle == null)
                    mensaje = "Falta tipo comprobante contable en tipo OP";
                foreach (var item in info.info_comprobante.lst_ct_cbtecble_det)
                {
                    if (item.IdCtaCble==null | item.IdCtaCble == "")
                        mensaje = "Falta cuenta contable";
                }

                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean guardar_op_x_fpDB(cp_orden_pago_Info info)
        {
            try
            {                              
                oData.guardarDB(info);                
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool aprobarOP(int IdEmpresa, string[] Lista, string MotivoAprobacion, string IdFormaPago, string IdUsuarioAprobacion)
        {
            try
            {
                return oData.aprobarOP(IdEmpresa, Lista, MotivoAprobacion, IdFormaPago, IdUsuarioAprobacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool rechazarOP(int IdEmpresa, string[] Lista, string MotivoAprobacion, string IdUsuarioAprobacion)
        {
            try
            {
                return oData.rechazarOP(IdEmpresa, Lista, MotivoAprobacion, IdUsuarioAprobacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
