using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_Archivo_Transferencia_Det_Bus
    {
        ba_Archivo_Transferencia_Det_Data odata = new ba_Archivo_Transferencia_Det_Data();
        public List<ba_Archivo_Transferencia_Det_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_Archivo_Transferencia_Det_Info> get_list_con_saldo(int IdEmpresa, decimal IdPersona, string IdTipo_Persona, decimal IdEntidad, string IdEstado_Aprobacion, string IdUsuario, int IdSucursal, bool mostrar_saldo_0)

        {
            try
            {
                return odata.get_list_con_saldo(IdEmpresa, IdPersona, IdTipo_Persona, IdEntidad, IdEstado_Aprobacion, IdUsuario, IdSucursal, mostrar_saldo_0);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal GetIdSecuencial(int IdEmpresa, int IdBanco, int IdProceso_bancario)
        {
            try
            {
                return odata.GetIdSecuencial(IdEmpresa, IdBanco, IdProceso_bancario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
