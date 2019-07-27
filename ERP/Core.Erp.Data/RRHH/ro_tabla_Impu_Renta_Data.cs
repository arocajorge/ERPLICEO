using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_tabla_Impu_Renta_Data
    {
        public List<ro_tabla_Impu_Renta_Info> get_list()
        {
            try
            {
                List<ro_tabla_Impu_Renta_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.ro_tabla_Impu_Renta
                                 select new ro_tabla_Impu_Renta_Info
                                 {
                                     AnioFiscal = q.AnioFiscal,
                                     Secuencia = q.Secuencia,
                                     FraccionBasica = q.FraccionBasica,
                                     ExcesoHasta = q.ExcesoHasta,
                                     ImpFraccionBasica=q.ImpFraccionBasica,
                                     Por_ImpFraccion_Exce=q.Por_ImpFraccion_Exce
                                 }).ToList();
                   
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_tabla_Impu_Renta_Info> get_list(int AnioFiscal)
        {
            try
            {
                List<ro_tabla_Impu_Renta_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_tabla_Impu_Renta
                             where q.AnioFiscal==AnioFiscal
                             select new ro_tabla_Impu_Renta_Info
                             {
                                 AnioFiscal = q.AnioFiscal,
                                 Secuencia = q.Secuencia,
                                 FraccionBasica = q.FraccionBasica,
                                 ExcesoHasta = q.ExcesoHasta,
                                 ImpFraccionBasica = q.ImpFraccionBasica,
                                 Por_ImpFraccion_Exce = q.Por_ImpFraccion_Exce
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_tabla_Impu_Renta_Info get_info(int AnioFiscal, int Secuencia)
        {
            try
            {
                ro_tabla_Impu_Renta_Info info = new ro_tabla_Impu_Renta_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tabla_Impu_Renta Entity = Context.ro_tabla_Impu_Renta.FirstOrDefault(q => q.AnioFiscal == AnioFiscal && q.Secuencia == Secuencia);
                    if (Entity == null) return null;

                    info = new ro_tabla_Impu_Renta_Info
                    {
                        AnioFiscal = Entity.AnioFiscal,
                        Secuencia = Entity.Secuencia,
                        FraccionBasica = Entity.FraccionBasica,
                        ExcesoHasta = Entity.ExcesoHasta,
                        ImpFraccionBasica = Entity.ImpFraccionBasica,
                        Por_ImpFraccion_Exce = Entity.Por_ImpFraccion_Exce
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int AnioFiscal)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_tabla_Impu_Renta
                              where q.AnioFiscal == AnioFiscal
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.Secuencia) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_tabla_Impu_Renta_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tabla_Impu_Renta Entity = new ro_tabla_Impu_Renta
                    {
                        
                        AnioFiscal = info.AnioFiscal,
                        Secuencia = info.Secuencia=get_id(info.AnioFiscal),
                        FraccionBasica = info.FraccionBasica,
                        ExcesoHasta = info.ExcesoHasta,
                        ImpFraccionBasica = info.ImpFraccionBasica,
                        Por_ImpFraccion_Exce = info.Por_ImpFraccion_Exce
                    };
                    Context.ro_tabla_Impu_Renta.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_tabla_Impu_Renta_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tabla_Impu_Renta Entity = Context.ro_tabla_Impu_Renta.FirstOrDefault(q => q.AnioFiscal == info.AnioFiscal && q.Secuencia == info.Secuencia);
                    if (Entity == null)
                        return false;
                    Entity.FraccionBasica = info.FraccionBasica;
                    Entity.ImpFraccionBasica = info.ImpFraccionBasica;
                    Entity.Por_ImpFraccion_Exce = info.Por_ImpFraccion_Exce;
                    Entity.ExcesoHasta = info.ExcesoHasta;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_tabla_Impu_Renta_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tabla_Impu_Renta Entity = Context.ro_tabla_Impu_Renta.FirstOrDefault(q => q.AnioFiscal == info.AnioFiscal && q.Secuencia == info.Secuencia);
                    if (Entity == null)
                        return false;
                    Context.ro_tabla_Impu_Renta.Remove(Entity);
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
