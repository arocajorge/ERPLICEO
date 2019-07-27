using Core.Erp.Info.Migraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Migraciones
{
    public class ImportacionDiarios_Data
    {
        public List<ImportacionDiarios_Info> get_list(string tipo_documento)
        {
            try
            {
                var Secuencial = 0;
                List<ImportacionDiarios_Info> Lista;
                using (DBSACEntities Context = new DBSACEntities())
                {
                    Lista = (from q in Context.vw_diarios_contables_migracion                             
                             where q.tipo_documento == tipo_documento
                             orderby q.Glosa ascending
                             select new ImportacionDiarios_Info
                             {
                                 Empresa = q.Empresa,
                                 centro = q.centro,
                                 Numero = q.Numero,
                                 Secuencia = q.Secuencia,
                                 Fecha = q.Fecha,
                                 Valor = q.Valor,
                                 TipoMov = q.TipoMov,
                                 Glosa = q.Glosa,
                                 Detalle = q.Detalle,
                                 tipo_documento = q.tipo_documento,
                                 IdCtaCble = q.IdCtaCble,
                                 dc_ValorDebe = q.dc_ValorDebe,
                                 dc_ValorHaber = q.dc_ValorHaber
                             }).ToList();                    
                }

                foreach (var item in Lista)
                {
                    Secuencial++;
                    item.Secuencial = Secuencial;
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminar_x_tipo_doc(string tipo_documento)
        {
            try
            {
                using (DBSACEntities Context = new DBSACEntities())
                {
                    var sql = "";

                    switch (tipo_documento)
                    {
                        case "FACTURAS":
                            sql = "delete from conta_factura_exporta";
                            Context.Database.ExecuteSqlCommand(sql);
                            break;
                        case "COBROS":
                            sql = "delete from conta_cobranza_exporta";
                            Context.Database.ExecuteSqlCommand(sql);
                            break;
                        case "NOTADEBITO":
                            sql = "delete from conta_nd_exporta";
                            Context.Database.ExecuteSqlCommand(sql);
                            break;
                        case "NOTACREDITO":
                            sql = "delete from conta_nc_exporta";
                            Context.Database.ExecuteSqlCommand(sql);
                            break;
                    }                    
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
