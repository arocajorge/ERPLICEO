using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_presentacion_Bus
    {
        in_presentacion_Data odata = new in_presentacion_Data();
        public List<in_presentacion_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public in_presentacion_Info get_info(int IdEmpresa, string IdPresentacion)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdPresentacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_IdPresentacion(string IdPresentacion)
        {
            try
            {
                return odata.validar_existe_IdPresentacion(IdPresentacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_presentacion_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(in_presentacion_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(in_presentacion_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
