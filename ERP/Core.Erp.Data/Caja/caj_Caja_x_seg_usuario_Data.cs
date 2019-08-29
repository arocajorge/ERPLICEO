using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class caj_Caja_x_seg_usuario_Data
    {
        public List<caj_Caja_x_seg_usuario_Info> get_list(int IdEmpresa, int IdCaja)
        {
            try
            {
                List<caj_Caja_x_seg_usuario_Info> Lista;

                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.caj_Caja_x_seg_usuario
                             where q.IdEmpresa == IdEmpresa
                              && q.IdCaja == IdCaja
                             select new caj_Caja_x_seg_usuario_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Secuencia = q.Secuencia,
                                 IdCaja = q.IdCaja,
                                 IdUsuario = q.IdUsuario
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public caj_Caja_x_seg_usuario_Info get_info(int IdEmpresa, int IdCaja, int Secuencia)
        {
            try
            {
                caj_Caja_x_seg_usuario_Info info = new caj_Caja_x_seg_usuario_Info();

                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_x_seg_usuario Entity = Context.caj_Caja_x_seg_usuario.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCaja == IdCaja && q.Secuencia == Secuencia);
                    if (Entity == null) return null;
                    info = new caj_Caja_x_seg_usuario_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCaja = Entity.IdCaja,
                        Secuencia = Entity.Secuencia,
                        IdUsuario = Entity.IdUsuario,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_secuencia(int IdEmpresa, decimal IdCaja)
        {
            try
            {
                int Secuencia = 1;

                using (Entities_caja Context = new Entities_caja())
                {
                    var lst = from q in Context.caj_Caja_x_seg_usuario
                              where q.IdEmpresa == IdEmpresa
                              && q.IdCaja == IdCaja
                              select q;

                    if (lst.Count() > 0)
                        Secuencia = lst.Max(q => q.Secuencia) + 1;
                }

                return Secuencia;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(caj_Caja_x_seg_usuario_Info info)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_x_seg_usuario Entity = new caj_Caja_x_seg_usuario
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCaja = info.IdCaja,
                        Secuencia = info.Secuencia,
                        IdUsuario = info.IdUsuario
                    };
                    Context.caj_Caja_x_seg_usuario.Add(Entity);
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
