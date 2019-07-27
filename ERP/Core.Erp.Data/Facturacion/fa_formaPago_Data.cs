using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_formaPago_Data
    {
        public List<fa_formaPago_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<fa_formaPago_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.fa_formaPago
                             select new fa_formaPago_Info
                             {
                                 IdFormaPago = q.IdFormaPago,
                                 nom_FormaPago = q.nom_FormaPago,
                                 Estado = q.Estado
                             
                             }).ToList();
                    else
                        Lista = (from q in Context.fa_formaPago
                                 where q.Estado == true
                                 select new fa_formaPago_Info
                                 {
                                     IdFormaPago = q.IdFormaPago,
                                     nom_FormaPago = q.nom_FormaPago,
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

        public fa_formaPago_Info GetInfo(string IdFormaPago)
        {
            try
            {
                fa_formaPago_Info info = new fa_formaPago_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_formaPago Entity = Context.fa_formaPago.Where(q => q.IdFormaPago == IdFormaPago).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_formaPago_Info
                        {
                            IdFormaPago = Entity.IdFormaPago,
                            nom_FormaPago = Entity.nom_FormaPago,
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

        public bool ValidarIdFormaPago(string IdFormaPago)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_formaPago
                              where q.IdFormaPago == IdFormaPago
                              select q;
                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(fa_formaPago_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {

                    Context.fa_formaPago.Add(new fa_formaPago
                    {
                        IdFormaPago = info.IdFormaPago,
                        nom_FormaPago = info.nom_FormaPago,
                        Estado = true,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
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

        public bool ModificarDB(fa_formaPago_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_formaPago Entity = Context.fa_formaPago.Where(q => q.IdFormaPago == info.IdFormaPago).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.nom_FormaPago = info.nom_FormaPago;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
            }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(fa_formaPago_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_formaPago Entity = Context.fa_formaPago.Where(q => q.IdFormaPago == info.IdFormaPago).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.Estado = false;
                    Entity.IdUsuarioUltAnu  = info.IdUsuarioUltAnu;
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


    }
}
