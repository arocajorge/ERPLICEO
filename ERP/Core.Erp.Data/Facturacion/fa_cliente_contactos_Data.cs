using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
  public  class fa_cliente_contactos_Data
    {
        public List<fa_cliente_contactos_Info> get_list(int IdEmpresa, decimal IdCliente)

        {
            try
            {
                List<fa_cliente_contactos_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_cliente_contactos
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCliente == IdCliente
                             select new fa_cliente_contactos_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCiudad = q.IdCiudad,
                                 IdCliente = q.IdCliente,
                                 IdContacto = q.IdContacto,
                                 IdParroquia = q.IdParroquia,
                                 Celular = q.Celular,
                                 Correo = q.Correo,
                                 Direccion = q.Direccion,
                                 Nombres = q.Nombres,
                                 Telefono = q.Telefono,
                                 nom_ciudad = q.Descripcion_Ciudad,
                                 nom_parroquia = q.nom_parroquia
                             }).ToList();
                    Lista.ForEach(q => q.Nombres_combo = q.Direccion + " - " + q.nom_ciudad + " - " + q.Telefono + " " + q.Nombres);
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }      

        public fa_cliente_contactos_Info get_info(int IdEmpresa, decimal IdCliente, int IdContacto)
        {
            try
            {
                fa_cliente_contactos_Info info = new fa_cliente_contactos_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_cliente_contactos Entity = Context.fa_cliente_contactos.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCliente == IdCliente && q.IdContacto == IdContacto);
                    if (Entity == null) return null;
                    info = new fa_cliente_contactos_Info
                    {

                        IdEmpresa = Entity.IdEmpresa,
                        IdCiudad = Entity.IdCiudad,
                        IdCliente = Entity.IdCliente,
                        IdContacto = Entity.IdContacto,
                        IdParroquia = Entity.IdParroquia,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        Direccion = Entity.Direccion,
                        Nombres = Entity.Nombres,
                        Telefono = Entity.Telefono
                    };

                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<fa_cliente_contactos_Info> Lista)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    foreach (var item in Lista)
                    {
                        fa_cliente_contactos Entity = Context.fa_cliente_contactos.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdCliente == item.IdCliente && q.IdContacto == item.IdContacto);
                        if (Entity == null)
                        {
                            Entity = new fa_cliente_contactos

                            {
                                IdEmpresa = item.IdEmpresa,
                                IdCliente = item.IdCliente,
                                IdContacto = item.IdContacto,
                                IdCiudad = item.IdCiudad,
                                IdParroquia = item.IdParroquia,
                                Celular = item.Celular,
                                Correo = item.Correo,
                                Direccion = item.Direccion,
                                Nombres = item.Nombres,
                                Telefono = item.Telefono
                            };
                            Context.fa_cliente_contactos.Add(Entity);
                        }
                        else
                        {
                            Entity.IdCiudad = item.IdCiudad;
                            Entity.IdParroquia = item.IdParroquia;
                            Entity.Celular = item.Celular;
                            Entity.Correo = item.Correo;
                            Entity.Direccion = item.Direccion;
                            Entity.Nombres = item.Nombres;
                            Entity.Telefono = item.Telefono;
                        };
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

        public bool guardarDB(fa_cliente_contactos_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                        fa_cliente_contactos Entity = Context.fa_cliente_contactos.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente && q.IdContacto == info.IdContacto);
                        if (Entity == null)
                        {
                            Entity = new fa_cliente_contactos

                            {
                                IdEmpresa = info.IdEmpresa,
                                IdCliente = info.IdCliente,
                                IdContacto = 1,
                                IdCiudad = info.IdCiudad,
                                IdParroquia = info.IdParroquia,
                                Celular = info.Celular,
                                Correo = info.Correo,
                                Direccion = info.Direccion,
                                Nombres = info.Nombres,
                                Telefono = info.Telefono
                            };
                            Context.fa_cliente_contactos.Add(Entity);
                        }
                        else
                        {
                            Entity.IdCiudad = info.IdCiudad;
                            Entity.IdParroquia = info.IdParroquia;
                            Entity.Celular = info.Celular;
                            Entity.Correo = info.Correo;
                            Entity.Direccion = info.Direccion;
                            Entity.Nombres = info.Nombres;
                            Entity.Telefono = info.Telefono;
                        };

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
