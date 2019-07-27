using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Data.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Core.Erp.Data.Banco
{
    public class ba_Cbte_Ban_Data
    {
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        cp_orden_pago_cancelaciones_Data odata_can = new cp_orden_pago_cancelaciones_Data();
        public List<ba_Cbte_Ban_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal, string CodCbte, bool mostrar_anulados)
        {
            try
            {
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;

                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;

                List<ba_Cbte_Ban_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.vwba_Cbte_Ban
                             where q.IdEmpresa == IdEmpresa
                             && Fecha_ini <= q.cb_Fecha
                             && q.cb_Fecha <= Fecha_fin
                             && q.CodTipoCbteBan == CodCbte
                             && IdSucursal_ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_fin
                             orderby q.IdCbteCble descending
                             select new ba_Cbte_Ban_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipocbte = q.IdTipocbte,
                                 IdCbteCble = q.IdCbteCble,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_Observacion = q.cb_Observacion,
                                 Estado = q.Estado,
                                 CodTipoCbteBan = q.CodTipoCbteBan,
                                 ba_descripcion = q.ba_descripcion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Su_Descripcion = q.Su_Descripcion,
                                 cb_Cheque = q.cb_Cheque,
                                 cb_giradoA = q.cb_giradoA,
                                 cb_Valor = q.cb_Valor,
                                 Imprimir_Solo_el_cheque = q.Imprimir_Solo_el_cheque,
                                 IdBanco = q.IdBanco,
                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.vwba_Cbte_Ban
                                 where q.IdEmpresa == IdEmpresa
                                 && Fecha_ini <= q.cb_Fecha
                                 && q.cb_Fecha <= Fecha_fin
                                 && q.CodTipoCbteBan == CodCbte
                                 && IdSucursal_ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_fin
                                 && q.Estado == "A"
                                 orderby q.IdCbteCble descending
                                 select new ba_Cbte_Ban_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipocbte = q.IdTipocbte,
                                     IdCbteCble = q.IdCbteCble,
                                     cb_Fecha = q.cb_Fecha,
                                     cb_Observacion = q.cb_Observacion,
                                     Estado = q.Estado,
                                     CodTipoCbteBan = q.CodTipoCbteBan,
                                     ba_descripcion = q.ba_descripcion,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     Su_Descripcion = q.Su_Descripcion,
                                     cb_Cheque = q.cb_Cheque,
                                     cb_giradoA = q.cb_giradoA,
                                     cb_Valor = q.cb_Valor,
                                     Imprimir_Solo_el_cheque = q.Imprimir_Solo_el_cheque,
                                     IdBanco = q.IdBanco,
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

        public ba_Cbte_Ban_Info get_info(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                ba_Cbte_Ban_Info info;

                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Cbte_Ban Entity = Context.ba_Cbte_Ban.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipocbte == IdTipoCbte && q.IdCbteCble == IdCbteCble).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new ba_Cbte_Ban_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCbteCble = Entity.IdCbteCble,
                        IdTipocbte = Entity.IdTipocbte,
                        Cod_Cbtecble = Entity.Cod_Cbtecble,
                        IdPeriodo = Entity.IdPeriodo,
                        IdBanco = Entity.IdBanco,
                        cb_Fecha = Entity.cb_Fecha,
                        cb_Observacion = Entity.cb_Observacion,
                        cb_Valor = Entity.cb_Valor,
                        cb_Cheque = Entity.cb_Cheque,
                        Estado = Entity.Estado,
                        IdPersona_Girado_a = Entity.IdPersona_Girado_a,
                        cb_giradoA = Entity.cb_giradoA,
                        cb_ciudadChq = Entity.cb_ciudadChq,
                        IdTipoFlujo = Entity.IdTipoFlujo,
                        IdTipoNota = Entity.IdTipoNota,
                        ValorEnLetras = Entity.ValorEnLetras,
                        IdSucursal = Entity.IdSucursal,
                        IdEstado_Cbte_Ban_cat = Entity.IdEstado_Cbte_Ban_cat,
                        IdEstado_Preaviso_ch_cat = Entity.IdEstado_Preaviso_ch_cat,
                        IdEstado_cheque_cat = Entity.IdEstado_cheque_cat,
                        IdPersona = Entity.IdPersona == null ? 0 : Convert.ToDecimal(Entity.IdPersona),
                        IdEntidad = Entity.IdEntidad == null ? 0 : Convert.ToDecimal(Entity.IdEntidad),
                        IdTipo_Persona = Entity.IdTipo_Persona,
                        IdSolicitudPago = Entity.IdSolicitudPago
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_Cbte_Ban_Info info, cl_enumeradores.eTipoCbteBancario TipoCbteBanco)
        {
            Entities_banco Context_b = new Entities_banco();
            Entities_contabilidad Context_ct = new Entities_contabilidad();
            Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
            try
            {
                #region Variables
                string TipoCbteBancoS = TipoCbteBanco.ToString();
                int secuencia = 1;
                decimal Idcancelacion = 0;
                #endregion

                #region Datos para el diario
                var e_TipoCbteBan = Context_b.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.Where(q => q.CodTipoCbteBan == TipoCbteBancoS && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (e_TipoCbteBan == null)
                    return false;
                info.IdTipocbte = e_TipoCbteBan.IdTipoCbteCble;
                info.IdCbteCble = odata_ct.get_id(info.IdEmpresa, info.IdTipocbte);
                #endregion

                #region Diario
                Context_ct.ct_cbtecble.Add(new ct_cbtecble
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = info.IdTipocbte,
                    IdCbteCble = info.IdCbteCble,
                    cb_Fecha = info.cb_Fecha.Date,
                    cb_Observacion = info.cb_Observacion,
                    IdPeriodo = info.IdPeriodo = Convert.ToInt32(info.cb_Fecha.Date.ToString("yyyyMM")),
                    IdSucursal = info.IdSucursal,
                    
                    cb_FechaTransac = DateTime.Now,
                    cb_Estado = "A",
                    cb_Valor = Math.Round(info.lst_det_ct.Where(q=> q.dc_Valor > 0).Sum(q=> q.dc_Valor),2,MidpointRounding.AwayFromZero),
                    IdUsuario = info.IdUsuario
                });

                foreach (var item in info.lst_det_ct)
                {
                    Context_ct.ct_cbtecble_det.Add(new ct_cbtecble_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = info.IdTipocbte,
                        IdCbteCble = info.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = item.IdCtaCble,
                        dc_Valor = Math.Round(item.dc_Valor, 2, MidpointRounding.AwayFromZero),
                        dc_para_conciliar = item.dc_para_conciliar,
                        IdPunto_cargo = item.IdPunto_cargo,
                        IdPunto_cargo_grupo = item.IdPunto_cargo_grupo,
                        IdCentroCosto = item.IdCentroCosto,
                    });
                }
                #endregion

                #region Cbte bancario
                Context_b.ba_Cbte_Ban.Add(new ba_Cbte_Ban
                {
                    IdEmpresa = info.IdEmpresa,
                    IdCbteCble = info.IdCbteCble,
                    IdTipocbte = info.IdTipocbte,
                    Cod_Cbtecble = info.Cod_Cbtecble,
                    IdPeriodo = info.IdPeriodo,
                    IdBanco = info.IdBanco,
                    cb_Fecha = info.cb_Fecha,
                    cb_Observacion = info.cb_Observacion,
                    cb_Valor = info.cb_Valor = Math.Round(info.lst_det_ct.Where(q => q.dc_Valor > 0).Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero),
                    cb_Cheque = info.cb_Cheque,
                    Estado = "A",
                    IdPersona_Girado_a = info.IdPersona_Girado_a,
                    cb_giradoA = info.cb_giradoA,
                    cb_ciudadChq = info.cb_ciudadChq,
                    IdTipoFlujo = info.IdTipoFlujo,
                    IdTipoNota = info.IdTipoNota,
                    ValorEnLetras = info.ValorEnLetras,
                    IdSucursal = info.IdSucursal,
                    IdEstado_Cbte_Ban_cat = info.IdEstado_Cbte_Ban_cat,
                    IdEstado_Preaviso_ch_cat = info.IdEstado_Preaviso_ch_cat,
                    IdEstado_cheque_cat = info.IdEstado_cheque_cat = "ESTCBEMI",
                    IdPersona = info.IdPersona,
                    IdEntidad = info.IdEntidad,
                    IdTipo_Persona = info.IdTipo_Persona,
                    IdSolicitudPago = info.IdSolicitudPago,

                    IdUsuario = info.IdUsuario,
                    Fecha_Transac = DateTime.Now,
                });
                #endregion

                #region Flujo
                info.list_det = info.list_det == null ? new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>() : info.list_det;
                if (info.list_det.Count > 0)
                {
                    int Secuencia = 1;
                    foreach (var item in info.list_det)
                    {
                        Context_b.ba_Cbte_Ban_x_ba_TipoFlujo.Add(new ba_Cbte_Ban_x_ba_TipoFlujo
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTipocbte = info.IdTipocbte,
                            IdCbteCble = info.IdCbteCble,
                            IdTipoFlujo = item.IdTipoFlujo,
                            Valor = item.Valor,
                            Porcentaje = item.Porcentaje,
                            Secuencia = Secuencia ++
                        });
                    }
                }
                #endregion
                switch (TipoCbteBanco)
                {
                    case cl_enumeradores.eTipoCbteBancario.CHEQ:
#region Guardo cancelaciones
                        Idcancelacion = odata_can.get_id(info.IdEmpresa);
                        secuencia = 1;
                        foreach (var item in info.lst_det_canc_op)
                        {
                            Context_cxp.cp_orden_pago_cancelaciones.Add(new cp_orden_pago_cancelaciones
                            {
                                IdEmpresa = info.IdEmpresa,
                                Idcancelacion = Idcancelacion,
                                Secuencia = secuencia++,

                                IdEmpresa_op = item.IdEmpresa_op,
                                IdOrdenPago_op = item.IdOrdenPago_op,
                                Secuencia_op = item.Secuencia_op,

                                IdEmpresa_cxp = item.IdEmpresa_cxp,
                                IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                                IdCbteCble_cxp = item.IdCbteCble_cxp,

                                IdEmpresa_pago = info.IdEmpresa,
                                IdTipoCbte_pago = info.IdTipocbte,
                                IdCbteCble_pago = info.IdCbteCble,

                                MontoAplicado = item.MontoAplicado,
                                SaldoActual = 0,
                                SaldoAnterior = 0,
                                fechaTransaccion = DateTime.Now,
                                Observacion = item.Observacion
                            });
                        }
#endregion

                        var cheque = Context_b.ba_Talonario_cheques_x_banco.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco && q.Num_cheque == info.cb_Cheque).FirstOrDefault();
                        if(cheque != null)
                        {
                            cheque.IdEmpresa_cbtecble_Usado = info.IdEmpresa;
                            cheque.IdTipoCbte_cbtecble_Usado = info.IdTipocbte;
                            cheque.IdCbteCble_cbtecble_Usado = info.IdCbteCble;
                            cheque.Usado = true;
                        }
                        break;
                    case cl_enumeradores.eTipoCbteBancario.DEPO:
#region Guardo ingresos
                        foreach (var item in info.lst_det_ing)
                        {
                            Context_b.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.Add(new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito
                            {
                                mba_IdEmpresa = info.IdEmpresa,
                                mba_IdTipocbte = info.IdTipocbte,
                                mba_IdCbteCble = info.IdCbteCble,

                                mcj_IdEmpresa = item.mcj_IdEmpresa,
                                mcj_IdTipocbte = item.mcj_IdTipocbte,
                                mcj_IdCbteCble = item.mcj_IdCbteCble,
                                mcj_Secuencia = item.mcj_Secuencia
                            });
                        }
#endregion
                        break;
                    case cl_enumeradores.eTipoCbteBancario.NCBA:
                        break;
                    case cl_enumeradores.eTipoCbteBancario.NDBA:
#region Guardo cancelaciones
                        Idcancelacion = odata_can.get_id(info.IdEmpresa);
                        secuencia = 1;
                        foreach (var item in info.lst_det_canc_op)
                        {
                            Context_cxp.cp_orden_pago_cancelaciones.Add(new cp_orden_pago_cancelaciones
                            {
                                IdEmpresa = info.IdEmpresa,
                                Idcancelacion = Idcancelacion,
                                Secuencia = secuencia++,

                                IdEmpresa_op = item.IdEmpresa_op,
                                IdOrdenPago_op = item.IdOrdenPago_op,
                                Secuencia_op = item.Secuencia_op,

                                IdEmpresa_cxp = item.IdEmpresa_cxp,
                                IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                                IdCbteCble_cxp = item.IdCbteCble_cxp,

                                IdEmpresa_pago = info.IdEmpresa,
                                IdTipoCbte_pago = info.IdTipocbte,
                                IdCbteCble_pago = info.IdCbteCble,

                                MontoAplicado = item.MontoAplicado,
                                SaldoActual = 0,
                                SaldoAnterior = 0,
                                fechaTransaccion = DateTime.Now,
                                Observacion = item.Observacion
                            });
                        }
#endregion
                        break;
                }

                Context_ct.SaveChanges();
                Context_b.SaveChanges();
                Context_cxp.SaveChanges();

                Context_ct.Dispose();
                Context_b.Dispose();
                Context_cxp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Context_ct.Dispose();
                Context_b.Dispose();
                Context_cxp.Dispose();
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Cbte_Ban_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB_EstadoCheque(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble, string EstadoCheque)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var cbte = Context.ba_Cbte_Ban.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipocbte == IdTipoCbte && q.IdCbteCble == IdCbteCble).FirstOrDefault();
                    if (cbte != null)
                        cbte.IdEstado_cheque_cat = EstadoCheque;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ba_Cbte_Ban_Info info, cl_enumeradores.eTipoCbteBancario TipoCbteBanco)
        {
            Entities_banco Context_b = new Entities_banco();
            Entities_contabilidad Context_ct = new Entities_contabilidad();
            Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
            try
            {
#region Variables
                string TipoCbteBancoS = TipoCbteBanco.ToString();
                int secuencia = 1;
                decimal Idcancelacion = 0;
#endregion

#region Diario
                var diario = Context_ct.ct_cbtecble.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipocbte && q.IdCbteCble == info.IdCbteCble).FirstOrDefault();
                if (diario == null)
                    return false;
                
                diario.cb_Fecha = info.cb_Fecha.Date;
                diario.cb_Observacion = info.cb_Observacion;
                diario.IdPeriodo = info.IdPeriodo = Convert.ToInt32(info.cb_Fecha.Date.ToString("yyyyMM"));
                diario.IdSucursal = info.IdSucursal;
                diario.cb_Valor = info.cb_Valor;
                diario.cb_FechaUltModi = DateTime.Now;
                diario.IdUsuarioUltModi = info.IdUsuario;

                var lst_diario_det = Context_ct.ct_cbtecble_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipocbte && q.IdCbteCble == info.IdCbteCble).ToList();
                Context_ct.ct_cbtecble_det.RemoveRange(lst_diario_det);
                foreach (var item in info.lst_det_ct)
                {
                    Context_ct.ct_cbtecble_det.Add(new ct_cbtecble_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = info.IdTipocbte,
                        IdCbteCble = info.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = item.IdCtaCble,
                        dc_Valor = Math.Round(item.dc_Valor, 2, MidpointRounding.AwayFromZero),
                        dc_para_conciliar = item.dc_para_conciliar,
                        IdPunto_cargo = item.IdPunto_cargo,
                        IdPunto_cargo_grupo = item.IdPunto_cargo_grupo,
                        IdCentroCosto = item.IdCentroCosto,
                    });
                }
                #endregion
                var list_det = Context_b.ba_Cbte_Ban_x_ba_TipoFlujo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipocbte == info.IdTipocbte && q.IdCbteCble == info.IdCbteCble).ToList();
                Context_b.ba_Cbte_Ban_x_ba_TipoFlujo.RemoveRange(list_det);
                info.list_det = info.list_det == null ? new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>() : info.list_det;
                if (info.list_det.Count>0)
                {
                    foreach (var item in info.list_det)
                    {
                        Context_b.ba_Cbte_Ban_x_ba_TipoFlujo.Add(new ba_Cbte_Ban_x_ba_TipoFlujo
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTipocbte = info.IdTipocbte,
                            IdCbteCble = info.IdCbteCble,
                            IdTipoFlujo = item.IdTipoFlujo,
                            Valor = item.Valor,
                            Porcentaje = item.Porcentaje,
                            Secuencia = item.Secuencia
                        });
                    }
                }
                
                #region Cbte bancario
                var mov_ban = Context_b.ba_Cbte_Ban.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipocbte == info.IdTipocbte && q.IdCbteCble == info.IdCbteCble).FirstOrDefault();
                if (mov_ban == null)
                    Context_b.ba_Cbte_Ban.Add(new ba_Cbte_Ban
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCbteCble = info.IdCbteCble,
                        IdTipocbte = info.IdTipocbte,
                        Cod_Cbtecble = info.Cod_Cbtecble,
                        IdPeriodo = info.IdPeriodo,
                        IdBanco = info.IdBanco,
                        cb_Fecha = info.cb_Fecha,
                        cb_Observacion = info.cb_Observacion,
                        cb_Valor = info.cb_Valor,
                        cb_Cheque = info.cb_Cheque,
                        Estado = "A",
                        IdPersona_Girado_a = info.IdPersona_Girado_a,
                        cb_giradoA = info.cb_giradoA,
                        cb_ciudadChq = info.cb_ciudadChq,
                        IdTipoFlujo = info.IdTipoFlujo,
                        IdTipoNota = info.IdTipoNota,
                        ValorEnLetras = info.ValorEnLetras,
                        IdSucursal = info.IdSucursal,
                        IdEstado_Cbte_Ban_cat = info.IdEstado_Cbte_Ban_cat,
                        IdEstado_Preaviso_ch_cat = info.IdEstado_Preaviso_ch_cat,
                        IdEstado_cheque_cat = info.IdEstado_cheque_cat,
                        IdPersona = info.IdPersona,
                        IdEntidad = info.IdEntidad,
                        IdTipo_Persona = info.IdTipo_Persona,
                        IdSolicitudPago = info.IdSolicitudPago,
                        IdUsuarioUltMod = info.IdUsuarioUltMod,
                        Fecha_UltMod = DateTime.Now
                    });
                else
                {                    
                    mov_ban.Cod_Cbtecble = info.Cod_Cbtecble;
                    mov_ban.IdPeriodo = info.IdPeriodo;
                    mov_ban.IdBanco = info.IdBanco;
                    mov_ban.cb_Fecha = info.cb_Fecha;
                    mov_ban.cb_Observacion = info.cb_Observacion;
                    mov_ban.cb_Valor = info.cb_Valor;
                    mov_ban.cb_Cheque = info.cb_Cheque;
                    mov_ban.IdPersona_Girado_a = info.IdPersona_Girado_a;
                    mov_ban.cb_giradoA = info.cb_giradoA;
                    mov_ban.cb_ciudadChq = info.cb_ciudadChq;
                    mov_ban.IdTipoFlujo = info.IdTipoFlujo;
                    mov_ban.IdTipoNota = info.IdTipoNota;
                    mov_ban.ValorEnLetras = info.ValorEnLetras;
                    mov_ban.IdSucursal = info.IdSucursal;
                    mov_ban.IdEstado_Cbte_Ban_cat = info.IdEstado_Cbte_Ban_cat;
                    mov_ban.IdEstado_Preaviso_ch_cat = info.IdEstado_Preaviso_ch_cat;
                    mov_ban.IdEstado_cheque_cat = info.IdEstado_cheque_cat;
                    mov_ban.IdPersona = info.IdPersona;
                    mov_ban.IdEntidad = info.IdEntidad;
                    mov_ban.IdTipo_Persona = info.IdTipo_Persona;
                    mov_ban.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    mov_ban.Fecha_UltMod = DateTime.Now;
                    mov_ban.IdSolicitudPago = info.IdSolicitudPago;
                }
