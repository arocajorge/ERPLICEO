using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Talonario_cheques_x_banco_Data
    {
        public List<ba_Talonario_cheques_x_banco_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                List<ba_Talonario_cheques_x_banco_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ba_Talonario_cheques_x_banco
                                 join p in Context.ba_Banco_Cuenta
                                 on new { q.IdEmpresa , q.IdBanco} equals new {p.IdEmpresa , p.IdBanco}
                                 join s in Context.ba_Banco_Cuenta_x_tb_sucursal
                                 on new { p.IdEmpresa, p.IdBanco} equals new { s.IdEmpresa, s.IdBanco}
                                 where q.IdEmpresa == IdEmpresa && s.IdSucursal == IdSucursal
                                 select new ba_Talonario_cheques_x_banco_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdBanco = q.IdBanco,
                                     Num_cheque = q.Num_cheque,
                                     Usado = q.Usado,
                                     Estado = q.Estado,
                                     secuencia = q.secuencia,
                                     IdCbteCble_cbtecble_Usado = q.IdCbteCble_cbtecble_Usado,
                                     IdEmpresa_cbtecble_Usado = q.IdEmpresa_cbtecble_Usado,
                                     IdTipoCbte_cbtecble_Usado = q.IdTipoCbte_cbtecble_Usado,
                                     Fecha_uso= q.Fecha_uso,
                                     ba_descripcion = p.ba_descripcion,

                                     Estado_bool = q.Estado == "A" ? true : false


                                 }).ToList();
                    else
                        Lista = (from q in Context.ba_Talonario_cheques_x_banco
                                 join p in Context.ba_Banco_Cuenta
                                 on new { q.IdEmpresa, q.IdBanco } equals new { p.IdEmpresa, p.IdBanco }
                                 join s in Context.ba_Banco_Cuenta_x_tb_sucursal
                                 on new { p.IdEmpresa, p.IdBanco } equals new { s.IdEmpresa, s.IdBanco }
                                 where q.IdEmpresa == IdEmpresa && s.IdSucursal == IdSucursal
                                 && q.Estado == "A"
                                 select new ba_Talonario_cheques_x_banco_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdBanco = q.IdBanco,
                                     Num_cheque = q.Num_cheque,
                                     Usado = q.Usado,
                                     Estado = q.Estado,
                                     secuencia = q.secuencia,
                                     IdCbteCble_cbtecble_Usado = q.IdCbteCble_cbtecble_Usado,
                                     IdEmpresa_cbtecble_Usado = q.IdEmpresa_cbtecble_Usado,
                                     IdTipoCbte_cbtecble_Usado = q.IdTipoCbte_cbtecble_Usado,
                                     Fecha_uso = q.Fecha_uso,
                                     ba_descripcion = p.ba_descripcion,

                                     Estado_bool = q.Estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_Talonario_cheques_x_banco_Info get_info(int IdEmpresa, int IdBanco, string Num_cheque)
        {
            try
            {
                ba_Talonario_cheques_x_banco_Info info = new ba_Talonario_cheques_x_banco_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Talonario_cheques_x_banco Entity = Context.ba_Talonario_cheques_x_banco.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdBanco == IdBanco && q.Num_cheque == Num_cheque);
                    if (Entity == null) return null;
                    info = new ba_Talonario_cheques_x_banco_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdBanco = Entity.IdBanco,
                        IdCbteCble_cbtecble_Usado = Entity.IdCbteCble_cbtecble_Usado,
                        IdEmpresa_cbtecble_Usado = Entity.IdEmpresa_cbtecble_Usado,
                        IdTipoCbte_cbtecble_Usado = Entity.IdTipoCbte_cbtecble_Usado,
                        Estado_bool = Entity.Estado == "A",
                        Num_cheque = Entity.Num_cheque,
                        secuencia = Entity.secuencia,
                        Usado = Entity.Usado,
                        Fecha_uso = Entity.Fecha_uso
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string get_id(int IdEmpresa, int IdBanco, int NumDigitos)
        {
            try
            {
                decimal ID = 1;
                string relleno = string.Empty;
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.vwba_Talonario_cheques_x_banco_ID
                              where q.IdEmpresa == IdEmpresa
                              && q.IdBanco == IdBanco
                              select q;
                    if (lst.Count() > 0)
                        ID = Convert.ToDecimal(lst.Max(q => q.Num_cheque) + 1);

                    for (int i = 0; i < NumDigitos; i++)
                    {
                        relleno += "0";
                    }
                }
                return ID.ToString(relleno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_secuencia(int IdEmpresa, int IdBanco)
        {
            try
            {
                decimal secuencia = 1;
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Talonario_cheques_x_banco
                              where q.IdEmpresa == IdEmpresa
                              && q.IdBanco == IdBanco
                              && q.secuencia != null
                              select q;

                    if (lst.Count() > 0)
                        secuencia = (decimal)lst.Max(q => q.secuencia) + 1;
                }
                return secuencia;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(ba_Talonario_cheques_x_banco_Info info)
        {
            try
            {
                decimal secuencia = get_secuencia(info.IdEmpresa, info.IdBanco);
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Talonario_cheques_x_banco Entity = new ba_Talonario_cheques_x_banco
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdBanco = info.IdBanco,
                        Num_cheque = info.Num_cheque,
                        Usado = info.Usado,
                        secuencia = info.secuencia = secuencia++,
                        Fecha_uso = info.Fecha_uso,
                        Estado = info.Estado ="A"
                    };
                    Context.ba_Talonario_cheques_x_banco.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ba_Talonario_cheques_x_banco_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Talonario_cheques_x_banco Entity = Context.ba_Talonario_cheques_x_banco.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco && q.Num_cheque == info.Num_cheque);
                    if (Entity == null) return false;

                    Entity.Usado = info.Usado;
                    Entity.Estado = info.Estado_bool == true ? "A" : "I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ba_Talonario_cheques_x_banco_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Talonario_cheques_x_banco Entity = Context.ba_Talonario_cheques_x_banco.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdBanco == info.IdBanco && q.Num_cheque == info.Num_cheque);
                    if (Entity == null) return false;
                    
                    Entity.Estado = info.Estado ="I";

                    Entity.IdUsuario_Anu = info.IdUsuario_Anu;
                    Entity.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_Numcheque( string Num_cheque)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Talonario_cheques_x_banco
                              where q.Num_cheque == Num_cheque
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

        public string get_ult_NumCheque_no_usado(int IdEmpresa, int IdBanco)
        {
            try
            {
                string NumCheque = string.Empty;
                decimal secuencia = 0;
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Talonario_cheques_x_banco
                              where q.IdEmpresa == IdEmpresa
                              && q.IdBanco == IdBanco
                              && q.Usado == false
                              && q.Estado == "A"
                              select q;

                    if (lst.Count() > 0)
                    {
                        secuencia = (decimal)lst.Min(q => q.secuencia);
                        NumCheque = lst.Where(q => q.secuencia == secuencia).FirstOrDefault().Num_cheque;
                    }         
                }

                return NumCheque;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCantidadTalonariosDisponibles(int IdEmpresa, int IdBanco)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var list = Context.ba_Talonario_cheques_x_banco.Where(q => q.IdEmpresa == IdEmpresa && q.IdBanco == IdBanco && q.Usado == false).ToList();


                    return list.Count();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
