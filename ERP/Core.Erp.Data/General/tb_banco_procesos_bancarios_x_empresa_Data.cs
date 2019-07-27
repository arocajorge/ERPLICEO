using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
   public class tb_banco_procesos_bancarios_x_empresa_Data
    {
        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<tb_banco_procesos_bancarios_x_empresa_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.vwtb_banco_procesos_bancarios_x_empresa
                                 where q.IdEmpresa==IdEmpresa
                                 select new tb_banco_procesos_bancarios_x_empresa_Info
                                 {
                                     IdEmpresa=q.IdEmpresa,
                                     IdProceso=q.IdProceso,
                                     IdBanco = q.IdBanco,
                                     IdProceso_bancario_tipo = q.IdProceso_bancario_tipo,
                                     NombreProceso=q.NombreProceso,
                                     Codigo_Empresa=q.Codigo_Empresa,
                                     ba_descripcion=q.ba_descripcion,
                                      CodigoLegal=q.CodigoLegal,
                                      estado=q.estado

                                 }).ToList();
                    else
                        Lista = (from q in Context.vwtb_banco_procesos_bancarios_x_empresa
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado=="A"
                                 select new tb_banco_procesos_bancarios_x_empresa_Info
                                 {
                                     IdEmpresa=q.IdEmpresa,
                                     IdBanco = q.IdBanco,
                                     IdProceso_bancario_tipo = q.IdProceso_bancario_tipo,
                                     NombreProceso = q.NombreProceso,
                                     Codigo_Empresa = q.Codigo_Empresa,
                                     CodigoLegal=q.CodigoLegal,
                                     estado = q.estado,
                                     IdProceso = q.IdProceso


                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(int IdEmpresa, int IdBanco, bool SeContabiliza)
        {
            try
            {
                List<tb_banco_procesos_bancarios_x_empresa_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                        Lista = (from q in Context.tb_banco_procesos_bancarios_x_empresa
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdBanco==IdBanco
                                 && q.Se_contabiliza == SeContabiliza
                                 select new tb_banco_procesos_bancarios_x_empresa_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProceso = q.IdProceso,
                                     IdBanco = q.IdBanco,
                                     IdProceso_bancario_tipo = q.IdProceso_bancario_tipo,
                                     NombreProceso = q.NombreProceso,
                                     Codigo_Empresa = q.Codigo_Empresa,
                                     estado = q.estado,
                                     IdTipoNota = q.IdTipoNota

                                 }).ToList();
                   
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(int IdEmpresa, int IdBanco)
        {
            try
            {
                List<tb_banco_procesos_bancarios_x_empresa_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    Lista = (from q in Context.tb_banco_procesos_bancarios_x_empresa
                             where q.IdEmpresa == IdEmpresa
                             && q.IdBanco == IdBanco
                             select new tb_banco_procesos_bancarios_x_empresa_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdProceso = q.IdProceso,
                                 IdBanco = q.IdBanco,
                                 IdProceso_bancario_tipo = q.IdProceso_bancario_tipo,
                                 NombreProceso = q.NombreProceso,
                                 Codigo_Empresa = q.Codigo_Empresa,
                                 estado = q.estado,
                                 IdTipoNota = q.IdTipoNota

                             }).ToList();

                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_banco_procesos_bancarios_x_empresa_Info get_info( int IdEmpresa, int IdProceso)
        {
            try
            {
                tb_banco_procesos_bancarios_x_empresa_Info info = new tb_banco_procesos_bancarios_x_empresa_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco_procesos_bancarios_x_empresa Entity = Context.tb_banco_procesos_bancarios_x_empresa.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdProceso== IdProceso);
                    if (Entity == null) return null;

                    info = new tb_banco_procesos_bancarios_x_empresa_Info
                    {
                        IdBanco = Entity.IdBanco,
                        IdProceso_bancario_tipo = Entity.IdProceso_bancario_tipo,
                        estado = Entity.estado,
                        Codigo_Empresa = Entity.Codigo_Empresa,
                        NombreProceso=Entity.NombreProceso


                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_banco_procesos_bancarios_x_empresa
                              where q.IdEmpresa==IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdProceso) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_banco_procesos_bancarios_x_empresa_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco_procesos_bancarios_x_empresa Entity = new tb_banco_procesos_bancarios_x_empresa
                    {
                        IdEmpresa = info.IdEmpresa,
                        NombreProceso=info.NombreProceso,
                        IdProceso = info.IdProceso = get_id(info.IdEmpresa),
                       // IdProceso = info.IdProceso = get_id(),
                        IdBanco=info.IdBanco,
                        IdProceso_bancario_tipo = info.IdProceso_bancario_tipo,
                        Codigo_Empresa = info.Codigo_Empresa,
                        estado = info.estado="A",
                        IdTipoNota = info.IdTipoNota,
                        Se_contabiliza=info.Se_contabiliza
                    };
                    Context.tb_banco_procesos_bancarios_x_empresa.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(tb_banco_procesos_bancarios_x_empresa_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    var list = Context.tb_banco_procesos_bancarios_x_empresa.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdProceso == info.IdProceso).FirstOrDefault();
                    if(list != null)
                    {
                        list.IdProceso_bancario_tipo = info.IdProceso_bancario_tipo;
                        list.IdBanco = info.IdBanco;
                        list.NombreProceso = info.NombreProceso;
                        list.Codigo_Empresa = info.Codigo_Empresa;
                        list.Se_contabiliza = info.Se_contabiliza;
                        list.IdTipoNota = info.IdTipoNota;
                    }
                    else
                    {
                        Context.tb_banco_procesos_bancarios_x_empresa.Add(new tb_banco_procesos_bancarios_x_empresa
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdBanco = info.IdBanco,
                            IdProceso = info.IdProceso = get_id(info.IdEmpresa),
                            Codigo_Empresa = info.Codigo_Empresa,
                            IdProceso_bancario_tipo = info.IdProceso_bancario_tipo,
                            IdTipoNota = info.IdTipoNota,
                            NombreProceso = info.NombreProceso,
                            Se_contabiliza = info.Se_contabiliza
                        });
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
        public bool anularDB(tb_banco_procesos_bancarios_x_empresa_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco_procesos_bancarios_x_empresa Entity = Context.tb_banco_procesos_bancarios_x_empresa.FirstOrDefault(q => q.IdProceso == info.IdProceso&& q.IdEmpresa==info.IdEmpresa);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = "I";
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
