using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_UnidadMedida_Equiv_conversion_Bus
    {
        in_UnidadMedida_Equiv_conversion_Data odata = new in_UnidadMedida_Equiv_conversion_Data();
        public List<in_UnidadMedida_Equiv_conversion_Info> get_list(string IdUnidadMedida)
        {
            try
            {
                return odata.get_list(IdUnidadMedida);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_UnidadMedida_Equiv_conversion_Info> get_list_combo(string IdUnidadMedida)
        {
            try
            {
                return odata.get_list_combo(IdUnidadMedida);
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
                return odata.guardarDB(Lista);
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
                return odata.eliminarDB(IdUnidadMedida);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
