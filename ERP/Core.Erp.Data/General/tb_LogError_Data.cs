using Core.Erp.Info.General;
using System;

namespace Core.Erp.Data.General
{
    public class tb_LogError_Data
    {
        public bool GuardarDB(tb_LogError_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    Context.tb_LogError.Add(new tb_LogError
                    {
                        Clase = info.Clase,
                        Descripcion = info.Descripcion,
                        Fecha = DateTime.Now,
                        IdUsuario = info.IdUsuario,
                        Metodo = info.Metodo,
                        InnerException = info.InnerException
                    });
                    Context.SaveChanges();
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
