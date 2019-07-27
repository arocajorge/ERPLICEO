using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
   public class tb_sis_reporte_x_seg_usuario_Data
    {
        public List<tb_sis_reporte_x_seg_usuario_Info> get_list(int IdEmpresa, string IdUsuario, bool MostrarNoAsignados)
        {
            try
            {
                List<tb_sis_reporte_x_seg_usuario_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    Lista = (from q in Context.tb_sis_reporte_x_seg_usuario
                             join r in Context.tb_sis_reporte
                             on q.CodReporte equals r.CodReporte
                             where q.IdEmpresa == IdEmpresa
                             && q.IdUsuario == IdUsuario
                             && r.se_muestra_administrador_reportes == true
                             select new tb_sis_reporte_x_seg_usuario_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdUsuario = q.IdUsuario,
                                 CodReporte = q.CodReporte,
                                 nom_reporte = r.nom_reporte,
                                 observacion = r.observacion,
                                 mvc_area = r.mvc_area,
                                 mvc_controlador = r.mvc_controlador,
                                 mvc_accion = r.mvc_accion,
                                 seleccionado = true
                             }).ToList();

                    if (MostrarNoAsignados)
                    {
                        Lista.AddRange((from q in Context.tb_sis_reporte
                                        where !Context.tb_sis_reporte_x_seg_usuario.Any(meu => meu.CodReporte == q.CodReporte && meu.IdEmpresa == IdEmpresa && meu.IdUsuario == IdUsuario)
                                        && q.se_muestra_administrador_reportes == true
                                        select new tb_sis_reporte_x_seg_usuario_Info
                                        {
                                            IdEmpresa = IdEmpresa,
                                            IdUsuario = IdUsuario,
                                            CodReporte = q.CodReporte,
                                            nom_reporte = q.nom_reporte,
                                            observacion = q.observacion,
                                            seleccionado = false
                                        }).ToList());
                    }                    
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, string IdUsuario)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    Context.Database.ExecuteSqlCommand("DELETE web.tb_sis_reporte_x_seg_usuario WHERE IdEmpresa = " + IdEmpresa + " AND IdUsuario = '" + IdUsuario + "'");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<tb_sis_reporte_x_seg_usuario_Info> Lista, int IdEmpresa, string IdUsuario)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    foreach (var item in Lista)
                    {
                        Context.tb_sis_reporte_x_seg_usuario.Add(new tb_sis_reporte_x_seg_usuario
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdUsuario = item.IdUsuario,
                            CodReporte = item.CodReporte
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

    }
}
