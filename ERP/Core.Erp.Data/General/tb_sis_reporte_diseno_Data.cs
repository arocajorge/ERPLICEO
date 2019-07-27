using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.General;
namespace Core.Erp.Data.General
{
   public class tb_sis_reporte_diseno_Data
    {
        public List<tb_sis_reporte_diseno_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<tb_sis_reporte_diseno_Info> Lista=null;
                //using (Entities_general Context = new Entities_general())
                //{
                //        Lista = (from q in Context.tb_sis_reporte_diseno
                //                 select new tb_sis_reporte_diseno_Info
                //                 {
                //                     IdEmpresa = q.IdEmpresa,
                //                     codDocumentoTipo = q.codDocumentoTipo,
                //                     File_Disenio_Reporte = q.File_Disenio_Reporte,
                //                 }).ToList();                   
                //}
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    
        public bool guardarDB(tb_sis_reporte_diseno_Info info)
        {
            try
            {
                //using (Entities_general Context = new Entities_general())
                //{
                //    tb_sis_reporte_diseno Entity = new tb_sis_reporte_diseno
                //    {
                //        IdEmpresa = info.IdEmpresa,
                //        codDocumentoTipo = info.codDocumentoTipo,
                //        Fecha_Transac = info.Fecha_Transac,
                //        IdUsuario = info.IdUsuario 
                //    };
                //    Context.tb_sis_reporte_diseno.Add(Entity);
                //    Context.SaveChanges();

                //}
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sis_reporte_diseno_Info get_info(string codDocumentoTipo)
        {
            try
            {
                tb_sis_reporte_diseno_Info info = new tb_sis_reporte_diseno_Info();
                //using (Entities_general Context = new Entities_general())
                //{
                //    tb_sis_reporte_diseno Entity = Context.tb_sis_reporte_diseno.FirstOrDefault(q => q.codDocumentoTipo == codDocumentoTipo);
                //    if (Entity == null) return null;

                //    info = new tb_sis_reporte_diseno_Info
                //    {
                //        IdEmpresa = Entity.IdEmpresa,
                //        codDocumentoTipo = Entity.codDocumentoTipo,
                //        File_Disenio_Reporte = Entity.File_Disenio_Reporte
                        
                //    };
                //}
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_sis_reporte_diseno_Info info)
        {
            try
            {
                //using (Entities_general Context = new Entities_general())
                //{
                //    tb_sis_reporte_diseno Entity = Context.tb_sis_reporte_diseno.FirstOrDefault(q => q.codDocumentoTipo == info.codDocumentoTipo);
                //    if (Entity == null)
                //        return false;
                //    Entity.Fecha_UltMod = info.Fecha_UltMod;
                //    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                //    Context.SaveChanges();

                //}
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_sis_reporte_diseno_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    //tb_sis_reporte_diseno Entity = Context.tb_sis_reporte_diseno.FirstOrDefault(q => q.codDocumentoTipo == info.codDocumentoTipo);
                    //if (Entity == null)
                    //    return false;
                    //Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    //Context.SaveChanges();

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
