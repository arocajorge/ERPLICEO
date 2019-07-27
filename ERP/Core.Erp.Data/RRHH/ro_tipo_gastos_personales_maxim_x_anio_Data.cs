using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
  public  class ro_tipo_gastos_personales_maxim_x_anio_Data
    {
        public List<ro_tipo_gastos_personales_maxim_x_anio_Info> get_list(string IdTipoGasto)
        {
            try
            {
                List<ro_tipo_gastos_personales_maxim_x_anio_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    Lista = (from q in Context.ro_tipo_gastos_personales_tabla_valores_x_anio
                             where q.IdTipoGasto==IdTipoGasto
                             select new ro_tipo_gastos_personales_maxim_x_anio_Info
                             {
                                 IdGasto=q.IdGasto,
                                 IdTipoGasto = q.IdTipoGasto,
                                 AnioFiscal = q.AnioFiscal,
                                 estado = q.estado,
                                 Monto_max=q.Monto_max,
                                 EstadoBool = q.estado == "A" ? true : false

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_tipo_gastos_personales_maxim_x_anio_Info> get_list_gastos_tope_x_anio(int anio)
        {
            try
            {
                List<ro_tipo_gastos_personales_maxim_x_anio_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    Lista = (from q in Context.ro_tipo_gastos_personales_tabla_valores_x_anio
                             where q.AnioFiscal == anio
                             select new ro_tipo_gastos_personales_maxim_x_anio_Info
                             {
                                 IdGasto = q.IdGasto,
                                 IdTipoGasto = q.IdTipoGasto,
                                 AnioFiscal = q.AnioFiscal,
                                 estado = q.estado,
                                 Monto_max = q.Monto_max,
                                 EstadoBool = q.estado == "A" ? true : false

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_tipo_gastos_personales_maxim_x_anio_Info get_info(int IdGasto)
        {
            try
            {
                ro_tipo_gastos_personales_maxim_x_anio_Info info = new ro_tipo_gastos_personales_maxim_x_anio_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales_tabla_valores_x_anio Entity = Context.ro_tipo_gastos_personales_tabla_valores_x_anio.FirstOrDefault(q => q.IdGasto == IdGasto);
                    if (Entity == null) return null;

                    info = new ro_tipo_gastos_personales_maxim_x_anio_Info
                    {
                        IdGasto=Entity.IdGasto,
                        IdTipoGasto = Entity.IdTipoGasto,
                        AnioFiscal = Entity.AnioFiscal,
                        estado = Entity.estado,
                        Monto_max = Entity.Monto_max,
                        EstadoBool = Entity.estado == "A" ? true : false
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_tipo_gastos_personales_maxim_x_anio_Info si_existe(string IdTipoGasto, int anio)
        {
            try
            {
                ro_tipo_gastos_personales_maxim_x_anio_Info info = new ro_tipo_gastos_personales_maxim_x_anio_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales_tabla_valores_x_anio Entity = Context.ro_tipo_gastos_personales_tabla_valores_x_anio.FirstOrDefault(q => q.IdTipoGasto == IdTipoGasto&& q.AnioFiscal==anio);
                    if (Entity == null) return null;

                    info = new ro_tipo_gastos_personales_maxim_x_anio_Info
                    {
                        IdTipoGasto = Entity.IdTipoGasto,
                        AnioFiscal = Entity.AnioFiscal,
                        estado = Entity.estado,
                        Monto_max = Entity.Monto_max,
                        EstadoBool = Entity.estado == "A" ? true : false
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_tipo_gastos_personales_maxim_x_anio_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales_tabla_valores_x_anio Entity = new ro_tipo_gastos_personales_tabla_valores_x_anio
                    {
                        IdGasto=info.IdGasto=get_id(),
                        IdTipoGasto = info.IdTipoGasto,
                        AnioFiscal = info.AnioFiscal,
                        Monto_max = info.Monto_max,
                        estado = info.estado = "A",
                        Fecha_Transac = DateTime.Now,
                        IdUsuario = info.IdUsuario,
                        observacion=info.observacion=" "
                    };
                    Context.ro_tipo_gastos_personales_tabla_valores_x_anio.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_tipo_gastos_personales_maxim_x_anio_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales_tabla_valores_x_anio Entity = Context.ro_tipo_gastos_personales_tabla_valores_x_anio.FirstOrDefault(q => q.IdTipoGasto == info.IdTipoGasto&& q.IdGasto==info.IdGasto);
                    if (Entity == null)
                        return false;
                    Entity.Monto_max = info.Monto_max;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_tipo_gastos_personales_maxim_x_anio_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales_tabla_valores_x_anio Entity = Context.ro_tipo_gastos_personales_tabla_valores_x_anio.FirstOrDefault(q => q.IdTipoGasto == info.IdTipoGasto&& q.IdGasto==info.IdGasto);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id()
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_tipo_gastos_personales_tabla_valores_x_anio
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdGasto) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
