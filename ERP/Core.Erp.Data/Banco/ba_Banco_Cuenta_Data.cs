using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Banco_Cuenta_Data
    {
        public List<ba_Banco_Cuenta_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 99999 : IdSucursal;
                List<ba_Banco_Cuenta_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    if (mostrar_anulados)
                    {
                        Lista = (from q in Context.ba_Banco_Cuenta
                                 where q.IdEmpresa == IdEmpresa
                                 select new ba_Banco_Cuenta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     ba_descripcion = q.ba_descripcion,
                                     ba_Num_Cuenta = q.ba_Num_Cuenta,
                                     ba_num_digito_cheq = q.ba_num_digito_cheq,
                                     ba_Tipo = q.ba_Tipo,
                                     Estado = q.Estado,
                                     IdBanco = q.IdBanco,
                                     IdCtaCble = q.IdCtaCble,
                                     EsFlujoObligatorio = q.EsFlujoObligatorio,
                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    }
                    else
                    {
                        if(IdSucursal==0)
                        {
                            Lista = (from q in Context.ba_Banco_Cuenta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.Estado == "A"
                                     select new ba_Banco_Cuenta_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         ba_descripcion = q.ba_descripcion,
                                         ba_Num_Cuenta = q.ba_Num_Cuenta,
                                         ba_num_digito_cheq = q.ba_num_digito_cheq,
                                         ba_Tipo = q.ba_Tipo,
                                         Estado = q.Estado,
                                         IdBanco = q.IdBanco,
                                         IdCtaCble = q.IdCtaCble,
                                         EsFlujoObligatorio = q.EsFlujoObligatorio,

                                         EstadoBool = q.Estado == "A" ? true : false

                                     }).ToList();
                        }
                        else
                        {
                            Lista = (from q in Context.ba_Banco_Cuenta
                                     join b in Context.ba_Banco_Cuenta_x_tb_sucursal
                                     on new { q.IdEmpresa, q.IdBanco } equals new { b.IdEmpresa, b.IdBanco }
                                     where q.IdEmpresa == IdEmpresa
                                     && q.Estado == "A"
                                     && IdSucursalIni <= b.IdSucursal
                                     && b.IdSucursal <= IdSucursalFin
                                     select new ba_Banco_Cuenta_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         ba_descripcion = q.ba_descripcion,
                                         ba_Num_Cuenta = q.ba_Num_Cuenta,
                                         ba_num_digito_cheq = q.ba_num_digito_cheq,
                                         ba_Tipo = q.ba_Tipo,
                                         Estado = q.Estado,
                                         IdBanco = q.IdBanco,
                                         IdCtaCble = q.IdCtaCble,
                                         EsFlujoObligatorio = q.EsFlujoObligatorio,

                                         EstadoBool = q.Estado == "A" ? true : false

                                     }).ToList();
                        }
                    }                        
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ba_Banco_Cuenta_Info get_info(int IdEmpresa, int idBanco)
        {
            try
            {
                ba_Banco_Cuenta_Info info = new ba_Banco_Cuenta_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Banco_Cuenta Entity = Context.ba_Banco_Cuenta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdBanco == idBanco);
                    if (Entity == null) return null;
                    info = new ba_Banco_Cuenta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        ba_descripcion = Entity.ba_descripcion,
                        ba_Num_Cuenta = Entity.ba_Num_Cuenta,
                        ba_num_digito_cheq = Entity.ba_num_digito_cheq,
                        ba_Tipo = Entity.ba_Tipo,
                        Estado = Entity.Estado,
                        IdBanco = Entity.IdBanco,
                        IdCtaCble = Entity.IdCtaCble,
                        Imprimir_Solo_el_cheque = Entity.Imprimir_Solo_el_cheque,
                        IdBanco_Financiero = Entity.IdBanco_Financiero,
                        ReporteCheque = Entity.ReporteCheque,
                        ReporteChequeComprobante = Entity.ReporteChequeComprobante,
                        EsFlujoObligatorio = Entity.EsFlujoObligatorio,
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_banco Context =  new Entities_banco())
                {
                    var lst = from q in Context.ba_Banco_Cuenta
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdBanco) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ba_Banco_Cuenta_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    Context.ba_Banco_Cuenta.Add(new ba_Banco_Cuenta 
                    {
                        IdEmpresa = info.IdEmpresa,
                        Estado = info.Estado = "A",
                        IdBanco = info.IdBanco = get_id(info.IdEmpresa),
                        ba_descripcion = info.ba_descripcion,
                        ba_Num_Cuenta = info.ba_Num_Cuenta,
                        ba_num_digito_cheq = info.ba_num_digito_cheq,
                        ba_Tipo = info.ba_Tipo,
                        IdCtaCble = info.IdCtaCble,
                        Imprimir_Solo_el_cheque = info.Imprimir_Solo_el_cheque,
                        IdBanco_Financiero = info.IdBanco_Financiero,

                        EsFlujoObligatorio = info.EsFlujoObligatorio,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    });


                   if(info.lstDet.Count>0)
                    {
                        foreach (var item in info.lstDet)
                        {
                            Context.ba_Banco_Cuenta_x_tb_sucursal.Add(new ba_Banco_Cuenta_x_tb_sucursal
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdBanco = info.IdBanco,
                                IdSucursal = item.IdSucursal,
                                Secuencia = item.Secuencia

                            });
                        }
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
        public bool modificarDB(ba_Banco_Cuenta_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Banco_Cuenta Entity = Context.ba_Banco_Cuenta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco);
                    if (Entity == null) return false;

                    Entity.ba_descripcion = info.ba_descripcion;
                    Entity.ba_Num_Cuenta = info.ba_Num_Cuenta;
                    Entity.ba_num_digito_cheq = info.ba_num_digito_cheq;
                    Entity.ba_Tipo = info.ba_Tipo;
                    Entity.IdCtaCble = info.IdCtaCble;
                    Entity.IdBanco_Financiero = info.IdBanco_Financiero;                    
                    Entity.Imprimir_Solo_el_cheque = info.Imprimir_Solo_el_cheque;
                    Entity.EsFlujoObligatorio = info.EsFlujoObligatorio;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;


                    var lst_det = Context.ba_Banco_Cuenta_x_tb_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco).ToList();
                    Context.ba_Banco_Cuenta_x_tb_sucursal.RemoveRange(lst_det);
                    if (info.lstDet.Count > 0)
                    {
                        foreach (var item in info.lstDet)
                        {
                            Context.ba_Banco_Cuenta_x_tb_sucursal.Add(new ba_Banco_Cuenta_x_tb_sucursal
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdBanco = info.IdBanco,
                                IdSucursal = item.IdSucursal,
                                Secuencia = item.Secuencia

                            });
                        }
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
        public bool anularDB(ba_Banco_Cuenta_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Banco_Cuenta Entity = Context.ba_Banco_Cuenta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDbImportacion(List<ba_Banco_Cuenta_Info> Lista_Banco)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    if(Lista_Banco.Count>0)
                    {
                        foreach (var item in Lista_Banco)
                        {
                            ba_Banco_Cuenta Entity = new ba_Banco_Cuenta
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdBanco = item.IdBanco,
                                IdBanco_Financiero = item.IdBanco_Financiero,
                                ba_Tipo = item.ba_Tipo,
                                ba_Num_Cuenta = item.ba_Num_Cuenta,
                                ba_num_digito_cheq = item.ba_num_digito_cheq,
                                IdCtaCble = item.IdCtaCble,
                                Estado = item.Estado="A",
                                ba_descripcion = item.ba_descripcion,
                                Imprimir_Solo_el_cheque = item.Imprimir_Solo_el_cheque,
                                EsFlujoObligatorio = item.EsFlujoObligatorio
                             };
                            Context.ba_Banco_Cuenta.Add(Entity);
                            Context.SaveChanges();
                        }                        
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDisenioDB(int IdEmpresa, int IdBanco, byte[] Disenio)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    var Entity = db.ba_Banco_Cuenta.Where(q => q.IdEmpresa == IdEmpresa && q.IdBanco == IdBanco).FirstOrDefault();
                    if (Entity == null)
                        return false;

                    if (Entity.Imprimir_Solo_el_cheque)
                        Entity.ReporteCheque = Disenio;
                    else
                        Entity.ReporteChequeComprobante = Disenio;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidarSaldoCuenta(int IdEmpresa, string IdCtaCble,  double Valor)
        {
            try
            {
                Entities_contabilidad db_c = new Entities_contabilidad();
                Entities_banco db_b = new Entities_banco();

                var saldo = db_c.ct_cbtecble_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdCtaCble == IdCtaCble).Sum(q => q.dc_Valor);
                var saldo_act = Math.Round(saldo + Valor, 2, MidpointRounding.AwayFromZero);

                if (saldo_act < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
