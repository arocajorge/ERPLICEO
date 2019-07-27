using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
 public   class ro_tipo_prestamo_Data
    {
        public List<ro_tipo_prestamo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_tipo_prestamo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.ro_Tipo_Prestamo
                             where q.IdEmpresa == IdEmpresa
                             select new ro_tipo_prestamo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipoPrestamo = q.IdTipoPrestamo,
                                 tp_Descripcion = q.tp_Descripcion,
                                 tp_Monto=q.tp_Monto,
                                 tp_Estado = q.tp_Estado,

                                 EstadoBool = q.tp_Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.ro_Tipo_Prestamo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.tp_Estado=="A"
                                 select new ro_tipo_prestamo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoPrestamo = q.IdTipoPrestamo,
                                     tp_Descripcion = q.tp_Descripcion,
                                     tp_Monto = q.tp_Monto,
                                     tp_Estado = q.tp_Estado,

                                     EstadoBool = q.tp_Estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception )
            {

                throw;
            }
        }
        public ro_tipo_prestamo_Info get_info(int IdEmpresa, int IdTipoPrestamo)
        {
            try
            {
                ro_tipo_prestamo_Info info = new ro_tipo_prestamo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Tipo_Prestamo Entity = Context.ro_Tipo_Prestamo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTipoPrestamo == IdTipoPrestamo);
                    if (Entity == null) return null;

                    info = new ro_tipo_prestamo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoPrestamo = Entity.IdTipoPrestamo,
                        tp_Descripcion = Entity.tp_Descripcion,
                        tp_Monto=Entity.tp_Monto,
                        tp_Estado = Entity.tp_Estado,
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

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_Tipo_Prestamo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoPrestamo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_tipo_prestamo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Tipo_Prestamo Entity = new ro_Tipo_Prestamo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoPrestamo = info.IdTipoPrestamo = get_id(info.IdEmpresa),
                        tp_Monto = info.tp_Monto,
                        tp_Descripcion = info.tp_Descripcion,
                        tp_Estado = info.tp_Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.ro_Tipo_Prestamo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_tipo_prestamo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Tipo_Prestamo Entity = Context.ro_Tipo_Prestamo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoPrestamo == info.IdTipoPrestamo);
                    if (Entity == null)
                        return false;
                    Entity.tp_Descripcion = info.tp_Descripcion;
                    Entity.tp_Monto = info.tp_Monto;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_Transac = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_tipo_prestamo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Tipo_Prestamo Entity = Context.ro_Tipo_Prestamo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoPrestamo == info.IdTipoPrestamo);
                    if (Entity == null)
                        return false;
                    Entity.tp_Estado = info.tp_Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.FechaHoraAnul = DateTime.Now;
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
