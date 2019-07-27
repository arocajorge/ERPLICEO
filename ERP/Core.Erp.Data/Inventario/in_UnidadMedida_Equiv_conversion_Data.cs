using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_UnidadMedida_Equiv_conversion_Data
    {
        public List<in_UnidadMedida_Equiv_conversion_Info> get_list(string IdUnidadMedida)
        {
            try
            {
                List<in_UnidadMedida_Equiv_conversion_Info> Lista;
                int secuencia = 0;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.in_UnidadMedida_Equiv_conversion
                             where q.IdUnidadMedida == IdUnidadMedida
                             select new in_UnidadMedida_Equiv_conversion_Info
                             {
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 IdUnidadMedida_equiva = q.IdUnidadMedida_equiva,
                                 valor_equiv = q.valor_equiv,
                                 interpretacion = q.interpretacion,
                             }).ToList();
                    Lista.ForEach(q => q.secuencia = secuencia++);
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_UnidadMedida_Equiv_conversion_Info> get_list_combo (string IdUnidadMedida)
        {
            try
            {
                List<in_UnidadMedida_Equiv_conversion_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_UnidadMedida_Equiv_conversion
                             where q.IdUnidadMedida == IdUnidadMedida
                             select new in_UnidadMedida_Equiv_conversion_Info
                             {
                                 IdUnidadMedida_equiva = q.IdUnidadMedida_equiva,
                                 Descripcion = q.Descripcion
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<in_UnidadMedida_Equiv_conversion_Info> Lista)
        {
            try
            {
                foreach (var item in Lista)
                {
                    using (Entities_inventario Context = new Entities_inventario())
                    {

                        var lst = from q in Context.in_UnidadMedida_Equiv_conversion
                                  where q.IdUnidadMedida == item.IdUnidadMedida
                                  && q.IdUnidadMedida_equiva == item.IdUnidadMedida_equiva
                                  select q;
                        if (lst.Count() == 0)
                        {
                            in_UnidadMedida_Equiv_conversion Entity = new in_UnidadMedida_Equiv_conversion
                            {
                                IdUnidadMedida = item.IdUnidadMedida,
                                IdUnidadMedida_equiva = item.IdUnidadMedida_equiva,
                                valor_equiv = item.valor_equiv,
                                interpretacion = item.interpretacion
                            };
                            Context.in_UnidadMedida_Equiv_conversion.Add(Entity);
                            Context.SaveChanges();
                        }

                        if(Context.in_UnidadMedida_Equiv_conversion.Where(q=> q.IdUnidadMedida == item.IdUnidadMedida && q.IdUnidadMedida_equiva == item.IdUnidadMedida).Count() == 0)
                        {
                            in_UnidadMedida_Equiv_conversion Entity = new in_UnidadMedida_Equiv_conversion
                            {
                                IdUnidadMedida = item.IdUnidadMedida,
                                IdUnidadMedida_equiva = item.IdUnidadMedida,
                                valor_equiv = 1,
                                interpretacion = item.interpretacion
                            };
                            Context.in_UnidadMedida_Equiv_conversion.Add(Entity);
                        }
                    }

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(string IdUnidadMedida)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Context.Database.ExecuteSqlCommand("DELETE in_UnidadMedida_Equiv_conversion WHERE IdUnidadMedida ='"+IdUnidadMedida+"'");
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
