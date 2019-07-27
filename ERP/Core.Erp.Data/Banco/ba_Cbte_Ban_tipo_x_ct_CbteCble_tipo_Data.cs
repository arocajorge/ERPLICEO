using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Data
    {
        public List<ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Info> GetList(int IdEmpresa)
        {
            try
            {
                List<ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Info> Lista;
                using (Entities_banco Contex = new Entities_banco())
                {
                    Lista = Contex.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.Where(q=>q.IdEmpresa == IdEmpresa)
                        .Select(q => new ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Info
                    {
                        CodTipoCbteBan = q.CodTipoCbteBan,
                        IdCtaCble = q.IdCtaCble,
                        IdEmpresa = q.IdEmpresa,
                        IdTipoCbteCble = q.IdTipoCbteCble,
                        IdTipoCbteCble_Anu = q.IdTipoCbteCble_Anu,
                        Tipo_DebCred = q.Tipo_DebCred
                    }).ToList();
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
