using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_Marca_Bus
    {
        in_Marca_Data odata = new in_Marca_Data();
        public List<in_Marca_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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
        public in_Marca_Info get_info(int IdEmpresa, int IdMarca)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdMarca);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_Marca_Info info)
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
        public bool modificarDB(in_Marca_Info info)
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
        public bool anularDB(in_Marca_Info info)
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

        public bool si_esta_en_uso(int IdEmpresa, int IdMarca)
        {
            try
            {
                return odata.si_esta_en_uso(IdEmpresa,IdMarca);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
