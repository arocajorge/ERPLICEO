using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_ConciliacionAnticipo_Data
    {
        cp_parametros_Data data_cp_parametro = new cp_parametros_Data();
        ct_cbtecble_Data data_cbtecble = new ct_cbtecble_Data();
        ct_cbtecble_det_Data data_cbtecble_det = new ct_cbtecble_det_Data();
        cp_orden_pago_cancelaciones_Data data_op_cancelaciones = new cp_orden_pago_cancelaciones_Data();

        public List<cp_conciliacionAnticipo_Info> getlist(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                List<cp_conciliacionAnticipo_Info> Lista = new List<cp_conciliacionAnticipo_Info>();

                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in db.vwcp_ConciliacionAnticipo
                                where q.IdEmpresa == IdEmpresa
                                && q.IdSucursal >= IdSucursalInicio 
                                && q.IdSucursal <= IdSucursalFin
                                && q.Estado == (MostrarAnulados== true ? q.Estado : true)
                                select new cp_conciliacionAnticipo_Info
                                {
                                    IdEmpresa = q.IdEmpresa,
                                    IdConciliacion = q.IdConciliacion,
                                    IdSucursal = q.IdSucursal,
                                    Su_Descripcion = q.Su_Descripcion,
                                    IdProveedor = q.IdProveedor,
                                    pe_nombreCompleto = q.pe_nombreCompleto,
                                    Fecha = q.Fecha,
                                    Observacion = q.Observacion,
                                    Estado = q.Estado

                                }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cp_conciliacionAnticipo_Info get_info(int IdEmpresa, int IdConciliacion)
        {
            try
            {
                cp_conciliacionAnticipo_Info info = new cp_conciliacionAnticipo_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_ConciliacionAnticipo Entity = Context.cp_ConciliacionAnticipo.Where(q => q.IdConciliacion == IdConciliacion && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new cp_conciliacionAnticipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConciliacion = Entity.IdConciliacion,
                        IdSucursal = Entity.IdSucursal,
                        IdProveedor = Entity.IdProveedor,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdCbteCble = Entity.IdCbteCble
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int get_id(int IdEmpresa)
        {

            try
            {
                int ID = 1;
                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    var Lista = db.cp_ConciliacionAnticipo.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdConciliacion);

                    if (Lista.Count() > 0)
                        ID = Convert.ToInt32(Lista.Max() + 1);
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarBD(cp_conciliacionAnticipo_Info info)
        {
            try
            {
                cp_parametros_Info info_parametros = new cp_parametros_Info();
                info_parametros = data_cp_parametro.get_info(info.IdEmpresa);

                using (Entities_contabilidad db_cont = new Entities_contabilidad())
                {                 
                    if(info_parametros!= null && info_parametros.pa_TipoCbte_para_conci_x_antcipo!= null)
                    {
                        ct_cbtecble_Info info_diario = armar_info(info.Lista_det_Cbte, info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info_parametros.pa_TipoCbte_para_conci_x_antcipo), 0, info.Observacion, info.Fecha);                        

                        if (info_diario != null)
                        {
                            info_diario.IdUsuario = info.IdUsuarioCreacion;
                            if (data_cbtecble.guardarDB(info_diario))
                            {
                                info.IdTipoCbte = info_diario.IdTipoCbte;
                                info.IdCbteCble = info_diario.IdCbteCble;
                            }
                        }
                    }
          
                }

                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    db.cp_ConciliacionAnticipo.Add(new cp_ConciliacionAnticipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConciliacion = info.IdConciliacion = get_id(info.IdEmpresa),
                        IdSucursal = info.IdSucursal,
                        IdProveedor = info.IdProveedor,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        IdTipoCbte = info.IdTipoCbte,
                        IdCbteCble = info.IdCbteCble,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });



                    if (info.Lista_det_OP != null)
                    {
                        int SecuenciaOP = 1;

                        foreach (var item in info.Lista_det_OP)
                        {

                            db.cp_ConciliacionAnticipoDetAnt.Add(new cp_ConciliacionAnticipoDetAnt
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConciliacion = info.IdConciliacion,
                                Secuencia = SecuenciaOP++,
                                IdOrdenPago = item.IdOrdenPago,
                                MontoAplicado = item.MontoAplicado
                            });

                        }
                    }

                    if (info.Lista_det_Fact != null)
                    {
                        int SecuenciaFact = 1;
                        int SecuenciaCanc = 1;

                        foreach (var item in info.Lista_det_Fact)
                        {

                            db.cp_ConciliacionAnticipoDetCXP.Add(new cp_ConciliacionAnticipoDetCXP
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConciliacion = info.IdConciliacion,
                                Secuencia = SecuenciaFact++,
                                IdOrdenPago = item.IdOrdenPago,
                                IdEmpresa_cxp = item.IdEmpresa_cxp,
                                IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                                IdCbteCble_cxp = item.IdCbteCble_cxp,
                                Fecha_cxp = item.Fecha_cxp,
                                Observacion_cxp = item.Observacion_cxp,
                                MontoAplicado = item.MontoAplicado
                            });

                            cp_orden_pago_cancelaciones_Info info_cancelacion = new cp_orden_pago_cancelaciones_Info();
                            info_cancelacion.IdEmpresa = info.IdEmpresa;
                            info_cancelacion.Secuencia = SecuenciaCanc++;
                            info_cancelacion.IdEmpresa_op = item.IdEmpresa_cxp;
                            info_cancelacion.IdOrdenPago_op = item.IdOrdenPago;
                            info_cancelacion.Secuencia_op = 1;
                            info_cancelacion.IdEmpresa_cxp = item.IdEmpresa_cxp;
                            info_cancelacion.IdTipoCbte_cxp = item.IdTipoCbte_cxp;
                            info_cancelacion.IdCbteCble_cxp = item.IdCbteCble_cxp;
                            info_cancelacion.IdEmpresa_pago = info.IdEmpresa;
                            info_cancelacion.IdTipoCbte_pago = Convert.ToInt32(info.IdTipoCbte);
                            info_cancelacion.IdCbteCble_pago = Convert.ToInt32(info.IdCbteCble);
                            info_cancelacion.MontoAplicado = item.MontoAplicado;
                            info_cancelacion.Observacion = "CONCILIACION"+ info.IdConciliacion;

                            data_op_cancelaciones.guardarDB(info_cancelacion);

                        }
                    }

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                eliminar(info.IdEmpresa, Convert.ToInt32(info.IdTipoCbte), Convert.ToDecimal(info.IdCbteCble));

                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacionAnticipo_Data", Metodo = "GuardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }


        public bool ModificarBD(cp_conciliacionAnticipo_Info info)
        {
            try
            {
                using (Entities_contabilidad db_cont = new Entities_contabilidad())
                {
                    ct_cbtecble_Info info_diario = armar_info(info.Lista_det_Cbte, info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info.IdTipoCbte), 0, info.Observacion, info.Fecha);
                    ct_cbtecble Entity_cbte = db_cont.ct_cbtecble.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte && q.IdCbteCble == info.IdCbteCble).FirstOrDefault();

                    if (info_diario != null)
                    {
                        info_diario.IdCbteCble = Convert.ToDecimal(info.IdCbteCble);
                        info_diario.IdUsuarioUltModi = info.IdUsuarioModificacion;
                        if (data_cbtecble.modificarDB(info_diario))
                        {
                            info.IdTipoCbte = info_diario.IdTipoCbte;
                            info.IdCbteCble = info_diario.IdCbteCble;
                        }
                    }
                }

                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    cp_ConciliacionAnticipo entity = db.cp_ConciliacionAnticipo.Where(q => q.IdConciliacion == info.IdConciliacion && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.IdProveedor = info.IdProveedor;
                    entity.Fecha = info.Fecha;
                    entity.Observacion = info.Observacion;
                    entity.IdSucursal = info.IdSucursal;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    var lst_det_OP = db.cp_ConciliacionAnticipoDetAnt.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                    db.cp_ConciliacionAnticipoDetAnt.RemoveRange(lst_det_OP);

                    var lst_det_Fact = db.cp_ConciliacionAnticipoDetCXP.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                    db.cp_ConciliacionAnticipoDetCXP.RemoveRange(lst_det_Fact);

                    var lst_Cancelaciones = db.cp_orden_pago_cancelaciones.Where(q => q.IdEmpresa_op == info.IdEmpresa && q.IdTipoCbte_pago == info.IdTipoCbte && q.IdCbteCble_pago == info.IdCbteCble).ToList();
                    db.cp_orden_pago_cancelaciones.RemoveRange(lst_Cancelaciones);

                    if (info.Lista_det_OP != null)
                    {
                        int SecuenciaOP = 1;

                        foreach (var item in info.Lista_det_OP)
                        {

                            db.cp_ConciliacionAnticipoDetAnt.Add(new cp_ConciliacionAnticipoDetAnt
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConciliacion = info.IdConciliacion,
                                Secuencia = SecuenciaOP++,
                                IdOrdenPago = item.IdOrdenPago,
                                MontoAplicado = item.MontoAplicado
                            });

                        }
                    }

                    if (info.Lista_det_Fact != null)
                    {
                        int SecuenciaFact = 1;
                        int SecuenciaCanc = 1;

                        foreach (var item in info.Lista_det_Fact)
                        {

                            db.cp_ConciliacionAnticipoDetCXP.Add(new cp_ConciliacionAnticipoDetCXP
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConciliacion = info.IdConciliacion,
                                Secuencia = SecuenciaFact++,
                                IdOrdenPago = item.IdOrdenPago,
                                IdEmpresa_cxp = item.IdEmpresa,
                                IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                                IdCbteCble_cxp = item.IdCbteCble_cxp,
                                Fecha_cxp = item.Fecha_cxp,
                                Observacion_cxp = item.Observacion_cxp,
                                MontoAplicado = item.MontoAplicado
                            });

                            cp_orden_pago_cancelaciones_Info info_cancelacion = new cp_orden_pago_cancelaciones_Info();
                            info_cancelacion.IdEmpresa = info.IdEmpresa;
                            info_cancelacion.Secuencia = SecuenciaCanc++;
                            info_cancelacion.IdEmpresa_op = item.IdEmpresa_cxp;
                            info_cancelacion.IdOrdenPago_op = item.IdOrdenPago;
                            info_cancelacion.Secuencia_op = 1;
                            info_cancelacion.IdEmpresa_cxp = item.IdEmpresa_cxp;
                            info_cancelacion.IdTipoCbte_cxp = item.IdTipoCbte_cxp;
                            info_cancelacion.IdCbteCble_cxp = item.IdCbteCble_cxp;
                            info_cancelacion.IdEmpresa_pago = info.IdEmpresa;
                            info_cancelacion.IdTipoCbte_pago = Convert.ToInt32(info.IdTipoCbte);
                            info_cancelacion.IdCbteCble_pago = Convert.ToInt32(info.IdCbteCble);
                            info_cancelacion.MontoAplicado = item.MontoAplicado;
                            info_cancelacion.Observacion = "CONCILIACION" + info.IdConciliacion;

                            data_op_cancelaciones.guardarDB(info_cancelacion);
                        }
                    }

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacionAnticipo_Data", Metodo = "ModificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool AnularBD(cp_conciliacionAnticipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    cp_ConciliacionAnticipo entity = db.cp_ConciliacionAnticipo.Where(q => q.IdConciliacion == info.IdConciliacion && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Estado = false;
                    entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    entity.FechaAnulacion = DateTime.Now;
                    entity.MotivoAnulacion = info.MotivoAnulacion;

                    var lst_det_OP = db.cp_ConciliacionAnticipoDetAnt.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                    db.cp_ConciliacionAnticipoDetAnt.RemoveRange(lst_det_OP);

                    var lst_det_Fact = db.cp_ConciliacionAnticipoDetCXP.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                    db.cp_ConciliacionAnticipoDetCXP.RemoveRange(lst_det_Fact);

                    var lst_Cancelaciones = db.cp_orden_pago_cancelaciones.Where(q => q.IdEmpresa_op == info.IdEmpresa && q.IdTipoCbte_pago == info.IdTipoCbte && q.IdCbteCble_pago == info.IdCbteCble).ToList();
                    db.cp_orden_pago_cancelaciones.RemoveRange(lst_Cancelaciones);

                    if (data_cbtecble.anularDB(new ct_cbtecble_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = Convert.ToInt32(info.IdTipoCbte),
                        IdCbteCble = Convert.ToInt32(info.IdCbteCble),
                        IdUsuarioAnu = info.IdUsuarioAnulacion,
                        cb_MotivoAnu = info.MotivoAnulacion
                    }))

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_cbtecble_Info armar_info(List<ct_cbtecble_det_Info> lista, int IdEmpresa, int IdSucursal, int IdTipoCbte, decimal IdCbteCble, string Observacion, DateTime Fecha)
        {
            try
            {
                ct_cbtecble_Info info = new ct_cbtecble_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdTipoCbte = IdTipoCbte,
                    IdCbteCble = IdCbteCble,
                    IdSucursal = IdSucursal,
                    cb_Observacion = Observacion,
                    cb_Fecha = Fecha,
                    cb_Valor = lista.Where(q => q.dc_Valor > 0).Sum(q => q.dc_Valor)
                };

                info.lst_ct_cbtecble_det = lista;
                info.lst_ct_cbtecble_det.ForEach(q => { q.IdEmpresa = IdEmpresa; q.IdTipoCbte = IdTipoCbte; q.IdCbteCble = IdCbteCble; });

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminar(int IdEmpresa, int IdTipoCbte, decimal IdCbtecble)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    var lst_cbte_det = db.ct_cbtecble_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte == IdTipoCbte && q.IdCbteCble == IdCbtecble).ToList();
                    db.ct_cbtecble_det.RemoveRange(lst_cbte_det);

                    var cbte = db.ct_cbtecble.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte == IdTipoCbte && q.IdCbteCble == IdCbtecble).FirstOrDefault();
                    db.ct_cbtecble.Remove(cbte);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
