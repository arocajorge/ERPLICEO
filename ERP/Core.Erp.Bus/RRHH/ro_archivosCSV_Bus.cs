using Core.Erp.Data.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH.MTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_archivosCSV_Bus
    {
        ro_archivosCSV_Data odata = new ro_archivosCSV_Data();
        public List<ro_archivosCSV_Info> get_lis(int idEmpresa, int IdRol, int IdRubro)
        {
            try
            {
                return odata.get_lis(idEmpresa, IdRol, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string Get_decimoIII(List<ro_archivosCSV_Info> listado)
        {
            try
            {
                string archivo = "";

                foreach (var item in listado)
                {
                    item.pe_apellido = item.pe_apellido.Replace(".", " ").Replace("ñ", "n").Replace("Ñ", "N");
                    item.pe_nombre = item.pe_nombre.Replace(".", " ").Replace("ñ", "n").Replace("Ñ", "N");
                    archivo += item.pe_cedulaRuc + ";";
                    archivo += item.pe_nombre + ";";
                    archivo += item.pe_apellido + ";";
                    if (item.pe_sexo == cl_enumeradores.eTipoSexoGeneral.SEXO_FEM.ToString())
                        archivo += "F" + ";";
                    else
                        archivo += "M" + ";";
                    archivo += item.CodigoSectorial + ";";
                    archivo += item.DiasA_considerar_Decimo + ";";
                    archivo += "A" + ";";//Tipo de Deposito
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";

                    archivo += "\n";
                }
                return archivo;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public string Get_decimoIV(List<ro_archivosCSV_Info> listado)
        {
            try
            {
                string archivo = "";

                foreach (var item in listado)
                {
                    item.pe_nombre = item.pe_nombre.Replace(".", " ").Replace("ñ", "n").Replace("Ñ", "N");
                    item.pe_apellido = item.pe_apellido.Replace(".", " ").Replace("ñ", "n").Replace("Ñ", "N");
                    archivo += item.pe_cedulaRuc + ";";
                    archivo += item.pe_apellido + ";";
                    archivo += item.pe_nombre + ";";
                    if (item.pe_sexo == cl_enumeradores.eTipoSexoGeneral.SEXO_FEM.ToString())
                        archivo += "F" + ";";
                    else
                        archivo += "M" + ";";
                    archivo += item.CodigoSectorial + ";";
                    archivo += item.DiasA_considerar_Decimo + ";";
                    archivo += "A" + ";";//Tipo de Deposito
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";
                    archivo += ";";

                    archivo += "\n";
                }
                return archivo;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    }
