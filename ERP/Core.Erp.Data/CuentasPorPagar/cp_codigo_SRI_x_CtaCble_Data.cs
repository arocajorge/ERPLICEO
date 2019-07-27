using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_codigo_SRI_x_CtaCble_Data
    {
        public cp_codigo_SRI_x_CtaCble_Info get_info(int idCodigo_SRI, int IdEmpresa)
        {
            try
            {
                cp_codigo_SRI_x_CtaCble_Info info = new cp_codigo_SRI_x_CtaCble_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI_x_CtaCble Entity = Context.cp_codigo_SRI_x_CtaCble.FirstOrDefault(q => q.idCodigo_SRI == idCodigo_SRI && q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new cp_codigo_SRI_x_CtaCble_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        idCodigo_SRI = Entity.idCodigo_SRI,
                        IdCtaCble = Entity.IdCtaCble
                        
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cp_codigo_SRI_x_CtaCble_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI_x_CtaCble Entity = new cp_codigo_SRI_x_CtaCble
                    {
                        idCodigo_SRI = info.idCodigo_SRI,
                        IdEmpresa = info.IdEmpresa,
                        IdCtaCble = info.IdCtaCble, fecha_UltMod = DateTime.Now                     
                    };
                    Context.cp_codigo_SRI_x_CtaCble.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool eliminarDB(int idCodigo_SRI, int IdEmpresa)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Context.Database.ExecuteSqlCommand("delete cp_codigo_SRI_x_CtaCble where idCodigo_SRI = "+idCodigo_SRI+"  and IdEmpresa =" +IdEmpresa);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_codigo_SRI_Info> get_list(int IdEmpresa)
        {
            try
            {

                List<cp_codigo_SRI_Info> lista = new List<cp_codigo_SRI_Info>();
                using (Entities_cuentas_por_pagar Contex=new Entities_cuentas_por_pagar())
                {
                    var select_ = from q in Contex.vwcp_codigo_SRI
                                  where q.IdEmpresa == IdEmpresa
                                  && q.IdTipoSRI== "COD_IDCREDITO"
                                  && q.co_estado=="A"
                                  orderby q.co_f_valides_hasta descending
                                  select q;

                    foreach (var item in select_)
                    {
                        cp_codigo_SRI_Info dat = new cp_codigo_SRI_Info();
                        dat.IdCodigo_SRI = item.IdCodigo_SRI;
                        dat.codigoSRI = item.codigoSRI;
                        dat.co_codigoBase = item.co_codigoBase;
                        dat.co_descripcion = item.co_descripcion;
                        dat.co_porRetencion = item.co_porRetencion;
                        dat.co_f_valides_desde = item.co_f_valides_desde;
                        dat.co_f_valides_hasta = item.co_f_valides_hasta;
                        dat.co_estado = item.co_estado;
                        dat.IdTipoSRI = item.IdTipoSRI;
                        if (item.codigoSRI == "01")
                            dat.co_descripcion = "[" + item.codigoSRI + "] - " + item.co_descripcion + " " + item.co_porRetencion.ToString().Replace("0", "") + "12%";
                        else
                            dat.co_descripcion = "[" + item.codigoSRI + "] - " + item.co_descripcion + " " + item.co_porRetencion + "%";



                        lista.Add(dat);
                    }
                }
              
                return (lista);
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
