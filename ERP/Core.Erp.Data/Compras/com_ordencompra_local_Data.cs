using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.Compras
{
    public class com_ordencompra_local_Data
    {
        public List<com_ordencompra_local_Info> get_list(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime  fecha_fin, bool mostrar_anulados, string Tipo)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 999999 : IdSucursal;

                List<com_ordencompra_local_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.vwcom_ordencompra_local
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursal_ini
                                 && q.IdSucursal <= IdSucursal_fin
                                 && q.oc_fecha >= fecha_ini
                                 && q.oc_fecha <= fecha_fin
                                 && q.Tipo == Tipo
                                 select new com_ordencompra_local_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdOrdenCompra = q.IdOrdenCompra,
                                     IdEstadoAprobacion_cat = q.IdEstadoAprobacion_cat,
                                     IdEstado_cierre = q.IdEstado_cierre,
                                     IdSucursal = q.IdSucursal,
                                     Tipo = q.Tipo,
                                     SecuenciaTipo = q.SecuenciaTipo,
                                     Estado = q.Estado,
                                     oc_plazo = q.oc_plazo,
                                     oc_observacion = q.oc_observacion,
                                     oc_fecha = q.oc_fecha,

                                     EstadoBool = q.Estado == "A" ? true : false,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     pr_codigo = q.Codigo,
                                     Nombre = q.pe_nombreCompleto,
                                     Su_Descripcion = q.Su_Descripcion

                                 }).ToList();
                    else
                        Lista = (from q in Context.vwcom_ordencompra_local
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.oc_fecha >= fecha_ini
                                 && q.oc_fecha <= fecha_fin
                                 && q.Tipo == Tipo
                                 && q.Estado == "A"
                                 select new com_ordencompra_local_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdOrdenCompra = q.IdOrdenCompra,
                                     IdEstadoAprobacion_cat = q.IdEstadoAprobacion_cat,
                                     IdEstado_cierre = q.IdEstado_cierre,
                                     IdSucursal = q.IdSucursal,
                                     Tipo = q.Tipo,
                                     SecuenciaTipo = q.SecuenciaTipo,
                                     Estado = q.Estado,
                                     oc_plazo = q.oc_plazo,
                                     oc_observacion = q.oc_observacion,
                                     oc_fecha = q.oc_fecha,

                                     EstadoBool = q.Estado == "A" ? true : false,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     pr_codigo = q.Codigo,
                                     Nombre = q.pe_nombreCompleto

                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public com_ordencompra_local_Info get_info(int IdEmpresa, int IdSucursal, decimal IdOrdenCompra)
        {
            try
            {
                com_ordencompra_local_Info info = new com_ordencompra_local_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_ordencompra_local Entity = Context.com_ordencompra_local.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdOrdenCompra == IdOrdenCompra).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new com_ordencompra_local_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdComprador = Entity.IdComprador,
                        IdOrdenCompra = Entity.IdOrdenCompra,
                        IdDepartamento = Entity.IdDepartamento,
                        IdEstadoAprobacion_cat = Entity.IdEstadoAprobacion_cat,
                        IdEstado_cierre = Entity.IdEstado_cierre,
                        IdProveedor = Entity.IdProveedor,
                        IdSucursal = Entity.IdSucursal,
                        Tipo = Entity.Tipo,
                        SecuenciaTipo = Entity.SecuenciaTipo,
                        IdTerminoPago = Entity.IdTerminoPago,
                        Estado = Entity.Estado,
                        oc_plazo = Entity.oc_plazo,
                        oc_observacion = Entity.oc_observacion,
                        oc_fecha = Entity.oc_fecha,
                        oc_fechaVencimiento = Entity.oc_fechaVencimiento,
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_id(int IdEmpresa, int idSucursal)
        {
            try
            {
                decimal Id = 1;
                using (Entities_compras Context = new Entities_compras())
                {
                    var lst = from q in Context.com_ordencompra_local
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == idSucursal
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdOrdenCompra) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id_by_tipo(int IdEmpresa, int idSucursal, string Tipo)
        {
            try
            {
                int Id = 1;
                using (Entities_compras Context = new Entities_compras())
                {
                    var lst = from q in Context.com_ordencompra_local
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == idSucursal
                              && q.Tipo == Tipo
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.SecuenciaTipo) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(com_ordencompra_local_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_ordencompra_local Entity = new com_ordencompra_local
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdComprador = info.IdComprador,
                        IdOrdenCompra = info.IdOrdenCompra = get_id(info.IdEmpresa, info.IdSucursal),
                        IdDepartamento = info.IdDepartamento,
                        IdEstadoAprobacion_cat = info.IdEstadoAprobacion_cat,
                        IdEstado_cierre = info.IdEstado_cierre,
                        IdProveedor = info.IdProveedor,
                        IdSucursal = info.IdSucursal,
                        Tipo = info.Tipo,
                        SecuenciaTipo = info.SecuenciaTipo = get_id_by_tipo(info.IdEmpresa, info.IdSucursal, info.Tipo),
                        IdTerminoPago = info.IdTerminoPago,
                        Estado = "A",
                        oc_plazo = info.oc_plazo,
                        oc_observacion = info.oc_observacion,
                        oc_fecha = info.oc_fecha,
                        oc_fechaVencimiento = info.oc_fechaVencimiento,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context.com_ordencompra_local.Add(Entity);

                    foreach (var item in info.lst_det)
                        {
                            com_ordencompra_local_det Entity_det = new com_ordencompra_local_det
                             {
                                IdEmpresa = info.IdEmpresa,
                                IdOrdenCompra = info.IdOrdenCompra,
                                IdSucursal = info.IdSucursal,
                                IdProducto = item.IdProducto,
                                IdCod_Impuesto = item.IdCod_Impuesto,
                                IdUnidadMedida = item.IdUnidadMedida,
                                do_Cantidad = item.do_Cantidad,
                                do_descuento = item.do_descuento,
                                do_iva = item.do_iva,
                                do_observacion = item.do_observacion,
                                do_porc_des = item.do_porc_des,
                                do_precioCompra = item.do_precioCompra,
                                do_precioFinal = item.do_precioFinal,
                                do_subtotal = item.do_subtotal,
                                do_total = item.do_total,
                                Por_Iva = item.Por_Iva,
                                Secuencia = item.Secuencia
                            };
                        Context.com_ordencompra_local_det.Add(Entity_det);

                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "com_ordencompra_local_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(com_ordencompra_local_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_ordencompra_local Entity = Context.com_ordencompra_local.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdOrdenCompra == info.IdOrdenCompra).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.IdComprador = info.IdComprador;
                    Entity.IdDepartamento = info.IdDepartamento;
                    Entity.Tipo = info.Tipo;
                    Entity.SecuenciaTipo = info.SecuenciaTipo;
                    Entity.IdEstadoAprobacion_cat = info.IdEstadoAprobacion_cat;
                    Entity.IdEstado_cierre = info.IdEstado_cierre;
                    Entity.IdProveedor = info.IdProveedor;
                    Entity.IdSucursal = info.IdSucursal;
                    Entity.IdTerminoPago = info.IdTerminoPago;
                    Entity.oc_plazo = info.oc_plazo;
                    Entity.oc_observacion = info.oc_observacion;
                    Entity.oc_fecha = info.oc_fecha;
                    Entity.oc_fechaVencimiento = info.oc_fechaVencimiento;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;

                    var det = Context.com_ordencompra_local_det.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdSucursal == info.IdSucursal && v.IdOrdenCompra == info.IdOrdenCompra);
                    Context.com_ordencompra_local_det.RemoveRange(det);
                    foreach (var item in info.lst_det)
                    {
                        com_ordencompra_local_det Entity_det = new com_ordencompra_local_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdOrdenCompra = info.IdOrdenCompra,
                            IdProducto = item.IdProducto,
                            IdCod_Impuesto = item.IdCod_Impuesto,
                            IdUnidadMedida = item.IdUnidadMedida,
                            do_Cantidad = item.do_Cantidad,
                            do_descuento = item.do_descuento,
                            do_iva = item.do_iva,
                            do_observacion = item.do_observacion,
                            do_porc_des = item.do_porc_des,
                            do_precioCompra = item.do_precioCompra,
                            do_precioFinal = item.do_precioFinal,
                            do_subtotal = item.do_subtotal,
                            do_total = item.do_total,
                            Por_Iva = item.Por_Iva,
                            Secuencia = item.Secuencia
                        };
                        Context.com_ordencompra_local_det.Add(Entity_det);

                    }
                    Context.SaveChanges();

            }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "com_ordencompra_local_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool AprobarOC(int IdEmpresa, int IdSucursal, string[] Lista, string MotivoAprobacion, string IdUsuarioAprobacion)
        {
            try
            {

                using (Entities_compras Context = new Entities_compras())
                {
                    foreach (var item in Lista)
                    {
                        var IdOrdenCompraAprobacion = Convert.ToDecimal(item);
                        com_ordencompra_local Entity = Context.com_ordencompra_local.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdOrdenCompra == IdOrdenCompraAprobacion);
                        if (Entity != null)
                        {
                            Entity.IdEstadoAprobacion_cat = "APRO";
                            Entity.MotivoAprobacion = MotivoAprobacion;
                            Entity.IdUsuarioAprobacion = IdUsuarioAprobacion;
                            Entity.FechaAprobacion = DateTime.Now;
                        }
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

        public bool RechazarOC(int IdEmpresa, int IdSucursal, string[] Lista, string MotivoAprobacion, string IdUsuarioAprobacion)
        {
            try
            {

                using (Entities_compras Context = new Entities_compras())
                {
                    foreach (var item in Lista)
                    {
                        var IdOrdenCompraAprobacion = Convert.ToDecimal(item);
                        com_ordencompra_local Entity = Context.com_ordencompra_local.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdOrdenCompra == IdOrdenCompraAprobacion);
                        if (Entity != null)
                        {
                            Entity.IdEstadoAprobacion_cat = "ANU";
                            Entity.MotivoAprobacion = MotivoAprobacion;
                            Entity.IdUsuarioAprobacion = IdUsuarioAprobacion;
                            Entity.FechaAprobacion = DateTime.Now;

                            Entity.Estado = "I";

                            Entity.IdUsuarioUltAnu = IdUsuarioAprobacion;
                            Entity.Fecha_UltAnu = DateTime.Now;
                            Entity.MotivoAnulacion = MotivoAprobacion;
                        }
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

        public bool anularDB(com_ordencompra_local_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_ordencompra_local Entity = Context.com_ordencompra_local.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdOrdenCompra == info.IdOrdenCompra).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.IdEstadoAprobacion_cat = "ANU";
                    Entity.Fecha_UltAnu = DateTime.Now;

                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<com_ordencompra_local_Info> GetListPorAprobar(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<com_ordencompra_local_Info> List;
                using (Entities_compras Context = new Entities_compras())
                {
                    List = Context.vwcom_ordencompra_local.Where(
                        q => q.IdEmpresa == IdEmpresa
                        && q.IdSucursal == IdSucursal
                        && q.oc_fecha >= fecha_ini
                        && q.oc_fecha <= fecha_fin
                        && q.IdEstadoAprobacion_cat == "xAPRO"
                        && q.Estado == "A").Select(q => new com_ordencompra_local_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdOrdenCompra = q.IdOrdenCompra,
                            SecuenciaTipo = q.SecuenciaTipo,
                            IdEstadoAprobacion_cat = q.IdEstadoAprobacion_cat,
                            oc_observacion = q.oc_observacion,
                            EstadoBool = q.Estado == "A" ? true : false,
                            oc_fecha = q.oc_fecha,
                            Su_Descripcion = q.Su_Descripcion,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            TerminoPago = q.TerminoPago,
                            Total = q.Total
                            

                    }).ToList();
                }
                return List;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<com_ordencompra_local_Info> get_list_x_ingresar(int IdEmpresa, int IdSucursal, decimal IdResponsable)
        {
            try
            {
                List<com_ordencompra_local_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                        Lista = (from q in Context.vwcom_ordencompra_local_detPorIngresar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdProveedor == IdResponsable
                                 select new com_ordencompra_local_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdOrdenCompra = q.IdOrdenCompra,
                                     Secuencia = q.Secuencia,
                                     SecuenciaTipo = q.SecuenciaTipo,
                                     oc_fecha = q.oc_fecha,
                                     IdProducto = q.IdProducto,
                                     pr_descripcion = q.pr_descripcion,
                                     do_precioFinal = q.do_precioFinal,
                                     do_Cantidad_vw = q.do_Cantidad,
                                     IdUnidadMedida = q.IdUnidadMedida,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     oc_fechaVencimiento = q.oc_fecha,
                                     oc_observacion = q.oc_observacion,
                                     IdProveedor = q.IdProveedor,
                                     Saldo = q.Saldo
                                 }).ToList();

                    Lista.ForEach(q=>q.IdInventarioOrdenCompra = (q.IdEmpresa.ToString("000") + q.IdSucursal.ToString("000")  + q.IdOrdenCompra.ToString("000000") + q.Secuencia.ToString("000000")) );                  
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
