using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

                List<tb_banco_procesos_bancarios_x_empresa_Info> Lista = new List<tb_banco_procesos_bancarios_x_empresa_Info>();
                using (SqlConnection connection = new SqlConnection(ConexionERP.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT        pxb.IdEmpresa, pxb.IdProceso, pxb.IdProceso_bancario_tipo, pxb.Codigo_Empresa, b.ba_descripcion, b.CodigoLegal, pxb.NombreProceso, pxb.IdTipoNota, pxb.Se_contabiliza, pxb.estado, b.IdBanco, pxb.TipoFiltro"
                                        + " FROM dbo.tb_banco AS b INNER JOIN"
                                        +" dbo.tb_banco_procesos_bancarios_x_empresa AS pxb ON b.IdBanco = pxb.IdBanco"
                                        +" where pxb.IdEmpresa = "+IdEmpresa.ToString() +" and pxb.Estado = 'A'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new tb_banco_procesos_bancarios_x_empresa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProceso = Convert.ToInt32(reader["IdProceso"]),
                            IdBanco = Convert.ToInt32(reader["IdBanco"]),
                            IdProceso_bancario_tipo = Convert.ToString(reader["IdProceso_bancario_tipo"]),
                            NombreProceso = Convert.ToString(reader["NombreProceso"]),
                            Codigo_Empresa = Convert.ToString(reader["Codigo_Empresa"]),
                            ba_descripcion = Convert.ToString(reader["ba_descripcion"]),
                            CodigoLegal = Convert.ToString(reader["CodigoLegal"]),
                            estado = Convert.ToString(reader["estado"]),
                            TipoFiltro = Convert.ToString(reader["TipoFiltro"])
                        });
                    }
                    reader.Close();
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
                List<tb_banco_procesos_bancarios_x_empresa_Info> Lista = new List<tb_banco_procesos_bancarios_x_empresa_Info>();
                using (SqlConnection connection = new SqlConnection(ConexionERP.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT [IdEmpresa],[IdProceso],[IdProceso_bancario_tipo],[IdBanco],[Codigo_Empresa],[IdTipoNota],[Se_contabiliza],[estado],[NombreProceso],[Academico],[ERP],[TipoFiltro]"
                                          + " FROM[dbo].[tb_banco_procesos_bancarios_x_empresa]"
                                          + " WHERE IdBanco = " +IdBanco.ToString()+" and IdEmpresa = "+IdEmpresa.ToString();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new tb_banco_procesos_bancarios_x_empresa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProceso = Convert.ToInt32(reader["IdProceso"]),
                            IdBanco = Convert.ToInt32(reader["IdBanco"]),
                            IdProceso_bancario_tipo = Convert.ToString(reader["IdProceso_bancario_tipo"]),
                            NombreProceso = Convert.ToString(reader["NombreProceso"]),
                            Codigo_Empresa = Convert.ToString(reader["Codigo_Empresa"]),
                            estado = Convert.ToString(reader["estado"]),
                            TipoFiltro = Convert.ToString(reader["TipoFiltro"]),
                            IdTipoNota = reader["IdTipoNota"] == DBNull.Value ? null : (int?)reader["IdTipoNota"]
                        });
                    }
                    reader.Close();
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
                using (SqlConnection connection = new SqlConnection(ConexionERP.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT [IdEmpresa],[IdProceso],[IdProceso_bancario_tipo],[IdBanco],[Codigo_Empresa],[IdTipoNota],[Se_contabiliza],[estado],[NombreProceso],[Academico],[ERP],[TipoFiltro]"
                                          + " FROM[dbo].[tb_banco_procesos_bancarios_x_empresa]"
                                          + " WHERE IdProceso = "+IdProceso.ToString()+" and IdEmpresa = "+IdEmpresa.ToString();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        info = new tb_banco_procesos_bancarios_x_empresa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProceso = Convert.ToInt32(reader["IdProceso"]),
                            IdBanco = Convert.ToInt32(reader["IdBanco"]),
                            IdProceso_bancario_tipo = Convert.ToString(reader["IdProceso_bancario_tipo"]),
                            NombreProceso = Convert.ToString(reader["NombreProceso"]),
                            Codigo_Empresa = Convert.ToString(reader["Codigo_Empresa"]),
                            estado = Convert.ToString(reader["estado"]),
                            TipoFiltro = Convert.ToString(reader["TipoFiltro"]),
                            IdTipoNota = reader["IdTipoNota"] == DBNull.Value ? null : (int?)reader["IdTipoNota"]
                        };
                    }
                    reader.Close();
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
