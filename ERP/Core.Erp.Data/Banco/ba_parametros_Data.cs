using Core.Erp.Info.Banco;
using System;
using System.Linq;

namespace Core.Erp.Data.Banco
{
    public class ba_parametros_Data
    {
        public ba_parametros_Info get_info(int IdEmpresa)
        {
            try
            {
                ba_parametros_Info info = new ba_parametros_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_parametros Entity = Context.ba_parametros.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new ba_parametros_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        CiudadDefaultParaCrearCheques = Entity.CiudadDefaultParaCrearCheques,
                        DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo,
                        CantidadChequesAlerta = Entity.CantidadChequesAlerta,
                        ValidarSoloCuentasArchivo = Entity.ValidarSoloCuentasArchivo,
                        PermitirSobreGiro = Entity.PermitirSobreGiro
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ba_parametros_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_parametros Entity = Context.ba_parametros.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new ba_parametros
                        {

                            IdEmpresa = info.IdEmpresa,
                            CiudadDefaultParaCrearCheques = info.CiudadDefaultParaCrearCheques,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo,
                            IdUsuario = info.IdUsuario,
                            FechaTransac = DateTime.Now,
                            PermitirSobreGiro = info.PermitirSobreGiro,
                            CantidadChequesAlerta = info.CantidadChequesAlerta,
                            ValidarSoloCuentasArchivo = info.ValidarSoloCuentasArchivo

                        };
                        Context.ba_parametros.Add(Entity);
                    }
                        else
                        {
                        Entity.CiudadDefaultParaCrearCheques = info.CiudadDefaultParaCrearCheques;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
                        Entity.CantidadChequesAlerta = info.CantidadChequesAlerta;
                        Entity.PermitirSobreGiro = info.PermitirSobreGiro;
                        Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                        Entity.FechaUltMod = DateTime.Now;
                        Entity.ValidarSoloCuentasArchivo = info.ValidarSoloCuentasArchivo;
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
