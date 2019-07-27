using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_sis_Impuesto_Data
    {
        public List<tb_sis_Impuesto_Info> get_list(string IdTipoImpuesto, bool mostrar_anulados)
        {
            try
            {
                List<tb_sis_Impuesto_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.tb_sis_Impuesto
                                 where q.IdTipoImpuesto.Contains(IdTipoImpuesto)
                             select new tb_sis_Impuesto_Info
                             {
                                 IdCod_Impuesto = q.IdCod_Impuesto,
                                 nom_impuesto = q.nom_impuesto,
                                 porcentaje = q.porcentaje,
                                 estado = q.estado,
                                 IdTipoImpuesto = q.IdTipoImpuesto
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_sis_Impuesto
                                 where q.IdTipoImpuesto.Contains(IdTipoImpuesto)
                                 select new tb_sis_Impuesto_Info
                                 {
                                     IdCod_Impuesto = q.IdCod_Impuesto,
                                     nom_impuesto = q.nom_impuesto,
                                     porcentaje = q.porcentaje,
                                     estado = q.estado== true,
                                     IdTipoImpuesto = q.IdTipoImpuesto
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public tb_sis_Impuesto_Info get_info(string IdCod_Impuesto = "")
        {
            try
            {
                tb_sis_Impuesto_Info info = new tb_sis_Impuesto_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Impuesto Entity = Context.tb_sis_Impuesto.FirstOrDefault(q => q.IdCod_Impuesto == IdCod_Impuesto);
                    if (Entity == null) return null;
                    info = new tb_sis_Impuesto_Info
                    {
                        IdCod_Impuesto = Entity.IdCod_Impuesto,
                        IdTipoImpuesto = Entity.IdTipoImpuesto,
                        nom_impuesto = Entity.nom_impuesto,
                        porcentaje = Entity.porcentaje,
                        IdCodigo_SRI = Entity.IdCodigo_SRI,
                        estado = Entity.estado
                    };
                } return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(tb_sis_Impuesto_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Impuesto Entity = new tb_sis_Impuesto
                    {
                        IdCod_Impuesto = info.IdCod_Impuesto,
                        IdTipoImpuesto = info.IdTipoImpuesto,
                        nom_impuesto = info.nom_impuesto,
                        estado = info.estado = true,
                        IdCodigo_SRI = info.IdCodigo_SRI,
                        porcentaje = info.porcentaje
                    };
                    Context.tb_sis_Impuesto.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_sis_Impuesto_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Impuesto Entity = Context.tb_sis_Impuesto.FirstOrDefault(q => q.IdCod_Impuesto == info.IdCod_Impuesto);
                    if (Entity == null)
                        return false;
                    Entity.IdTipoImpuesto = info.IdTipoImpuesto;
                    Entity.nom_impuesto = info.nom_impuesto;
                    Entity.porcentaje = info.porcentaje;
                    Entity.IdCodigo_SRI = info.IdCodigo_SRI;

                    Context.SaveChanges();
                } 
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_sis_Impuesto_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Impuesto Entity = Context.tb_sis_Impuesto.FirstOrDefault(q => q.IdCod_Impuesto == info.IdCod_Impuesto);
                    if (Entity == null)
                        return false;
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
        public bool validar_existe_IdCod_Impuesto(string IdCod_Impuesto)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_sis_Impuesto
                              where IdCod_Impuesto == q.IdCod_Impuesto
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
    }
}
