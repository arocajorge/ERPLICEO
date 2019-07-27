using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_TipoDocumento_Data
    {
        public Boolean guardarDB(cp_TipoDocumento_Info Info)
        {
            try
            {
                List<cp_TipoDocumento_Info> Lst = new List<cp_TipoDocumento_Info>();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var Address = new cp_TipoDocumento();
                    Address.CodTipoDocumento = Info.CodTipoDocumento;
                    Address.Codigo = Info.CodTipoDocumento;
                    Address.Descripcion = Info.Descripcion;
                    Address.Orden = Info.Orden;
                    Address.DeclaraSRI = Info.DeclaraSRI;
                    Address.GeneraRetencion = Info.GeneraRetencion;
                    Address.CodSRI = Info.CodSRI;
                    Address.Estado = "A";
                    Address.IdUsuario = Info.IdUsuario;
                    Address.Fecha_Transac = Info.Fecha_Transac;
                    Address.nom_pc = Info.nom_pc;
                    Address.ip = Info.ip;
                    Address.Codigo_Secuenciales_Transaccion = Info.Codigo_Secuenciales_Transaccion;
                    Address.Sustento_Tributario = Info.Sustento_Tributario;
                    Context.cp_TipoDocumento.Add(Address);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public List<cp_TipoDocumento_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<cp_TipoDocumento_Info> Lst = new List<cp_TipoDocumento_Info>();
                Entities_cuentas_por_pagar oEnti = new Entities_cuentas_por_pagar();
                if(mostrar_anulados)
                Lst = (from q in oEnti.cp_TipoDocumento
                       orderby q.Orden ascending
                       select new cp_TipoDocumento_Info
                       {
                           CodTipoDocumento = q.CodTipoDocumento,
                           Codigo = q.Codigo,
                           Descripcion = q.Descripcion,
                           Orden = q.Orden,
                           DeclaraSRI = q.DeclaraSRI,
                           CodSRI = q.CodSRI,
                           Estado = q.Estado,
                           GeneraRetencion = q.GeneraRetencion,
                           Codigo_Secuenciales_Transaccion = q.Codigo_Secuenciales_Transaccion,
                           Sustento_Tributario = q.Sustento_Tributario,

                           EstadoBool = q.Estado == "A" ? true : false
                       }).ToList();
                else
                    Lst = (from q in oEnti.cp_TipoDocumento
                           orderby q.Orden ascending
                           where q.Estado == "A"
                           select new cp_TipoDocumento_Info
                           {
                               CodTipoDocumento = q.CodTipoDocumento,
                               Codigo = q.Codigo,
                               Descripcion = q.Descripcion,
                               Orden = q.Orden,
                               DeclaraSRI = q.DeclaraSRI,
                               CodSRI = q.CodSRI,
                               Estado = q.Estado,
                               GeneraRetencion = q.GeneraRetencion,
                               Codigo_Secuenciales_Transaccion = q.Codigo_Secuenciales_Transaccion,
                               Sustento_Tributario = q.Sustento_Tributario,

                               EstadoBool = q.Estado == "A" ? true : false
                           }).ToList();

                return Lst;
            }
            catch (Exception )
            {
                throw;

            }
        }

        public List<cp_TipoDocumento_Info> get_list(string CodDocumento)
        {
            try
            {
                List<cp_TipoDocumento_Info> Lst = new List<cp_TipoDocumento_Info>();
                Entities_cuentas_por_pagar oEnti = new Entities_cuentas_por_pagar();

                var Query = from q in oEnti.cp_TipoDocumento
                            where q.CodTipoDocumento == CodDocumento
                            select q;

                foreach (var item in Query)
                {
                    cp_TipoDocumento_Info Obj = new cp_TipoDocumento_Info();
                    Obj.CodTipoDocumento = item.CodTipoDocumento;
                    Obj.Descripcion = item.Descripcion;
                    Obj.Orden = item.Orden;
                    Obj.DeclaraSRI = item.DeclaraSRI;
                    Obj.CodSRI = item.CodSRI;
                    Obj.Estado = item.Estado;
                    Obj.GeneraRetencion = item.GeneraRetencion;
                    Obj.Codigo_Secuenciales_Transaccion = item.Codigo_Secuenciales_Transaccion;
                    Obj.Sustento_Tributario = item.Sustento_Tributario;
                    Obj.GeneraRetencion = item.GeneraRetencion;
                    Lst.Add(Obj);

                }
                return Lst;
            }
            catch (Exception )
            {
                throw;


            }
        }

        public Boolean modificarDB(cp_TipoDocumento_Info info)
        {
            try
            {
                Boolean res = false;
                using (Entities_cuentas_por_pagar context = new Entities_cuentas_por_pagar())
                {
                    var contact = context.cp_TipoDocumento.FirstOrDefault(minfo => minfo.CodTipoDocumento == info.CodTipoDocumento);
                    if (contact != null)
                    {
                        contact.CodTipoDocumento = info.CodTipoDocumento;
                        contact.Descripcion = info.Descripcion;
                        contact.Orden = info.Orden;
                        contact.DeclaraSRI = info.DeclaraSRI;
                        contact.Estado = info.Estado;
                        contact.CodSRI = info.CodSRI;
                        contact.IdUsuarioUltMod = info.IdUsuarioUltMod;
                        contact.Fecha_UltMod = info.Fecha_UltMod;
                        contact.nom_pc = info.nom_pc;
                        contact.ip = info.ip;
                        contact.GeneraRetencion = info.GeneraRetencion;
                        contact.Codigo_Secuenciales_Transaccion = info.Codigo_Secuenciales_Transaccion;
                        contact.Sustento_Tributario = info.Sustento_Tributario;
                        contact.GeneraRetencion = info.GeneraRetencion;
                        context.SaveChanges();
                        res = true;
                    }
                }
                return res;
            }
            catch (Exception )
            {
                throw;

            }
        }

        public Boolean anularDB(cp_TipoDocumento_Info info)
        {
            try
            {
                Boolean res = false;
                using (Entities_cuentas_por_pagar context = new Entities_cuentas_por_pagar())
                {
                    var contact = context.cp_TipoDocumento.FirstOrDefault(minfo => minfo.CodTipoDocumento == info.CodTipoDocumento);
                    if (contact != null)
                    {
                        contact.Estado = "I";
                        contact.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        contact.Fecha_UltAnu = info.Fecha_UltAnu;
                        contact.nom_pc = info.nom_pc;
                        contact.ip = info.ip;
                        context.SaveChanges();
                        res = true;
                    }
                }
                return res;
            }
            catch (Exception )
            {
                throw;

            }
        }
    }
}
