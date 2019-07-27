using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_009_Data
    {
        public List<BAN_009_Info> GetList(int IdEmpresa, int IdBanco, DateTime fecha_fin, bool AgruparPorFlujo)
        {
            try
            {
                List<BAN_009_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPBAN_009(IdEmpresa, IdBanco, fecha_fin).Select(q => new BAN_009_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdBanco = q.IdBanco,
                        IdTipoFlujo = q.IdTipoFlujo,
                        ba_descripcion = q.ba_descripcion,
                        NomFlujo =  q.NomFlujo,
                        ValorFlujo = q.ValorFlujo,
                        Descripcion = AgruparPorFlujo ? q.NomFlujo : q.ba_descripcion
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
