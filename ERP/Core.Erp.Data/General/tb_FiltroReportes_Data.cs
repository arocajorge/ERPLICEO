using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_FiltroReportes_Data
    {
        public string GuardarDB(int IdEmpresa, int[] IdSucursal, string IdUsuario)
        {
            try
            {
                string Sucursal = string.Empty;
                using (Entities_general db = new Entities_general())
                {
                    var ls = db.tb_FiltroReportes.Where(q => q.IdUsuario == IdUsuario).ToList();
                    db.tb_FiltroReportes.RemoveRange(ls);
                    if (IdSucursal != null)
                    {
                        foreach (var item in IdSucursal)
                        {
                            db.tb_FiltroReportes.Add(new tb_FiltroReportes
                            {
                                IdEmpresa = IdEmpresa,
                                IdSucursal = item,
                                IdUsuario = IdUsuario,
                                Fecha = DateTime.Now
                            });
                        }
                    }
                    db.SaveChanges();
                    var suc = (from a in db.tb_FiltroReportes
                               join b in db.tb_sucursal
                               on new { a.IdEmpresa, a.IdSucursal } equals new { b.IdEmpresa, b.IdSucursal }
                               where a.IdEmpresa == IdEmpresa && a.IdUsuario == IdUsuario
                               orderby a.IdSucursal
                               select new
                               {
                                   a.IdEmpresa,
                                   a.IdSucursal,
                                   b.Su_Descripcion
                               }).ToList();
                    int s = 0;
                    foreach (var item in suc)
                    {
                        if (s == 0)
                        {
                            Sucursal += item.Su_Descripcion;
                            s = 1;
                        }
                        else
                            Sucursal += ", " + item.Su_Descripcion;
                    }
                }

                return Sucursal;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
