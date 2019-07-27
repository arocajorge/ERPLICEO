using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
  public  class ba_Archivo_Transferencia_Data
    {
        public List<ba_Archivo_Transferencia_Info> GetList(int IdEmpresa, int IdSucursal, DateTime fechaini, DateTime fechafin, bool mostrar_anulados)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<ba_Archivo_Transferencia_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    if(mostrar_anulados)
                    {
                        Lista = Context.ba_Archivo_Transferencia.Where(q => q.IdEmpresa == IdEmpresa
                             && IdSucursal_ini <= q.IdSucursal 
                             && q.IdSucursal <= IdSucursal_fin
                             && fechaini <= q.Fecha 
                             && q.Fecha <= fechafin
                        ).Select(q => new ba_Archivo_Transferencia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            cod_archivo = q.cod_archivo,
                            Cod_Empresa = q.Cod_Empresa,
                            Contabilizado = q.Contabilizado,
                            Estado = q.Estado,
                            Fecha = q.Fecha,
                            Fecha_Proceso = q.Fecha_Proceso,
                            IdArchivo = q.IdArchivo,
                            IdBanco = q.IdBanco,
                            IdProceso_bancario = q.IdProceso_bancario,
                            Nom_Archivo = q.Nom_Archivo,
                            Observacion = q.Observacion,
                            IdSucursal = q.IdSucursal,
                            cb_Valor = q.cb_Valor,
                            IdCbteCble = q.IdCbteCble,
                            IdTipoCbte = q.IdTipoCbte
                        }).ToList();

                    }
                    else
                    {
                        Lista = Context.ba_Archivo_Transferencia.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Select(q => new ba_Archivo_Transferencia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            cod_archivo = q.cod_archivo,
                            Cod_Empresa = q.Cod_Empresa,
                            Contabilizado = q.Contabilizado,
                            Estado = true,
                            Fecha = q.Fecha,
                            Fecha_Proceso = q.Fecha_Proceso,
                            IdArchivo = q.IdArchivo,
                            IdBanco = q.IdBanco,
                            IdProceso_bancario = q.IdProceso_bancario,
                            Nom_Archivo = q.Nom_Archivo,
                            Observacion = q.Observacion,
                            IdSucursal = q.IdSucursal,
                            cb_Valor = q.cb_Valor,
                            IdCbteCble = q.IdCbteCble,
                            IdTipoCbte = q.IdTipoCbte
                        }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_Archivo_Transferencia_Info GetInfo(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                ba_Archivo_Transferencia_Info info = new ba_Archivo_Transferencia_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Archivo_Transferencia Entity = Context.ba_Archivo_Transferencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new ba_Archivo_Transferencia_Info
                    {
                            IdEmpresa = Entity.IdEmpresa,
                            cod_archivo = Entity.cod_archivo,
                            Cod_Empresa = Entity.Cod_Empresa,
                            Contabilizado = Entity.Contabilizado,
                            Estado = Entity.Estado,
                            Fecha = Entity.Fecha,
                            Fecha_Proceso = Entity.Fecha_Proceso,
                            IdArchivo = Entity.IdArchivo,
                            IdBanco = Entity.IdBanco,
                            IdProceso_bancario = Entity.IdProceso_bancario,
                            Nom_Archivo = Entity.Nom_Archivo,
                            Observacion = Entity.Observacion,
                            IdSucursal = Entity.IdSucursal,
                            SecuencialInicial = Entity.SecuencialInicial
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal Id = 1;
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Archivo_Transferencia
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdArchivo) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                info.Nom_Archivo = "PAGOS_MULTICASH_"+info.Fecha.ToString("yyyyMMdd")+"_01";
                using (Entities_banco Context = new Entities_banco())
                {
                    Context.ba_Archivo_Transferencia.Add(new ba_Archivo_Transferencia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdArchivo = info.IdArchivo=GetId(info.IdEmpresa),
                        cod_archivo = info.cod_archivo,
                        Cod_Empresa = info.Cod_Empresa = "",
                        Contabilizado = info.Contabilizado,
                        Estado = true,
                        Fecha = info.Fecha.Date,
                        IdBanco = info.IdBanco,
                        IdProceso_bancario = info.IdProceso_bancario,
                        Nom_Archivo = info.Nom_Archivo,
                        Observacion = info.Observacion,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now,
                        IdSucursal = info.IdSucursal,
                        SecuencialInicial = info.SecuencialInicial
                    });

                    int Secuencia = 1;
                    if(info.Lst_det.Count()>0)
                    {
                        foreach (var item in info.Lst_det)
                        {
                            Context.ba_Archivo_Transferencia_Det.Add(new ba_Archivo_Transferencia_Det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdArchivo = info.IdArchivo,
                                Estado = item.Estado,
                                IdOrdenPago = item.IdOrdenPago,
                                IdEmpresa_OP = info.IdEmpresa,
                                Secuencia = Secuencia++,
                                Secuencial_reg_x_proceso = item.Secuencial_reg_x_proceso,
                                Secuencia_OP = item.Secuencia_OP,
                                Referencia = item.Referencia,
                                
                                Valor = item.Valor
                            });
                        }
                    }

                    info.Lst_Flujo = info.Lst_Flujo == null ? new List<ba_archivo_transferencia_x_ba_tipo_flujo_Info>() : info.Lst_Flujo;
                    if (info.Lst_Flujo.Count()>0)
                    {
                        foreach (var item in info.Lst_Flujo)
                             {
                            Context.ba_archivo_transferencia_x_ba_tipo_flujo.Add(new ba_archivo_transferencia_x_ba_tipo_flujo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdArchivo = info.IdArchivo,
                                IdTipoFlujo = item.IdTipoFlujo,
                                Porcentaje = item.Porcentaje,
                                Secuencia = Secuencia++,
                                Valor = item.Valor
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Archivo_Transferencia_Data", Metodo = "GuardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool ModificarDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Archivo_Transferencia Entity = Context.ba_Archivo_Transferencia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Nom_Archivo = info.Nom_Archivo;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;

                    var Lst_det = Context.ba_Archivo_Transferencia_Det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).ToList();
                    Context.ba_Archivo_Transferencia_Det.RemoveRange(Lst_det);
                    if (info.Lst_det.Count() > 0)
                    {
                        foreach (var item in info.Lst_det)
                        {
                            Context.ba_Archivo_Transferencia_Det.Add(new ba_Archivo_Transferencia_Det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdArchivo = info.IdArchivo,
                                Estado = item.Estado,
                                IdOrdenPago = item.IdOrdenPago,
                                IdEmpresa_OP = info.IdEmpresa,
                                Secuencia = item.Secuencia,
                                Secuencial_reg_x_proceso = item.Secuencial_reg_x_proceso,
                                Secuencia_OP = item.Secuencia_OP,
                                Valor = item.Valor,
                                Referencia = item.Referencia
                            });
                        }
                    }
                    int Secuencia = 1;
                    info.Lst_Flujo = info.Lst_Flujo == null ? new List<ba_archivo_transferencia_x_ba_tipo_flujo_Info>() : info.Lst_Flujo;
                    var Lst_flujo = Context.ba_archivo_transferencia_x_ba_tipo_flujo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).ToList();
                    Context.ba_archivo_transferencia_x_ba_tipo_flujo.RemoveRange(Lst_flujo);
                    if (info.Lst_Flujo.Count() > 0)
                    {
                        foreach (var item in info.Lst_Flujo)
                        {
                            Context.ba_archivo_transferencia_x_ba_tipo_flujo.Add(new ba_archivo_transferencia_x_ba_tipo_flujo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdArchivo = info.IdArchivo,
                                IdTipoFlujo = item.IdTipoFlujo,
                                Porcentaje = item.Porcentaje,
                                Secuencia = Secuencia++,
                                Valor = item.Valor
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
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Archivo_Transferencia_Data", Metodo = "ModificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool AnularDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var Lst_det = Context.ba_Archivo_Transferencia_Det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).ToList();
                    Context.ba_Archivo_Transferencia_Det.RemoveRange(Lst_det);

                    var Lst_flujo = Context.ba_archivo_transferencia_x_ba_tipo_flujo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).ToList();
                    Context.ba_archivo_transferencia_x_ba_tipo_flujo.RemoveRange(Lst_flujo);

                    Context.ba_Archivo_Transferencia.Remove(Context.ba_Archivo_Transferencia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault());
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ContabilizarDB(ba_Archivo_Transferencia_Info info)
        {
            try
            {
                ba_Cbte_Ban_Data odata_cbte = new ba_Cbte_Ban_Data();
                tb_banco_procesos_bancarios_x_empresa_Data odataProceso = new tb_banco_procesos_bancarios_x_empresa_Data();
                using (Entities_banco db = new Entities_banco())
                {

                    var proceso = odataProceso.get_info(info.IdEmpresa, info.IdProceso_bancario);
                    if (proceso == null)
                        return false;

                    var cbte = new ba_Cbte_Ban_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        cb_Fecha = info.Fecha,
                        IdSucursal = info.IdSucursal,
                        IdBanco = info.IdBanco,
                        cb_Observacion = "Archivo # " + info.IdArchivo.ToString() + " " + info.Observacion,
                        IdTipoNota = proceso.IdTipoNota,
                        IdUsuario = info.IdUsuario,
                        list_det = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>(info.Lst_Flujo.Select(q => new ba_Cbte_Ban_x_ba_TipoFlujo_Info
                        {
                            IdTipoFlujo = q.IdTipoFlujo,
                            Valor = q.Valor,
                            Porcentaje = q.Porcentaje,
                            IdEmpresa = info.IdEmpresa
                        }).ToList()),
                        lst_det_ct = new List<ct_cbtecble_det_Info>(info.Lst_diario.Select(q => new ct_cbtecble_det_Info
                        {
                            IdCtaCble = q.IdCtaCble,
                            dc_Valor = q.dc_Valor,
                            dc_para_conciliar = q.dc_para_conciliar,
                            IdCentroCosto = q.IdCentroCosto,
                            IdPunto_cargo = q.IdPunto_cargo,
                            IdPunto_cargo_grupo = q.IdPunto_cargo_grupo
                        }).ToList()),
                        lst_det_canc_op = new List<cp_orden_pago_cancelaciones_Info>(info.Lst_det.Select(q => new cp_orden_pago_cancelaciones_Info
                        {
                            IdEmpresa_op = q.IdEmpresa_OP,
                            IdOrdenPago_op = q.IdOrdenPago,
                            Secuencia_op = q.Secuencia_OP,

                            IdEmpresa_cxp = q.IdEmpresa_cxp,
                            IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                            IdCbteCble_cxp = q.IdCbteCble_cxp,
                            MontoAplicado = q.Valor,

                            Observacion = "Archivo # " + info.IdArchivo.ToString() + " " +q.Referencia
                        }).ToList())
                    };
                    if (odata_cbte.guardarDB(cbte, Info.Helps.cl_enumeradores.eTipoCbteBancario.NDBA))
                    {
                        var Entity = db.ba_Archivo_Transferencia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault();
                        if(Entity != null)
                        {
                            Entity.Contabilizado = true;
                            Entity.Fecha_Proceso = DateTime.Now;
                            Entity.IdUsuarioContabiliza = info.IdUsuario;
                            Entity.IdTipoCbte = cbte.IdTipocbte;
                            Entity.IdCbteCble = cbte.IdCbteCble;
                            db.SaveChanges();
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
