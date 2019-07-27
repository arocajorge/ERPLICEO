using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
   public class ba_Talonario_cheques_x_banco_Bus
    {
        ba_Talonario_cheques_x_banco_Data odata = new ba_Talonario_cheques_x_banco_Data();

        public List<ba_Talonario_cheques_x_banco_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string get_ult_NumCheque_no_usado(int IdEmpresa, int IdBanco)
        {
            try
            {
                return odata.get_ult_NumCheque_no_usado(IdEmpresa, IdBanco);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ba_Talonario_cheques_x_banco_Info get_info(int IdEmpresa, int IdBanco, string Num_cheque)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdBanco, Num_cheque);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_Talonario_cheques_x_banco_Info info)
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

        public bool modificarDB(ba_Talonario_cheques_x_banco_Info info)
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

        public bool anularDB(ba_Talonario_cheques_x_banco_Info info)
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

        public bool validar_existe_Numcheque( string Num_cheque)
        {
            try
            {
                return odata.validar_existe_Numcheque( Num_cheque);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string get_id(int IdEmpresa, int IdBanco, int NumDigitos)
        {
            try
            {
                return odata.get_id(IdEmpresa, IdBanco, NumDigitos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidadTalonariosDisponibles(int IdEmpresa, int IdBanco)

        {
            try
            {
                return odata.GetCantidadTalonariosDisponibles(IdEmpresa, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