#endregion

                switch (TipoCbteBanco)
                {
                    case cl_enumeradores.eTipoCbteBancario.CHEQ:
#region Guardo cancelaciones
                        var lst_cancelaciones = Context_cxp.cp_orden_pago_cancelaciones.Where(q => q.IdEmpresa_pago == info.IdEmpresa && q.IdTipoCbte_pago == info.IdTipocbte && q.IdCbteCble_pago == info.IdCbteCble).ToList();
                        Context_cxp.cp_orden_pago_cancelaciones.RemoveRange(lst_cancelaciones);
                        Idcancelacion = odata_can.get_id(info.IdEmpresa);
                        secuencia = 1;
                        foreach (var item in info.lst_det_canc_op)
                        {
                            Context_cxp.cp_orden_pago_cancelaciones.Add(new cp_orden_pago_cancelaciones
                            {
                                IdEmpresa = info.IdEmpresa,
                                Idcancelacion = Idcancelacion,
                                Secuencia = secuencia++,

                                IdEmpresa_op = item.IdEmpresa_op,
                                IdOrdenPago_op = item.IdOrdenPago_op,
                                Secuencia_op = item.Secuencia_op,

                                IdEmpresa_cxp = item.IdEmpresa_cxp,
                                IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                                IdCbteCble_cxp = item.IdCbteCble_cxp,

                                IdEmpresa_pago = info.IdEmpresa,
                                IdTipoCbte_pago = info.IdTipocbte,
                                IdCbteCble_pago = info.IdCbteCble,

                                MontoAplicado = item.MontoAplicado,
                                SaldoActual = 0,
                                SaldoAnterior = 0,
                                fechaTransaccion = DateTime.Now,
                                Observacion = item.Observacion
                            });
                        }
#endregion
                        break;
                    case cl_enumeradores.eTipoCbteBancario.DEPO:
#region Guardo ingresos
                        var lst_ing = Context_b.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.Where(q => q.mba_IdEmpresa == info.IdEmpresa && q.mba_IdTipocbte == info.IdTipocbte && q.mba_IdCbteCble == info.IdCbteCble).ToList();
                        Context_b.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.RemoveRange(lst_ing);
                        foreach (var item in info.lst_det_ing)
                        {
                            Context_b.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.Add(new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito
                            {
                                mba_IdEmpresa = info.IdEmpresa,
                                mba_IdTipocbte = info.IdTipocbte,
                                mba_IdCbteCble = info.IdCbteCble,

                                mcj_IdEmpresa = item.mcj_IdEmpresa,
                                mcj_IdTipocbte = item.mcj_IdTipocbte,
                                mcj_IdCbteCble = item.mcj_IdCbteCble,
                                mcj_Secuencia = item.mcj_Secuencia
                            });
                        }
#endregion
                        break;
                    case cl_enumeradores.eTipoCbteBancario.NCBA:
                        break;
                    case cl_enumeradores.eTipoCbteBancario.NDBA:
#region Guardo cancelaciones
                        var lst_cancelaciones_ndba = Context_cxp.cp_orden_pago_cancelaciones.Where(q => q.IdEmpresa_pago == info.IdEmpresa && q.IdTipoCbte_pago == info.IdTipocbte && q.IdCbteCble_pago == info.IdCbteCble).ToList();
                        Context_cxp.cp_orden_pago_cancelaciones.RemoveRange(lst_cancelaciones_ndba);
                        Idcancelacion = odata_can.get_id(info.IdEmpresa);
                        secuencia = 1;
                        foreach (var item in info.lst_det_canc_op)
                        {
                            Context_cxp.cp_orden_pago_cancelaciones.Add(new cp_orden_pago_cancelaciones
                            {
                                IdEmpresa = info.IdEmpresa,
                                Idcancelacion = Idcancelacion,
                                Secuencia = secuencia++,

                                IdEmpresa_op = item.IdEmpresa_op,
                                IdOrdenPago_op = item.IdOrdenPago_op,
                                Secuencia_op = item.Secuencia_op,

                                IdEmpresa_cxp = item.IdEmpresa_cxp,
                                IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                                IdCbteCble_cxp = item.IdCbteCble_cxp,

                                IdEmpresa_pago = info.IdEmpresa,
                                IdTipoCbte_pago = info.IdTipocbte,
                                IdCbteCble_pago = info.IdCbteCble,

                                MontoAplicado = item.MontoAplicado,
                                SaldoActual = 0,
                                SaldoAnterior = 0,
                                fechaTransaccion = DateTime.Now,
                                Observacion = item.Observacion
                            });
                        }
#endregion
                        break;
                }

                Context_ct.SaveChanges();
                Context_b.SaveChanges();
                Context_cxp.SaveChanges();

                Context_ct.Dispose();
                Context_b.Dispose();
                Context_cxp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Context_ct.Dispose();
                Context_b.Dispose();
                Context_cxp.Dispose();
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Cbte_Ban_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(ba_Cbte_Ban_Info info)
        {
            Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
            Entities_banco Context = new Entities_banco();
            try
            {
                ba_Cbte_Ban Entity = Context.ba_Cbte_Ban.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipocbte == info.IdTipocbte && q.IdCbteCble == info.IdCbteCble).FirstOrDefault();
                if (Entity == null) return false;
                Entity.MotivoAnulacion = info.MotivoAnulacion;
                Entity.IdUsuario_Anu = info.IdUsuario_Anu;
                Entity.FechaAnulacion = DateTime.Now;
                Entity.IdEstado_cheque_cat = "ESTCBANU";
                Entity.Estado = "I";

                var lst_ing = Context.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.Where(q => q.mba_IdEmpresa == info.IdEmpresa && q.mba_IdTipocbte == info.IdTipocbte && q.mba_IdCbteCble == info.IdCbteCble).ToList();
                Context.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.RemoveRange(lst_ing);

                var lst_cance = Context_cxp.cp_orden_pago_cancelaciones.Where(q => q.IdEmpresa_pago == info.IdEmpresa && q.IdTipoCbte_pago == info.IdTipocbte && q.IdCbteCble_pago == info.IdCbteCble).ToList();
                Context_cxp.cp_orden_pago_cancelaciones.RemoveRange(lst_cance);

                Context_cxp.SaveChanges();
                Context.SaveChanges();


                Context_cxp.Dispose();
                Context.Dispose();
                return true;
            }
            catch (Exception)
            {
                Context_cxp.Dispose();
                Context.Dispose();
                throw;
            }
        }
    }
}
