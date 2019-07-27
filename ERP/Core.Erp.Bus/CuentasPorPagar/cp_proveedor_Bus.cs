using Core.Erp.Bus.General;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Data.General;
using Core.Erp.Info.CuentasPorPagar;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_proveedor_Bus
    {
        cp_proveedor_Data odata = new cp_proveedor_Data();
        tb_persona_Data odata_per = new tb_persona_Data();

        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public List<cp_proveedor_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public cp_proveedor_Info get_info(int IdEmpresa, decimal IdProveedor)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdProveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cp_proveedor_Info info)
        {
            try
            {
                bool si_grabo = false;
                decimal IdPersona = bus_persona.validar_existe_cedula(info.info_persona.pe_cedulaRuc);

                if (IdPersona == 0)
                {
                    info.info_persona.pe_telfono_Contacto = info.pr_telefonos;
                    info.info_persona.pe_direccion = info.pr_direccion;
                    info.info_persona.pe_correo = info.pr_correo;
                    info.info_persona.pe_celular = info.pr_celular;

                    info.info_persona = odata_per.armar_info(info.info_persona);

                    si_grabo = bus_persona.guardarDB(info.info_persona);
                }
                else
                {
                    info.info_persona.IdPersona = IdPersona;
                    si_grabo = bus_persona.modificarDB(info.info_persona);
                }

                if (si_grabo)
                {
                    info.IdPersona = info.info_persona.IdPersona;
                    si_grabo = odata.guardarDB(info);
                }

                return si_grabo;
            
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB_importacion(cp_proveedor_Info info)
        {
            try
            {
                if (info.IdPersona == 0)
                {
                    info.info_persona = odata_per.armar_info(info.info_persona);
                    info.info_persona.pe_nombreCompleto = (info.info_persona.pe_nombreCompleto == "" ? info.info_persona.pe_razonSocial : info.info_persona.pe_nombreCompleto);
                    if (odata_per.guardarDB(info.info_persona))
                    {
                        info.IdPersona = info.info_persona.IdPersona;
                        return odata.guardarDB(info);
                    }
                }
                else
                {
                    if (odata_per.modificarDB(info.info_persona))
                    {
                        return odata.guardarDB(info);
                    }
                    return odata.guardarDB(info);
                }
                    
                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(cp_proveedor_Info info)
        {
            try
            {
                bool si_grabo = false;
                var infoPersona = bus_persona.get_info(info.IdPersona);

                if (infoPersona.IdPersona != 0)
                {
                    info.info_persona.pe_nombreCompleto = (info.info_persona.pe_nombreCompleto == "" ? info.info_persona.pe_razonSocial : info.info_persona.pe_nombreCompleto);
                    si_grabo = odata_per.modificarDB(info.info_persona);

                    if (si_grabo)
                    {
                        si_grabo = odata.modificarDB(info);
                    }
                }
                return si_grabo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_proveedor_Info info)
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
        public cp_proveedor_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                return odata.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<cp_proveedor_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa);
        }

        public cp_proveedor_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            return odata.get_info_bajo_demanda(args, IdEmpresa);
        }


    }
}
