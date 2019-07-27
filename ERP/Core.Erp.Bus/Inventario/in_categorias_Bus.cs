using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_categorias_Bus
    {
        in_categorias_Data odata = new in_categorias_Data();
        public List<in_categorias_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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
        public in_categorias_Info get_info(int IdEmpresa, string IdCategoria)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCategoria);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_IdCategoria(int IdEmpresa, string IdCategoria)
        {
            try
            {
                return odata.validar_existe_IdCategoria(IdEmpresa, IdCategoria);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_categorias_Info info)
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
        public bool modificarDB(in_categorias_Info info)
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
        public bool anularDB(in_categorias_Info info)
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
