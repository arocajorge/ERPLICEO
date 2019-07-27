using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_TerminoPago_Data
    { 
        public List<fa_TerminoPago_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<fa_TerminoPago_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.fa_TerminoPago
                             select new fa_TerminoPago_Info
                             {
                                  IdTerminoPago = q.IdTerminoPago,
                                  Dias_Vct = q.Dias_Vct,
                                  nom_TerminoPago = q.nom_TerminoPago,
                                  Num_Coutas = q.Num_Coutas,
                                  estado = q.estado
                             }).ToList();
                    else
                        Lista = (from q in Context.fa_TerminoPago
                                 where q.estado == true
                                 select new fa_TerminoPago_Info
                                 {
                                     IdTerminoPago = q.IdTerminoPago,
                                     Dias_Vct = q.Dias_Vct,
                                     nom_TerminoPago = q.nom_TerminoPago,
                                     Num_Coutas = q.Num_Coutas,
                                     estado = q.estado
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_TerminoPago_Info get_info(string IdTerminoPago)
        {
            try
            {
                fa_TerminoPago_Info info = new fa_TerminoPago_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_TerminoPago Entity = Context.fa_TerminoPago.FirstOrDefault(q => q.IdTerminoPago == IdTerminoPago);
                    if (Entity == null) return null;
                    info = new fa_TerminoPago_Info
                    {
                        IdTerminoPago = Entity.IdTerminoPago,
                        Dias_Vct = Entity.Dias_Vct,
                        nom_TerminoPago = Entity.nom_TerminoPago,
                        Num_Coutas = Entity.Num_Coutas,
                        estado = Entity.estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdTerminoPago(string IdTerminoPago)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_TerminoPago
                              where IdTerminoPago == q.IdTerminoPago
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

        public bool guardarDB(fa_TerminoPago_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_TerminoPago Entity = new fa_TerminoPago
                    {
                        IdTerminoPago = info.IdTerminoPago,
                        Dias_Vct = info.Dias_Vct,
                        nom_TerminoPago = info.nom_TerminoPago,
                        Num_Coutas = info.Num_Coutas,
                        estado = info.estado = true

                    };
                    Context.fa_TerminoPago.Add(Entity);
                    int secuencia = 1;
                    foreach (var item in info.Lst_fa_TerminoPago_Distribucion)
                    {
                        fa_TerminoPago_Distribucion det = new fa_TerminoPago_Distribucion
                        {
                            IdTerminoPago = info.IdTerminoPago,
                            Num_Dias_Vcto = item.Num_Dias_Vcto,
                            Por_distribucion = item.Por_distribucion,
                            Secuencia = item.Secuencia = secuencia++
                        };
                        Context.fa_TerminoPago_Distribucion.Add(det);
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_TerminoPago_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_TerminoPago Entity = Context.fa_TerminoPago.FirstOrDefault(q => q.IdTerminoPago == info.IdTerminoPago);
                    if (Entity == null) return false;
                    
                    Entity.Dias_Vct = info.Dias_Vct;
                    Entity.nom_TerminoPago = info.nom_TerminoPago;
                    Entity.Num_Coutas = info.Num_Coutas;
                    
                    foreach (var item in info.Lst_fa_TerminoPago_Distribucion)
                    {
                        fa_TerminoPago_Distribucion det = new fa_TerminoPago_Distribucion
                        {
                            IdTerminoPago = info.IdTerminoPago,
                            Num_Dias_Vcto = item.Num_Dias_Vcto,
                            Por_distribucion = item.Por_distribucion,
                            Secuencia = item.Secuencia
                        };
                        Context.fa_TerminoPago_Distribucion.Add(det);
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(fa_TerminoPago_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_TerminoPago Entity = Context.fa_TerminoPago.FirstOrDefault(q => q.IdTerminoPago == info.IdTerminoPago);
                    if (Entity == null) return false;

                    Entity.estado = info.estado = false;
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
