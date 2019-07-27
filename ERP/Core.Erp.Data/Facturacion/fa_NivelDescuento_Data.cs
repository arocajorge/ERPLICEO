using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_NivelDescuento_Data
    {
        public List<fa_NivelDescuento_Info> GetList(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<fa_NivelDescuento_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if(mostrar_anulados)
                    Lista = Context.fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new fa_NivelDescuento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        Estado = q.Estado,
                        Descripcion = q.Descripcion,
                        IdNivel = q.IdNivel,
                        Observacion = q.Observacion,
                        Porcentaje = q.Porcentaje
                    }).ToList();
                    else
                    Lista = Context.fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Select(q => new fa_NivelDescuento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        Estado = q.Estado,
                        Descripcion = q.Descripcion,
                        IdNivel = q.IdNivel,
                        Observacion = q.Observacion,
                        Porcentaje = q.Porcentaje
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public fa_NivelDescuento_Info GetInfo(int IdEmpresa, int IdNivel)
        {
            try
            {
                fa_NivelDescuento_Info info = new fa_NivelDescuento_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_NivelDescuento Entity = Context.fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa && q.IdNivel == IdNivel).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new fa_NivelDescuento_Info
                    {

                        IdEmpresa = Entity.IdEmpresa,
                        IdNivel = Entity.IdNivel,
                        Estado = Entity.Estado,
                        Descripcion = Entity.Descripcion,
                        Observacion = Entity.Observacion,
                        Porcentaje = Entity.Porcentaje
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
                    var lst = Context.fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdNivel);
                    if (lst.Count() > 0)
                        Id = lst.Max() + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(fa_NivelDescuento_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Context.fa_NivelDescuento.Add(new fa_NivelDescuento
                    {

                        IdEmpresa = info.IdEmpresa,
                        IdNivel = info.IdNivel=GetId(info.IdEmpresa),
                        Estado = true,
                        Descripcion = info.Descripcion,
                        Observacion = info.Observacion,
                        Porcentaje = info.Porcentaje
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
        public bool ModificarDB(fa_NivelDescuento_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_NivelDescuento Entity = Context.fa_NivelDescuento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel).FirstOrDefault();
                    if (Entity == null) return false;
                    
                       Entity.Descripcion = info.Descripcion;
                       Entity.Observacion = info.Observacion;
                       Entity.Porcentaje = info.Porcentaje;
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AnularDB(fa_NivelDescuento_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_NivelDescuento Entity = Context.fa_NivelDescuento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel).FirstOrDefault();
                    if (Entity == null) return false;
                    
                    Entity.Estado = false;
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
