using Core.Erp.Info.General;
using System;
using System.Linq;

namespace Core.Erp.Data.General
{
    public class tb_ColaImpresionDirecta_Data
    {
        public bool GuardarDB(tb_ColaImpresionDirecta_Info info)
        {
            try
            {
                using (Entities_general db = new Entities_general())
                {
                    db.tb_ColaImpresionDirecta.Add(new tb_ColaImpresionDirecta
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdImpresion = info.IdImpresion = GetID(info.IdEmpresa),
                        CodReporte = info.CodReporte,
                        IPUsuario = info.IPUsuario,
                        IPImpresora = info.IPImpresora,
                        Parametros = info.Parametros,
                        Usuario = info.Usuario,
                        NombreEmpresa = info.NombreEmpresa,
                        FechaEnvio = DateTime.Now ,
                        NumCopias = info.NumCopias                       
                    });
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarDB(tb_ColaImpresionDirecta_Info info)
        {
            try
            {
                using (Entities_general db = new Entities_general())
                {
                    var Entity = db.tb_ColaImpresionDirecta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdImpresion == info.IdImpresion).FirstOrDefault();
                    if (Entity == null)
                        return false;
                    Entity.FechaImpresion = DateTime.Now;
                    Entity.Comentario = info.Comentario;

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_ColaImpresionDirecta_Info GetInfoPorImprimir(string IPUsuario)
        {
            try
            {
                using (Entities_general db = new Entities_general())
                {
                    var entity = db.tb_ColaImpresionDirecta.Where(q => q.IPUsuario == IPUsuario && q.FechaImpresion == null).FirstOrDefault();
                    if (entity == null)
                        return null;
                    return new tb_ColaImpresionDirecta_Info
                    {
                        IdEmpresa = entity.IdEmpresa,
                        IdImpresion = entity.IdImpresion,
                        CodReporte = entity.CodReporte,
                        IPUsuario = entity.IPUsuario,
                        IPImpresora = entity.IPImpresora,
                        Parametros = entity.Parametros,
                        Usuario = entity.Usuario,
                        NombreEmpresa = entity.NombreEmpresa,
                        FechaEnvio = entity.FechaEnvio,
                        FechaImpresion = entity.FechaImpresion,
                        Comentario = entity.Comentario,
                        NumCopias = entity.NumCopias
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal GetID(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_general db = new Entities_general())
                {
                    var lst = db.tb_ColaImpresionDirecta.Where(q => q.IdEmpresa == IdEmpresa).ToList();
                    if (lst.Count > 0)
                        ID = lst.Max(q => q.IdImpresion) +1;
                }
                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
