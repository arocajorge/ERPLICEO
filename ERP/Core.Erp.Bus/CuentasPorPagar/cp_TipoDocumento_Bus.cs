using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_TipoDocumento_Bus
    {
        cp_TipoDocumento_Data odata = new cp_TipoDocumento_Data();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus(); 
        public Boolean GuardarDB(ref cp_TipoDocumento_Info Info)
        {
            try
            {
                return odata.guardarDB(Info);
            }
            catch (Exception )
            {
                throw;

            }
        }

        public cp_TipoDocumento_Info GetInfo(string CodDocumentoTipo)
        {
            try
            {
                return odata.GetInfo(CodDocumentoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_TipoDocumento_Info> get_list(int IdEmpresa, decimal IdProveddor, string codigoSRI)
        {
            try
            {
                /*if (codigoSRI.Length > 2)
                    return new List<cp_TipoDocumento_Info>();*/
                odata = new cp_TipoDocumento_Data();
                cp_codigo_SRI_Data data_codigo = new cp_codigo_SRI_Data();
                var lst_data = data_codigo.get_info(Convert.ToInt32(codigoSRI));
                cp_proveedor_Info info_proveedor = new cp_proveedor_Info();
                if(lst_data!=null)
                codigoSRI = lst_data.codigoSRI;
                info_proveedor = bus_proveedor.get_info(IdEmpresa, IdProveddor);
                List<cp_TipoDocumento_Info> lista = new List<cp_TipoDocumento_Info>();
                List<cp_TipoDocumento_Info> lista_retorna = new List<cp_TipoDocumento_Info>();

                lista = odata.get_list(false);

                foreach (var item in lista)
                {
                    if (item.CodSRI == "03")
                    {
                        item.CodSRI = item.CodSRI;
                    }

                    if (item.Sustento_Tributario != null)
                    {
                        string[] arreglo = item.Sustento_Tributario.Split(',');

                        for (int i = 0; i < arreglo.Length; i++)
                        {
                            arreglo[i] = arreglo[i].Trim();

                            if (arreglo[i] == codigoSRI)
                            {


                                string secuencial = "";
                                if (info_proveedor.info_persona.IdTipoDocumento.Trim() == "RUC")
                                    secuencial = "01";
                                else if (info_proveedor.info_persona. IdTipoDocumento.Trim() == "CED")
                                    secuencial = "02";
                                else
                                    secuencial = "03";

                                string[] arregloSecuenci = item.Codigo_Secuenciales_Transaccion.Split(',');
                                for (int ise = 0; ise < arregloSecuenci.Length; ise++)
                                {
                                    arregloSecuenci[ise] = arregloSecuenci[ise].Trim();
                                    if (arregloSecuenci[ise] == secuencial)
                                    {
                                        lista_retorna.Add(item);
                                        ise = arregloSecuenci.Length;
                                        i = arreglo.Length;
                                    }
                                }

                            }
                        }
                    }

                }
                return lista_retorna;
            }
            catch (Exception )
            {
                throw;

            }
        }

        public List<cp_TipoDocumento_Info> get_list(string CodDocumento)
        {
            try
            {
                return odata.get_list(CodDocumento);
            }
            catch (Exception )
            {
                throw;
            }
        }
        public List<cp_TipoDocumento_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean ModificarDB(cp_TipoDocumento_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception )
            {
                throw;
            }
        }

        public Boolean AnularDB(cp_TipoDocumento_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception )
            {
                throw;
            }
        }

    }
}
