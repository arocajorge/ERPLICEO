using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Banco_Cuenta_x_tb_sucursal_Data
    {
        public List<ba_Banco_Cuenta_x_tb_sucursal_Info> GetList(int IdEmpresa, int IdBanco)
        {
            try
            {
                List<ba_Banco_Cuenta_x_tb_sucursal_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = Context.ba_Banco_Cuenta_x_tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdBanco == IdBanco
                    ).Select(q => new ba_Banco_Cuenta_x_tb_sucursal_Info
                    {
                        IdBanco = q.IdBanco,
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        Secuencia=q.Secuencia
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_Banco_Cuenta_x_tb_sucursal_Info> GetListSuc(int IdEmpresa, int IdSucursal)
        {
            try
            {
                List<ba_Banco_Cuenta_x_tb_sucursal_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = Context.ba_Banco_Cuenta_x_tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdSucursal == IdSucursal
                    ).Select(q => new ba_Banco_Cuenta_x_tb_sucursal_Info
                    {
                        IdBanco = q.IdBanco,
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        Secuencia = q.Secuencia
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
