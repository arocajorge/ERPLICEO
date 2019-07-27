using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Inventario;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.Inventario;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_orden_giro_Data
    {
        in_Ing_Egr_Inven_Data data_inv = new in_Ing_Egr_Inven_Data();
        public bool guardarDB(cp_orden_giro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_giro Entity = new cp_orden_giro
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                        IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                        IdOrden_giro_Tipo = info.IdOrden_giro_Tipo,
                        IdProveedor = info.IdProveedor,
                        co_fechaOg = info.co_fechaOg,
                        co_serie = info.co_serie,
                        co_factura = info.co_factura,
                        co_FechaFactura = info.co_FechaFactura,
                        co_FechaContabilizacion = info.co_FechaContabilizacion,
                        co_FechaFactura_vct = info.co_FechaFactura_vct,
                        co_plazo = info.co_plazo,
                        co_observacion = info.co_observacion,
                        co_subtotal_iva = info.co_subtotal_iva,
                        co_subtotal_siniva = info.co_subtotal_siniva,
                        co_baseImponible = info.co_baseImponible,
                        co_Por_iva = info.co_Por_iva,
                        co_valoriva = info.co_valoriva,
                        IdCod_ICE = info.IdCod_ICE,
                        co_total = info.co_total,
                        co_valorpagar = info.co_valorpagar,
                        co_vaCoa = "S",
                        IdIden_credito = info.IdIden_credito,
                        IdCod_101 = info.IdCod_101,
                        IdTipoServicio = info.IdTipoServicio,
                        IdSucursal = info.IdSucursal,
                        PagoLocExt = info.PagoLocExt,
                        PaisPago = info.PaisPago,
                        ConvenioTributacion = info.ConvenioTributacion_bool == true ? "SI" : "NO",
                        PagoSujetoRetencion = info.PagoSujetoRetencion_bool == true ? "SI" : "NO",
                        BseImpNoObjDeIva = info.BseImpNoObjDeIva,
                        fecha_autorizacion = info.fecha_autorizacion,
                        Num_Autorizacion = info.Num_Autorizacion,
                        Num_Autorizacion_Imprenta = info.Num_Autorizacion_Imprenta,
                        cp_es_comprobante_electronico = info.cp_es_comprobante_electronico,
                        Tipodoc_a_Modificar = info.Tipodoc_a_Modificar,
                        estable_a_Modificar = info.estable_a_Modificar,
                        ptoEmi_a_Modificar = info.ptoEmi_a_Modificar,
                        num_docu_Modificar = info.num_docu_Modificar,
                        aut_doc_Modificar = info.aut_doc_Modificar,
                        IdTipoMovi = info.IdTipoMovi,
                        Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now,
                        IdBodega = info.IdBodega,    
                        IdSucursal_cxp = info.IdSucursal_cxp
                    };

                    if (info.lst_det.Count > 0)
                    {
                        int secuencia = 1;
                        foreach (var item in info.lst_det)
                        {
                            Context.cp_orden_giro_det.Add(new cp_orden_giro_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                                Secuencia = secuencia++,
                                IdProducto = item.IdProducto,
                                Cantidad = item.Cantidad,
                                CostoUni = item.CostoUni,
                                PorDescuento = item.PorDescuento,
                                PorIva = item.PorIva,
                                DescuentoUni = item.DescuentoUni,
                                IdUnidadMedida = item.IdUnidadMedida,
                                CostoUniFinal = item.CostoUniFinal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Subtotal = item.Subtotal,
                                ValorIva = item.ValorIva,
                                Total = item.Total,
                                IdCtaCbleInv = item.IdCtaCbleInv,

                                IdEmpresa_oc = item.IdEmpresa_oc,
                                IdSucursal_oc = item.IdSucursal_oc,
                                IdOrdenCompra = item.IdOrdenCompra,
                                Secuencia_oc = item.Secuencia_oc
                            });
                        }

                        var movi = armar_movi_inven(info, null);

                        if (movi != null)
                        {
                            if (data_inv.guardarDB(movi, "+"))
                            {
                                Context.cp_orden_giro_x_in_Ing_Egr_Inven.Add(new cp_orden_giro_x_in_Ing_Egr_Inven
                                {
                                    og_IdEmpresa = info.IdEmpresa,
                                    og_IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                    og_IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,

                                    inv_IdEmpresa = movi.IdEmpresa,
                                    inv_IdSucursal = movi.IdSucursal,
                                    inv_IdMovi_inven_tipo = movi.IdMovi_inven_tipo,
                                    inv_IdNumMovi = movi.IdNumMovi
                                });
                            }
                        }
                    }

                    if (info.lst_det_oc.Count > 0)
                    {
                        int secuencia_det_oc = 1;
                        foreach (var item in info.lst_det_oc)
                        {
                            Context.cp_orden_giro_det_ing_x_oc.Add(new cp_orden_giro_det_ing_x_oc
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                                IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                Secuencia = secuencia_det_oc++,
                                inv_IdSucursal = item.inv_IdSucursal,
                                inv_IdMovi_inven_tipo = item.inv_IdMovi_inven_tipo,
                                inv_IdNumMovi = item.inv_IdNumMovi,
                                inv_Secuencia = item.inv_Secuencia,
                                oc_IdSucursal = item.oc_IdSucursal??0,
                                oc_IdOrdenCompra = item.oc_IdOrdenCompra??0,
                                oc_Secuencia = item.oc_Secuencia??0,
                                IdCtaCble = item.IdCtaCble,
                                dm_cantidad = item.dm_cantidad,
                                do_porc_des = item.do_porc_des,
                                do_descuento = item.do_descuento,
                                do_precioFinal = item.do_precioFinal,
                                do_subtotal = item.do_subtotal,
                                IdCod_Impuesto = item.IdCod_Impuesto,
                                do_iva = item.do_iva,
                                Por_Iva = item.Por_Iva,
                                do_total = item.do_total,
                                IdUnidadMedida = item.IdUnidadMedida,
                                IdProducto = item.IdProducto,
                                do_precioCompra = item.do_precioCompra
                            });
                        }
                    }

                    if (info.lst_det_os.Count > 0)
                    {
                        int secuencia_det_os = 1;
                        foreach (var item in info.lst_det_os)
                        {
                            Context.cp_orden_giro_det_ing_x_os.Add(new cp_orden_giro_det_ing_x_os
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                                IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                Secuencia = secuencia_det_os++,
                                oc_IdSucursal = item.oc_IdSucursal,
                                oc_IdOrdenCompra = item.oc_IdOrdenCompra,
                                oc_Secuencia = item.oc_Secuencia,
                                dm_cantidad = item.dm_cantidad,
                                do_porc_des = item.do_porc_des,
                                do_descuento = item.do_descuento,
                                do_precioFinal = item.do_precioFinal,
                                do_subtotal = item.do_subtotal,
                                IdCod_Impuesto = item.IdCod_Impuesto,
                                do_iva = item.do_iva,
                                Por_Iva = item.Por_Iva,
                                do_total = item.do_total,
                                IdUnidadMedida = item.IdUnidadMedida,
                                IdProducto = item.IdProducto,
                                do_precioCompra = item.do_precioCompra
                            });
                        }
                    }

                    Context.cp_orden_giro.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_orden_giro_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(cp_orden_giro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_giro Entity = Context.cp_orden_giro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro
                    && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro);
                    if (Entity == null) return false;
                    {
                        Entity.IdProveedor = info.IdProveedor;
                        Entity.co_fechaOg = info.co_fechaOg;
                        Entity.co_serie = info.co_serie;
                        Entity.co_factura = info.co_factura;
                        Entity.co_FechaFactura = info.co_FechaFactura;
                        Entity.co_FechaContabilizacion = info.co_FechaContabilizacion;
                        Entity.co_FechaFactura_vct = info.co_FechaFactura_vct;
                        Entity.co_plazo = info.co_plazo;
                        Entity.co_observacion = info.co_observacion;
                        Entity.co_subtotal_iva = info.co_subtotal_iva;
                        Entity.co_subtotal_siniva = info.co_subtotal_siniva;
                        Entity.co_baseImponible = info.co_baseImponible;
                        Entity.co_Por_iva = info.co_Por_iva;
                        Entity.co_valoriva = info.co_valoriva;
                        Entity.IdCod_ICE = info.IdCod_ICE;
                        Entity.co_total = info.co_total;
                        Entity.co_valorpagar = info.co_valorpagar;
                        Entity.co_vaCoa = "S";
                        Entity.IdIden_credito = info.IdIden_credito;
                        Entity.IdCod_101 = info.IdCod_101;
                        Entity.IdOrden_giro_Tipo = info.IdOrden_giro_Tipo;
                        Entity.IdTipoServicio = info.IdTipoServicio;
                        Entity.IdSucursal = info.IdSucursal;
                        Entity.PagoLocExt = info.PagoLocExt;
                        Entity.PaisPago = info.PaisPago;
                        Entity.ConvenioTributacion = info.ConvenioTributacion_bool == true ? "SI" : "NO";
                        Entity.PagoSujetoRetencion = info.PagoSujetoRetencion_bool == true ? "SI" : "NO";
                        Entity.BseImpNoObjDeIva = info.BseImpNoObjDeIva;
                        Entity.fecha_autorizacion = info.fecha_autorizacion;
                        Entity.Num_Autorizacion = info.Num_Autorizacion;
                        Entity.Num_Autorizacion_Imprenta = info.Num_Autorizacion_Imprenta;
                        Entity.cp_es_comprobante_electronico = info.cp_es_comprobante_electronico;
                        Entity.Tipodoc_a_Modificar = info.Tipodoc_a_Modificar;
                        Entity.estable_a_Modificar = info.estable_a_Modificar;
                        Entity.ptoEmi_a_Modificar = info.ptoEmi_a_Modificar;
                        Entity.num_docu_Modificar = info.num_docu_Modificar;
                        Entity.aut_doc_Modificar = info.aut_doc_Modificar;
                        Entity.IdTipoMovi = info.IdTipoMovi;
                        Entity.IdBodega = info.IdBodega;
                        Entity.IdSucursal_cxp = info.IdSucursal_cxp;
                        Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                        Entity.Fecha_UltMod = DateTime.Now;
                    }

                    if (info.lst_det.Count > 0)
                    {
                        var lst_det = Context.cp_orden_giro_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).ToList();
                        Context.cp_orden_giro_det.RemoveRange(lst_det);
                        int secuencia = 1;
                        foreach (var item in info.lst_det)
                        {
                            Context.cp_orden_giro_det.Add(new cp_orden_giro_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                                Secuencia = secuencia++,
                                IdProducto = item.IdProducto,
                                Cantidad = item.Cantidad,
                                CostoUni = item.CostoUni,
                                PorDescuento = item.PorDescuento,
                                PorIva = item.PorIva,
                                DescuentoUni = item.DescuentoUni,
                                IdUnidadMedida = item.IdUnidadMedida,
                                CostoUniFinal = item.CostoUniFinal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Subtotal = item.Subtotal,
                                ValorIva = item.ValorIva,
                                Total = item.Total,
                                IdCtaCbleInv = item.IdCtaCbleInv,
                                IdEmpresa_oc = item.IdEmpresa_oc,
                                IdSucursal_oc = item.IdSucursal_oc,
                                IdOrdenCompra = item.IdOrdenCompra,
                                Secuencia_oc = item.Secuencia_oc
                            });
                        }

                        var rel = Context.cp_orden_giro_x_in_Ing_Egr_Inven.Where(q => q.og_IdEmpresa == info.IdEmpresa && q.og_IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.og_IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).FirstOrDefault();
                        var movi = armar_movi_inven(info, rel);
                        if (movi != null)
                        {
                            if(rel == null)
                            {
                                if (data_inv.guardarDB(movi, "+"))
                                {
                                    Context.cp_orden_giro_x_in_Ing_Egr_Inven.Add(new cp_orden_giro_x_in_Ing_Egr_Inven
                                    {
                                        og_IdEmpresa = info.IdEmpresa,
                                        og_IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                        og_IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,

                                        inv_IdEmpresa = movi.IdEmpresa,
                                        inv_IdSucursal = movi.IdSucursal,
                                        inv_IdMovi_inven_tipo = movi.IdMovi_inven_tipo,
                                        inv_IdNumMovi = movi.IdNumMovi
                                    });
                                }
                            }else
                                data_inv.modificarDB(movi);
                                
                        }
                    }

                    if (info.lst_det_oc.Count > 0)
                    {
                        var lst_det_oc = Context.cp_orden_giro_det_ing_x_oc.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).ToList();
                        Context.cp_orden_giro_det_ing_x_oc.RemoveRange(lst_det_oc);

                        int secuencia_det_oc = 1;
                        foreach (var item in info.lst_det_oc)
                        {
                            Context.cp_orden_giro_det_ing_x_oc.Add(new cp_orden_giro_det_ing_x_oc
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                                IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                Secuencia = secuencia_det_oc++,
                                inv_IdSucursal = item.inv_IdSucursal,
                                inv_IdMovi_inven_tipo = item.inv_IdMovi_inven_tipo,
                                inv_IdNumMovi = item.inv_IdNumMovi,
                                inv_Secuencia = item.inv_Secuencia,
                                oc_IdSucursal = item.oc_IdSucursal??0,
                                oc_IdOrdenCompra = item.oc_IdOrdenCompra??0,
                                oc_Secuencia = item.oc_Secuencia??0,
                                IdCtaCble = item.IdCtaCble,
                                dm_cantidad = item.dm_cantidad,
                                do_porc_des = item.do_porc_des,
                                do_descuento = item.do_descuento,
                                do_precioFinal = item.do_precioFinal,
                                do_subtotal = item.do_subtotal,
                                IdCod_Impuesto = item.IdCod_Impuesto,
                                do_iva = item.do_iva,
                                Por_Iva = item.Por_Iva,
                                do_total = item.do_total,
                                IdUnidadMedida = item.IdUnidadMedida,
                                IdProducto = item.IdProducto,
                                do_precioCompra = item.do_precioCompra
                            });
                        }
                    }

                    if (info.lst_det_os.Count > 0)
                    {
                        var lst_det_os = Context.cp_orden_giro_det_ing_x_os.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).ToList();
                        Context.cp_orden_giro_det_ing_x_os.RemoveRange(lst_det_os);

                        int secuencia_det_os = 1;
                        foreach (var item in info.lst_det_os)
                        {
                            Context.cp_orden_giro_det_ing_x_os.Add(new cp_orden_giro_det_ing_x_os
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                                IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                                Secuencia = secuencia_det_os++,
                                oc_IdSucursal = item.oc_IdSucursal,
                                oc_IdOrdenCompra = item.oc_IdOrdenCompra,
                                oc_Secuencia = item.oc_Secuencia,
                                dm_cantidad = item.dm_cantidad,
                                do_porc_des = item.do_porc_des,
                                do_descuento = item.do_descuento,
                                do_precioFinal = item.do_precioFinal,
                                do_subtotal = item.do_subtotal,
                                IdCod_Impuesto = item.IdCod_Impuesto,
                                do_iva = item.do_iva,
                                Por_Iva = item.Por_Iva,
                                do_total = item.do_total,
                                IdUnidadMedida = item.IdUnidadMedida,
                                IdProducto = item.IdProducto,
                                do_precioCompra = item.do_precioCompra
                            });
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_orden_giro_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        private in_Ing_Egr_Inven_Info armar_movi_inven(cp_orden_giro_Info info, cp_orden_giro_x_in_Ing_Egr_Inven rel)
        {
            Entities_inventario db_inv = new Entities_inventario();
            try
            {
                #region Variables
                var motivo = db_inv.in_Motivo_Inven.Where(q => q.IdEmpresa == info.IdEmpresa && q.Tipo_Ing_Egr == "ING" && q.Genera_Movi_Inven == "S").FirstOrDefault();
                if (motivo == null)
                    return null;

                var param = db_inv.in_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (param == null)
                    return null;
                #endregion

                in_Ing_Egr_Inven_Info movi = new in_Ing_Egr_Inven_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = (int)info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdMotivo_Inv = motivo.IdMotivo_Inv,
                    IdNumMovi = rel == null ? 0 : rel.inv_IdNumMovi,
                    cm_fecha = info.co_fechaOg.Date,
                    cm_observacion = info.co_observacion,
                    CodMoviInven = "FXP-"+Convert.ToDecimal(info.co_factura).ToString(),
                    signo = "+",
                    IdMovi_inven_tipo = rel == null ? (int)param.P_IdMovi_inven_tipo_ingreso_x_compra : (rel.inv_IdMovi_inven_tipo),
                    Estado = "A",
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltModi = info.IdUsuario
                };

                foreach (var item in info.lst_det)
                {
                    movi.lst_in_Ing_Egr_Inven_det.Add(new in_Ing_Egr_Inven_det_Info
                    {
                        IdEmpresa = movi.IdEmpresa,
                        IdSucursal = movi.IdSucursal,
                        IdBodega = (int)movi.IdBodega,
                        IdMovi_inven_tipo = movi.IdMovi_inven_tipo,
                        IdNumMovi = movi.IdNumMovi,
                        dm_cantidad_sinConversion = item.Cantidad,
                        dm_cantidad = item.Cantidad,
                        mv_costo_sinConversion = item.CostoUniFinal,
                        mv_costo = item.CostoUniFinal,
                        IdUnidadMedida_sinConversion = item.IdUnidadMedida,
                        IdUnidadMedida = item.IdUnidadMedida,
                        IdProducto = item.IdProducto,                        
                    });
                }
                db_inv.Dispose();
                return movi;
            }
            catch (Exception)
            {
                db_inv.Dispose();
                throw;
            }
        }
        public bool anularDB(cp_orden_giro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_giro Entity = Context.cp_orden_giro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro
                    && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro);
                    if (Entity == null) return false;

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Entity.Estado = "I";

                    var rel = Context.cp_orden_giro_x_in_Ing_Egr_Inven.Where(q => q.og_IdEmpresa == info.IdEmpresa && q.og_IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.og_IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).FirstOrDefault();
                    if (rel != null)
                        data_inv.anularDB(new in_Ing_Egr_Inven_Info { IdEmpresa = rel.inv_IdEmpresa, IdSucursal = rel.inv_IdSucursal, IdMovi_inven_tipo = rel.inv_IdMovi_inven_tipo, IdNumMovi = rel.inv_IdNumMovi, IdusuarioUltAnu = info.IdUsuarioUltAnu });

                    if (info.lst_det_oc.Count > 0)
                    {
                        var lst_det_oc = Context.cp_orden_giro_det_ing_x_oc.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).ToList();
                        Context.cp_orden_giro_det_ing_x_oc.RemoveRange(lst_det_oc);
                    }

                    if (info.lst_det_os.Count > 0)
                    {
                        var lst_det_os = Context.cp_orden_giro_det_ing_x_os.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).ToList();
                        Context.cp_orden_giro_det_ing_x_os.RemoveRange(lst_det_os);
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_orden_giro_Info> get_lst(int IdEmpresa, int IdSucursal, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<cp_orden_giro_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.co_FechaFactura >= FechaInicio
                              && q.co_FechaFactura <= FechaFin
                             orderby q.IdCbteCble_Ogiro descending
                             select new cp_orden_giro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                 IdProveedor = q.IdProveedor,
                                 co_fechaOg = q.co_fechaOg,
                                 co_serie = q.co_serie,
                                 co_factura = q.co_factura,
                                 co_FechaFactura = q.co_FechaFactura,
                                 co_FechaContabilizacion = q.co_FechaContabilizacion,
                                 co_FechaFactura_vct = q.co_FechaFactura_vct,
                                 co_plazo = q.co_plazo,
                                 co_observacion = q.co_observacion,
                                 co_subtotal_iva = q.co_subtotal_iva,
                                 co_subtotal_siniva = q.co_subtotal_siniva,
                                 co_baseImponible = q.co_baseImponible,
                                 co_Por_iva = q.co_Por_iva,
                                 co_valoriva = q.co_valoriva,
                                 IdCod_ICE = q.IdCod_ICE,
                                 co_total = q.co_total,
                                 co_valorpagar = q.co_valorpagar,
                                 co_vaCoa = q.co_vaCoa,
                                 IdIden_credito = q.IdIden_credito,
                                 IdCod_101 = q.IdCod_101,
                                 IdTipoServicio = q.IdTipoServicio,
                                 IdSucursal = q.IdSucursal,
                                 PagoLocExt = q.PagoLocExt,
                                 PaisPago = q.PaisPago,
                                 ConvenioTributacion = q.ConvenioTributacion,
                                 PagoSujetoRetencion = q.PagoSujetoRetencion,
                                 BseImpNoObjDeIva = q.BseImpNoObjDeIva,
                                 fecha_autorizacion = q.fecha_autorizacion,
                                 Num_Autorizacion = q.Num_Autorizacion,
                                 Num_Autorizacion_Imprenta = q.Num_Autorizacion_Imprenta,
                                 cp_es_comprobante_electronico = q.cp_es_comprobante_electronico,
                                 Tipodoc_a_Modificar = q.Tipodoc_a_Modificar,
                                 estable_a_Modificar = q.estable_a_Modificar,
                                 ptoEmi_a_Modificar = q.ptoEmi_a_Modificar,
                                 num_docu_Modificar = q.num_docu_Modificar,
                                 aut_doc_Modificar = q.aut_doc_Modificar,
                                 IdTipoMovi = q.IdTipoMovi,
                                 Estado = q.Estado,
                                 info_proveedor = new cp_proveedor_Info
                                 {
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         pe_apellido = q.pe_apellido,
                                         pe_nombre = q.pe_nombre,
                                         pe_nombreCompleto = q.pe_nombreCompleto,
                                         pe_cedulaRuc = q.pe_cedulaRuc
                                     }
                                 },

                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();

                }
                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_orden_giro_Info> get_lst_sin_ret(int IdEmpresa, int IdSucursal, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;
                FechaInicio = FechaInicio.Date;
                FechaFin = FechaFin.Date;

                List<cp_orden_giro_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = Context.vwcp_orden_giro_SinRetencion.Where(q => q.IdEmpresa == IdEmpresa
                     && IdSucursalIni <= q.IdSucursal && q.IdSucursal <= IdSucursalFin && FechaInicio <= q.co_fechaOg && q.co_fechaOg <= FechaFin).Select(q => new cp_orden_giro_Info
                     {
                         IdEmpresa = q.IdEmpresa,
                         IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                         IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                         co_fechaOg = q.co_fechaOg,
                         co_serie = q.co_serie,
                         co_factura = q.co_factura,
                         co_observacion = q.co_observacion,
                         co_subtotal_iva = q.co_subtotal_iva,
                         co_subtotal_siniva = q.co_subtotal_siniva,
                         co_baseImponible = q.co_baseImponible,
                         co_valoriva = q.co_valoriva,
                         co_total = q.co_total,
                         IdSucursal = q.IdSucursal,

                         info_proveedor = new cp_proveedor_Info
                         {
                             info_persona = new Info.General.tb_persona_Info
                             {
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 pe_cedulaRuc = q.pe_cedulaRuc
                             }
                         }
                         
                     }).ToList();
                }
                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_orden_giro_Info> get_lst_orden_giro_x_pagar(int IdEmpresa)
        {
            try
            {
                List<cp_orden_giro_Info> Lista = null;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_x_pagar
                             where q.IdEmpresa == IdEmpresa
                             & q.Saldo_OG > 0
                             orderby q.co_fechaOg descending
                             select new cp_orden_giro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                 IdProveedor = q.IdProveedor,
                                 co_fechaOg = q.co_fechaOg,
                                 co_serie = q.co_serie,
                                 co_factura = q.cod_Documento + "-" + q.co_serie + "-" + q.co_factura,
                                 co_FechaFactura = q.co_FechaFactura,
                                 co_observacion = q.co_observacion,
                                 co_subtotal_iva = q.co_subtotal_iva,
                                 co_subtotal_siniva = q.co_subtotal_siniva,
                                 co_baseImponible = q.co_baseImponible,
                                 co_Por_iva = q.co_Por_iva,
                                 co_valoriva = q.co_valoriva,
                                 co_total = q.co_total,
                                 co_valorpagar = q.co_valorpagar,
                                 Total_Pagado = q.Total_Pagado,
                                 Saldo_OG = q.Saldo_OG,
                                 Fecha_Transac = q.co_FechaFactura_vct,
                                 nom_tipo_Documento = q.nom_tipo_Documento,
                                 info_proveedor = new cp_proveedor_Info
                                 {
                                     IdPersona = q.IdPersona,
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         pe_razonSocial = q.nom_proveedor,
                                         IdPersona = q.IdPersona,
                                         pe_nombreCompleto = q.nom_proveedor
                                     }
                                 },
                                 IdSucursal = q.IdSucursal

                             }).ToList();

                    Lista.ForEach(item =>
                {
                    item.SecuencialID = item.IdEmpresa.ToString("00") + item.IdTipoCbte_Ogiro.ToString("00") + item.IdCbteCble_Ogiro.ToString("000000000");
                    item.co_FechaFactura_vct = item.Fecha_Transac == null ? DateTime.Now.Date : Convert.ToDateTime(item.Fecha_Transac);
                    TimeSpan ts = Convert.ToDateTime(item.Fecha_Transac == null ? DateTime.Now.Date : Convert.ToDateTime(item.Fecha_Transac)) - Convert.ToDateTime(DateTime.Now);
                    int dias = ts.Days;
                    item.Dias_Vencidos = dias;
                    if (dias < 0) //Por vencer
                    {
                        item.Tipo_Vcto = "VENCIDO";
                    }
                    if (dias == 0) //normal
                    {
                        item.Tipo_Vcto = "VENCE_HOY";
                    }
                    if (dias > 0) // vencido
                    {
                        item.Tipo_Vcto = "X_VENCER";
                    }
                });
                    return Lista;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public cp_orden_giro_Info get_info(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                cp_orden_giro_Info info = new cp_orden_giro_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_giro Entity = Context.cp_orden_giro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa
                    && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                    && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro);
                    if (Entity == null) return null;
                    info = new cp_orden_giro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCbteCble_Ogiro = Entity.IdCbteCble_Ogiro,
                        IdTipoCbte_Ogiro = Entity.IdTipoCbte_Ogiro,
                        IdOrden_giro_Tipo = Entity.IdOrden_giro_Tipo,
                        IdProveedor = Entity.IdProveedor,
                        co_fechaOg = Entity.co_fechaOg,
                        co_serie = Entity.co_serie,
                        co_factura = Entity.co_factura,
                        co_FechaFactura = Entity.co_FechaFactura,
                        co_FechaContabilizacion = Entity.co_FechaContabilizacion,
                        co_FechaFactura_vct = Entity.co_FechaFactura_vct,
                        co_plazo = Entity.co_plazo,
                        co_observacion = Entity.co_observacion,
                        co_subtotal_iva = Entity.co_subtotal_iva,
                        co_subtotal_siniva = Entity.co_subtotal_siniva,
                        co_baseImponible = Entity.co_baseImponible,
                        co_Por_iva = Entity.co_Por_iva,
                        co_valoriva = Entity.co_valoriva,
                        IdCod_ICE = Entity.IdCod_ICE,
                        co_total = Entity.co_total,
                        co_valorpagar = Entity.co_valorpagar,
                        co_vaCoa = Entity.co_vaCoa,
                        IdIden_credito = Entity.IdIden_credito,
                        IdCod_101 = Entity.IdCod_101,
                        IdTipoServicio = Entity.IdTipoServicio,
                        IdSucursal = Entity.IdSucursal,
                        PagoLocExt = Entity.PagoLocExt,
                        PaisPago = Entity.PaisPago,
                        ConvenioTributacion_bool = Entity.ConvenioTributacion == "SI" ? true : false,
                        PagoSujetoRetencion_bool = Entity.PagoSujetoRetencion == "SI" ? true : false,
                        BseImpNoObjDeIva = Entity.BseImpNoObjDeIva,
                        fecha_autorizacion = Entity.fecha_autorizacion,
                        Num_Autorizacion = Entity.Num_Autorizacion,
                        Num_Autorizacion_Imprenta = Entity.Num_Autorizacion_Imprenta,
                        cp_es_comprobante_electronico = Entity.cp_es_comprobante_electronico,
                        Tipodoc_a_Modificar = Entity.Tipodoc_a_Modificar,
                        estable_a_Modificar = Entity.estable_a_Modificar,
                        ptoEmi_a_Modificar = Entity.ptoEmi_a_Modificar,
                        num_docu_Modificar = Entity.num_docu_Modificar,
                        aut_doc_Modificar = Entity.aut_doc_Modificar,
                        IdTipoMovi = Entity.IdTipoMovi,
                        Estado = Entity.Estado,
                        IdBodega = Entity.IdBodega,
                        IdSucursal_cxp = Entity.IdSucursal_cxp
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public cp_orden_giro_Info get_info_retencion(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                cp_orden_giro_Info info = new cp_orden_giro_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {   
                    vwcp_orden_giro Entity = Context.vwcp_orden_giro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa
                    && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                    && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro);
                    if (Entity == null) return null;
                    info = new cp_orden_giro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCbteCble_Ogiro = Entity.IdCbteCble_Ogiro,
                        IdTipoCbte_Ogiro = Entity.IdTipoCbte_Ogiro,
                        IdOrden_giro_Tipo = Entity.IdOrden_giro_Tipo,
                        IdProveedor = Entity.IdProveedor,
                        co_fechaOg = Entity.co_fechaOg,
                        co_serie = Entity.co_serie,
                        co_factura = Entity.co_factura,
                        co_FechaFactura = Entity.co_FechaFactura,
                        co_FechaContabilizacion = Entity.co_FechaContabilizacion,
                        co_FechaFactura_vct = Entity.co_FechaFactura_vct,
                        co_plazo = Entity.co_plazo,
                        co_observacion = Entity.co_observacion,
                        co_subtotal_iva = Entity.co_subtotal_iva,
                        co_subtotal_siniva = Entity.co_subtotal_siniva,
                        co_baseImponible = Entity.co_baseImponible,
                        co_Por_iva = Entity.co_Por_iva,
                        co_valoriva = Entity.co_valoriva,
                        IdCod_ICE = Entity.IdCod_ICE,
                        co_total = Entity.co_total,
                        co_valorpagar = Entity.co_valorpagar,
                        co_vaCoa = Entity.co_vaCoa,
                        IdIden_credito = Entity.IdIden_credito,
                        IdCod_101 = Entity.IdCod_101,
                        IdTipoServicio = Entity.IdTipoServicio,
                        IdSucursal = Entity.IdSucursal,
                        PagoLocExt = Entity.PagoLocExt,
                        PaisPago = Entity.PaisPago,
                        ConvenioTributacion_bool = Entity.ConvenioTributacion == "SI" ? true : false,
                        PagoSujetoRetencion_bool = Entity.PagoSujetoRetencion == "SI" ? true : false,
                        BseImpNoObjDeIva = Entity.BseImpNoObjDeIva,
                        fecha_autorizacion = Entity.fecha_autorizacion,
                        Num_Autorizacion = Entity.Num_Autorizacion,
                        Num_Autorizacion_Imprenta = Entity.Num_Autorizacion_Imprenta,
                        cp_es_comprobante_electronico = Entity.cp_es_comprobante_electronico,
                        Tipodoc_a_Modificar = Entity.Tipodoc_a_Modificar,
                        estable_a_Modificar = Entity.estable_a_Modificar,
                        ptoEmi_a_Modificar = Entity.ptoEmi_a_Modificar,
                        num_docu_Modificar = Entity.num_docu_Modificar,
                        aut_doc_Modificar = Entity.aut_doc_Modificar,
                        IdTipoMovi = Entity.IdTipoMovi,
                        Estado = Entity.Estado,
                        Descripcion = Entity.Descripcion,
                        info_proveedor = new cp_proveedor_Info
                        {
                            info_persona = new Info.General.tb_persona_Info
                            {
                                pe_apellido = Entity.pe_apellido,
                                pe_nombre = Entity.pe_nombre,
                                pe_nombreCompleto = Entity.pe_nombreCompleto,
                                pe_cedulaRuc = Entity.pe_cedulaRuc,
                                pe_razonSocial = Entity.pe_razonSocial
                            }
                        }
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool si_existe(cp_orden_giro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_orden_giro
                              where
                               q.IdEmpresa == info.IdEmpresa
                             && q.co_serie == info.co_serie
                              && q.co_factura == info.co_factura
                              && q.IdProveedor == info.IdProveedor
                              && q.Estado == "A"
                              select q;
                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarExisteOrdenPAgo(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = Context.cp_orden_pago_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte_cxp == IdTipoCbte && q.IdCbteCble_cxp == IdCbteCble).ToList();
                    if (lst.Count() > 0)
                        return true;
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDBCabecera(cp_orden_giro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_giro Entity = Context.cp_orden_giro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.co_observacion = info.co_observacion;
                    Entity.Tipodoc_a_Modificar = info.Tipodoc_a_Modificar;
                    Entity.co_plazo = info.co_plazo;
                    Entity.IdProveedor = info.IdProveedor;
                    Entity.IdIden_credito = info.IdIden_credito;
                    Entity.IdOrden_giro_Tipo = info.IdOrden_giro_Tipo;
                    Entity.PagoLocExt = info.PagoLocExt;
                    Entity.Num_Autorizacion= info.Num_Autorizacion;
                    Entity.co_serie = info.co_serie;
                    Entity.co_factura = info.co_factura;

                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}