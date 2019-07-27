using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_banco_Data
    {
        public List<tb_banco_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<tb_banco_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.tb_banco
                             select new tb_banco_Info
                             {
                                 IdBanco = q.IdBanco,
                                 ba_descripcion = q.ba_descripcion,
                                 Estado = q.Estado,
                                 CodigoLegal = q.CodigoLegal,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_banco
                                 where q.Estado == "A"
                                 select new tb_banco_Info
                                 {
                                     IdBanco = q.IdBanco,
                                     ba_descripcion = q.ba_descripcion,
                                     Estado = q.Estado,
                                     CodigoLegal = q.CodigoLegal,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_banco_Info get_info(int IdBanco)
        {
            try
            {
                tb_banco_Info info = new tb_banco_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco Entity = Context.tb_banco.FirstOrDefault(q => q.IdBanco == IdBanco);
                    if (Entity == null) return null;

                    info = new tb_banco_Info
                    {
                        
                        IdBanco = Entity.IdBanco,
                        ba_descripcion = Entity.ba_descripcion,
                        Estado = Entity.Estado,
                        CodigoLegal = Entity.CodigoLegal,
                        TieneFormatoTransferencia = Entity.TieneFormatoTransferencia

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private int get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_banco
                             select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdBanco) +1;
                }
                return ID;
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

                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_banco_procesos_bancarios_x_empresa
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdProceso) + 1;

                }

                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_banco_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco Entity = new tb_banco
                    {
                        IdBanco = info.IdBanco = get_id(),
                        ba_descripcion = info.ba_descripcion,
                        Estado = info.Estado = "A",
                        CodigoLegal = info.CodigoLegal,
                        TieneFormatoTransferencia = info.TieneFormatoTransferencia
                    };
                    Context.tb_banco.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(tb_banco_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco Entity = Context.tb_banco.FirstOrDefault(q => q.IdBanco == info.IdBanco);
                    if (Entity == null)
                        return false;
                    Entity.CodigoLegal = info.CodigoLegal;
                    Entity.ba_descripcion = info.ba_descripcion;
                    Entity.TieneFormatoTransferencia = info.TieneFormatoTransferencia;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(tb_banco_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_banco Entity = Context.tb_banco.FirstOrDefault(q => q.IdBanco == info.IdBanco);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<tb_banco_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_banco_Info> Lista = new List<tb_banco_Info>();
            Lista = get_list( skip, take, args.Filter);

            return Lista;
        }

        public tb_banco_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            int id;
            if (args.Value == null || !int.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(Convert.ToInt32(args.Value));
        }

        public tb_banco_Info get_info_demanda(int IdBanco)
        {
            tb_banco_Info info = new tb_banco_Info();
            using (Entities_general Contex = new Entities_general())
            {
                info = (from q in Contex.tb_banco
                        where q.IdBanco == IdBanco
                        select new tb_banco_Info
                        {
                            IdBanco = q.IdBanco,
                            ba_descripcion = q.ba_descripcion
                        }).FirstOrDefault();
            }
            return info;
        }

        public List<tb_banco_Info> get_list( int skip, int take, string filter)
        {
            try
            {
                List<tb_banco_Info> Lista = new List<tb_banco_Info>();
                Entities_general Context = new Entities_general();
                var lst = (from
                          p in Context.tb_banco
                           where  (p.IdBanco.ToString() + " " + p.ba_descripcion).Contains(filter)
                           select new
                           {
                               p.IdBanco,
                               p.ba_descripcion,
                               p.CodigoLegal,
                               p.TieneFormatoTransferencia,
                               p.Estado

                           })
                             .OrderBy(p => p.IdBanco)
                             .Skip(skip)
                             .Take(take)
                             .ToList();
                foreach (var q in lst)
                {
                    Lista.Add(new tb_banco_Info
                    {
                        IdBanco = q.IdBanco,
                        ba_descripcion = q.ba_descripcion,
                        CodigoLegal = q.CodigoLegal,
                        TieneFormatoTransferencia = q.TieneFormatoTransferencia,
                        Estado = q.Estado
                    });
                }
                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
