using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Data.General;
using Core.Erp.Info.Caja;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Caja
{
    public class cp_conciliacion_Caja_Data
    {
        cp_orden_pago_Data data_op = new cp_orden_pago_Data();
        ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
        cp_orden_pago_cancelaciones_Data data_can = new cp_orden_pago_cancelaciones_Data();        
        public List<cp_conciliacion_Caja_Info> get_list(int IdEmpresa, int IdCaja, DateTime Fecha_ini, DateTime Fecha_fin)

        {
            try
            {
                List<cp_conciliacion_Caja_Info> Lista;
                int IdCaja_ini = IdCaja;
                int IdCaja_fin = IdCaja == 0 ? 999999 : IdCaja;
                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.cp_conciliacion_Caja
                             where q.IdEmpresa == IdEmpresa
                             && Fecha_ini <= q.Fecha && q.Fecha <= Fecha_fin
                             && IdCaja_ini <= q.IdCaja && q.IdCaja <= IdCaja_fin
                             orderby q.IdConciliacion_Caja descending
                             select new cp_conciliacion_Caja_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion_Caja = q.IdConciliacion_Caja,
                                 Observacion = q.Observacion,
                                 Fecha = q.Fecha,
                                 IdPeriodo = q.IdPeriodo,
                                 Ingresos = q.Ingresos,
                                 Dif_x_pagar_o_cobrar = q.Dif_x_pagar_o_cobrar,
                                 Total_fact_vale = q.Total_fact_vale,
                                 IdEstadoCierre = q.IdEstadoCierre,
                                 Fecha_ini = q.Fecha_ini,
                                 Fecha_fin = q.Fecha_fin,
                                 IdEmpresa_op = q.IdEmpresa_op,
                                 IdOrdenPago_op = q.IdOrdenPago_op,
                                 SecuenciaCaja = q.SecuenciaCaja
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cp_conciliacion_Caja_Info get_info(int IdEmpresa, decimal IdConciliacion_caja)
        {
            try
            {
                cp_conciliacion_Caja_Info info;
                using (Entities_caja Context = new Entities_caja())
                {
                    cp_conciliacion_Caja Entity = Context.cp_conciliacion_Caja.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion_Caja == IdConciliacion_caja).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new cp_conciliacion_Caja_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConciliacion_Caja = Entity.IdConciliacion_Caja,
                        IdPeriodo = Entity.IdPeriodo,
                        Fecha_ini = Entity.Fecha_ini,
                        Fecha_fin = Entity.Fecha_fin,
                        Fecha = Entity.Fecha,
                        IdCaja = Entity.IdCaja,
                        IdEstadoCierre = Entity.IdEstadoCierre,
                        Observacion = Entity.Observacion,
                        IdEmpresa_op = Entity.IdEmpresa_op,
                        IdOrdenPago_op = Entity.IdOrdenPago_op,
                        Saldo_cont_al_periodo = Entity.Saldo_cont_al_periodo,
                        Ingresos = Entity.Ingresos,
                        Total_Ing = Entity.Total_Ing,
                        Total_fact_vale = Entity.Total_fact_vale,
                        Total_fondo = Entity.Total_fondo,
                        Dif_x_pagar_o_cobrar = Entity.Dif_x_pagar_o_cobrar,
                        IdTipoFlujo = Entity.IdTipoFlujo,
                        IdEmpresa_mov_caj = Entity.IdEmpresa_mov_caj,
                        IdTipoCbte_mov_caj = Entity.IdTipoCbte_mov_caj,
                        IdCbteCble_mov_caj = Entity.IdCbteCble_mov_caj,
                        IdCtaCble = Entity.IdCtaCble
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal get_Secuencia(int IdEmpresa, int IdCaja)
        {

            try
            {
                decimal SecuenciaCaja = 1;
                using (Entities_caja db = new Entities_caja())
                {
                    var Lista = db.cp_conciliacion_Caja.Where(q => q.IdEmpresa == IdEmpresa && q.IdCaja == IdCaja).Select(q => q.SecuenciaCaja);

                    if (Lista.Count() > 0)
                        SecuenciaCaja = Convert.ToDecimal(Lista.Max() + 1);
                }
                return SecuenciaCaja;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_caja Context = new Entities_caja())
                {
                    var lst = from q in Context.cp_conciliacion_Caja
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdConciliacion_Caja) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_conciliacion_Caja_Info info)
        {
            Entities_caja Context = new Entities_caja();
            Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
            Entities_contabilidad Context_ct = new Entities_contabilidad();
            try
            {
                #region Variables
                int Secuencia = 1;
                decimal IdOrdenPago = 1;
                decimal IdCbteCble_NC = 1;
                decimal IdCbteCble_EG = 1;
                decimal IdCbteCble_OP = 1;
                decimal IdCancelacion = 1;
                decimal IdCbteCble_IN = 1;

                cp_parametros Entity_p = Context_cxp.cp_parametros.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                caj_parametro Entity_pc = Context.caj_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                cp_orden_pago_tipo_x_empresa Entity_op_tipo = Context_cxp.cp_orden_pago_tipo_x_empresa.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipo_op == cl_enumeradores.eTipoOrdenPago.OTROS_CONC.ToString()).FirstOrDefault();
                caj_Caja Caja = Context.caj_Caja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == info.IdCaja).FirstOrDefault();
                if (Entity_p == null || Entity_pc == null || Entity_op_tipo == null || Caja == null)
                    return false;
                int IdTipoCbte_NC = Convert.ToInt32(Entity_p.pa_TipoCbte_NC);
                int IdTipoCbte_EG = Entity_pc.IdTipoCbteCble_MoviCaja_Egr;
                int IdTipoCbte_op = Convert.ToInt32(Entity_op_tipo.IdTipoCbte_OP);
                int IdTipoCbte_IN = Entity_pc.IdTipoCbteCble_MoviCaja_Ing;
                int IdSucursal = Convert.ToInt32(Caja.IdSucursal);
                #endregion

                #region Cabecera
                cp_conciliacion_Caja Entity_c = new cp_conciliacion_Caja
                {
                    IdEmpresa = info.IdEmpresa,
                    IdConciliacion_Caja = info.IdConciliacion_Caja = get_id(info.IdEmpresa),
                    IdCaja = info.IdCaja,
                    IdCtaCble = info.IdCtaCble,
                    Observacion = info.Observacion,
                    Fecha = info.Fecha.Date,
                    Fecha_ini = info.Fecha_ini,
                    Fecha_fin = info.Fecha_fin,
                    IdEstadoCierre = info.IdEstadoCierre,
                    IdPeriodo = info.IdPeriodo,

                    Ingresos = info.Ingresos,
                    Total_fondo = info.Total_fondo,
                    Saldo_cont_al_periodo = info.Saldo_cont_al_periodo,
                    Total_fact_vale = info.Total_fact_vale,
                    Dif_x_pagar_o_cobrar = info.Dif_x_pagar_o_cobrar,
                    Total_Ing = info.Total_Ing,

                    SecuenciaCaja =  info.SecuenciaCaja= get_Secuencia(info.IdEmpresa, info.IdCaja),

                    IdEmpresa_op = info.IdEmpresa_op,
                    IdOrdenPago_op = info.IdOrdenPago_op,

                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now
                };

                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    IdOrdenPago = data_op.get_id(Entity_c.IdEmpresa);
                    IdCbteCble_NC = data_ct.get_id(Entity_c.IdEmpresa, IdTipoCbte_NC);
                    IdCancelacion = data_can.get_id(Entity_c.IdEmpresa);
                    IdCbteCble_OP = data_ct.get_id(info.IdEmpresa, IdTipoCbte_op);

                    #region Orden de pago
                    cp_orden_pago op = new cp_orden_pago
                    {
                        IdEmpresa = Entity_c.IdEmpresa,
                        IdSucursal = IdSucursal,
                        IdOrdenPago = IdOrdenPago++,
                        Observacion = "Caja #" + Entity_c.IdConciliacion_Caja + " "+info.ObservacionOP,
                        IdTipo_op = cl_enumeradores.eTipoOrdenPago.OTROS_CONC.ToString(),
                        IdTipo_Persona = info.IdTipoPersona,
                        IdPersona = info.IdPersona,
                        IdEntidad = info.IdEntidad,
                        Fecha = info.FechaOP.Date,
                        IdEstadoAprobacion = "APRO",
                        IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                        Estado = "A",
                        IdUsuario = info.IdUsuario
                    };
                    Entity_c.IdEmpresa_op = op.IdEmpresa;
                    Entity_c.IdOrdenPago_op = op.IdOrdenPago;
                    Context_cxp.cp_orden_pago.Add(op);

                    ct_cbtecble diario = new ct_cbtecble
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = IdTipoCbte_op,
                        IdCbteCble = IdCbteCble_OP,
                        cb_Fecha = info.FechaOP.Date,
                        cb_Observacion = op.Observacion,
                        IdPeriodo = Convert.ToInt32(info.FechaOP.ToString("yyyyMM")),
                        IdSucursal = IdSucursal,
                        cb_FechaTransac = DateTime.Now,                        
                        cb_Estado = "A",
                        IdUsuario = info.IdUsuario
                    };
                    Context_ct.ct_cbtecble.Add(diario);

                    int sec = 1;
                    foreach (var item in info.lst_det_ct)
                    {
                        ct_cbtecble_det diario_det = new ct_cbtecble_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = sec++,
                            IdCtaCble = item.IdCtaCble,
                            dc_Valor = Math.Round(Convert.ToDouble(item.dc_Valor), 2, MidpointRounding.AwayFromZero),
                        };
                        Context_ct.ct_cbtecble_det.Add(diario_det);
                    }
                    cp_orden_pago_det op_det = new cp_orden_pago_det
                    {
                        IdEmpresa = op.IdEmpresa,
                        IdOrdenPago = op.IdOrdenPago,
                        Secuencia = 1,

                        IdEmpresa_cxp = diario.IdEmpresa,
                        IdTipoCbte_cxp = diario.IdTipoCbte,
                        IdCbteCble_cxp = diario.IdCbteCble,

                        Valor_a_pagar = Convert.ToDouble(info.lst_det_ct.Sum(q=>q.dc_Valor_debe)),
                        IdEstadoAprobacion = Entity_op_tipo.IdEstadoAprobacion,
                        IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                        Fecha_Pago = op.Fecha
                    };
                    Context_cxp.cp_orden_pago_det.Add(op_det);
                    #endregion

                    #region Ingreso por reposicion
                    if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                    {
                        if (info.IdCbteCble_mov_caj == null)
                        {
                            IdCbteCble_IN = data_ct.get_id(info.IdEmpresa, IdTipoCbte_IN);
                            ct_cbtecble repo = new ct_cbtecble
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdTipoCbte = IdTipoCbte_IN,
                                IdCbteCble = IdCbteCble_IN,
                                cb_Fecha = info.FechaOP.Date,
                                cb_Observacion = "Caja # " + info.IdConciliacion_Caja + " Reposición",
                                IdPeriodo = Convert.ToInt32(info.FechaOP.ToString("yyyyMM")),
                                IdSucursal = IdSucursal,
                                cb_FechaTransac = DateTime.Now,
                                cb_Estado = "A",
                                cb_Valor = info.lst_det_ct.Sum(q => q.dc_Valor_debe),
                                IdUsuario = info.IdUsuario
                            };
                            Context_ct.ct_cbtecble.Add(repo);

                            ct_cbtecble_det Debe = new ct_cbtecble_det
                            {
                                IdEmpresa = repo.IdEmpresa,
                                IdTipoCbte = repo.IdTipoCbte,
                                IdCbteCble = repo.IdCbteCble,
                                secuencia = 1,
                                IdCtaCble = info.IdCtaCble,
                                dc_Valor = Math.Round(Convert.ToDouble(repo.cb_Valor), 2, MidpointRounding.AwayFromZero),
                            };

                            ct_cbtecble_det Haber = new ct_cbtecble_det
                            {
                                IdEmpresa = repo.IdEmpresa,
                                IdTipoCbte = repo.IdTipoCbte,
                                IdCbteCble = repo.IdCbteCble,
                                secuencia = 2,
                                IdCtaCble = info.IdCtaCble,
                                dc_Valor = Math.Round(Convert.ToDouble(repo.cb_Valor), 2, MidpointRounding.AwayFromZero) * -1,
                            };
                            Context_ct.ct_cbtecble_det.Add(Debe);
                            Context_ct.ct_cbtecble_det.Add(Haber);

                            caj_Caja_Movimiento Entity_caj = new caj_Caja_Movimiento
                            {
                                IdEmpresa = repo.IdEmpresa,
                                IdTipocbte = repo.IdTipoCbte,
                                IdCbteCble = repo.IdCbteCble,
                                CodMoviCaja = "Caja # " + info.IdConciliacion_Caja,
                                cm_Signo = "+",
                                cm_valor = repo.cb_Valor,
                                IdTipoMovi = Convert.ToInt32(Entity_pc.IdTipo_movi_ing_x_reposicion),
                                cm_observacion = repo.cb_Observacion,
                                IdCaja = info.IdCaja,
                                IdPeriodo = Convert.ToInt32(repo.cb_Fecha.ToString("yyyyMM")),
                                cm_fecha = repo.cb_Fecha.Date,
                                IdTipo_Persona = info.IdTipoPersona,
                                IdEntidad = (decimal)info.IdEntidad,
                                IdPersona = info.IdPersona,
                                Estado = "A",
                                IdUsuario = info.IdUsuario,
                                Fecha_Transac = DateTime.Now
                            };
                            Context.caj_Caja_Movimiento.Add(Entity_caj);
                            caj_Caja_Movimiento_det Entity_caj_det = new caj_Caja_Movimiento_det
                            {
                                IdEmpresa = repo.IdEmpresa,
                                IdTipocbte = repo.IdTipoCbte,
                                IdCbteCble = repo.IdCbteCble,
                                Secuencia = 1,
                                IdCobro_tipo = "EFEC",
                                cr_Valor = repo.cb_Valor
                            };
                            Context.caj_Caja_Movimiento_det.Add(Entity_caj_det);
                            Entity_c.IdEmpresa_mov_caj = repo.IdEmpresa;
                            Entity_c.IdTipoCbte_mov_caj = repo.IdTipoCbte;
                            Entity_c.IdCbteCble_mov_caj = repo.IdCbteCble;
                        }                        
                    }
                    #endregion
                }                
                Context.cp_conciliacion_Caja.Add(Entity_c);
                #endregion

                #region Facturas
                foreach (var item in info.lst_det_fact)
                {
                    if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                    {
                        if (item.IdOrdenPago_OP == null)
                        {
                            #region Orden de pago
                            cp_orden_pago op = new cp_orden_pago
                            {
                                IdEmpresa  = Entity_c.IdEmpresa,
                                IdSucursal = IdSucursal,
                                IdOrdenPago = IdOrdenPago++,
                                Observacion = "Caja #" + Entity_c.IdConciliacion_Caja,
                                IdTipo_op = cl_enumeradores.eTipoOrdenPago.FACT_PROVEE.ToString(),
                                IdTipo_Persona = cl_enumeradores.eTipoPersona.PROVEE.ToString(),
                                IdPersona = item.IdPersona,
                                IdEntidad = item.idEntidad,
                                Fecha = Convert.ToDateTime(Entity_c.Fecha_fin).Date,
                                IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.APRO.ToString(),
                                IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                                Estado = "A",
                                IdUsuario = info.IdUsuario
                            };
                            item.IdEmpresa_OP = op.IdEmpresa;
                            item.IdOrdenPago_OP = op.IdOrdenPago;
                            Context_cxp.cp_orden_pago.Add(op);

                            cp_orden_pago_det op_det = new cp_orden_pago_det
                            {
                                IdEmpresa = op.IdEmpresa,
                                IdOrdenPago = op.IdOrdenPago,
                                Secuencia = 1,

                                IdEmpresa_cxp = item.IdEmpresa_OGiro,
                                IdTipoCbte_cxp = item.IdTipoCbte_Ogiro,
                                IdCbteCble_cxp = item.IdCbteCble_Ogiro,

                                Valor_a_pagar = Convert.ToDouble(item.Valor_a_aplicar),
                                IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.APRO.ToString(),
                                IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                                Fecha_Pago = op.Fecha
                            };
                            Context_cxp.cp_orden_pago_det.Add(op_det);
                            #endregion

                            #region Nota de crédito
                            ct_cbtecble diario = new ct_cbtecble
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdTipoCbte = IdTipoCbte_NC,
                                IdCbteCble = IdCbteCble_NC,
                                cb_Fecha = op.Fecha,
                                cb_Observacion = op.Observacion,
                                IdPeriodo = Convert.ToInt32(op.Fecha.ToString("yyyyMM")),
                                IdSucursal = IdSucursal,
                                cb_FechaTransac = DateTime.Now,
                                cb_Estado = "A"
                            };
                            Context_ct.ct_cbtecble.Add(diario);

                            ct_cbtecble_det Debe = new ct_cbtecble_det
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = 1,
                                IdCtaCble = item.IdCtaCble_cxp,
                                dc_Valor = Math.Round(Convert.ToDouble(item.Valor_a_aplicar),2,MidpointRounding.AwayFromZero),
                            };

                            ct_cbtecble_det Haber = new ct_cbtecble_det
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = 2,
                                IdCtaCble = info.IdCtaCble,
                                dc_Valor = Math.Round(Convert.ToDouble(item.Valor_a_aplicar), 2, MidpointRounding.AwayFromZero)*-1,
                            };
                            Context_ct.ct_cbtecble_det.Add(Debe);
                            Context_ct.ct_cbtecble_det.Add(Haber);

                            cp_nota_DebCre Entity_nc = new cp_nota_DebCre
                            {
                                IdEmpresa = Entity_c.IdEmpresa,
                                IdTipoCbte_Nota = IdTipoCbte_NC,
                                IdCbteCble_Nota = IdCbteCble_NC++,
                                DebCre = "C",
                                IdTipoNota = cl_enumeradores.eTipoNotaCXP.T_TIP_NOTA_INT.ToString(),
                                IdProveedor = item.idEntidad,
                                IdSucursal = Convert.ToInt32(item.idSucursal),
                                cn_fecha = op.Fecha,
                                Fecha_contable = op.Fecha,
                                cn_Fecha_vcto = op.Fecha,
                                cn_observacion = op.Observacion,
                                cn_subtotal_iva = 0,
                                cn_subtotal_siniva = Convert.ToDouble(item.Valor_a_aplicar),
                                cn_Por_iva = item.por_iva,
                                cn_valoriva = 0,
                                cn_baseImponible = 0,
                                cn_Ice_valor = 0,
                                cn_Ice_base = 0,
                                cn_Ice_por = 0,
                                cn_Serv_por = 0,
                                cn_Serv_valor = 0,
                                cn_vaCoa = "N",
                                cn_BaseSeguro = 0,
                                cn_total = item.Valor_a_aplicar,
                                Estado = "A",
                                PagoLocExt = "LOC",
                                PagoSujetoRetencion = "NA"
                            };
                            Context_cxp.cp_nota_DebCre.Add(Entity_nc);
                            #endregion

                            #region Cancelación
                            cp_orden_pago_cancelaciones cancelacion = new cp_orden_pago_cancelaciones
                            {
                                IdEmpresa = op.IdEmpresa,
                                Idcancelacion = IdCancelacion++,
                                Secuencia = 1,
                                IdEmpresa_op = op.IdEmpresa,
                                IdOrdenPago_op = op.IdOrdenPago,
                                Secuencia_op = 1,
                                IdEmpresa_cxp = item.IdEmpresa_OGiro,
                                IdTipoCbte_cxp = item.IdTipoCbte_Ogiro,
                                IdCbteCble_cxp = item.IdCbteCble_Ogiro,
                                IdEmpresa_pago = diario.IdEmpresa,
                                IdTipoCbte_pago = diario.IdTipoCbte,
                                IdCbteCble_pago = diario.IdCbteCble,
                                MontoAplicado = Debe.dc_Valor,
                                SaldoActual = 0,
                                SaldoAnterior = 0,
                                fechaTransaccion = DateTime.Now,
                                Observacion = op.Observacion
                            };
                            Context_cxp.cp_orden_pago_cancelaciones.Add(cancelacion);
                            #endregion
                        }
                    }

                    cp_conciliacion_Caja_det Entity_d = new cp_conciliacion_Caja_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConciliacion_Caja = Entity_c.IdConciliacion_Caja,
                        Secuencia = Secuencia++,

                        IdEmpresa_OGiro = item.IdEmpresa_OGiro,
                        IdTipoCbte_Ogiro = item.IdTipoCbte_Ogiro,
                        IdCbteCble_Ogiro = item.IdCbteCble_Ogiro,
                        Tipo_documento = item.Tipo_documento,
                        Valor_a_aplicar = item.Valor_a_aplicar,

                        IdEmpresa_OP = item.IdEmpresa_OP,
                        IdOrdenPago_OP = item.IdOrdenPago_OP
                    };
                    Context.cp_conciliacion_Caja_det.Add(Entity_d);
                }
                #endregion

                #region Vales
                Secuencia = 1;
                IdCbteCble_EG = data_ct.get_id(Entity_c.IdEmpresa, IdTipoCbte_EG);
                foreach (var item in info.lst_det_vale)
                {
                    if (item.IdCbteCble_movcaja == 0)
                    {
                        ct_cbtecble diario = new ct_cbtecble
                        {
                            IdEmpresa = item.IdEmpresa_movcaja = info.IdEmpresa,
                            IdTipoCbte = item.IdTipocbte_movcaja = IdTipoCbte_EG,
                            IdCbteCble = item.IdCbteCble_movcaja = IdCbteCble_EG++,
                            cb_Fecha = item.fecha,
                            cb_Observacion = "Caja # "+info.IdConciliacion_Caja + " " + item.Observacion,
                            IdPeriodo = Convert.ToInt32(item.fecha.ToString("yyyyMM")),
                            IdSucursal = IdSucursal,
                            cb_FechaTransac = DateTime.Now,
                            cb_Estado = "A",
                            cb_Valor = item.valor,
                            IdUsuario = info.IdUsuario
                        };
                        Context_ct.ct_cbtecble.Add(diario);

                        ct_cbtecble_det Debe = new ct_cbtecble_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = 1,
                            IdCtaCble = item.IdCtaCble,
                            dc_Valor = Math.Round(Convert.ToDouble(item.valor), 2, MidpointRounding.AwayFromZero),
                        };

                        ct_cbtecble_det Haber = new ct_cbtecble_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = 2,
                            IdCtaCble = info.IdCtaCble,
                            dc_Valor = Math.Round(Convert.ToDouble(item.valor), 2, MidpointRounding.AwayFromZero) * -1,
                        };
                        Context_ct.ct_cbtecble_det.Add(Debe);
                        Context_ct.ct_cbtecble_det.Add(Haber);

                        caj_Caja_Movimiento Entity_caj = new caj_Caja_Movimiento
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipocbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            CodMoviCaja = "Caja # "+info.IdConciliacion_Caja,
                            cm_Signo = "-",
                            cm_valor = item.valor,
                            IdTipoMovi = Convert.ToInt32(item.idTipoMovi),
                            cm_observacion = item.Observacion,
                            IdCaja = info.IdCaja,
                            IdPeriodo = Convert.ToInt32(item.fecha.ToString("yyyyMM")),
                            cm_fecha = item.fecha,
                            IdTipo_Persona = cl_enumeradores.eTipoPersona.PERSONA.ToString(),
                            IdEntidad = item.IdPersona,
                            IdPersona = item.IdPersona,
                            Estado = "A",
                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = DateTime.Now
                        };
                        Context.caj_Caja_Movimiento.Add(Entity_caj);
                        caj_Caja_Movimiento_det Entity_caj_det = new caj_Caja_Movimiento_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipocbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            Secuencia = 1,
                            IdCobro_tipo = "EFEC",
                            cr_Valor = item.valor
                        };
                        Context.caj_Caja_Movimiento_det.Add(Entity_caj_det);
                    }

                    cp_conciliacion_Caja_det_x_ValeCaja Entity_d = new cp_conciliacion_Caja_det_x_ValeCaja
                    {
                        IdEmpresa = Entity_c.IdEmpresa,
                        IdConciliacion_Caja = Entity_c.IdConciliacion_Caja,
                        Secuencia = Secuencia++,
                        IdEmpresa_movcaja = item.IdEmpresa_movcaja,
                        IdTipocbte_movcaja = item.IdTipocbte_movcaja,
                        IdCbteCble_movcaja = item.IdCbteCble_movcaja,
                        IdCtaCble = item.IdCtaCble
                    };
                    Context.cp_conciliacion_Caja_det_x_ValeCaja.Add(Entity_d);
                }
                #endregion

                #region Ingresos
                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    Secuencia = 1;
                    double saldo_egresos = info.Total_fact_vale;
                    foreach (var item in info.lst_det_ing)
                    {
                        if (saldo_egresos > 0)
                        {
                            item.valor_aplicado = item.valor_disponible <= saldo_egresos ? item.valor_disponible : saldo_egresos;
                            saldo_egresos = saldo_egresos - item.valor_aplicado;
                        }
                        cp_conciliacion_Caja_det_Ing_Caja Entity_d = new cp_conciliacion_Caja_det_Ing_Caja
                        {
                            IdEmpresa = Entity_c.IdEmpresa,
                            IdConciliacion_Caja = Entity_c.IdConciliacion_Caja,
                            secuencia = Secuencia++,
                            IdEmpresa_movcaj = item.IdEmpresa_movcaj,
                            IdTipocbte_movcaj = item.IdTipocbte_movcaj,
                            IdCbteCble_movcaj = item.IdCbteCble_movcaj,
                            valor_aplicado = item.valor_aplicado,
                            valor_disponible = item.valor_disponible
                        };
                        Context.cp_conciliacion_Caja_det_Ing_Caja.Add(Entity_d);
                    }
                }
                #endregion                

                Context_ct.SaveChanges();
                Context_cxp.SaveChanges();
                Context.SaveChanges();
                
                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    int secuencia = 1;
                    var lst_vales = Context.vwcaj_Caja_Movimiento_ValesNoConciliados.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == info.IdCaja).ToList();
                    foreach (var item in lst_vales)
                    {
                        Context.cp_conciliacion_Caja_ValesNoConciliados.Add(new cp_conciliacion_Caja_ValesNoConciliados
                        {
                            IdEmpresa = info.IdEmpresa, 
                            IdConciliacion_Caja = info.IdConciliacion_Caja,
                            Secuencia = secuencia++,
                            IdCbteCble_movcaja = item.IdCbteCble,
                            IdEmpresa_movcaja = item.IdEmpresa,
                            IdTipocbte_movcaja = item.IdTipocbte,
                            Valor = item.cm_valor
                        });
                    }
                    //AQUI PONES LO DE LOS VALES :*
                    Context.SaveChanges();
                }
                

                Context_ct.Dispose();
                Context.Dispose();
                Context_cxp.Dispose();
                return true;                
            }
            catch (Exception ex)
            {
                Context_ct.Dispose();
                Context_cxp.Dispose();
                Context.Dispose();
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacion_Caja_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(cp_conciliacion_Caja_Info info)
        {
            Entities_caja Context = new Entities_caja();
            Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
            Entities_contabilidad Context_ct = new Entities_contabilidad();
            try
            {
                
                #region Variables
                int Secuencia = 1;
                decimal IdOrdenPago = 1;
                decimal IdCbteCble_NC = 1;
                decimal IdCbteCble_EG = 1;
                decimal IdCbteCble_OP = 1;
                decimal IdCancelacion = 1;
                decimal IdCbteCble_IN = 1;

                cp_parametros Entity_p = Context_cxp.cp_parametros.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                caj_parametro Entity_pc = Context.caj_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                cp_orden_pago_tipo_x_empresa Entity_op_tipo = Context_cxp.cp_orden_pago_tipo_x_empresa.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipo_op == cl_enumeradores.eTipoOrdenPago.OTROS_CONC.ToString()).FirstOrDefault();
                caj_Caja Caja = Context.caj_Caja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == info.IdCaja).FirstOrDefault();
                if (Entity_p == null || Entity_pc == null || Entity_op_tipo == null || Caja == null)
                    return false;
                int IdTipoCbte_NC = Convert.ToInt32(Entity_p.pa_TipoCbte_NC);
                int IdTipoCbte_EG = Entity_pc.IdTipoCbteCble_MoviCaja_Egr;
                int IdTipoCbte_op = Convert.ToInt32(Entity_op_tipo.IdTipoCbte_OP);
                int IdTipoCbte_IN = Entity_pc.IdTipoCbteCble_MoviCaja_Ing;
                int IdSucursal = Convert.ToInt32(Caja.IdSucursal);
                #endregion

                cp_conciliacion_Caja Entity = Context.cp_conciliacion_Caja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion_Caja == info.IdConciliacion_Caja).FirstOrDefault();
                if (Entity == null) return false;
                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    IdOrdenPago = data_op.get_id(info.IdEmpresa);
                    IdCbteCble_NC = data_ct.get_id(info.IdEmpresa, IdTipoCbte_NC);
                    IdCancelacion = data_can.get_id(info.IdEmpresa);
                    IdCbteCble_OP = data_ct.get_id(info.IdEmpresa, IdTipoCbte_op);
                }

                #region Facturas
                var lst_fact = Context.cp_conciliacion_Caja_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion_Caja == info.IdConciliacion_Caja).ToList();
                foreach (var item in lst_fact)
                {
                    Context.cp_conciliacion_Caja_det.Remove(item);
                }

                foreach (var item in info.lst_det_fact)
                {
                    if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                    {
                        if (item.IdOrdenPago_OP == null)
                        {
                            #region Orden de pago
                            cp_orden_pago op = new cp_orden_pago
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdSucursal = IdSucursal,
                                IdOrdenPago = IdOrdenPago++,
                                Observacion = "Caja #" + info.IdConciliacion_Caja,
                                IdTipo_op = cl_enumeradores.eTipoOrdenPago.FACT_PROVEE.ToString(),
                                IdTipo_Persona = cl_enumeradores.eTipoPersona.PROVEE.ToString(),
                                IdPersona = item.IdPersona,
                                IdEntidad = item.idEntidad,
                                Fecha = Convert.ToDateTime(info.Fecha_fin).Date,
                                IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.APRO.ToString(),
                                IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                                Estado = "A"
                            };
                            item.IdEmpresa_OP = op.IdEmpresa;
                            item.IdOrdenPago_OP = op.IdOrdenPago;
                            Context_cxp.cp_orden_pago.Add(op);

                            cp_orden_pago_det op_det = new cp_orden_pago_det
                            {
                                IdEmpresa = op.IdEmpresa,
                                IdOrdenPago = op.IdOrdenPago,
                                Secuencia = 1,

                                IdEmpresa_cxp = item.IdEmpresa_OGiro,
                                IdTipoCbte_cxp = item.IdTipoCbte_Ogiro,
                                IdCbteCble_cxp = item.IdCbteCble_Ogiro,

                                Valor_a_pagar = Convert.ToDouble(item.Valor_a_aplicar),
                                IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.APRO.ToString(),
                                IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                                Fecha_Pago = op.Fecha
                            };
                            Context_cxp.cp_orden_pago_det.Add(op_det);
                            #endregion

                            #region Nota de crédito
                            ct_cbtecble diario = new ct_cbtecble
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdTipoCbte = IdTipoCbte_NC,
                                IdCbteCble = IdCbteCble_NC,
                                cb_Fecha = op.Fecha,
                                cb_Observacion = op.Observacion,
                                IdPeriodo = Convert.ToInt32(op.Fecha.ToString("yyyyMM")),
                                IdSucursal = IdSucursal,
                                cb_FechaTransac = DateTime.Now,
                                cb_Estado = "A"
                            };
                            Context_ct.ct_cbtecble.Add(diario);

                            ct_cbtecble_det Debe = new ct_cbtecble_det
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = 1,
                                IdCtaCble = item.IdCtaCble_cxp,
                                dc_Valor = Math.Round(Convert.ToDouble(item.Valor_a_aplicar), 2, MidpointRounding.AwayFromZero),
                            };

                            ct_cbtecble_det Haber = new ct_cbtecble_det
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = 2,
                                IdCtaCble = info.IdCtaCble,
                                dc_Valor = Math.Round(Convert.ToDouble(item.Valor_a_aplicar), 2, MidpointRounding.AwayFromZero) * -1,
                            };
                            Context_ct.ct_cbtecble_det.Add(Debe);
                            Context_ct.ct_cbtecble_det.Add(Haber);

                            cp_nota_DebCre Entity_nc = new cp_nota_DebCre
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdTipoCbte_Nota = IdTipoCbte_NC,
                                IdCbteCble_Nota = IdCbteCble_NC++,
                                DebCre = "C",
                                IdTipoNota = cl_enumeradores.eTipoNotaCXP.T_TIP_NOTA_INT.ToString(),
                                IdProveedor = item.idEntidad,
                                IdSucursal = Convert.ToInt32(item.idSucursal),
                                cn_fecha = op.Fecha,
                                Fecha_contable = op.Fecha,
                                cn_Fecha_vcto = op.Fecha,
                                cn_observacion = op.Observacion,
                                cn_subtotal_iva = 0,
                                cn_subtotal_siniva = Convert.ToDouble(item.Valor_a_aplicar),
                                cn_Por_iva = item.por_iva,
                                cn_valoriva = 0,
                                cn_baseImponible = 0,
                                cn_Ice_valor = 0,
                                cn_Ice_base = 0,
                                cn_Ice_por = 0,
                                cn_Serv_por = 0,
                                cn_Serv_valor = 0,
                                cn_vaCoa = "N",
                                cn_BaseSeguro = 0,
                                cn_total = item.Valor_a_aplicar,
                                Estado = "A",
                                PagoLocExt = "LOC",
                                PagoSujetoRetencion = "NA"
                            };
                            Context_cxp.cp_nota_DebCre.Add(Entity_nc);
                            #endregion

                            #region Cancelación
                            cp_orden_pago_cancelaciones cancelacion = new cp_orden_pago_cancelaciones
                            {
                                IdEmpresa = op.IdEmpresa,
                                Idcancelacion = IdCancelacion++,
                                Secuencia = 1,
                                IdEmpresa_op = op.IdEmpresa,
                                IdOrdenPago_op = op.IdOrdenPago,
                                Secuencia_op = 1,
                                IdEmpresa_cxp = item.IdEmpresa_OGiro,
                                IdTipoCbte_cxp = item.IdTipoCbte_Ogiro,
                                IdCbteCble_cxp = item.IdCbteCble_Ogiro,
                                IdEmpresa_pago = diario.IdEmpresa,
                                IdTipoCbte_pago = diario.IdTipoCbte,
                                IdCbteCble_pago = diario.IdCbteCble,
                                MontoAplicado = Debe.dc_Valor,
                                SaldoActual = 0,
                                SaldoAnterior = 0,
                                fechaTransaccion = DateTime.Now,
                                Observacion = op.Observacion
                            };
                            Context_cxp.cp_orden_pago_cancelaciones.Add(cancelacion);
                            #endregion
                        }

                    }

                    cp_conciliacion_Caja_det Entity_d = new cp_conciliacion_Caja_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConciliacion_Caja = info.IdConciliacion_Caja,
                        Secuencia = Secuencia++,

                        IdEmpresa_OGiro = item.IdEmpresa_OGiro,
                        IdTipoCbte_Ogiro = item.IdTipoCbte_Ogiro,
                        IdCbteCble_Ogiro = item.IdCbteCble_Ogiro,
                        Tipo_documento = item.Tipo_documento,
                        Valor_a_aplicar = item.Valor_a_aplicar,

                        IdEmpresa_OP = item.IdEmpresa_OP,
                        IdOrdenPago_OP = item.IdOrdenPago_OP
                    };
                    Context.cp_conciliacion_Caja_det.Add(Entity_d);

                }
                #endregion

                #region Vales
                var lst_vales = Context.cp_conciliacion_Caja_det_x_ValeCaja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion_Caja == info.IdConciliacion_Caja).ToList();
                foreach (var item in lst_vales)
                {
                    Context.cp_conciliacion_Caja_det_x_ValeCaja.Remove(item);
                }

                Secuencia = 1;
                IdCbteCble_EG = data_ct.get_id(info.IdEmpresa, IdTipoCbte_EG);
                foreach (var item in info.lst_det_vale)
                {
                    if (item.IdCbteCble_movcaja == 0)
                    {
                        ct_cbtecble diario = new ct_cbtecble
                        {
                            IdEmpresa = item.IdEmpresa_movcaja = info.IdEmpresa,
                            IdTipoCbte = item.IdTipocbte_movcaja = IdTipoCbte_EG,
                            IdCbteCble = item.IdCbteCble_movcaja = IdCbteCble_EG++,
                            cb_Fecha = item.fecha,
                            cb_Observacion = "Caja # " + info.IdConciliacion_Caja +" "+ item.Observacion,
                            IdPeriodo = Convert.ToInt32(item.fecha.ToString("yyyyMM")),
                            IdSucursal = IdSucursal,
                            cb_FechaTransac = DateTime.Now,
                            cb_Estado = "A",
                            cb_Valor = item.valor,
                            IdUsuario = info.IdUsuario
                        };
                        Context_ct.ct_cbtecble.Add(diario);

                        ct_cbtecble_det Debe = new ct_cbtecble_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = 1,
                            IdCtaCble = item.IdCtaCble,
                            dc_Valor = Math.Round(Convert.ToDouble(item.valor), 2, MidpointRounding.AwayFromZero),
                        };

                        ct_cbtecble_det Haber = new ct_cbtecble_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = 2,
                            IdCtaCble = info.IdCtaCble,
                            dc_Valor = Math.Round(Convert.ToDouble(item.valor), 2, MidpointRounding.AwayFromZero) * -1,
                        };
                        Context_ct.ct_cbtecble_det.Add(Debe);
                        Context_ct.ct_cbtecble_det.Add(Haber);

                        caj_Caja_Movimiento Entity_caj = new caj_Caja_Movimiento
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipocbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            CodMoviCaja = "Caja # " + info.IdConciliacion_Caja,
                            cm_Signo = "-",
                            cm_valor = item.valor,
                            IdTipoMovi = Convert.ToInt32(item.idTipoMovi),
                            cm_observacion = item.Observacion,
                            IdCaja = info.IdCaja,
                            IdPeriodo = Convert.ToInt32(item.fecha.ToString("yyyyMM")),
                            cm_fecha = item.fecha,
                            IdTipo_Persona = cl_enumeradores.eTipoPersona.PERSONA.ToString(),
                            IdEntidad = item.IdPersona,
                            IdPersona = item.IdPersona,
                            Estado = "A",
                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = DateTime.Now
                        };
                        Context.caj_Caja_Movimiento.Add(Entity_caj);
                        caj_Caja_Movimiento_det Entity_caj_det = new caj_Caja_Movimiento_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipocbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            Secuencia = 1,
                            IdCobro_tipo = "EFEC",
                            cr_Valor = item.valor
                        };
                        Context.caj_Caja_Movimiento_det.Add(Entity_caj_det);
                    }
                    else
                        if (item.IdCbteCble_movcaja != 0 && item.se_modifico)
                    {
                        ct_cbtecble diario = Context_ct.ct_cbtecble.Where(q => q.IdEmpresa == item.IdEmpresa_movcaja && q.IdTipoCbte == item.IdTipocbte_movcaja && q.IdCbteCble == item.IdCbteCble_movcaja).FirstOrDefault();
                        if (diario != null)
                        {
                            diario.cb_Fecha = item.fecha;
                            diario.cb_Observacion = item.Observacion;
                            diario.IdPeriodo = Convert.ToInt32(item.fecha.ToString("yyyyMM"));
                            diario.IdSucursal = IdSucursal;
                            diario.cb_FechaUltModi = DateTime.Now;
                            diario.cb_Valor = item.valor;
                            diario.IdUsuarioUltModi = info.IdUsuario;

                            var lst_det_ct = Context_ct.ct_cbtecble_det.Where(q => q.IdEmpresa == item.IdEmpresa_movcaja && q.IdTipoCbte == item.IdTipocbte_movcaja && q.IdCbteCble == item.IdCbteCble_movcaja).ToList();
                            Context_ct.ct_cbtecble_det.RemoveRange(lst_det_ct);

                            ct_cbtecble_det Debe = new ct_cbtecble_det
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = 1,
                                IdCtaCble = item.IdCtaCble,
                                dc_Valor = Math.Round(Convert.ToDouble(item.valor), 2, MidpointRounding.AwayFromZero),
                            };

                            ct_cbtecble_det Haber = new ct_cbtecble_det
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = 2,
                                IdCtaCble = info.IdCtaCble,
                                dc_Valor = Math.Round(Convert.ToDouble(item.valor), 2, MidpointRounding.AwayFromZero) * -1,
                            };
                            Context_ct.ct_cbtecble_det.Add(Debe);
                            Context_ct.ct_cbtecble_det.Add(Haber);

                            caj_Caja_Movimiento Entity_caj = Context.caj_Caja_Movimiento.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdTipocbte == item.IdTipocbte_movcaja && q.IdCbteCble == item.IdCbteCble_movcaja).FirstOrDefault();
                            if (Entity_caj != null)
                            {
                                Entity_caj.IdEmpresa = diario.IdEmpresa;
                                Entity_caj.IdTipocbte = diario.IdTipoCbte;
                                Entity_caj.IdCbteCble = diario.IdCbteCble;
                                Entity_caj.CodMoviCaja = "Caja # " + info.IdConciliacion_Caja;
                                Entity_caj.cm_valor = item.valor;
                                Entity_caj.IdTipoMovi = Convert.ToInt32(item.idTipoMovi);
                                Entity_caj.cm_observacion = diario.cb_Observacion;
                                Entity_caj.IdPeriodo = Convert.ToInt32(item.fecha.ToString("yyyyMM"));
                                Entity_caj.cm_fecha = item.fecha;
                                Entity_caj.IdTipo_Persona = cl_enumeradores.eTipoPersona.PERSONA.ToString();
                                Entity_caj.IdEntidad = item.IdPersona;
                                Entity_caj.IdPersona = item.IdPersona;
                                Entity_caj.IdUsuarioUltMod = info.IdUsuario;
                                Entity_caj.Fecha_UltMod = DateTime.Now;

                                var Entity_caj_det = Context.caj_Caja_Movimiento_det.Where(q => q.IdEmpresa == diario.IdEmpresa && q.IdTipocbte == diario.IdTipoCbte && q.IdCbteCble == diario.IdCbteCble).FirstOrDefault();
                                Entity_caj_det.cr_Valor = item.valor;
                            }
                        }
                    }

                    cp_conciliacion_Caja_det_x_ValeCaja Entity_d = new cp_conciliacion_Caja_det_x_ValeCaja
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConciliacion_Caja = info.IdConciliacion_Caja,
                        Secuencia = Secuencia++,
                        IdEmpresa_movcaja = item.IdEmpresa_movcaja,
                        IdTipocbte_movcaja = item.IdTipocbte_movcaja,
                        IdCbteCble_movcaja = item.IdCbteCble_movcaja,
                        IdCtaCble = item.IdCtaCble
                    };
                    Context.cp_conciliacion_Caja_det_x_ValeCaja.Add(Entity_d);
                }
                #endregion

                #region Ingresos
                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    Secuencia = 1;
                    double saldo_egresos = info.Total_fact_vale;
                    foreach (var item in info.lst_det_ing)
                    {
                        if (saldo_egresos > 0)
                        {
                            item.valor_aplicado = item.valor_disponible <= saldo_egresos ? item.valor_disponible : saldo_egresos;
                            saldo_egresos = saldo_egresos - item.valor_aplicado;
                        }
                        cp_conciliacion_Caja_det_Ing_Caja Entity_d = new cp_conciliacion_Caja_det_Ing_Caja
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdConciliacion_Caja = info.IdConciliacion_Caja,
                            secuencia = Secuencia++,
                            IdEmpresa_movcaj = item.IdEmpresa_movcaj,
                            IdTipocbte_movcaj = item.IdTipocbte_movcaj,
                            IdCbteCble_movcaj = item.IdCbteCble_movcaj,
                            valor_aplicado = item.valor_aplicado,
                            valor_disponible = item.valor_disponible
                        };
                        Context.cp_conciliacion_Caja_det_Ing_Caja.Add(Entity_d);
                    }
                }
                #endregion

                #region Orden de pago
                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    #region Orden de pago
                    cp_orden_pago op = new cp_orden_pago
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = IdSucursal,
                        IdOrdenPago = IdOrdenPago++,
                        Observacion = "Caja #" + info.IdConciliacion_Caja+" "+info.ObservacionOP,
                        IdTipo_op = cl_enumeradores.eTipoOrdenPago.OTROS_CONC.ToString(),
                        IdTipo_Persona = info.IdTipoPersona,
                        IdPersona = info.IdPersona,
                        IdEntidad = info.IdEntidad,
                        Fecha = info.FechaOP.Date,
                        IdEstadoAprobacion = Entity_op_tipo.IdEstadoAprobacion,
                        IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.CHEQUE.ToString(),
                        Estado = "A",
                        IdUsuario = info.IdUsuario
                    };
                    info.IdEmpresa_op = op.IdEmpresa;
                    info.IdOrdenPago_op = op.IdOrdenPago;
                    Context_cxp.cp_orden_pago.Add(op);

                    ct_cbtecble diario = new ct_cbtecble
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = IdTipoCbte_op,
                        IdCbteCble = IdCbteCble_OP,
                        cb_Fecha = info.FechaOP.Date,
                        cb_Observacion = op.Observacion,
                        IdPeriodo = Convert.ToInt32(info.FechaOP.ToString("yyyyMM")),
                        IdSucursal = IdSucursal,
                        cb_FechaTransac = DateTime.Now,
                        cb_Estado = "A",
                        IdUsuario = info.IdUsuario
                    };
                    Context_ct.ct_cbtecble.Add(diario);

                    int sec = 1;
                    foreach (var item in info.lst_det_ct)
                    {
                        ct_cbtecble_det diario_det = new ct_cbtecble_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = sec++,
                            IdCtaCble = item.IdCtaCble,
                            dc_Valor = Math.Round(Convert.ToDouble(item.dc_Valor), 2, MidpointRounding.AwayFromZero),
                        };
                        Context_ct.ct_cbtecble_det.Add(diario_det);
                    }
                    cp_orden_pago_det op_det = new cp_orden_pago_det
                    {
                        IdEmpresa = op.IdEmpresa,
                        IdOrdenPago = op.IdOrdenPago,
                        Secuencia = 1,

                        IdEmpresa_cxp = diario.IdEmpresa,
                        IdTipoCbte_cxp = diario.IdTipoCbte,
                        IdCbteCble_cxp = diario.IdCbteCble,

                        Valor_a_pagar = Convert.ToDouble(info.lst_det_ct.Sum(q => q.dc_Valor_debe)),
                        IdEstadoAprobacion = Entity_op_tipo.IdEstadoAprobacion,
                        IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.EFEC.ToString(),
                        Fecha_Pago = op.Fecha
                    };
                    Context_cxp.cp_orden_pago_det.Add(op_det);
                    Entity.IdEmpresa_op = op.IdEmpresa;
                    Entity.IdOrdenPago_op = op.IdOrdenPago;
                    #endregion
                }
                #endregion

                #region Ingreso por reposicion
                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    IdCbteCble_IN = data_ct.get_id(info.IdEmpresa, IdTipoCbte_IN);
                    ct_cbtecble repo = new ct_cbtecble
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = IdTipoCbte_IN,
                        IdCbteCble = IdCbteCble_IN,
                        cb_Fecha = info.FechaOP,
                        cb_Observacion = "Caja # " + info.IdConciliacion_Caja + " Reposición",
                        IdPeriodo = Convert.ToInt32(info.FechaOP.ToString("yyyyMM")),
                        IdSucursal = IdSucursal,
                        cb_FechaTransac = DateTime.Now,
                        cb_Estado = "A",
                        cb_Valor = info.lst_det_ct.Sum(q => q.dc_Valor_debe),
                        IdUsuario = info.IdUsuario
                    };
                    Context_ct.ct_cbtecble.Add(repo);

                    ct_cbtecble_det Debe = new ct_cbtecble_det
                    {
                        IdEmpresa = repo.IdEmpresa,
                        IdTipoCbte = repo.IdTipoCbte,
                        IdCbteCble = repo.IdCbteCble,
                        secuencia = 1,
                        IdCtaCble = info.IdCtaCble,
                        dc_Valor = Math.Round(Convert.ToDouble(repo.cb_Valor), 2, MidpointRounding.AwayFromZero),
                    };

                    ct_cbtecble_det Haber = new ct_cbtecble_det
                    {
                        IdEmpresa = repo.IdEmpresa,
                        IdTipoCbte = repo.IdTipoCbte,
                        IdCbteCble = repo.IdCbteCble,
                        secuencia = 2,
                        IdCtaCble = info.IdCtaCble,
                        dc_Valor = Math.Round(Convert.ToDouble(repo.cb_Valor), 2, MidpointRounding.AwayFromZero) * -1,
                    };
                    Context_ct.ct_cbtecble_det.Add(Debe);
                    Context_ct.ct_cbtecble_det.Add(Haber);

                    caj_Caja_Movimiento Entity_caj = new caj_Caja_Movimiento
                    {
                        IdEmpresa = repo.IdEmpresa,
                        IdTipocbte = repo.IdTipoCbte,
                        IdCbteCble = repo.IdCbteCble,
                        CodMoviCaja = "Caja # " + info.IdConciliacion_Caja,
                        cm_Signo = "+",
                        cm_valor = repo.cb_Valor,
                        IdTipoMovi = Convert.ToInt32(Entity_pc.IdTipo_movi_ing_x_reposicion),
                        cm_observacion = repo.cb_Observacion,
                        IdCaja = info.IdCaja,
                        IdPeriodo = Convert.ToInt32(repo.cb_Fecha.ToString("yyyyMM")),
                        cm_fecha = repo.cb_Fecha,
                        IdTipo_Persona = info.IdTipoPersona,
                        IdEntidad = (decimal)info.IdEntidad,
                        IdPersona = info.IdPersona,
                        Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.caj_Caja_Movimiento.Add(Entity_caj);
                    caj_Caja_Movimiento_det Entity_caj_det = new caj_Caja_Movimiento_det
                    {
                        IdEmpresa = repo.IdEmpresa,
                        IdTipocbte = repo.IdTipoCbte,
                        IdCbteCble = repo.IdCbteCble,
                        Secuencia = 1,
                        IdCobro_tipo = "EFEC",
                        cr_Valor = repo.cb_Valor
                    };
                    Context.caj_Caja_Movimiento_det.Add(Entity_caj_det);
                    info.IdEmpresa_mov_caj = repo.IdEmpresa;
                    info.IdTipoCbte_mov_caj = repo.IdTipoCbte;
                    info.IdCbteCble_mov_caj = repo.IdCbteCble;
                }
                #endregion

                #region Cabecera
                Entity.IdCaja = info.IdCaja;
                Entity.IdCtaCble = info.IdCtaCble;
                Entity.Observacion = info.Observacion;
                Entity.Fecha = info.Fecha.Date;
                Entity.Fecha_ini = info.Fecha_ini;
                Entity.Fecha_fin = info.Fecha_fin;
                Entity.IdEstadoCierre = info.IdEstadoCierre;
                Entity.IdPeriodo = info.IdPeriodo;

                Entity.IdEmpresa_mov_caj = info.IdEmpresa_mov_caj;
                Entity.IdTipoCbte_mov_caj = info.IdTipoCbte_mov_caj;
                Entity.IdCbteCble_mov_caj = info.IdCbteCble_mov_caj;

                Entity.Ingresos = info.Ingresos;
                Entity.Total_fondo = info.Total_fondo;
                Entity.Saldo_cont_al_periodo = info.Saldo_cont_al_periodo;
                Entity.Total_fact_vale = info.Total_fact_vale;
                Entity.Dif_x_pagar_o_cobrar = info.Dif_x_pagar_o_cobrar;
                Entity.Total_Ing = info.Total_Ing;

                Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                #endregion

                Context_ct.SaveChanges();
                Context_cxp.SaveChanges();
                Context.SaveChanges();

                if (info.IdEstadoCierre == cl_enumeradores.eEstadoCierreCaja.EST_CIE_CER.ToString())
                {
                    int secuencia = 1;
                    var lstvales = Context.vwcaj_Caja_Movimiento_ValesNoConciliados.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == info.IdCaja).ToList();
                    foreach (var item in lstvales)
                    {
                        Context.cp_conciliacion_Caja_ValesNoConciliados.Add(new cp_conciliacion_Caja_ValesNoConciliados
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdConciliacion_Caja = info.IdConciliacion_Caja,
                            Secuencia = secuencia++,
                            IdCbteCble_movcaja = item.IdCbteCble,
                            IdEmpresa_movcaja = item.IdEmpresa,
                            IdTipocbte_movcaja = item.IdTipocbte,
                            Valor = item.cm_valor
                        });
                    }
                    //AQUI PONES LO DE LOS VALES :*
                    Context.SaveChanges();
                }

                Context_ct.Dispose();
                Context.Dispose();
                Context_cxp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Context_ct.Dispose();
                Context_cxp.Dispose();
                Context.Dispose();
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacion_Caja_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
    }
}
