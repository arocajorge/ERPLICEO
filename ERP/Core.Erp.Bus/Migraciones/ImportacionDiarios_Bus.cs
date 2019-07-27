using Core.Erp.Data.Migraciones;
using Core.Erp.Info.Migraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Migraciones
{
    public class ImportacionDiarios_Bus
    {
        ImportacionDiarios_Data odata = new ImportacionDiarios_Data();

        public List<ImportacionDiarios_Info> get_list(string tipo_documento)
        {
            try
            {
                return odata.get_list(tipo_documento);
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
                return odata.eliminar_x_tipo_doc(tipo_documento);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
