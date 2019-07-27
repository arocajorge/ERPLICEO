using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_MotivoTraslado_Data
    {
        public List<fa_MotivoTraslado_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<fa_MotivoTraslado_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.fa_MotivoTraslado
                                 where q.IdEmpresa == IdEmpresa
                                 select new fa_MotivoTraslado_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMotivoTraslado = q.IdMotivoTraslado,
                                     tr_Descripcion = q.tr_Descripcion,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        Lista = (from q in Context.fa_MotivoTraslado
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == true
                                 select new fa_MotivoTraslado_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMotivoTraslado = q.IdMotivoTraslado,
                                     tr_Descripcion = q.tr_Descripcion,
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

        public fa_MotivoTraslado_Info GetInfo(int IdEmpresa, int IdMotivoTraslado)
        {
            try
            {
                fa_MotivoTraslado_Info info = new fa_MotivoTraslado_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_MotivoTraslado Entity = Context.fa_MotivoTraslado.Where(q => q.IdEmpresa == IdEmpresa && q.IdMotivoTraslado == IdMotivoTraslado).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_MotivoTraslado_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMotivoTraslado = Entity.IdMotivoTraslado,
                        tr_Descripcion = Entity.tr_Descripcion,
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

        private int GetId(int IdEmpresa)
        {
            try
            {
                int Id = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_MotivoTraslado
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdMotivoTraslado) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(fa_MotivoTraslado_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Context.fa_MotivoTraslado.Add(new fa_MotivoTraslado
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMotivoTraslado = info.IdMotivoTraslado=GetId(info.IdEmpresa),
                        tr_Descripcion = info.tr_Descripcion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(fa_MotivoTraslado_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_MotivoTraslado Entity = Context.fa_MotivoTraslado.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivoTraslado == info.IdMotivoTraslado).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.tr_Descripcion = info.tr_Descripcion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Context.SaveChanges();


                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(fa_MotivoTraslado_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_MotivoTraslado Entity = Context.fa_MotivoTraslado.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivoTraslado == info.IdMotivoTraslado).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
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
