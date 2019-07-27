using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_sis_Documento_Tipo_Data
    {
        public List<tb_sis_Documento_Tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<tb_sis_Documento_Tipo_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.tb_sis_Documento_Tipo
                                 select new tb_sis_Documento_Tipo_Info
                                 {
                                     codDocumentoTipo = q.codDocumentoTipo,
                                     descripcion = q.descripcion,
                                    estado = q.estado,
                                    Posicion  = q.Posicion 

                                 }).ToList();

                    else
                        Lista = (from q in Context.tb_sis_Documento_Tipo
                                 where q.estado == "A"
                                 select new tb_sis_Documento_Tipo_Info
                                 {
                                     codDocumentoTipo = q.codDocumentoTipo,
                                     descripcion = q.descripcion,
                                     estado = q.estado,
                                     Posicion = q.Posicion

                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sis_Documento_Tipo_Info get_info(string CodDocumentoTipo)
        {
            try
            {
                tb_sis_Documento_Tipo_Info info = new tb_sis_Documento_Tipo_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo Entity = Context.tb_sis_Documento_Tipo.FirstOrDefault(q => q.codDocumentoTipo == CodDocumentoTipo);
                    if (Entity == null) return null;
                    info = new tb_sis_Documento_Tipo_Info
                    {
                        codDocumentoTipo = Entity.codDocumentoTipo,
                        descripcion = Entity.descripcion,
                        estado = Entity.estado,
                        Posicion = Entity.Posicion


                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodDocumento(string CodDocumentoTipo)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_sis_Documento_Tipo
                              where CodDocumentoTipo == q.codDocumentoTipo
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

        public bool guardarDB(tb_sis_Documento_Tipo_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo Entity = new tb_sis_Documento_Tipo
                    {
                        codDocumentoTipo = info.codDocumentoTipo,
                        descripcion = info.descripcion,
                        estado = info.estado="A",
                        Posicion = info.Posicion

                    };
                    Context.tb_sis_Documento_Tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_sis_Documento_Tipo_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo Entity = Context.tb_sis_Documento_Tipo.FirstOrDefault(q => q.codDocumentoTipo == info.codDocumentoTipo);
                    if (Entity == null) return false;

                    Entity.descripcion = info.descripcion;
                    Entity.Posicion = info.Posicion;

                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_sis_Documento_Tipo_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo Entity = Context.tb_sis_Documento_Tipo.FirstOrDefault(q => q.codDocumentoTipo == info.codDocumentoTipo);
                    if (Entity == null) return false;

                    Entity.estado = info.estado="I";

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
