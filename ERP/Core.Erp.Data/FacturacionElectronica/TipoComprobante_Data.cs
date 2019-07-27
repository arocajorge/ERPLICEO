using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.FacturacionElectronica.Factura_V2;
using Core.Erp.Info.FacturacionElectronica.GuiaRemision;
using Core.Erp.Info.FacturacionElectronica.NotaCredito;
using Core.Erp.Info.FacturacionElectronica.NotaDebito;
using Core.Erp.Info.FacturacionElectronica.Retencion;
using Core.Erp.Info.FacturacionElectronica;
using System.Text.RegularExpressions;
using Core.Erp.Info.Helps;

namespace Core.Erp.Data.FacturacionElectronica
{
    public class TipoComprobante_Data
    {


        public TipoComprobante_Info get_info_comprobante_a_generar()
        {
            try
            {
                DateTime fi = DateTime.Now.AddMonths(-1);
                DateTime ff = DateTime.Now.Date;
                TipoComprobante_Info info = new TipoComprobante_Info();
                using (Entity_facturacion_electronica context=new Entity_facturacion_electronica())
                {
                    var entity = context.vwfe_documentos_peniente_enviar_sri.Where(v=>v.vt_fecha>=fi && v.vt_fecha<=ff).FirstOrDefault();
                    if(entity!=null)
                    {
                        info.TipoDocumento= (cl_enumeradores.eTipoDocumento)Enum.Parse(typeof(cl_enumeradores.eTipoDocumento), entity.TipoDocumento);
                        info.IdCbteVta = entity.IdCbteVta;
                        info.IdEmpresa = entity.IdEmpresa;
                        info.IdSucursal = entity.IdSucursal;
                        info.IdBodega = entity.IdBodega;

                    }
                }


                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public factura get_info_factura(TipoComprobante_Info info_)
        {

           
            factura myObject = new factura();
            #region Facturas Fixed

            try
            {
                using (Entity_facturacion_electronica context = new Entity_facturacion_electronica())
                {
                    var item = context.vwfe_factura.Where(v => v.IdEmpresa == info_.IdEmpresa && v.IdCbteVta == info_.IdCbteVta && v.IdBodega == info_.IdBodega && v.IdSucursal == info_.IdSucursal).FirstOrDefault();
                    if (item != null)
                    {
                        myObject.version = "1.1.0";
                        myObject.id = facturaID.comprobante;
                        myObject.idSpecified = true;
                        infoTributaria info = new infoTributaria();
                        myObject.infoFactura = new facturaInfoFactura();
                        myObject.infoFactura.totalConImpuestos = new List<facturaInfoFacturaTotalImpuesto>();
                        myObject.infoFactura.pagos = new List<pagosPago>();
                        pagosPago Pago = new pagosPago();
                        myObject.infoTributaria = info;
                        myObject.detalles = new List<facturaDetalle>();
                        facturaInfoFacturaTotalImpuesto impuesto = null;
                        info.ambiente = "1";
                        myObject.infoTributaria.tipoEmision = "1";
                        myObject.infoTributaria.razonSocial = item.RazonSocial;
                        myObject.infoTributaria.nombreComercial = item.NombreComercial;
                        myObject.infoTributaria.ruc = item.em_ruc;
                        myObject.infoTributaria.claveAcceso = "0000000000000000000000000000000000000000000000000";
                        myObject.infoTributaria.codDoc = "01";
                        myObject.infoTributaria.estab = item.vt_serie1;
                        myObject.infoTributaria.ptoEmi = item.vt_serie2;
                        myObject.infoTributaria.secuencial = item.vt_NumFactura;
                        myObject.infoTributaria.dirMatriz = item.em_direccion;
                        myObject.infoFactura.fechaEmision = string.Format("{0:dd/MM/yyyy}", item.vt_fecha);
                        myObject.infoFactura.dirEstablecimiento = item.em_direccion;
                        myObject.infoFactura.obligadoContabilidadSpecified = true;
                        myObject.infoFactura.obligadoContabilidad = obligadoContabilidad.SI;
                        if (item.IdTipoDocumento == "RUC")
                            myObject.infoFactura.tipoIdentificacionComprador = "04";

                        if (item.IdTipoDocumento == "PAS")
                            myObject.infoFactura.tipoIdentificacionComprador = "06";

                        if (item.IdTipoDocumento == "CED")
                            myObject.infoFactura.tipoIdentificacionComprador = "05";

                        myObject.infoFactura.razonSocialComprador = (item.Nombres.ToString().Replace("S.A", "")).Trim();
                        myObject.infoFactura.identificacionComprador = item.pe_cedulaRuc;
                        myObject.infoFactura.direccionComprador = item.Direccion;
                        myObject.infoFactura.totalSinImpuestos = Convert.ToDecimal(item.total_sin_impuesto);
                        myObject.infoFactura.totalDescuento = Convert.ToDecimal(item.totalDescuento);

                        //campos de propina
                        myObject.infoFactura.propinaSpecified = true;
                        myObject.infoFactura.propina = 0;

                        //valor total de la factura
                        myObject.infoFactura.importeTotal = Convert.ToDecimal(item.importeTotal);
                        myObject.infoFactura.moneda = "DOLAR";

                        //forma de pago quemada por decisión del cliente, siempre va a usar esta forma de pago
                        Pago.formaPago = item.IdFormaPago;
                        Pago.total = Convert.ToDecimal(item.importeTotal);
                        Pago.plazoSpecified = true;
                        Pago.plazo = item.Dias_Vct;
                        Pago.unidadTiempo = "Días";
                        myObject.infoFactura.pagos.Add(Pago);
                        var facturas_imuestos = context.vwfe_factura_impuestos.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdCbteVta == item.IdCbteVta);
                        foreach (var item_imp in facturas_imuestos)
                        {
                            impuesto = new facturaInfoFacturaTotalImpuesto();
                            impuesto.codigo = "2";
                            if (item_imp.vt_por_iva == 0)
                            {
                                impuesto.baseImponible = Convert.ToDecimal("0.00");
                                impuesto.codigoPorcentaje = "1";
                            }
                            if (item_imp.vt_por_iva == 12)
                            {
                                impuesto.baseImponible = Convert.ToDecimal(item_imp.Base_imponible);
                                impuesto.codigoPorcentaje = "2";
                            }
                            if (item_imp.vt_por_iva == 14)
                            {
                                impuesto.codigoPorcentaje = "3";
                                impuesto.baseImponible = Convert.ToDecimal(item_imp.Base_imponible);
                            }
                            impuesto.valor = Convert.ToDecimal(item_imp.impuesto);
                            myObject.infoFactura.totalConImpuestos.Add(impuesto);
                        }


                        var facturas_detalle = context.vwfe_factura_detalle.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdCbteVta == item.IdCbteVta);
                        decimal totalDescuento = 0;
                        foreach (var item_det in facturas_detalle)
                        {

                            Info.FacturacionElectronica.Factura_V2.impuesto imp = new Info.FacturacionElectronica.Factura_V2.impuesto();
                            facturaDetalle fDetalle = new facturaDetalle();
                            fDetalle.codigoPrincipal = item_det.pr_codigo;
                            fDetalle.codigoAuxiliar = item_det.pr_codigo;
                            fDetalle.descripcion = item_det.pr_descripcion;
                            fDetalle.cantidad = Convert.ToDecimal(item_det.vt_cantidad);
                            fDetalle.precioUnitario = Convert.ToDecimal(item_det.vt_Precio);
                            fDetalle.descuento = Convert.ToDecimal(Convert.ToDecimal(item_det.vt_cantidad) * item_det.vt_DescUnitario);
                            totalDescuento = totalDescuento + fDetalle.descuento;
                            fDetalle.precioTotalSinImpuesto = Convert.ToDecimal(item_det.vt_Subtotal);
                            if (item_det.vt_por_iva == 12)
                            {
                                imp.codigo = "2";
                                imp.codigoPorcentaje = "2";
                                imp.tarifa = Convert.ToDecimal(item_det.vt_por_iva);
                                imp.baseImponible = Convert.ToDecimal(item_det.vt_Subtotal);
                                imp.valor = Convert.ToDecimal(item_det.vt_iva);
                            }

                            if (item_det.vt_por_iva == 14)
                            {
                                imp.codigo = "2";
                                imp.codigoPorcentaje = "3";
                                imp.tarifa = Convert.ToDecimal(item_det.vt_por_iva);
                                imp.baseImponible = Convert.ToDecimal(item_det.vt_Subtotal);
                                imp.valor = Convert.ToDecimal(item_det.vt_iva);
                            }
                            if (item_det.vt_por_iva == 0)
                            {
                                imp.codigo = "2";
                                imp.codigoPorcentaje = "0";
                                imp.tarifa = Convert.ToDecimal(item_det.vt_por_iva);
                                imp.baseImponible = Convert.ToDecimal(item_det.vt_Subtotal);
                                imp.valor = Convert.ToDecimal(item_det.vt_iva);
                            }

                            fDetalle.impuestos = new List<Info.FacturacionElectronica.Factura_V2.impuesto>();
                            fDetalle.impuestos.Add(imp);
                            myObject.detalles.Add(fDetalle);
                        }

                        myObject.infoFactura.totalDescuento = totalDescuento;
                        // campos adicionales 
                        if (item.Correo != null)
                        {
                            if (validar_correo(item.Correo) == true)
                            {
                                myObject.infoAdicional = new List<facturaCampoAdicional>();
                                facturaCampoAdicional compoadicional = new facturaCampoAdicional();
                                compoadicional.nombre = "MAIL";
                                compoadicional.Value = item.Correo;
                                myObject.infoAdicional.Add(compoadicional);
                            }
                        }



                    }
                    #region Actualizando estado a generado
                    using (Entities_facturacion entity_fac = new Entities_facturacion())
                    {
                        fa_factura Entity = entity_fac.fa_factura.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdBodega == item.IdBodega && q.IdCbteVta == item.IdCbteVta);

                        if (Entity != null)
                            Entity.Generado = true;
                        entity_fac.SaveChanges();
                    }
                    #endregion
                }


                return myObject;
            }
            catch (Exception)
            {
                return myObject;
            }





