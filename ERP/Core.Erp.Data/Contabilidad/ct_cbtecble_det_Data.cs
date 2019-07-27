using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_cbtecble_det_Data
    {
        public List<ct_cbtecble_det_Info> get_list(int IdEmpresa,int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                List<ct_cbtecble_det_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    Lista = (from q in Context.vwct_cbtecble_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipoCbte == IdTipoCbte
                             && q.IdCbteCble == IdCbteCble
                             select new ct_cbtecble_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 dc_Observacion = q.dc_Observacion,
                                 dc_Valor = q.dc_Valor,
                                 IdCbteCble = q.IdCbteCble,
                                 IdCtaCble = q.IdCtaCble,                                 
                                 IdTipoCbte = q.IdTipoCbte,
                                 secuencia = q.secuencia,
                                 dc_para_conciliar_null = q.dc_para_conciliar,
                                 pc_Cuenta = q.pc_Cuenta,
                                 IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                                 IdPunto_cargo = q.IdPunto_cargo,
                                 IdCentroCosto = q.IdCentroCosto
                             }).ToList();
                }
                Lista.ForEach(q => { q.dc_Valor_debe = q.dc_Valor > 0 ? q.dc_Valor : 0; q.dc_Valor_haber = q.dc_Valor < 0 ? Math.Abs( q.dc_Valor) : 0; q.dc_para_conciliar = q.dc_para_conciliar_null == null ? false : Convert.ToBoolean(q.dc_para_conciliar_null); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ct_cbtecble_det_Info> get_list_para_cierre(int IdEmpresa, int IdSucursal, int Idaniofiscal)
        {
            try
            {
                List<ct_cbtecble_det_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    Lista = Context.SPCONTA_CierreAnual(IdEmpresa, IdSucursal, Idaniofiscal).Select(q => new ct_cbtecble_det_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        dc_Valor = Convert.ToDouble(q.dc_Valor),
                        IdCtaCble = q.IdCtaCble,
                        secuencia = Convert.ToInt32(q.Secuencia),
                        pc_Cuenta = q.pc_Cuenta,
                        dc_Valor_debe = Convert.ToDouble(q.dc_Valor_Debe),
                        dc_Valor_haber = Convert.ToDouble(q.dc_Valor_Haber),
                        IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,                        
                        IdPunto_cargo = q.IdPunto_cargo,
                        IdCentroCosto = q.IdCentroCosto
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

