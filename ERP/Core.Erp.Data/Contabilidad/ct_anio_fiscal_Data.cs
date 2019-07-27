using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_anio_fiscal_Data
    {
        public List<ct_anio_fiscal_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<ct_anio_fiscal_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.ct_anio_fiscal
                                 select new ct_anio_fiscal_Info
                                 {
                                     IdanioFiscal = q.IdanioFiscal,
                                     af_fechaIni = q.af_fechaIni,
                                     af_fechaFin = q.af_fechaFin,
                                     af_estado = q.af_estado,

                                     EstadoBool = q.af_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ct_anio_fiscal
                                 where q.af_estado == "A"
                                 select new ct_anio_fiscal_Info
                                 {
                                     IdanioFiscal = q.IdanioFiscal,
                                     af_fechaIni = q.af_fechaIni,
                                     af_fechaFin = q.af_fechaFin,
                                     af_estado = q.af_estado,

                                     EstadoBool = q.af_estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ct_anio_fiscal_Info get_info(int IdanioFiscal)
        {
            try
            {
                ct_anio_fiscal_Info info = new ct_anio_fiscal_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_anio_fiscal Entity = Context.ct_anio_fiscal.FirstOrDefault(q => q.IdanioFiscal == IdanioFiscal);
                    if(Entity == null) return null;
                    info = new ct_anio_fiscal_Info
                    {
                        IdanioFiscal = Entity.IdanioFiscal,
                        af_fechaIni = Entity.af_fechaIni,
                        af_fechaFin = Entity.af_fechaFin,
                        af_estado = Entity.af_estado
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int get_id(int IdanioFiscal = 0)
        {
            try
            {
                int ID = 1;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_anio_fiscal
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdanioFiscal) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_anio_fiscal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (Context.ct_anio_fiscal_x_cuenta_utilidad.Where(q => q.IdEmpresa == info.info_anio_ctautil.IdEmpresa && q.IdanioFiscal == info.IdanioFiscal).FirstOrDefault() != null)                    
                        return modificarDB(info);

                    if (Context.ct_anio_fiscal.Where(q => q.IdanioFiscal == info.IdanioFiscal).FirstOrDefault() == null)
                    {
                        ct_anio_fiscal Entity = new ct_anio_fiscal()
                        {
                            IdanioFiscal = info.IdanioFiscal,
                            af_fechaIni = info.af_fechaIni,
                            af_estado = info.af_estado = "A",
                            af_fechaFin = info.af_fechaFin

                        };
                        Context.ct_anio_fiscal.Add(Entity);
                    }           
                        ct_anio_fiscal_x_cuenta_utilidad Entity_det = new ct_anio_fiscal_x_cuenta_utilidad
                        {
                            IdEmpresa = info.info_anio_ctautil.IdEmpresa,
                            IdanioFiscal = info.IdanioFiscal,
                            IdCtaCble = info.info_anio_ctautil.IdCtaCble,
                            IdCtaCbleCierre = info.info_anio_ctautil.IdCtaCbleCierre
                        };
                        Context.ct_anio_fiscal_x_cuenta_utilidad.Add(Entity_det);                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(ct_anio_fiscal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context  = new Entities_contabilidad())
                {
                    ct_anio_fiscal Entity = Context.ct_anio_fiscal.FirstOrDefault(q => q.IdanioFiscal == info.IdanioFiscal);
                    if (Entity == null)
                        return false;
                    Entity.IdanioFiscal = info.IdanioFiscal;
                    Entity.af_fechaIni = info.af_fechaIni;
                    Entity.af_fechaFin = info.af_fechaFin;

                    var det = Context.ct_anio_fiscal_x_cuenta_utilidad.Where(q => q.IdEmpresa == info.info_anio_ctautil.IdEmpresa && q.IdanioFiscal == info.IdanioFiscal).ToList();
                    Context.ct_anio_fiscal_x_cuenta_utilidad.RemoveRange(det);

                    Context.ct_anio_fiscal_x_cuenta_utilidad.Add(new ct_anio_fiscal_x_cuenta_utilidad
                    {
                        IdEmpresa = info.info_anio_ctautil.IdEmpresa,
                        IdanioFiscal = info.IdanioFiscal,
                        IdCtaCble = info.info_anio_ctautil.IdCtaCble,
                        IdCtaCbleCierre = info.info_anio_ctautil.IdCtaCbleCierre,
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

        public bool anularDB(ct_anio_fiscal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_anio_fiscal Entity = Context.ct_anio_fiscal.FirstOrDefault(q => q.IdanioFiscal == info.IdanioFiscal);
                    if (Entity == null)
                        return false;
                    Entity.af_estado = info.af_estado = "I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool validar_existe_Idanio(int IdanioFiscal)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_anio_fiscal
                              where IdanioFiscal == q.IdanioFiscal
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

        public List<ct_anio_fiscal_Info> get_list_anio_sincierre(int IdEmpresa, int IdSucursal)
        {
            try
            {
                List<ct_anio_fiscal_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                { 
                    Lista = Context.vwct_anio_fiscal_x_tb_sucursal_SinCierre.Where(q=>q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal).Select(q => new ct_anio_fiscal_Info
                    {
                        IdanioFiscal = q.IdanioFiscal
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
