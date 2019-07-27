using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Data.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Facturacion
{
    public class fa_notaCreDeb_Data
    {
        public List<fa_notaCreDeb_consulta_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, string CreDeb)
        {
            try
            {
                List<fa_notaCreDeb_consulta_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && Fecha_ini <= q.no_fecha
                             && q.no_fecha <= Fecha_fin
                             && q.CreDeb == CreDeb
                             select new fa_notaCreDeb_consulta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 CreDeb = q.CreDeb,
                                 NumNota_Impresa = q.NumNota_Impresa,
                                 no_fecha = q.no_fecha,
                                 Nombres = q.Nombres,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_iva = q.sc_iva,
                                 sc_total = q.sc_total,
                                 Estado = q.Estado,

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
        public fa_notaCreDeb_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                fa_notaCreDeb_Info info;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var Entity = Context.fa_notaCreDeb.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_notaCreDeb_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdNota = Entity.IdNota,
                        IdPuntoVta = Entity.IdPuntoVta,
                        dev_IdEmpresa = Entity.dev_IdEmpresa,
                        dev_IdDev_Inven = Entity.dev_IdDev_Inven,
                        CodNota = Entity.CodNota,
                        CreDeb = Entity.CreDeb.Trim(),
                        CodDocumentoTipo = Entity.CodDocumentoTipo,
                        Serie1 = Entity.Serie1,
                        Serie2 = Entity.Serie2,
                        NumNota_Impresa = Entity.NumNota_Impresa,
                        NumAutorizacion = Entity.NumAutorizacion,
                        Fecha_Autorizacion = Entity.Fecha_Autorizacion,
                        IdCliente = Entity.IdCliente,
                        no_fecha = Entity.no_fecha,
                        no_fecha_venc = Entity.no_fecha_venc,
                        IdTipoNota = Entity.IdTipoNota,
                        sc_observacion = Entity.sc_observacion,
                        Estado = Entity.Estado,
                        NaturalezaNota = Entity.NaturalezaNota,
                        IdCtaCble_TipoNota = Entity.IdCtaCble_TipoNota
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DocumentoExiste(int IdEmpresa, string CodDocumentoTipo, string Serie1, string Serie2, string NumNota_Impresa)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_notaCreDeb
                              where q.IdEmpresa == IdEmpresa
                              && q.CodDocumentoTipo == CodDocumentoTipo
                              && q.Serie1 == Serie1
                              && q.Serie2 == Serie2
                              && q.NumNota_Impresa == NumNota_Impresa
                              select q;

                    if (lst.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private decimal get_id(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                decimal ID = 1;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_notaCreDeb
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              && q.IdBodega == IdBodega
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNota) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(fa_notaCreDeb_Info info)
        {            
            try
            {
                #region Variables
                int Secuencia = 1;
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                cxc_cobro_Data odata_cobr = new cxc_cobro_Data();
                #endregion

                using (Entities_facturacion db_f = new Entities_facturacion())
                {
                    #region Nota de debito credito

                    #region Cabecera
                    fa_notaCreDeb Entity = new fa_notaCreDeb
                    //db_f.fa_notaCreDeb.Add(new fa_notaCreDeb
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdNota = info.IdNota = get_id(info.IdEmpresa, info.IdSucursal, info.IdBodega),
                        IdPuntoVta = info.IdPuntoVta,
                        CodNota = info.CodNota,
                        CreDeb = info.CreDeb.Trim(),
                        CodDocumentoTipo = info.CodDocumentoTipo,
                        Serie1 = info.Serie1,
                        Serie2 = info.Serie2,
                        NumNota_Impresa = info.NumNota_Impresa,
                        NumAutorizacion = info.NumAutorizacion,
                        Fecha_Autorizacion = info.Fecha_Autorizacion,
                        IdCliente = info.IdCliente,
                        no_fecha = info.no_fecha.Date,
                        no_fecha_venc = info.no_fecha_venc.Date,
                        IdTipoNota = info.IdTipoNota,
                        sc_observacion = info.sc_observacion,
                        Estado = info.Estado = "A",
                        NaturalezaNota = info.NaturalezaNota,
                        IdCtaCble_TipoNota = info.IdCtaCble_TipoNota,

                        IdUsuario = info.IdUsuario
                    };
                    #endregion

                    #region Detalle
                    foreach (var item in info.lst_det)
                    {
                        db_f.fa_notaCreDeb_det.Add(new fa_notaCreDeb_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdNota = info.IdNota,
                            Secuencia = Secuencia++,
                            IdProducto = item.IdProducto,
                            sc_cantidad = item.sc_cantidad,
                            sc_cantidad_factura=item.sc_cantidad_factura,
                            sc_Precio = item.sc_Precio,
                            sc_descUni = item.sc_descUni,
                            sc_PordescUni = item.sc_PordescUni,
                            sc_precioFinal = item.sc_precioFinal,
                            vt_por_iva = item.vt_por_iva,
                            sc_iva = item.sc_iva,
                            IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                            sc_subtotal = item.sc_subtotal,
                            sc_total = item.sc_total,
                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_Cargo = item.IdPunto_Cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        });
                    }
                    #endregion

                    #region Cruce
                    Secuencia = 1;
                    foreach (var item in info.lst_cruce)
                    {
                        db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                        {
                            IdEmpresa_nt = info.IdEmpresa,
                            IdSucursal_nt = info.IdSucursal,
                            IdBodega_nt = info.IdBodega,
                            IdNota_nt = info.IdNota,
                            secuencia = Secuencia++,
                            IdEmpresa_fac_nd_doc_mod = item.IdEmpresa_fac_nd_doc_mod,
                            IdSucursal_fac_nd_doc_mod = item.IdSucursal_fac_nd_doc_mod,
                            IdBodega_fac_nd_doc_mod = item.IdBodega_fac_nd_doc_mod,
                            IdCbteVta_fac_nd_doc_mod = item.IdCbteVta_fac_nd_doc_mod,
                            vt_tipoDoc = item.vt_tipoDoc,
                            Valor_Aplicado = item.Valor_Aplicado,
                            fecha_cruce = DateTime.Now,
                        });
                    }
                    #endregion

                    #region Talonario
                    fa_PuntoVta_Data data_puntovta = new fa_PuntoVta_Data();
                    tb_sis_Documento_Tipo_Talonario_Data data_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
                    fa_PuntoVta_Info info_puntovta = new fa_PuntoVta_Info();
                    tb_sis_Documento_Tipo_Talonario_Info ultimo_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
                    tb_sis_Documento_Tipo_Talonario_Info info_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
                    info_puntovta = data_puntovta.get_info(info.IdEmpresa, info.IdSucursal, info.IdPuntoVta);

                    if (info_puntovta != null && info.NaturalezaNota == "SRI")
                    {
                        if (info_puntovta.EsElectronico == true)
                        {
                            ultimo_talonario = data_talonario.GetUltimoNoUsado(info.IdEmpresa, info_puntovta.codDocumentoTipo, info_puntovta.Su_CodigoEstablecimiento, info_puntovta.cod_PuntoVta, info_puntovta.EsElectronico,true);

                            if (ultimo_talonario != null)
                            {
                                Entity.Serie1 = info.Serie1 = ultimo_talonario.Establecimiento;
                                Entity.Serie2 = info.Serie2 = ultimo_talonario.PuntoEmision;
                                Entity.NumNota_Impresa = info.NumNota_Impresa = ultimo_talonario.NumDocumento;
                            }
                        }
                        else
                        {
                            info_talonario.IdEmpresa = info.IdEmpresa;
                            info_talonario.CodDocumentoTipo = info.CodDocumentoTipo;
                            info_talonario.Establecimiento = info.Serie1;
                            info_talonario.PuntoEmision = info.Serie2;
                            info_talonario.NumDocumento = info.NumNota_Impresa;
                            info_talonario.IdSucursal = info.IdSucursal;
                            info_talonario.Usado = true;

                            data_talonario.modificar_estado_usadoDB(info_talonario);
                        }
                    }
                    else{
                        Entity.Serie1 = null;
                        Entity.Serie2 = null;
                        Entity.NumNota_Impresa = null;
                    }
                    #endregion

                    db_f.fa_notaCreDeb.Add(Entity);
                    db_f.SaveChanges();

                    #endregion

                    #region Parametros
                    var parametros = db_f.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    var cliente = db_f.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                    #endregion

                    #region Contabilidad
                    if (parametros != null)
                    {
                        ct_cbtecble_Info diario = armar_diario(info, info.CreDeb.Trim() == "C" ? (int)parametros.IdTipoCbteCble_NC : (int)parametros.IdTipoCbteCble_ND, cliente.IdCtaCble_cxc_Credito, info.IdCtaCble_TipoNota);
                        if (diario != null)
                        {
                            if (odata_ct.guardarDB(diario))
                            {
                                db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                {
                                    no_IdEmpresa = info.IdEmpresa,
                                    no_IdSucursal = info.IdSucursal,
                                    no_IdBodega = info.IdBodega,
                                    no_IdNota = info.IdNota,

                                    ct_IdEmpresa = diario.IdEmpresa,
                                    ct_IdTipoCbte = diario.IdTipoCbte,
                                    ct_IdCbteCble = diario.IdCbteCble,

                                    observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                });
                                db_f.SaveChanges();
                            }
                        }                        
                    }
                    #endregion

                    #region Cobranza
                    if (info.CreDeb.Trim() == "C" && info.lst_cruce.Count != 0)
                    {
                        cxc_cobro_Info cobro = armar_cobro(info);
                        if (cobro != null)
                        {
                            if (odata_cobr.guardarDB(cobro))
                            {
                                db_f.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                                {
                                    IdEmpresa_nt = info.IdEmpresa,
                                    IdSucursal_nt = info.IdSucursal,
                                    IdBodega_nt = info.IdBodega,
                                    IdNota_nt = info.IdNota,
                                    IdEmpresa_cbr = cobro.IdEmpresa,
                                    IdSucursal_cbr = cobro.IdSucursal,
                                    IdCobro_cbr = cobro.IdCobro,
                                    Valor_cobro = Math.Round(info.lst_cruce.Sum(q=>q.Valor_Aplicado),2,MidpointRounding.AwayFromZero)
                                });
                                db_f.SaveChanges();
                            }
                        }
                    }
                    
                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(fa_notaCreDeb_Info info)
        {
            try
            {
                #region Variables
                int Secuencia = 1;
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                cxc_cobro_Data odata_cobr = new cxc_cobro_Data();
                #endregion

                using (Entities_facturacion db_f = new Entities_facturacion())
                {
                    #region Nota de debito credito

                    #region Cabecera
                    var entity = db_f.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                    if (entity == null) return false;

                    //entity.IdPuntoVta = info.IdPuntoVta;
                    entity.CodNota = info.CodNota;
                    entity.CreDeb = info.CreDeb.Trim();
                    entity.CodDocumentoTipo = info.CodDocumentoTipo;
                    entity.Serie1 = info.Serie1;
                    entity.Serie2 = info.Serie2;
                    entity.NumNota_Impresa = info.NumNota_Impresa;
                    entity.NumAutorizacion = info.NumAutorizacion;
                    entity.Fecha_Autorizacion = info.Fecha_Autorizacion;
                    entity.IdCliente = info.IdCliente;
                    entity.no_fecha = info.no_fecha.Date;
                    entity.no_fecha_venc = info.no_fecha_venc.Date;
                    entity.IdTipoNota = info.IdTipoNota;
                    entity.sc_observacion = info.sc_observacion;
                    entity.NaturalezaNota = info.NaturalezaNota;
                    entity.IdCtaCble_TipoNota = info.IdCtaCble_TipoNota;
                    entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    entity.Fecha_UltMod = DateTime.Now;

                    #endregion

                    #region Detalle
                    var lst = db_f.fa_notaCreDeb_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).ToList();
                    db_f.fa_notaCreDeb_det.RemoveRange(lst);

                    foreach (var item in info.lst_det)
                    {
                        db_f.fa_notaCreDeb_det.Add(new fa_notaCreDeb_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdNota = info.IdNota,
                            Secuencia = Secuencia++,
                            IdProducto = item.IdProducto,
                            sc_cantidad = item.sc_cantidad,
                            sc_cantidad_factura = item.sc_cantidad_factura,
                            sc_Precio = item.sc_Precio,
                            sc_descUni = item.sc_descUni,
                            sc_PordescUni = item.sc_PordescUni,
                            sc_precioFinal = item.sc_precioFinal,
                            vt_por_iva = item.vt_por_iva,
                            sc_iva = item.sc_iva,
                            IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                            sc_subtotal = item.sc_subtotal,
                            sc_total = item.sc_total,
                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_Cargo = item.IdPunto_Cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        });
                    }
                    #endregion

                    #region Cruce
                    var lst_cruce = db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).ToList();
                    db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.RemoveRange(lst_cruce);
                    Secuencia = 1;
                    foreach (var item in info.lst_cruce)
                    {
                        db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                        {
                            IdEmpresa_nt = info.IdEmpresa,
                            IdSucursal_nt = info.IdSucursal,
                            IdBodega_nt = info.IdBodega,
                            IdNota_nt = info.IdNota,
                            secuencia = Secuencia++,
                            IdEmpresa_fac_nd_doc_mod = item.IdEmpresa_fac_nd_doc_mod,
                            IdSucursal_fac_nd_doc_mod = item.IdSucursal_fac_nd_doc_mod,
                            IdBodega_fac_nd_doc_mod = item.IdBodega_fac_nd_doc_mod,
                            IdCbteVta_fac_nd_doc_mod = item.IdCbteVta_fac_nd_doc_mod,
                            vt_tipoDoc = item.vt_tipoDoc,
                            Valor_Aplicado = item.Valor_Aplicado,
                            fecha_cruce = DateTime.Now,
                        });
                    }
                    #endregion

                    db_f.SaveChanges();

                    #endregion

                    #region Parametros
                    var parametros = db_f.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    var cliente = db_f.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                    #endregion

                    #region Contabilidad
                    if (parametros != null)
                    {
                        var rel_conta = db_f.fa_notaCreDeb_x_ct_cbtecble.Where(q => q.no_IdEmpresa == info.IdEmpresa && q.no_IdSucursal == info.IdSucursal && q.no_IdBodega == info.IdBodega && q.no_IdNota == info.IdNota).FirstOrDefault();
                        ct_cbtecble_Info diario = armar_diario(info, info.CreDeb.Trim() == "C" ? (int)parametros.IdTipoCbteCble_NC : (int)parametros.IdTipoCbteCble_ND, cliente.IdCtaCble_cxc_Credito, info.IdCtaCble_TipoNota);
                        if (diario != null)
                        {
                            if (rel_conta == null)
                            {
                                if (odata_ct.guardarDB(diario))
                                {
                                    db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                    {
                                        no_IdEmpresa = info.IdEmpresa,
                                        no_IdSucursal = info.IdSucursal,
                                        no_IdBodega = info.IdBodega,
                                        no_IdNota = info.IdNota,

                                        ct_IdEmpresa = diario.IdEmpresa,
                                        ct_IdTipoCbte = diario.IdTipoCbte,
                                        ct_IdCbteCble = diario.IdCbteCble,

                                        observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                    });
                                    db_f.SaveChanges();
                                }
                            }
                            else
                            {
                                diario.IdCbteCble = rel_conta.ct_IdCbteCble;
                                odata_ct.modificarDB(diario);
                            }
                        }
                    }
                    #endregion

                    #region Cobranza
                    if (info.CreDeb.Trim() == "C" && info.lst_cruce.Count != 0)
                    {
                        cxc_cobro_Info cobro = armar_cobro(info);                                                
                        if (cobro != null)
                        {
                            var rel_cobr = db_f.fa_notaCreDeb_x_cxc_cobro.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).FirstOrDefault();
                            if (rel_cobr == null)
                            {
                                if (odata_cobr.guardarDB(cobro))
                                {
                                    db_f.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                                    {
                                        IdEmpresa_nt = info.IdEmpresa,
                                        IdSucursal_nt = info.IdSucursal,
                                        IdBodega_nt = info.IdBodega,
                                        IdNota_nt = info.IdNota,
                                        IdEmpresa_cbr = cobro.IdEmpresa,
                                        IdSucursal_cbr = cobro.IdSucursal,
                                        IdCobro_cbr = cobro.IdCobro,
                                        Valor_cobro = Math.Round(info.lst_cruce.Sum(q => q.Valor_Aplicado), 2, MidpointRounding.AwayFromZero)
                                    });
                                    db_f.SaveChanges();
                                }
                            }else
                            {
                                cobro.IdCobro = rel_cobr.IdCobro_cbr;
                                odata_cobr.modificarDB(cobro);
                            }
                        }
                    }

                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(fa_notaCreDeb_Info info)
        {
            try
            {
                #region Variables
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                cxc_cobro_Data odata_cobr = new cxc_cobro_Data();
                #endregion

                using (Entities_facturacion db_f = new Entities_facturacion())
                {
                    #region Nota de debito credito

                    #region Cabecera
                    var entity = db_f.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                    if (entity == null) return false;

                    entity.Estado = "I";
                    entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    entity.Fecha_UltAnu = DateTime.Now;
                    entity.MotiAnula = info.MotiAnula;
                    #endregion

                    var lst_cruce = db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).ToList();
                    db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.RemoveRange(lst_cruce);
                    #endregion

                    #region Contabilidad
                    var rel_conta = db_f.fa_notaCreDeb_x_ct_cbtecble.Where(q => q.no_IdEmpresa == info.IdEmpresa && q.no_IdSucursal == info.IdSucursal && q.no_IdBodega == info.IdBodega && q.no_IdNota == info.IdNota).FirstOrDefault();
                    if (rel_conta != null)
                        if (!odata_ct.anularDB(new ct_cbtecble_Info { IdEmpresa = rel_conta.ct_IdEmpresa, IdTipoCbte = rel_conta.ct_IdTipoCbte, IdCbteCble = rel_conta.ct_IdCbteCble, IdUsuarioAnu = info.IdUsuarioUltAnu }))
                        {
                            entity.Estado = "A";
                            entity.IdUsuarioUltAnu = null;
                            entity.Fecha_UltAnu = null;
                            entity.MotiAnula = null;
                        }


                    #endregion

                    #region Cobranza

                    var rel_cobr = db_f.fa_notaCreDeb_x_cxc_cobro.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).FirstOrDefault();
                    if (rel_cobr != null)
                        if (!odata_cobr.anularDB(new cxc_cobro_Info { IdEmpresa = rel_cobr.IdEmpresa_cbr, IdSucursal = rel_cobr.IdSucursal_cbr, IdCobro = rel_cobr.IdCobro_cbr, IdUsuarioUltAnu = info.IdUsuarioUltAnu }))
                        {
                            entity.Estado = "A";
                            entity.IdUsuarioUltAnu = null;
                            entity.Fecha_UltAnu = null;
                            entity.MotiAnula = null;
                        }
                    #endregion

                    db_f.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private cxc_cobro_Info armar_cobro(fa_notaCreDeb_Info info)
        {
            try
            {
                cxc_cobro_Info cobro = new cxc_cobro_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdCobro = 0,
                    IdCobro_tipo = info.CreDeb.Trim() == "C" ? "NTCR" : "NTDB",
                    cr_fecha = info.no_fecha,
                    cr_fechaCobro = info.no_fecha,
                    cr_fechaDocu = info.no_fecha,
                    cr_NumDocumento = info.CodDocumentoTipo +  (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000")),
                    cr_observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000")),
                    cr_TotalCobro = Math.Round(info.lst_cruce.Sum(q=>q.Valor_Aplicado),2,MidpointRounding.AwayFromZero),
                    IdCaja = 1,
                    IdCliente = info.IdCliente,
                    IdUsuario = info.IdUsuario,
                    IdTipoNotaCredito = info.IdTipoNota,
                    lst_det = new List<cxc_cobro_det_Info>()
                };

                int Secuencia = 1;
                foreach (var item in info.lst_cruce)
                {
                    cobro.lst_det.Add(new cxc_cobro_det_Info
                    {
                        IdEmpresa = cobro.IdEmpresa,
                        IdSucursal = cobro.IdSucursal,
                        IdCobro = cobro.IdCobro,
                        secuencial = Secuencia++,
                        IdCobro_tipo_det = cobro.IdCobro_tipo,
                        IdBodega_Cbte = item.IdBodega_fac_nd_doc_mod,
                        IdCbte_vta_nota = item.IdCbteVta_fac_nd_doc_mod,
                        dc_TipoDocumento = item.vt_tipoDoc,
                        dc_ValorPago = item.Valor_Aplicado,                        
                    });
                }

                return cobro;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private ct_cbtecble_Info armar_diario(fa_notaCreDeb_Info info, int IdTipoCbte, string IdCtaCble_cliente, string IdCtaCble_tipoNota)
        {
            try
            {

                string IdCtaCble_IVA = string.Empty;
                using (Entities_general Context = new Entities_general())
                {
                    var porcentajes = (from q in info.lst_det
                                       group q by new { q.IdCod_Impuesto_Iva } into g
                                       select g.Key).ToList();

                    foreach (var item in porcentajes)
                    {
                        var impuesto = Context.tb_sis_Impuesto_x_ctacble.Include("tb_sis_Impuesto").Where(q => q.IdEmpresa_cta == info.IdEmpresa && q.IdCod_Impuesto == item.IdCod_Impuesto_Iva).FirstOrDefault();
                        if (impuesto != null)
                        {
                            if (impuesto.tb_sis_Impuesto.porcentaje > 0)
                            {                                
                                IdCtaCble_IVA = impuesto.IdCtaCble;
                            }
                        }
                    }
                }

                #region Cabecera
                ct_cbtecble_Info diario = new ct_cbtecble_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = IdTipoCbte,
                    IdCbteCble = 0,
                    cb_Fecha = info.no_fecha.Date,
                    IdSucursal = info.IdSucursal,
                    IdPeriodo = Convert.ToInt32(info.no_fecha.ToString("yyyyMM")),
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltModi = info.IdUsuarioUltMod,
                    cb_Observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000")) + info.sc_observacion,
                    CodCbteCble = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? info.NumNota_Impresa : info.IdNota.ToString("000000000")),
                    cb_Valor = 0,
                    lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                };
                #endregion
                int secuencia = 1;

                #region Cuenta cliente
                if (!string.IsNullOrEmpty(IdCtaCble_cliente))
                {
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_cliente,
                        dc_Valor = Math.Round(info.lst_det.Sum(q => q.sc_total), 2, MidpointRounding.AwayFromZero) * (info.CreDeb.Trim() == "C" ? -1 : 1),
                        dc_para_conciliar = false
                    });
                }
                #endregion

                #region IVA
                if (!string.IsNullOrEmpty(IdCtaCble_IVA))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_IVA,
                        dc_Valor = Math.Round(info.lst_det.Where(q => q.vt_por_iva > 0).Sum(q => q.sc_iva), 2, MidpointRounding.AwayFromZero) * (info.CreDeb.Trim() == "C" ? 1 : -1)
                    });
                #endregion

                #region Cuenta tipo nota
                if (!string.IsNullOrEmpty(IdCtaCble_tipoNota))
                {
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_tipoNota,
                        dc_Valor = Math.Round(info.lst_det.Sum(q => q.sc_subtotal), 2, MidpointRounding.AwayFromZero) * (info.CreDeb.Trim() == "C" ? 1 : -1),
                        dc_para_conciliar = false,
                    });
                }
                #endregion

                if (info.lst_det.Count == 0)
                    return null;

                if (Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor),2,MidpointRounding.AwayFromZero) != 0)
                    return null;

                if (diario.lst_ct_cbtecble_det.Where(q=>q.dc_Valor == 0).Count() > 0)
                    return null;

                return diario;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarEstadoAutorizacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var Entity = Context.fa_notaCreDeb.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.aprobada_enviar_sri = true;
                        Context.SaveChanges();
                    }
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