            #endregion

           

        }

        public comprobanteRetencion get_info_retencion(TipoComprobante_Info info_)
        {


            
            comprobanteRetencion myObjectRete = new comprobanteRetencion();
            #region Retenciones
            try
            {
                using (Entity_facturacion_electronica Context = new Entity_facturacion_electronica())
                {
                    var item = Context.vwfe_retencion.Where(v => v.IdEmpresa == info_.IdEmpresa && v.IdRetencion == info_.IdCbteVta).FirstOrDefault();

                    if (item != null)
                    {
                        myObjectRete.id = comprobanteRetencionID.comprobante;
                        myObjectRete.version = "1.0.0";
                        myObjectRete.idSpecified = true;
                        myObjectRete.infoTributaria = new infoTributaria();
                        myObjectRete.infoCompRetencion = new comprobanteRetencionInfoCompRetencion();
                        myObjectRete.impuestos = new List<Info.FacturacionElectronica.Retencion.impuesto>();
                        myObjectRete.infoTributaria.ambiente = "1";
                        myObjectRete.infoTributaria.tipoEmision = "1";
                        myObjectRete.infoTributaria.razonSocial = item.RazonSocial;
                        myObjectRete.infoTributaria.nombreComercial = item.NombreComercial;
                        myObjectRete.infoTributaria.ruc = item.em_ruc;
                        myObjectRete.infoTributaria.claveAcceso = "0000000000000000000000000000000000000000000000000";
                        myObjectRete.infoTributaria.codDoc = "07";
                        myObjectRete.infoTributaria.estab = item.serie1;
                        myObjectRete.infoTributaria.ptoEmi = item.serie2;
                        myObjectRete.infoTributaria.secuencial = Convert.ToString(item.NumRetencion);
                        myObjectRete.infoTributaria.dirMatriz = item.em_direccion;
                        myObjectRete.infoCompRetencion.fechaEmision = string.Format("{0:dd/MM/yyyy}", item.fecha);//.Trim();
                        myObjectRete.infoCompRetencion.dirEstablecimiento = item.em_direccion;
                        //if (item.ContribuyenteEspecial != "0000")
                        //{
                        //    myObjectRete.infoCompRetencion.contribuyenteEspecial = item.ContribuyenteEspecial;
                        //} 
                        myObjectRete.infoCompRetencion.obligadoContabilidad = "SI";
                        if (item.IdTipoDocumento == "RUC")
                            myObjectRete.infoCompRetencion.tipoIdentificacionSujetoRetenido = "04";

                        if (item.IdTipoDocumento == "PAS")
                            myObjectRete.infoCompRetencion.tipoIdentificacionSujetoRetenido = "06";

                        if (item.IdTipoDocumento == "CED")
                            myObjectRete.infoCompRetencion.tipoIdentificacionSujetoRetenido = "05";
                        myObjectRete.infoCompRetencion.razonSocialSujetoRetenido = (item.pe_nombreCompleto.ToString().Replace("S.A", "")).Trim();
                        myObjectRete.infoCompRetencion.identificacionSujetoRetenido = Convert.ToString(item.pe_cedulaRuc);
                        myObjectRete.infoCompRetencion.periodoFiscal = Convert.ToString(myObjectRete.infoCompRetencion.fechaEmision).Substring(3, 7);
                        // sentencia para extraer detalle de retencion
                        var retencion_det = Context.vwfe_retencion_detalle.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdRetencion == item.IdRetencion).ToList();

                        foreach (var item_det in retencion_det)
                        {
                            Info.FacturacionElectronica.Retencion.impuesto imp = new Info.FacturacionElectronica.Retencion.impuesto();
                            if (item_det.re_tipoRet == "RTF")
                                imp.codigo = "1";
                            else
                                imp.codigo = "2";
                            if (item_det.re_tipoRet != "RTF")
                            {
                                if (item_det.re_Porcen_retencion == 10)
                                    imp.codigoRetencion = "09";
                                if (item_det.re_Porcen_retencion == 20)
                                    imp.codigoRetencion = "10";
                                if (item_det.re_Porcen_retencion == 30)
                                    imp.codigoRetencion = "1";
                                if (item_det.re_Porcen_retencion == 50)
                                    imp.codigoRetencion = "11";
                                if (item_det.re_Porcen_retencion == 70)
                                    imp.codigoRetencion = "2";
                                if (item_det.re_Porcen_retencion == 100)
                                    imp.codigoRetencion = "3";
                            }
                            else
                            {
                                imp.codigoRetencion = item_det.re_Codigo_impuesto;
                            }
                            imp.baseImponible = Convert.ToDecimal(item_det.baseRetencion);
                            imp.porcentajeRetener = Convert.ToDecimal(item_det.re_Porcen_retencion);
                            imp.valorRetenido = Convert.ToDecimal(item_det.re_valor_retencion);
                            imp.codDocSustento = item.IdOrden_giro_Tipo.ToString();
                            imp.numDocSustento = item.co_serie.Substring(0, 3) + item.co_serie.Substring(4, 3) + item.co_factura;
                            imp.fechaEmisionDocSustento = string.Format("{0:dd/MM/yyyy}", item.co_fechaOg);//.Trim();
                            myObjectRete.impuestos.Add(imp);
                        }
                        // campos adicionales 
                        if (item.pe_correo == null)
                            item.pe_correo = "";

                        if (validar_correo(item.pe_correo) == true)
                        {
                            myObjectRete.infoAdicional = new List<comprobanteRetencionCampoAdicional>();
                            comprobanteRetencionCampoAdicional compoadicional = new comprobanteRetencionCampoAdicional();
                            compoadicional.nombre = "MAIL";
                            compoadicional.Value = item.pe_correo;
                            myObjectRete.infoAdicional.Add(compoadicional);
                        }
                    }


                    #region Actualizando estado a generado
                    using (Entities_cuentas_por_pagar entity_ret = new Entities_cuentas_por_pagar())
                    {
                        cp_retencion Entity = entity_ret.cp_retencion.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdRetencion == item.IdRetencion );

                        if (Entity != null)
                            Entity.Generado = true;
                        entity_ret.SaveChanges();
                    }
                    #endregion
                }
                return myObjectRete;
            }
            catch (Exception)
            {
                return myObjectRete;
            }


            #endregion

        }

        public guiaRemision get_info_guia(TipoComprobante_Info info_)
        {


           
            guiaRemision myObject = new guiaRemision();
            #region Guia de remision
            try
            {
                using (Entity_facturacion_electronica context = new Entity_facturacion_electronica())
                {
                    var item = context.vwfe_guia_remision.Where(v => v.IdEmpresa == info_.IdEmpresa && v.IdGuiaRemision == info_.IdCbteVta && v.IdBodega==info_.IdBodega && v.IdSucursal==info_.IdSucursal).FirstOrDefault();
                    if (item != null)
                    {
                        myObject.version = "1.1.0";
                        myObject.id = guiaRemisionID.comprobante;
                        infoTributaria info = new infoTributaria();
                        myObject.infoGuiaRemision = new guiaRemisionInfoGuiaRemision();
                        destinatario destinatario = new destinatario();
                        myObject.infoTributaria = info;
                        myObject.destinatarios = new guiaRemisionDestinatarios();
                        info.ambiente = "1";
                        myObject.infoTributaria.tipoEmision = "1";
                        myObject.infoTributaria.razonSocial = item.RazonSocial;
                        myObject.infoTributaria.nombreComercial = item.NombreComercial;
                        myObject.infoTributaria.ruc = item.em_ruc;
                        myObject.infoTributaria.claveAcceso = "0000000000000000000000000000000000000000000000000";
                        //*********************************************************************************
                        myObject.infoTributaria.codDoc = "06";
                        myObject.infoTributaria.estab = item.Serie1;
                        myObject.infoTributaria.ptoEmi = item.Serie2;
                        myObject.infoTributaria.secuencial = item.NumGuia_Preimpresa;
                        myObject.infoTributaria.dirMatriz = item.em_direccion;
                        myObject.infoGuiaRemision.dirEstablecimiento = item.em_direccion;
                        myObject.infoGuiaRemision.dirPartida = item.Direccion_Origen;
                        myObject.infoGuiaRemision.razonSocialTransportista = item.Nombre;
                        myObject.infoGuiaRemision.rucTransportista = item.Cedula;
                        myObject.infoGuiaRemision.placa = item.placa;
                        if (item.Cedula.Length == 10)
                            myObject.infoGuiaRemision.tipoIdentificacionTransportista = "05";

                        if (item.Cedula.Length == 13)
                            myObject.infoGuiaRemision.tipoIdentificacionTransportista = "04";

                        myObject.infoGuiaRemision.fechaIniTransporte = string.Format("{0:dd/MM/yyyy}", item.gi_FechaInicioTraslado);
                        myObject.infoGuiaRemision.fechaFinTransporte = string.Format("{0:dd/MM/yyyy}", item.gi_FechaFinTraslado);
                        myObject.infoGuiaRemision.obligadoContabilidad = obligadoContabilidad.SI.ToString();
                        if (item.ContribuyenteEspecial == "S")
                        {
                            myObject.infoGuiaRemision.contribuyenteEspecial = "1234";
                        }

                        var facturas_x_guias = context.vwfe_guia_remision_x_factura.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdGuiaRemision == item.IdGuiaRemision).ToList();
                        myObject.destinatarios.destinatario = new List<destinatario>();
                        foreach (var item_fac in facturas_x_guias)
                        {
                            destinatario.identificacionDestinatario = item.pe_cedulaRuc;
                            destinatario.razonSocialDestinatario = item.pe_nombreCompleto;
                            destinatario.dirDestinatario = item.Direccion;
                            destinatario.motivoTraslado = item.gi_Observacion;
                            destinatario.codEstabDestino = item_fac.vt_serie1;
                            destinatario.ruta = item.ruta;
                            destinatario.codDocSustento = "01";
                            destinatario.numDocSustento = item_fac.vt_serie1 + "-" + item_fac.vt_serie2 + "-" + item_fac.vt_NumFactura;
                            destinatario.numAutDocSustento = item_fac.vt_autorizacion;
                            destinatario.fechaEmisionDocSustento = string.Format("{0:dd/MM/yyyy}", item.gi_fecha);
                            myObject.destinatarios.destinatario.Add(destinatario);
                        }

                        var guia_detalle = context.vwfe_guia_remision_detalle.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdGuiaRemision == item.IdGuiaRemision).ToList();
                        destinatario.detalles = new destinatarioDetalles();
                        destinatario.detalles.detalle = new List<detalle>();
                        foreach (var item_det in guia_detalle)
                        {
                            destinatario.detalles.detalle.Add(new detalle
                            {
                                codigoAdicional = item_det.pr_codigo,
                                codigoInterno = item_det.pr_codigo,
                                descripcion = item_det.pr_descripcion,
                                cantidad = Convert.ToDecimal(item_det.gi_cantidad),


                            });
                        }
                        // campos adicionales 

                        myObject.infoAdicional = new List<guiaRemisionCampoAdicional>();
                        if (item.Correo != null)
                        {
                            if (validar_correo(item.Correo) == true)
                            {
                                guiaRemisionCampoAdicional compoadicional = new guiaRemisionCampoAdicional();
                                compoadicional.nombre = "MAIL";
                                compoadicional.Value = item.Correo;
                                myObject.infoAdicional.Add(compoadicional);
                            }
                        }
                        if (item.Telefono != null)
                        {
                            guiaRemisionCampoAdicional compoadicional = new guiaRemisionCampoAdicional();
                            compoadicional.nombre = "TELEFONO";
                            compoadicional.Value = item.Telefono;
                            myObject.infoAdicional.Add(compoadicional);
                        }

                    }

                    #region Actualizando estado a generado
                    using (Entities_facturacion entity_fac = new Entities_facturacion())
                    {
                        fa_guia_remision Entity = entity_fac.fa_guia_remision.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdGuiaRemision == item.IdGuiaRemision);

                        if (Entity != null)
                            Entity.Generado = true;
                        entity_fac.SaveChanges();
                    }
                    #endregion
                }
                return myObject;
            }
            catch (Exception)
            {
                return myObject;
            }
            #endregion


        }

        public notaCredito get_info_nota_credito(TipoComprobante_Info info_)
        {
        
           
                notaCredito myObject = new notaCredito();
                #region Nota credito
                try
                {
                using (Entity_facturacion_electronica context = new Entity_facturacion_electronica())
                {
                    var nota_credito = context.vwfe_nota_credito.Where(v => v.IdEmpresa == info_.IdEmpresa && v.IdNota== info_.IdCbteVta && v.IdBodega== info_.IdBodega && v.IdSucursal== info_.IdSucursal);
                    foreach (var item in nota_credito)
                    {


                        totalConImpuestosTotalImpuesto impuesto = null;
                        myObject.version = "1.1.0";
                        myObject.id = new notaCreditoID();
                        infoTributaria info = new infoTributaria();
                        myObject.infoNotaCredito = new notaCreditoInfoNotaCredito();
                        myObject.infoNotaCredito.totalConImpuestos = new List<totalConImpuestosTotalImpuesto>();
                        myObject.infoTributaria = info;
                        myObject.detalles = new List<notaCreditoDetalle>();
                        info.ambiente = "1";
                        myObject.infoTributaria.tipoEmision = "1";
                        myObject.infoTributaria.razonSocial = item.NombreComercial.Trim().ToString().Replace("S.A", ""); ;
                        myObject.infoTributaria.nombreComercial = item.NombreComercial.Trim().ToString().Replace("S.A", ""); ;
                        myObject.infoTributaria.ruc = item.em_ruc;
                        myObject.infoTributaria.claveAcceso = "0000000000000000000000000000000000000000000000000";
                        //*********************************************************************************
                        myObject.infoTributaria.codDoc = "04";
                        myObject.infoTributaria.estab = item.Serie1;
                        myObject.infoTributaria.ptoEmi = item.Serie2;
                        myObject.infoTributaria.secuencial = item.NumNota_Impresa;
                        myObject.infoTributaria.dirMatriz = item.em_direccion;
                        myObject.infoNotaCredito.fechaEmision = string.Format("{0:dd/MM/yyyy}", item.no_fecha);
                        myObject.infoNotaCredito.dirEstablecimiento = item.em_direccion;
                        //if(item.ContribuyenteEspecial=="S")
                        //myObject.infoNotaCredito.contribuyenteEspecial = "00000";
                        myObject.infoNotaCredito.obligadoContabilidad = obligadoContabilidad.SI.ToString();
                        myObject.infoNotaCredito.codDocModificado = "01";
                        myObject.infoNotaCredito.numDocModificado = item.vt_serie1 + "-" + item.vt_serie2 + "-" + item.vt_NumFactura;
                        myObject.infoNotaCredito.fechaEmisionDocSustento = string.Format("{0:dd/MM/yyyy}", item.vt_fecha);
                        myObject.infoNotaCredito.motivo = item.sc_observacion;
                        if (item.IdTipoDocumento == "RUC")
                            myObject.infoNotaCredito.tipoIdentificacionComprador = "04";

                        if (item.IdTipoDocumento == "PAS")
                            myObject.infoNotaCredito.tipoIdentificacionComprador = "06";

                        if (item.IdTipoDocumento == "CED")
                            myObject.infoNotaCredito.tipoIdentificacionComprador = "05";

                        myObject.infoNotaCredito.razonSocialComprador = item.pe_nombreCompleto.ToString().Replace("S.A", "").Trim();
                        myObject.infoNotaCredito.identificacionComprador = item.pe_cedulaRuc;
                        myObject.infoNotaCredito.dirEstablecimiento = item.em_direccion;
                        myObject.infoNotaCredito.totalSinImpuestos = Convert.ToDecimal(item.total_sin_impuesto);
                        myObject.infoNotaCredito.valorModificacion = Convert.ToDecimal(item.importeTotal);

                        //valor total de la factura
                        myObject.infoNotaCredito.valorModificacion = Convert.ToDecimal(item.importeTotal);
                        myObject.infoNotaCredito.moneda = "DOLAR";


                        var facturas_imuestos = context.vwfe_nota_credito_impuestos.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdNota == item.IdNota).ToList();
                        foreach (var item_imp in facturas_imuestos)
                        {
                            impuesto = new totalConImpuestosTotalImpuesto();
                            impuesto.codigo = "2";
                            if (item_imp.vt_por_iva == 0)
                                impuesto.codigoPorcentaje = "1";
                            if (item_imp.vt_por_iva == 12)
                                impuesto.codigoPorcentaje = "2";
                            if (item_imp.vt_por_iva == 14)
                                impuesto.codigoPorcentaje = "3";
                            impuesto.baseImponible = Convert.ToDecimal(item_imp.Base_imponible);
                            impuesto.valor = Convert.ToDecimal(item_imp.impuesto);
                            myObject.infoNotaCredito.totalConImpuestos.Add(impuesto);
                        }


                        var facturas_detalle = context.vwfe_nota_credito_detalle.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdNota == item.IdNota).ToList();

                        foreach (var item_det in facturas_detalle)
                        {

                            Info.FacturacionElectronica.Factura_V2.impuesto imp = new Info.FacturacionElectronica.Factura_V2.impuesto();
                            notaCreditoDetalle fDetalle = new notaCreditoDetalle();
                            fDetalle.codigoInterno = item_det.pr_codigo;
                            fDetalle.codigoAdicional = item_det.pr_codigo;
                            fDetalle.descripcion = item_det.pr_descripcion;
                            fDetalle.cantidad = Convert.ToDecimal(item_det.sc_cantidad);
                            fDetalle.precioUnitario = Convert.ToDecimal(item_det.sc_Precio);
                            fDetalle.descuento = Convert.ToDecimal(item_det.sc_descUni * item_det.sc_cantidad);
                            fDetalle.descuentoSpecified = true;
                            fDetalle.precioTotalSinImpuesto = Convert.ToDecimal(item_det.sc_subtotal);
                            if (item_det.vt_por_iva == 12)
                            {
                                imp.codigo = "2";
                                imp.codigoPorcentaje = "2";
                                imp.tarifa = Convert.ToDecimal(item_det.vt_por_iva);
                                imp.baseImponible = Convert.ToDecimal(item_det.sc_subtotal);
                                imp.valor = Convert.ToDecimal(item_det.sc_iva);

                            }

                            if (item_det.vt_por_iva == 14)
                            {
                                imp.codigo = "2";
                                imp.codigoPorcentaje = "3";
                                imp.tarifa = Convert.ToDecimal(item_det.vt_por_iva);
                                imp.baseImponible = Convert.ToDecimal(item_det.sc_subtotal);
                                imp.valor = Convert.ToDecimal(item_det.sc_iva);

                            }
                            if (item_det.vt_por_iva == 0)
                            {
                                imp.codigo = "2";
                                imp.codigoPorcentaje = "0";
                                imp.tarifa = Convert.ToDecimal(item_det.vt_por_iva);
                                imp.baseImponible = Convert.ToDecimal(item_det.sc_subtotal);
                                imp.valor = Convert.ToDecimal(item_det.sc_iva);

                            }

                            fDetalle.impuestos = new List<Info.FacturacionElectronica.Factura_V2.impuesto>();
                            fDetalle.impuestos.Add(imp);
                            myObject.detalles.Add(fDetalle);
                        }


                        // campos adicionales 

                        myObject.infoAdicional = new List<notaCreditoCampoAdicional>();
                        if (item.Correo != null)
                        {
                            if (validar_correo(item.Correo) == true)
                            {
                                notaCreditoCampoAdicional compoadicional = new notaCreditoCampoAdicional();
                                compoadicional.nombre = "MAIL";
                                compoadicional.Value = item.Correo;
                                myObject.infoAdicional.Add(compoadicional);
                            }
                        }


                        #region Actualizando estado a generado
                        using (Entities_facturacion entity_fac = new Entities_facturacion())
                        {
                            fa_notaCreDeb Entity = entity_fac.fa_notaCreDeb.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdNota == item.IdNota && q.IdSucursal==item.IdSucursal && q.IdBodega==item.IdBodega);

                            if (Entity != null)
                                Entity.Generado = true;
                            entity_fac.SaveChanges();
                        }
                        #endregion
                    }



                }
                return myObject;
            }
            catch (Exception)
            {
                    return myObject;
            }

            #endregion
            
        }

        public notaDebito get_info_nota_debito(TipoComprobante_Info info_)
        {
  

          
            notaDebito myObject = new notaDebito();
                #region Nota debito

                try
                {
                using (Entity_facturacion_electronica context = new Entity_facturacion_electronica())
                {
                    var item = context.vwfe_nota_credito.Where(v => v.IdEmpresa == info_.IdEmpresa && v.IdNota == info_.IdCbteVta && v.IdBodega == info_.IdBodega && v.IdSucursal == info_.IdSucursal).FirstOrDefault();
                    if(item!=null)
                    {
                        myObject.version = "1.0.0";
                        myObject.id = new notaDebitoID();
                        infoTributaria info = new infoTributaria();
                        myObject.infoNotaDebito = new notaDebitoInfoNotaDebito();
                        myObject.infoNotaDebito.impuestos = new List<Info.FacturacionElectronica.Factura_V2.impuesto>();
                        myObject.infoTributaria = info;
                        info.ambiente = "1";
                        myObject.infoTributaria.tipoEmision = "1";
                        myObject.infoTributaria.razonSocial = item.NombreComercial;
                        myObject.infoTributaria.nombreComercial = item.NombreComercial;
                        myObject.infoTributaria.ruc = item.em_ruc;
                        myObject.infoTributaria.claveAcceso = "0000000000000000000000000000000000000000000000000";
                        myObject.idSpecified = true;
                        //*********************************************************************************
                        myObject.infoTributaria.codDoc = "05";
                        myObject.infoTributaria.estab = item.Serie1;
                        myObject.infoTributaria.ptoEmi = item.Serie2;
                        myObject.infoTributaria.secuencial = item.NumNota_Impresa;
                        myObject.infoTributaria.dirMatriz = item.em_direccion;
                        myObject.infoNotaDebito.fechaEmision = string.Format("{0:dd/MM/yyyy}", item.no_fecha);
                        myObject.infoNotaDebito.dirEstablecimiento = item.em_direccion;
                        //if(item.ContribuyenteEspecial=="S")
                        //myObject.infoNotaDebito.contribuyenteEspecial = "00000";
                        myObject.infoNotaDebito.obligadoContabilidad = obligadoContabilidad.SI.ToString();
                        myObject.infoNotaDebito.codDocModificado = "01";
                        myObject.infoNotaDebito.numDocModificado = item.vt_serie1 + "-" + item.vt_serie2 + "-" + item.vt_NumFactura;
                        myObject.infoNotaDebito.fechaEmisionDocSustento = string.Format("{0:dd/MM/yyyy}", item.vt_fecha);
                        if (item.IdTipoDocumento == "RUC")
                            myObject.infoNotaDebito.tipoIdentificacionComprador = "04";

                        if (item.IdTipoDocumento == "PAS")
                            myObject.infoNotaDebito.tipoIdentificacionComprador = "06";

                        if (item.IdTipoDocumento == "CED")
                            myObject.infoNotaDebito.tipoIdentificacionComprador = "05";

                        myObject.infoNotaDebito.razonSocialComprador = item.pe_nombreCompleto.ToString().Replace("S.A", "").Trim();
                        myObject.infoNotaDebito.identificacionComprador = item.pe_cedulaRuc;
                        myObject.infoNotaDebito.dirEstablecimiento = item.em_direccion;

                        decimal totalSinImpuestos = 0;
                        decimal total = 0;


                        var facturas_imuestos = context.vwfe_nota_debito_impuestos.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdNota == item.IdNota).ToList();
                        foreach (var item_imp in facturas_imuestos)
                        {
                            if (item_imp.vt_por_iva > 0)
                            {
                                Info.FacturacionElectronica.Factura_V2.impuesto impuesto = new Info.FacturacionElectronica.Factura_V2.impuesto();
                                impuesto.codigo = "2";
                                if (item_imp.vt_por_iva == 0)
                                    impuesto.codigoPorcentaje = "1";
                                if (item_imp.vt_por_iva == 12)
                                    impuesto.codigoPorcentaje = "2";
                                if (item_imp.vt_por_iva == 14)
                                    impuesto.codigoPorcentaje = "3";
                                impuesto.tarifa = Convert.ToDecimal(item_imp.vt_por_iva);
                                impuesto.baseImponible = Convert.ToDecimal(item_imp.Base_imponible);
                                totalSinImpuestos = totalSinImpuestos + impuesto.baseImponible;
                                impuesto.valor = Convert.ToDecimal(item_imp.impuesto);
                                total = total + impuesto.baseImponible + impuesto.valor;
                                myObject.infoNotaDebito.impuestos.Add(impuesto);
                            }
                        }
                        myObject.infoNotaDebito.totalSinImpuestos = Convert.ToDecimal(totalSinImpuestos);
                        myObject.infoNotaDebito.valorTotal = total;

                        myObject.motivos = new notaDebitoMotivos();
                        myObject.motivos.motivo = new List<notaDebitoMotivosMotivo>();
                        var facturas_detalle = context.vwfe_nota_debito_detalle.Where(v => v.IdEmpresa == item.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdNota == item.IdNota && v.sc_total > 0).ToList();

                        foreach (var item_det in facturas_detalle)
                        {
                            notaDebitoMotivosMotivo motivos = new notaDebitoMotivosMotivo();
                            motivos.razon = item_det.pr_descripcion;
                            motivos.valor = Convert.ToDecimal(item_det.sc_subtotal);
                            myObject.motivos.motivo.Add(motivos);

                        }



                        myObject.infoAdicional = new List<notaDebitoCampoAdicional>();
                        if (item.Correo != null)
                        {
                            if (validar_correo(item.Correo) == true)
                            {
                                notaDebitoCampoAdicional compoadicional = new notaDebitoCampoAdicional();
                                compoadicional.nombre = "MAIL";
                                compoadicional.Value = item.Correo;
                                myObject.infoAdicional.Add(compoadicional);
                            }
                        }
                        if (item.Telefono != null)
                        {
                            notaDebitoCampoAdicional compoadicional = new notaDebitoCampoAdicional();
                            compoadicional.nombre = "TELEFONO";
                            compoadicional.Value = item.Telefono;
                            myObject.infoAdicional.Add(compoadicional);
                        }
                        #region Actualizando estado a generado
                        using (Entities_facturacion entity_fac = new Entities_facturacion())
                        {
                            fa_notaCreDeb Entity = entity_fac.fa_notaCreDeb.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdNota == item.IdNota && q.IdSucursal==item.IdSucursal && q.IdBodega==item.IdBodega);

                            if (Entity != null)
                                Entity.Generado = true;
                            entity_fac.SaveChanges();
                        }
                        #endregion
                    }


                }
                return myObject;
            }
            catch (Exception)
            {
                    return myObject;
            }

            #endregion

        }


        public factura get_facturas_eventos(TipoComprobante_Info info_)
        {
            tb_empresa info_empresa = new tb_empresa();
            tb_facturas_eventos info_factura_evento = new tb_facturas_eventos();
            decimal? vt_NumFactura = 0;
            try
            {
                using (Entity_facturacion_electronica Context_fac = new Entity_facturacion_electronica())
                {
                    vt_NumFactura = Context_fac.tb_facturas_eventos.Where(v => v.IdEmpresa == info_empresa.IdEmpresa).Max(v => v.NumFactura);

                }
                using (Entities_general Context_fac = new Entities_general())
                {
                    info_empresa = Context_fac.tb_empresa.Where(v => v.em_ruc == "0991435786001").FirstOrDefault();

                }
            }
            catch (Exception)
            {


            }



            DateTime Fi = DateTime.Now.AddMonths(-1);
            DateTime Ff = DateTime.Now.Date;
            factura myObject = new factura();
            try
            {
                using (Entity_Eventos context = new Entity_Eventos())
                {
                    var item = context.vwFacturas_fac_electronica.Where(v => v.fecha >= Fi && v.fecha <= Ff && v.estado_aprobacion == "APRO" && v.bd_est == 1).FirstOrDefault();
                    if (item != null)
                    {


                        myObject = new factura();
                        myObject.version = "1.1.0";
                        myObject.id = facturaID.comprobante;
                        myObject.idSpecified = true;
                        infoTributaria info = new infoTributaria();
                        myObject.infoFactura = new facturaInfoFactura();
                        myObject.infoFactura.totalConImpuestos = new List<facturaInfoFacturaTotalImpuesto>();
                        myObject.infoFactura.pagos = new List<pagosPago>();
                        pagosPago Pago = new pagosPago();
                        myObject.infoTributaria = info;
                        myObject.detalles = new List<facturaDetalle>();
                        facturaInfoFacturaTotalImpuesto impuesto = null;
                        info.ambiente = "1";
                        myObject.infoTributaria.tipoEmision = "1";
                        myObject.infoTributaria.razonSocial = info_empresa.RazonSocial;
                        myObject.infoTributaria.nombreComercial = info_empresa.NombreComercial;
                        myObject.infoTributaria.ruc = info_empresa.em_ruc;
                        myObject.infoTributaria.claveAcceso = "0000000000000000000000000000000000000000000000000";
                        //*********************************************************************************
                        myObject.infoTributaria.codDoc = "01";
                        myObject.infoTributaria.estab = "001";
                        myObject.infoTributaria.ptoEmi = "003";
                        myObject.infoTributaria.secuencial = vt_NumFactura.ToString().PadLeft(9, '0');
                        myObject.infoTributaria.dirMatriz = info_empresa.em_direccion;
                        myObject.infoFactura.fechaEmision = string.Format("{0:dd/MM/yyyy}", item.fecha);
                        myObject.infoFactura.dirEstablecimiento = info_empresa.em_direccion;
                        //if (item.ContribuyenteEspecial == "S")
                        //{
                        //    myObject.infoFactura.contribuyenteEspecial = "1234";
                        //}

                        myObject.infoFactura.obligadoContabilidadSpecified = true;
                        myObject.infoFactura.obligadoContabilidad = obligadoContabilidad.SI;
                        if (item.nu_ced_clte.Length == 13)
                            myObject.infoFactura.tipoIdentificacionComprador = "04";

                        else
                            myObject.infoFactura.tipoIdentificacionComprador = "05";
                        string nombre = item.nombres.Trim() + "" + item.apellidos.Trim();
                        myObject.infoFactura.razonSocialComprador = (nombre.ToString().Replace("S.A", "")).Trim();
                        myObject.infoFactura.identificacionComprador = item.nu_ced_ruc.Trim();
                        myObject.infoFactura.direccionComprador = item.direccion.Trim();
                        myObject.infoFactura.totalSinImpuestos = Convert.ToDecimal(item.subtotal);
                        myObject.infoFactura.totalDescuento = Convert.ToDecimal(0.00);

                        //campos de propina
                        myObject.infoFactura.propinaSpecified = true;
                        myObject.infoFactura.propina = 0;

                        //valor total de la factura
                        myObject.infoFactura.importeTotal = Convert.ToDecimal(item.total);
                        myObject.infoFactura.moneda = "DOLAR";

                        //forma de pago quemada por decisión del cliente, siempre va a usar esta forma de pago
                        Pago.formaPago = "20";
                        Pago.total = Convert.ToDecimal(item.total);
                        Pago.plazoSpecified = true;
                        Pago.plazo = 0;
                        Pago.unidadTiempo = "Días";
                        myObject.infoFactura.pagos.Add(Pago);


                        impuesto = new facturaInfoFacturaTotalImpuesto();
                        impuesto.codigo = "2";
                        if (item.v_iva == 0)
                        {
                            impuesto.baseImponible = Convert.ToDecimal("0.00");
                            impuesto.codigoPorcentaje = "1";
                        }
                        if (item.v_iva > 0)
                        {
                            impuesto.baseImponible = Convert.ToDecimal(item.subtotal);
                            impuesto.codigoPorcentaje = "2";
                        }

                        impuesto.valor = Convert.ToDecimal(item.v_iva);
                        myObject.infoFactura.totalConImpuestos.Add(impuesto);



                        decimal totalDescuento = 0;
                        Info.FacturacionElectronica.Factura_V2.impuesto imp = new Info.FacturacionElectronica.Factura_V2.impuesto();
                        facturaDetalle fDetalle = new facturaDetalle();
                        fDetalle.codigoPrincipal = item.cod_evento.ToString();
                        fDetalle.codigoAuxiliar = item.cod_evento.ToString();
                        fDetalle.descripcion = item.descrip;
                        fDetalle.cantidad = Convert.ToDecimal(item.cant);
                        fDetalle.precioUnitario = Convert.ToDecimal(item.v_unit);
                        fDetalle.descuento = Convert.ToDecimal("0.00");
                        totalDescuento = totalDescuento + fDetalle.descuento;
                        fDetalle.precioTotalSinImpuesto = Convert.ToDecimal(item.subtotal);
                        if (item.v_iva > 0)
                        {
                            imp.codigo = "2";
                            imp.codigoPorcentaje = "2";
                            imp.tarifa = Convert.ToDecimal(12);
                            imp.baseImponible = Convert.ToDecimal(item.subtotal);
                            imp.valor = Convert.ToDecimal(item.v_iva);

                        }
                        else
                        {
                            imp.codigo = "2";
                            imp.codigoPorcentaje = "0";
                            imp.tarifa = Convert.ToDecimal(0);
                            imp.baseImponible = Convert.ToDecimal(item.subtotal);
                            imp.valor = Convert.ToDecimal(item.v_iva);
                        }

                        fDetalle.impuestos = new List<Info.FacturacionElectronica.Factura_V2.impuesto>();
                        fDetalle.impuestos.Add(imp);
                        myObject.detalles.Add(fDetalle);


                        myObject.infoFactura.totalDescuento = Math.Round(totalDescuento, 2);
                        // campos adicionales 
                        if (item.email != null)
                        {
                            if (validar_correo(item.email) == true)
                            {
                                myObject.infoAdicional = new List<facturaCampoAdicional>();
                                facturaCampoAdicional compoadicional = new facturaCampoAdicional();
                                compoadicional.nombre = "MAIL";
                                compoadicional.Value = item.email;
                                myObject.infoAdicional.Add(compoadicional);
                            }
                        }


                        #region Actualizar estado a generado


                        try
                        {
                            using (Entity_Eventos Context_fac_sis_ext = new Entity_Eventos())
                            {
                                var entity_modificar = Context_fac_sis_ext.Facturas.Where(v => v.cod_evento == item.cod_evento && v.cod_fact == item.cod_fact).FirstOrDefault();
                                entity_modificar.estado_aprobacion = "GENE";
                                Context_fac_sis_ext.SaveChanges();


                            }
                        }
                        catch (Exception)
                        {


                        }
                        #endregion
                        #region Actualizando secuancia
                        try
                        {
                            using (Entity_facturacion_electronica Context_fac = new Entity_facturacion_electronica())
                            {
                                var entity = Context_fac.tb_facturas_eventos.Where(v => v.Evento == item.cod_evento && v.Factura == item.cod_fact).FirstOrDefault();
                                if (entity == null)
                                {
                                    tb_facturas_eventos evento = new tb_facturas_eventos()
                                    {
                                        IdEmpresa = info_empresa.IdEmpresa,
                                        Establecimiento = "001",
                                        Puntoemision = "003",
                                        NumFactura = Convert.ToInt32(vt_NumFactura),
                                        Cantidad = item.cant,
                                        ValorUnitario = item.v_unit,
                                        Subtotal = item.subtotal,
                                        Iva = item.v_iva,
                                        Total = item.total,
                                        Evento = item.cod_evento,
                                        Factura = Convert.ToInt32(item.cod_fact)
                                    };

                                    Context_fac.tb_facturas_eventos.Add(evento);
                                    Context_fac.SaveChanges();
                                }
                                else
                                {
                                    myObject.infoTributaria.secuencial = entity.NumFactura.ToString().PadLeft(9, '0');
                                }
                            }
                        }
                        catch (Exception)
                        {


                        }
                        #endregion

                        vt_NumFactura = vt_NumFactura + 1;



                    }

                }


                return myObject;
            }
            catch (Exception)
            {
                return myObject;
            }




        }

        public Boolean validar_correo(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
}
}
