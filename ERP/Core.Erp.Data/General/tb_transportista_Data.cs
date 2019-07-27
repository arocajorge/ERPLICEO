using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_transportista_Data
    {
        public List<tb_transportista_Info> get_list(int IdEmpresa,bool mostrar_anulados)
        {
            try
            {
                List<tb_transportista_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.tb_transportista
                                 where q.IdEmpresa == IdEmpresa
                                 select new tb_transportista_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTransportista = q.IdTransportista,
                                     Cedula = q.Cedula,
                                     Nombre = q.Nombre,
                                     Estado = q.Estado,
                                     Placa = q.Placa,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_transportista
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new tb_transportista_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTransportista = q.IdTransportista,
                                     Cedula = q.Cedula,
                                     Nombre = q.Nombre,
                                     Estado = q.Estado,
                                     Placa = q.Placa,

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

        public tb_transportista_Info get_info(int IdEmpresa, decimal IdTransportista)
        {
            try
            {
                tb_transportista_Info info = new tb_transportista_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_transportista Entity = Context.tb_transportista.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTransportista == IdTransportista);
                    if (Entity == null) return null;
                    info = new tb_transportista_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTransportista = Entity.IdTransportista,
                        Cedula = Entity.Cedula,
                        Nombre = Entity.Nombre,
                        Estado = Entity.Estado,
                        Placa = Entity.Placa
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
         
        public bool guardarDB(tb_transportista_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_transportista Entity = new tb_transportista
                    {
                         IdEmpresa = info.IdEmpresa,
                         IdTransportista = info.IdTransportista = get_id(info.IdEmpresa),
                         Cedula = info.Cedula,
                         Nombre = info.Nombre,
                         Estado = info.Estado = "A",
                         Placa = info.Placa
                    };
                    Context.tb_transportista.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(tb_transportista_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_transportista Entity = Context.tb_transportista.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransportista == info.IdTransportista);
                    if (Entity == null)
                        return false;
                    Entity.Cedula = info.Cedula;
                    Entity.Nombre = info.Nombre;
                    Entity.Placa = info.Placa;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_transportista_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_transportista Entity = Context.tb_transportista.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransportista == info.IdTransportista);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

                    Context.SaveChanges();
                }
                return true;
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
               decimal  ID = 1;
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_transportista
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTransportista) + 1;
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
