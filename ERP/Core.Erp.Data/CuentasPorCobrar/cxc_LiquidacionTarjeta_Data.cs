using Core.Erp.Data.Banco;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.General;
using Core.Erp.Info;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_Data
    {
        #region Variables
        ba_Cbte_Ban_Data data_cbteban = new ba_Cbte_Ban_Data();
        ct_cbtecble_Data data_cbtecble = new ct_cbtecble_Data();
        #endregion
        public List<cxc_LiquidacionTarjeta_Info> get_list(int IdEmpresa, int IdSucursal, bool MostrarAnulados)
        {
            try
            {
                List<cxc_LiquidacionTarjeta_Info> Lista = new List <cxc_LiquidacionTarjeta_Info >();
                var IdSucursalIni = IdSucursal == 0 ? 0 : IdSucursal;
                var IdSucursalFin = IdSucursal == 0 ? 99999 : IdSucursal;

                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    if (MostrarAnulados)
                        Lista = (from q in db.vwcxc_LiquidacionTarjeta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursalIni
                                 && q.IdSucursal <= IdSucursalFin
                                 orderby q.IdLiquidacion descending
                                 select new cxc_LiquidacionTarjeta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdLiquidacion = q.IdLiquidacion,
                                     Fecha = q.Fecha,
                                     Estado = q.Estado,
                                     IdBanco = q.IdBanco,
                                     Observacion = q.Observacion,
                                     Valor = q.Valor,
                                     Lote = q.Lote,
                                     Su_Descripcion = q.Su_Descripcion,
                                     ba_descripcion = q.ba_descripcion
                                 }).ToList();
                    else
                        Lista = (from q in db.vwcxc_LiquidacionTarjeta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursalIni
                                 && q.IdSucursal <= IdSucursalFin
                                 && q.Estado == true
                                 orderby q.IdLiquidacion descending
                                 select new cxc_LiquidacionTarjeta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdLiquidacion = q.IdLiquidacion,
                                     Fecha = q.Fecha,
                                     Estado = q.Estado,
                                     IdBanco = q.IdBanco,
                                     Observacion = q.Observacion,
                                     Valor = q.Valor,
                                     Lote = q.Lote,
                                     Su_Descripcion = q.Su_Descripcion,
                                     ba_descripcion = q.ba_descripcion
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_id(int IdEmpresa, int IdSucursal)
        {

            try
            {
                decimal ID = 1;
                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    var Lista = db.cxc_LiquidacionTarjeta.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal).Select(q => q.IdLiquidacion);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return Convert.ToInt32(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_LiquidacionTarjeta_Info get_info(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {
                cxc_LiquidacionTarjeta_Info info = new cxc_LiquidacionTarjeta_Info();
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_LiquidacionTarjeta Entity = Context.cxc_LiquidacionTarjeta.Where(q => q.IdLiquidacion == IdLiquidacion && q.IdSucursal == IdSucursal && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new cxc_LiquidacionTarjeta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdLiquidacion = Entity.IdSucursal,
                        IdBanco = Entity.IdBanco,
                        Valor = Entity.Valor,
                        Fecha = Entity.Fecha,
                        Estado = Entity.Estado,
                        IdEmpresa_ct = Entity.IdEmpresa_ct,
                        IdTipoCbte_ct = Entity.IdTipoCbte_ct,
                        IdCbteCble_ct = Entity.IdCbteCble_ct,
                        Observacion = Entity.Observacion,
                        Lote = Entity.Lote
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_LiquidacionTarjeta_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    var Entity = new cxc_LiquidacionTarjeta
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdLiquidacion = info.IdLiquidacion = get_id(info.IdEmpresa,info.IdSucursal),
                        Lote = info.Lote,
                        Fecha = info.Fecha,
                        IdBanco = info.IdBanco,
                        Observacion = info.Observacion,
                        Estado = info.Estado = true,
                        Valor = info.Valor = Math.Round(info.ListaCobros.Sum(q=>q.Valor),2,MidpointRounding.AwayFromZero),
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    int Secuencia = 1;
                    foreach (var item in info.ListaCobros)
                    {
                        db.cxc_LiquidacionTarjeta_x_cxc_cobro.Add(new cxc_LiquidacionTarjeta_x_cxc_cobro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdLiquidacion = info.IdLiquidacion,
                            Secuencia = Secuencia++,
                            Valor = item.Valor,
                            IdCobro = item.IdCobro
                        });
                    }
                    Secuencia = 1;
                    foreach (var item in info.ListaDet)
                    {
                        var motivo = db.cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMotivo == item.IdMotivo).FirstOrDefault();
                        if(motivo != null)
                            item.IdCtaCble = motivo.IdCtaCble;
                        db.cxc_LiquidacionTarjetaDet.Add(new cxc_LiquidacionTarjetaDet
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdLiquidacion = info.IdLiquidacion,
                            Secuencia = Secuencia++,
                            IdMotivo = item.IdMotivo,
                            Porcentaje = item.Porcentaje,
                            Valor = item.Valor
                        });
                    }
                    db.cxc_LiquidacionTarjeta.Add(Entity);
                    db.SaveChanges();
                    var cobro_tipo = db.cxc_cobro_tipo_Param_conta_x_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro_tipo == "TARJ").FirstOrDefault();
                    if (cobro_tipo != null)
                    {
                        var CbteBan = ArmarDiario(info,cobro_tipo.IdCtaCble);
                        if (CbteBan != null)
                            if (data_cbteban.guardarDB(CbteBan, Info.Helps.cl_enumeradores.eTipoCbteBancario.NCBA))
                            {
                                Entity.IdEmpresa_ct = info.IdEmpresa_ct = CbteBan.IdEmpresa;
                                Entity.IdTipoCbte_ct = info.IdTipoCbte_ct = CbteBan.IdTipocbte;
                                Entity.IdCbteCble_ct =  info.IdCbteCble_ct = CbteBan.IdCbteCble;
                                db.SaveChanges();
                            }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_LiquidacionTarjeta_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public ba_Cbte_Ban_Info ArmarDiario(cxc_LiquidacionTarjeta_Info info, string IdCtaCble_tarjeta)
        {
            Entities_banco db_b = new Entities_banco();
            
            try
            {
                var TipoCbte = db_b.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.CodTipoCbteBan == "NCBA").FirstOrDefault();
                if (TipoCbte == null)
                    return null;

                if (info.IdTipoCbte_ct == null)
                    info.IdTipoCbte_ct = TipoCbte.IdTipoCbteCble;

                ba_Cbte_Ban_Info diario = new ba_Cbte_Ban_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipocbte = Convert.ToInt32(info.IdTipoCbte_ct),
                    IdCbteCble = info.IdCbteCble_ct == null ? 0 : Convert.ToInt32(info.IdCbteCble_ct),
                    cb_Fecha = info.Fecha,
                    IdUsuario = info.IdUsuarioCreacion,
                    IdUsuarioUltMod = info.IdUsuarioModificacion,
                    Estado = "A",
                    IdBanco = info.IdBanco,
                    cb_Valor = Math.Round(info.ListaCobros.Sum(q=>q.Valor),2,MidpointRounding.AwayFromZero),
                    IdSucursal = info.IdSucursal,
                    cb_Observacion = "LIQ. TARJ. #"+info.IdLiquidacion+" "+info.Observacion,
                    lst_det_ct = new List<ct_cbtecble_det_Info>()
                };
                int Secuencia = 1;
                var banco = db_b.ba_Banco_Cuenta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco).FirstOrDefault();
                diario.lst_det_ct.Add(new ct_cbtecble_det_Info
                {
                    secuencia = Secuencia++,
                    IdCtaCble = banco.IdCtaCble,
                    dc_Valor = Math.Round(diario.cb_Valor - info.ListaDet.Sum(q=> q.Valor),2,MidpointRounding.AwayFromZero),
                    dc_para_conciliar = true,
                    dc_para_conciliar_null = true,                    
                });
                foreach (var item in info.ListaDet)
                {
                    diario.lst_det_ct.Add(new ct_cbtecble_det_Info
                    {
                        secuencia = Secuencia++,
                        IdCtaCble = item.IdCtaCble,
                        dc_Valor = Math.Round(item.Valor,2,MidpointRounding.AwayFromZero),
                    });
                }
                diario.lst_det_ct.Add(new ct_cbtecble_det_Info
                {
                    secuencia = Secuencia++,
                    IdCtaCble = IdCtaCble_tarjeta,
                    dc_Valor = Math.Round(info.Valor, 2, MidpointRounding.AwayFromZero)*-1,
                });

                diario.lst_det_canc_op = new List<Info.CuentasPorPagar.cp_orden_pago_cancelaciones_Info>();
                diario.lst_det_ing = new List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info>();
                
                if (Math.Round(diario.lst_det_ct.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
                    return null;

                if (diario.lst_det_ct.Where(q => string.IsNullOrEmpty(q.IdCtaCble)).Count() > 0)
                    return null;
                db_b.Dispose();
                return diario;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cxc_LiquidacionTarjeta_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    var Entity = db.cxc_LiquidacionTarjeta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdLiquidacion == info.IdLiquidacion).FirstOrDefault();
                    if (Entity == null) return false;
                    
                    Entity.Lote = info.Lote;
                    Entity.Fecha = info.Fecha;
                    Entity.IdBanco = info.IdBanco;
                    Entity.Observacion = info.Observacion;
                    Entity.Valor = info.Valor = Math.Round(info.ListaCobros.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero);
                    Entity.IdUsuarioModificacion = info.IdUsuarioCreacion;
                    Entity.FechaModificacion = DateTime.Now;
                    
                    int Secuencia = 1;
                    var lst_cobros = db.cxc_LiquidacionTarjeta_x_cxc_cobro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdLiquidacion == info.IdLiquidacion).ToList();
                    db.cxc_LiquidacionTarjeta_x_cxc_cobro.RemoveRange(lst_cobros);

                    foreach (var item in info.ListaCobros)
                    {
                        db.cxc_LiquidacionTarjeta_x_cxc_cobro.Add(new cxc_LiquidacionTarjeta_x_cxc_cobro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdLiquidacion = info.IdLiquidacion,
                            Secuencia = Secuencia++,
                            Valor = item.Valor,
                            IdCobro = item.IdCobro
                        });
                    }
                    Secuencia = 1;
                    var lst_motivos = db.cxc_LiquidacionTarjetaDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdLiquidacion == info.IdLiquidacion).ToList();
                    db.cxc_LiquidacionTarjetaDet.RemoveRange(lst_motivos);
                    foreach (var item in info.ListaDet)
                    {
                        var motivo = db.cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMotivo == item.IdMotivo).FirstOrDefault();
                        if (motivo != null)
                            item.IdCtaCble = motivo.IdCtaCble;
                        db.cxc_LiquidacionTarjetaDet.Add(new cxc_LiquidacionTarjetaDet
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdLiquidacion = info.IdLiquidacion,
                            Secuencia = Secuencia++,
                            IdMotivo = item.IdMotivo,
                            Porcentaje = item.Porcentaje,
                            Valor = item.Valor
                        });
                    }
                    db.SaveChanges();
                    var cobro_tipo = db.cxc_cobro_tipo_Param_conta_x_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro_tipo == "TARJ").FirstOrDefault();
                    if (cobro_tipo != null)
                    {
                        var CbteBan = ArmarDiario(info, cobro_tipo.IdCtaCble);
                        if (CbteBan != null)
                            if (CbteBan.IdCbteCble == 0)
                            {
                                if (data_cbteban.guardarDB(CbteBan, Info.Helps.cl_enumeradores.eTipoCbteBancario.NCBA))
                                {
                                    Entity.IdEmpresa_ct = info.IdEmpresa_ct = CbteBan.IdEmpresa;
                                    Entity.IdTipoCbte_ct = info.IdTipoCbte_ct = CbteBan.IdTipocbte;
                                    Entity.IdCbteCble_ct = info.IdCbteCble_ct = CbteBan.IdCbteCble;
                                    db.SaveChanges();
                                }
                            }else
                            {
                                if(data_cbteban.modificarDB(CbteBan,Info.Helps.cl_enumeradores.eTipoCbteBancario.NCBA))
                                {

                                }
                            }                            
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_LiquidacionTarjeta_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool anularDB(cxc_LiquidacionTarjeta_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    var Entity = db.cxc_LiquidacionTarjeta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdLiquidacion == info.IdLiquidacion).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;
                    
                    var lst_cobros = db.cxc_LiquidacionTarjeta_x_cxc_cobro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdLiquidacion == info.IdLiquidacion).ToList();
                    db.cxc_LiquidacionTarjeta_x_cxc_cobro.RemoveRange(lst_cobros);

                    db.SaveChanges();
                    if (info.IdCbteCble_ct != null)
                    {
                        if (data_cbtecble.anularDB(new ct_cbtecble_Info { IdEmpresa = info.IdEmpresa, IdTipoCbte = (int)info.IdTipoCbte_ct, IdCbteCble = (decimal)info.IdCbteCble_ct, IdUsuarioAnu = info.IdUsuarioAnulacion, cb_MotivoAnu = info.MotivoAnulacion }))
                        {
                            if (data_cbteban.anularDB(new ba_Cbte_Ban_Info { IdEmpresa = info.IdEmpresa, IdTipocbte = (int)info.IdTipoCbte_ct, IdCbteCble = (decimal)info.IdCbteCble_ct, IdUsuario_Anu = info.IdUsuarioAnulacion, MotivoAnulacion = info.MotivoAnulacion }))
                            {

                            }
                        }
                        
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
