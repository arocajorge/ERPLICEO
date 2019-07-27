using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Activo_fijo_Categoria_Bus
    {
        Af_Activo_fijo_Categoria_Data odata = new Af_Activo_fijo_Categoria_Data();
    
        public List<Af_Activo_fijo_Categoria_Info> get_list(int IdEmpresa,int IdActivoFijoTipo, bool mostrar_Anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdActivoFijoTipo, mostrar_Anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Af_Activo_fijo_Categoria_Info get_info(int IdEmpresa, int IdCategoriaAF)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCategoriaAF);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Activo_fijo_Categoria_Info info)
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
        public bool modificarDB(Af_Activo_fijo_Categoria_Info info)
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

        public bool anularDB(Af_Activo_fijo_Categoria_Info info)
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
