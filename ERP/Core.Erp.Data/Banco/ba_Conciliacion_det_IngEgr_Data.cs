using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Conciliacion_det_IngEgr_Data
    {
        public List<ba_Conciliacion_det_IngEgr_Info> get_list(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                List<ba_Conciliacion_det_IngEgr_Info> Lista;

                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = (from q in Context.vwba_Conciliacion_det_IngEgr
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion == IdConciliacion
                             select new ba_Conciliacion_det_IngEgr_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion = q.IdConciliacion,
                                 
                                 tipo_IngEgr = q.dc_Valor > 0 ? "+" : "-",
                                 IngEgr = q.dc_Valor > 0 ? "INGRESOS" : "EGRESOS",
                                 IdCbteCble = q.IdCbteCble,
                                 IdTipocbte = q.IdTipoCbte,
                                 Secuencia = q.Secuencia,
                                 SecuenciaCbteCble = q.secuenciaCbte,
                                 IdBanco = q.IdBanco,
                                 IdCtaCble = q.IdCtaCble,
                                 dc_Valor = q.dc_Valor,
                                 ba_descripcion = q.ba_descripcion,
                                 cb_Observacion = q.cb_Observacion,
                                 cb_Cheque = q.cb_Cheque,
                                 tc_TipoCbte = q.tc_TipoCbte,
                                 cb_Fecha = q.cb_Fecha,
                                 seleccionado = true
                             }).ToList();
                }
                Lista.ForEach(q => q.IdPK = q.IdTipocbte.ToString("00") + q.IdCbteCble.ToString("0000000000") + q.SecuenciaCbteCble.ToString("000"));
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ba_Conciliacion_det_IngEgr_Info> get_list_x_conciliar(int IdEmpresa, int IdBanco, string IdCtaCble, DateTime F_fin)
        {
            try
            {
                List<ba_Conciliacion_det_IngEgr_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = (from T in Context.vwba_Conciliacion_det_IngEgr_x_conciliar
                             where T.IdEmpresa == IdEmpresa && T.IdCtaCble == IdCtaCble
                             && T.IdBanco == IdBanco
                             && T.cb_Fecha <= F_fin
                             select new ba_Conciliacion_det_IngEgr_Info
                             {
                                 IdEmpresa = T.IdEmpresa,
                                 tipo_IngEgr = T.dc_Valor > 0 ? "+" :  "-",
                                 IngEgr = T.dc_Valor > 0 ? "INGRESOS" : "EGRESOS",
                                 IdCbteCble = T.IdCbteCble,
                                 IdTipocbte = T.IdTipoCbte,
                                 SecuenciaCbteCble = T.secuencia,
                                 IdBanco = T.IdBanco,
                                 IdCtaCble = T.IdCtaCble,
                                 dc_Valor = T.dc_Valor,
                                 ba_descripcion = T.ba_descripcion,
                                 cb_Observacion = T.cb_Observacion,
                                 cb_Cheque = T.cb_Cheque,
                                 tc_TipoCbte = T.tc_TipoCbte,
                                 cb_Fecha = T.cb_Fecha
                             }).ToList();
                    Lista.ForEach(q => q.IdPK = q.IdTipocbte.ToString("00") + q.IdCbteCble.ToString("0000000000") + q.SecuenciaCbteCble.ToString("000"));
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
