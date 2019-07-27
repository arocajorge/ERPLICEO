using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;

namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_pago_det_Data
    {

        public List<cp_orden_pago_det_Info> Get_list_cuotas_x_doc_det(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                List<cp_orden_pago_det_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.cp_orden_pago_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdOrdenPago == IdOrdenPago
                             select new cp_orden_pago_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenPago = q.IdOrdenPago,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_cxp = q.IdEmpresa_cxp,
                                 IdCbteCble_cxp = q.IdCbteCble_cxp,
                                 IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                                 Valor_a_pagar = q.Valor_a_pagar,
                                 Referencia = q.Referencia,
                                 IdFormaPago = q.IdFormaPago,
                                 Fecha_Pago = q.Fecha_Pago,
                                 IdEstadoAprobacion = q.IdEstadoAprobacion,
                                 IdBanco = q.IdBanco,
                                 IdUsuario_Aprobacion = q.IdUsuario_Aprobacion,
                                 fecha_hora_Aproba = q.fecha_hora_Aproba,
                                 Motivo_aproba = q.Motivo_aproba
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarDB(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    string comando = "delete cp_orden_pago_det where IdEmpresa = " + IdEmpresa + " and IdOrdenPago = " + IdOrdenPago;
                    Context.Database.ExecuteSqlCommand(comando);
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(List<cp_orden_pago_det_Info> Lista)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    foreach (var item in Lista)
                    {
                        cp_orden_pago_det Entity = new cp_orden_pago_det
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdOrdenPago = item.IdOrdenPago,
                            Secuencia = item.Secuencia,
                            IdEmpresa_cxp = item.IdEmpresa_cxp,
                            IdCbteCble_cxp = item.IdCbteCble_cxp,
                            IdTipoCbte_cxp = item.IdTipoCbte_cxp,
                            Valor_a_pagar = item.Valor_a_pagar,
                            Referencia = item.Referencia,
                            IdFormaPago = item.IdFormaPago,
                            Fecha_Pago = item.Fecha_Pago,
                            IdEstadoAprobacion = item.IdEstadoAprobacion,
                            IdBanco = item.IdBanco,
                            IdUsuario_Aprobacion = item.IdUsuario_Aprobacion,
                            fecha_hora_Aproba = item.fecha_hora_Aproba,
                            Motivo_aproba = item.Motivo_aproba
                        };
                        Context.cp_orden_pago_det.Add(Entity);
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

        public bool modificar_estado_aprobacion(int Idempresa, decimal IdOrdenPago, string estado_aprobacion)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_det Entity = Context.cp_orden_pago_det.FirstOrDefault(q => q.IdEmpresa == Idempresa && q.IdOrdenPago == IdOrdenPago );
                    if (Entity != null)
                    {
                        Entity.IdEstadoAprobacion = estado_aprobacion;
                        Entity.fecha_hora_Aproba = DateTime.Now;
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


        public bool modificarDB(cp_orden_pago_det_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_det Entity = Context.cp_orden_pago_det.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdOrdenPago == info.IdOrdenPago);
                    if (Entity != null)
                    {
                        Entity.Valor_a_pagar = info.Valor_a_pagar;
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


        public bool anularDB(cp_orden_pago_det_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_det Entity = Context.cp_orden_pago_det.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdOrdenPago == info.IdOrdenPago);
                    if (Entity != null)
                    {
                      
                        Entity.IdTipoCbte_cxp = null;
                        Entity.IdCbteCble_cxp = null;
                        Entity.fecha_hora_Aproba = DateTime.Now;
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
