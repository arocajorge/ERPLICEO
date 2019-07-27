using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
    public class ro_horario_Data
    {
        public List<ro_horario_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_horario_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_horario                        
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_horario_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdHorario = q.IdHorario,
                                     HoraFin = q.HoraFin,
                                     HoraIni=q.HoraIni,
                                     SalLunch=q.SalLunch,
                                     RegLunch=q.RegLunch,
                                     ToleranciaEnt=q.ToleranciaEnt,
                                     ToleranciaReg_lunh=q.ToleranciaReg_lunh,
                                     Estado=q.Estado,
                                     Descripcion = q.Descripcion,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_horario
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new ro_horario_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdHorario = q.IdHorario,
                                     HoraFin = q.HoraFin,
                                     HoraIni = q.HoraIni,
                                     SalLunch = q.SalLunch,
                                     RegLunch = q.RegLunch,
                                     ToleranciaEnt = q.ToleranciaEnt,
                                     ToleranciaReg_lunh = q.ToleranciaReg_lunh,
                                     Estado = q.Estado,
                                     Descripcion = q.Descripcion,

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
        public ro_horario_Info get_info(int IdEmpresa, int IdHorario)
        {
            try
            {
                ro_horario_Info info = new ro_horario_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario Entity = Context.ro_horario.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdHorario == IdHorario);
                    if (Entity == null) return null;

                    info = new ro_horario_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdHorario = Entity.IdHorario,
                        HoraFin = Entity.HoraFin,
                        HoraIni = Entity.HoraIni,
                        SalLunch = Entity.SalLunch,
                        RegLunch = Entity.RegLunch,
                        ToleranciaEnt = Entity.ToleranciaEnt,
                        ToleranciaReg_lunh = Entity.ToleranciaReg_lunh,
                        Estado = Entity.Estado,
                        Descripcion = Entity.Descripcion
                    };
                }

                return info;
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

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_horario
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdHorario) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_horario_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario Entity = new ro_horario
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdHorario = get_id(info.IdEmpresa),
                        HoraFin = info.HoraFin,
                        HoraIni = info.HoraIni,
                        SalLunch = info.SalLunch,
                        RegLunch = info.RegLunch,
                        ToleranciaEnt = info.ToleranciaEnt,
                        ToleranciaReg_lunh = info.ToleranciaReg_lunh,
                        Descripcion = info.Descripcion,
                        Estado = "A",
                        IdUsuario=info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.ro_horario.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_horario_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario Entity = Context.ro_horario.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdHorario == info.IdHorario);
                    if (Entity == null)
                        return false;
                    Entity.HoraFin = info.HoraFin;
                    Entity.HoraIni = info.HoraIni;
                    Entity.SalLunch = info.SalLunch;
                    Entity.RegLunch = info.RegLunch;
                    Entity.ToleranciaEnt = info.ToleranciaEnt;
                    Entity.ToleranciaReg_lunh = info.ToleranciaReg_lunh;
                    Entity.Descripcion = info.Descripcion;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_horario_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario Entity = Context.ro_horario.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdHorario == info.IdHorario);
                    if (Entity == null)
                        return false;
                    Entity.Estado  = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
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
