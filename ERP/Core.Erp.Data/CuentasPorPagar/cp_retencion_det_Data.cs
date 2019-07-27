using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_retencion_det_Data
    {

        public List<cp_retencion_det_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                List<cp_retencion_det_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_retencion_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdRetencion == IdRetencion
                             select new cp_retencion_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRetencion = q.IdRetencion,
                                 Idsecuencia = q.Idsecuencia,
                                 re_tipoRet = q.re_tipoRet,
                                 re_baseRetencion = q.re_baseRetencion,
                                 IdCodigo_SRI = q.IdCodigo_SRI,
                                 re_Codigo_impuesto = q.re_Codigo_impuesto,
                                 re_valor_retencion = q.re_valor_retencion,
                                  re_Porcen_retencion=q.re_Porcen_retencion,
                                  IdCtacble = q.IdCtaCble
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    string comando = "delete cp_retencion_det where IdEmpresa = " + IdEmpresa + " and IdRetencion = " + IdRetencion;
                    Context.Database.ExecuteSqlCommand(comando);
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_retencion_Info info)
        {
            try
            {
                int sec = 1;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    foreach (var item in info.detalle)
                    {
                        cp_retencion_det Entity = new cp_retencion_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdRetencion = info.IdRetencion,
                            Idsecuencia = sec,
                            re_tipoRet = item.re_tipoRet,
                            re_baseRetencion = (double)item.re_baseRetencion,
                            IdCodigo_SRI = item.IdCodigo_SRI,
                            re_Codigo_impuesto = item.re_Codigo_impuesto,
                            re_valor_retencion = Math.Round((double)item.re_valor_retencion,2,MidpointRounding.AwayFromZero),
                            re_Porcen_retencion =(double) item.re_Porcen_retencion,
                            re_estado="A"
                        };
                        Context.cp_retencion_det.Add(Entity);
                        sec++;
                    }
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception )
            {
                throw;
            }
        }

    }
}
