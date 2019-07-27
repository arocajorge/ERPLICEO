using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Activo_fijo_Bus
    {

     
        public List<Af_Activo_fijo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa);
        }

        public Af_Activo_fijo_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            return odata.get_info_bajo_demanda(IdEmpresa, args);
        }

        Af_Activo_fijo_Data odata = new Af_Activo_fijo_Data();
    
        public List<Af_Activo_fijo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public Af_Activo_fijo_Info get_info(int IdEmpresa, int IdActivoFijo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdActivoFijo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Activo_fijo_Info info)
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

        public bool modificarDB(Af_Activo_fijo_Info info)
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

        public bool anularDB(Af_Activo_fijo_Info info)
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

        public Af_Activo_fijo_valores_Info get_valores(int IdEmpresa, int IdActivoFijo)
        {
            try
            {
                return odata.get_valores(IdEmpresa, IdActivoFijo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int get_id(int IdEmpresa)
        {
            try
            {
                return odata.get_id(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB_importacion(List<Af_Activo_fijo_tipo_Info> Lista_Tipo, List<Af_Activo_fijo_Categoria_Info> Lista_Categoria, List<Af_Departamento_Info> Lista_Departamento, List<Af_Catalogo_Info> Lista_Catalogo, List<Af_Activo_fijo_Info> Lista_ActivoFijo)
        {
            try
            {
                return odata.guardarDB_importacion(Lista_Tipo, Lista_Categoria, Lista_Departamento, Lista_Catalogo, Lista_ActivoFijo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
