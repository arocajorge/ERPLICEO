using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
    public class com_TerminoPago_Data
    {
        public List<com_TerminoPago_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<com_TerminoPago_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.com_TerminoPago
                                 where q.IdEmpresa == IdEmpresa
                                 select new com_TerminoPago_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTerminoPago = q.IdTerminoPago,
                                     Descripcion = q.Descripcion,
                                     Dias = q.Dias,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.com_TerminoPago
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new com_TerminoPago_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTerminoPago = q.IdTerminoPago,
                                     Descripcion = q.Descripcion,
                                     Dias = q.Dias,
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

        private int GetID(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (Entities_compras db = new Entities_compras())
                {
                    var lst = db.com_TerminoPago.Where(q => q.IdEmpresa == IdEmpresa).ToList();
                    if (lst.Count != 0)
                        ID = lst.Max(q => q.IdTerminoPago) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public com_TerminoPago_Info get_info(int IdEmpresa, int IdTerminoPago)
        {
            try
            {
                com_TerminoPago_Info info = new com_TerminoPago_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_TerminoPago Entity = Context.com_TerminoPago.Where(q => q.IdEmpresa == IdEmpresa && q.IdTerminoPago == IdTerminoPago).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new com_TerminoPago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTerminoPago = Entity.IdTerminoPago,
                        Descripcion = Entity.Descripcion,
                        Dias = Entity.Dias,
                        Estado = Entity.Estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(com_TerminoPago_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_TerminoPago Entity = new com_TerminoPago
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTerminoPago = info.IdTerminoPago = GetID(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Dias = info.Dias,
                        Estado = "A",
                        IdUsuario = info.IdUsuario

                    };
                    Context.com_TerminoPago.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(com_TerminoPago_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_TerminoPago Entity = Context.com_TerminoPago.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTerminoPago == info.IdTerminoPago).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.Dias = info.Dias;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.FechaUltMod = DateTime.Now;
                    
                Context.SaveChanges();

            }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(com_TerminoPago_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_TerminoPago Entity = Context.com_TerminoPago.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTerminoPago == info.IdTerminoPago).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
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

        public com_TerminoPago_Info get_info_termino_pago_x_proveedor(int IdEmpresa, int plazo, string signo)
        {
            try
            {
                com_TerminoPago_Info info = new com_TerminoPago_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_TerminoPago Entity = null;

                    if (signo == "=")
                    {
                        Entity = Context.com_TerminoPago.Where(q => q.IdEmpresa == IdEmpresa && q.Dias == plazo).FirstOrDefault();
                    }
                    else
                    {
                        Entity = Context.com_TerminoPago.Where(q => q.IdEmpresa == IdEmpresa && q.Dias >= plazo).FirstOrDefault();
                    }

                    if (Entity == null) return null;

                    info = new com_TerminoPago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTerminoPago = Entity.IdTerminoPago,
                        Descripcion = Entity.Descripcion,
                        Dias = Entity.Dias,
                        Estado = Entity.Estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
