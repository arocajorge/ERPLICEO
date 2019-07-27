using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Migraciones;
namespace Core.Erp.Data.Migraciones
{
   public class FacturasEventos_Data
    {

        public List<FacturasEventos_Info>get_lis(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                DateTime Fi = FechaInicio.Date;
                DateTime ff = FechaFin.Date;
                List<FacturasEventos_Info> lista;
                using (Entity_Eventos context = new Entity_Eventos())

                {

                    lista = (from q in context.vwFacturas_sin_aprobar
                             where q.fecha>=Fi
                             && q.fecha<=ff
                             && q.estado_aprobacion==null
                             && q.bd_est==1
                             select new FacturasEventos_Info
                             {
                                 cod_fact=q.cod_fact,
                                 cod_evento=q.cod_evento,
                                 nu_ced_ruc=q.nu_ced_ruc,
                                 nombres=q.nombres,
                                 apellidos=q.apellidos,
                                 cant=q.cant,
                                 v_unit=q.v_unit,
                                 subtotal=q.subtotal,
                                 v_iva=q.v_iva,
                                 total=q.total,
                                 fecha=q.fecha,
                                 
                                 
                             }
                           ).ToList();
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarEstado_aprobacion(FacturasEventos_Info info)
        {
            try
            {
                using (Entity_Eventos context = new Entity_Eventos())
                {

                    var entity = context.Facturas.Where(v => v.cod_evento == info.cod_evento && v.cod_fact == info.cod_fact).FirstOrDefault();
                    if(entity!=null)
                    {
                        entity.estado_aprobacion = "APRO";
                        context.SaveChanges();
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
