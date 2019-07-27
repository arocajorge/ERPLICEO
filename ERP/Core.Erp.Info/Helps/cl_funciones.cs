using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace Core.Erp.Info.Helps
{
    public class cl_funciones
    {
        public static string convertir_string_MD5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // NUMERO A LETRA
        public string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                if (decimales < 10)
                    dec = " CON 0" + decimales.ToString() + "/100 ****";
                else
                    dec = " CON " + decimales.ToString() + "/100 ****";
            }
            else
                dec = " CON 00/100 ****";

            res = NumeroALetras(Convert.ToDecimal(entero)) + dec;
            return res;
        }

        static string NumeroALetras(decimal value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(Convert.ToDecimal((value - 10)));

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(Convert.ToDecimal((value - 20)));

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Convert.ToDecimal((Math.Truncate(value / 10) * 10))) + " Y " + NumeroALetras(Convert.ToDecimal((value % 10)));

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(Convert.ToDecimal((value - 100)));

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Convert.ToDecimal((value / 100))) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = NumeroALetras(Convert.ToDecimal((Math.Truncate(value / 100) * 100))) + " " + NumeroALetras(Convert.ToDecimal((value % 100)));

            else if (value == 1000) Num2Text = "UN MIL";

            else if (value < 2000) Num2Text = "UN MIL " + NumeroALetras(Convert.ToDecimal((value % 1000)));

            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Convert.ToDecimal((Math.Truncate(value / 1000)))) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(Convert.ToDecimal((value % 1000)));
            }



            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(Convert.ToDecimal((value % 1000000)));

            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Convert.ToDecimal((value / 1000000))) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(Convert.ToDecimal((value - Math.Truncate(value / 1000000) * 1000000)));
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(Convert.ToDecimal((value - Math.Truncate(value / 1000000000000) * 1000000000000)));

            else
            {
                Num2Text = NumeroALetras(Convert.ToDecimal((Math.Truncate(value / 1000000000000)))) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(Convert.ToDecimal((value - Math.Truncate(value / 1000000000000) * 1000000000000)));
            }
            return Num2Text;
        }

        public static string QuitartildesEspaciosPuntos(string cadena)
        {
            try
            {
                //if (cadena == null)
                //    cadena = "";
                string Convertida = cadena;
                Convertida = Convertida.Trim();
                Convertida = Convertida.Replace("S.A","");
                Convertida = Convertida.Replace("CIA.", "");
                Convertida = Convertida.Replace("LTDA.", "");
                Convertida = Convertida.Replace("Ñ", "N").ToUpper();
                Convertida = Convertida.Replace("Á", "A").ToUpper();
                Convertida = Convertida.Replace("É", "E").ToUpper();
                Convertida = Convertida.Replace("Í", "I").ToUpper();
                Convertida = Convertida.Replace("Ú", "U").ToUpper();
                Convertida = Convertida.Replace("Ó", "O").ToUpper();
                Convertida = Convertida.Replace(".", "");

                Convertida = Convertida.Trim();


                return Convertida;
            }
            catch (Exception)
            {


                throw;
            }
        }

        public static string QuitarTildes(string strin)
        {
            try
            {
                strin = strin.ToUpper();

                strin = strin.Replace("Ñ", "N");
                strin = strin.Replace("Á", "A");
                strin = strin.Replace("É", "E");
                strin = strin.Replace("Í", "I");
                strin = strin.Replace("Ó", "O");
                strin = strin.Replace("Ú", "U");
                return strin;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static Boolean ValidaCedula(string cedula)
        {
            try
            {
                var longitud_cedula = cedula.Length;
                int[] coeficientes = {2,1,2,1,2,1,2,1,2};
                var num_provincias = 24;
                var total = 0;
                var digito_coeficiente = 0;
                var digito_ruc = 0;
                var tercer_digito = 6;
                var longitud = 10;
                var valor = 0;
                var valor_ = 0;

                if (longitud == longitud_cedula)
                {
                    var provincia = Convert.ToInt32(string.Concat(cedula[0], cedula[1]));
                    var digito_tres = Convert.ToInt32(cedula[2]+string.Empty);

                    if (( provincia == 30 || (provincia > 0 && provincia <= num_provincias)) && (digito_tres >=0 && digito_tres <= tercer_digito))
                    {
                        var digito_verificador_recibido = Convert.ToInt32(cedula[9] + string.Empty);
                        for (var a = 0; a < coeficientes.Length; a++)
                        {
                            digito_coeficiente = coeficientes[a];
                            digito_ruc = Convert.ToInt32(cedula[a] + string.Empty);
                            valor = digito_coeficiente * digito_ruc;
                            valor_ = valor >= 10 ? (valor - 9) : valor;

                            total =  total + valor_;                                                                        
                        }
                        var digito_verificador_obtenido = total > 10 ? (total % 10) != 0 ? 10 - (total % 10) : (total % 10) : total;

                        return digito_verificador_recibido == digito_verificador_obtenido;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public static Boolean ValidaIdentificacion(string tipo_documento, string naturaleza, string cedula_ruc, ref string return_naturaleza)
        {
            try
            {
                if (tipo_documento == "CED")
                {
                    return_naturaleza = "NATU";
                    return ValidaCedula(cedula_ruc.Trim());
                }
                else if (tipo_documento == "RUC")
                {
                    var longitud_ruc = 13;
                    var establecimiento = "001";
                    var tercer_digito_persona_juridica = 9;
                    var tercer_digito_persona_natural = 6;
                    long num_valido = 0;
                    var modulo = 11;
                    var total = 0;
                    var valor = 0;
                    var digito_coeficiente = 0;
                    var digito_verificador = 0;
                    var digito_ruc = 0;

                    bool isValid = long.TryParse(cedula_ruc, out num_valido);

                    if (isValid == true)
                    {
                        if (longitud_ruc == cedula_ruc.Length)
                        {
                            var provincia = Convert.ToInt32(string.Concat(cedula_ruc[0], cedula_ruc[1]));
                            var digito_tres = Convert.ToInt32(cedula_ruc[2] + string.Empty);

                            if (provincia >= 1 && provincia <= 24 && cedula_ruc.Substring(10, 3) == establecimiento)
                            {
                                if (digito_tres <= tercer_digito_persona_natural)
                                {

                                    var establecimiento_prov = cedula_ruc.Substring(10, 3);
                                    var isValid_cedula = ValidaCedula(cedula_ruc.Substring(0, 10));

                                    if (isValid_cedula == true && establecimiento_prov == establecimiento)
                                    {
                                        return_naturaleza = "NATU";
                                        return true;
                                    }
                                    else
                                    {
                                        digito_verificador = Convert.ToInt32(cedula_ruc[8] + string.Empty);
                                        int[] coeficientes = { 3, 2, 7, 6, 5, 4, 3, 2 };

                                        for (var a = 0; a < coeficientes.Length; a++)
                                        {
                                            digito_coeficiente = coeficientes[a];
                                            digito_ruc = Convert.ToInt32(cedula_ruc[a] + string.Empty);
                                            valor = digito_coeficiente * digito_ruc;
                                            total = total + valor;
                                        }

                                        var digito_verificador_obtenido = (total % modulo) == 0 ? 0 : modulo - (total % modulo);

                                        return_naturaleza = "JURI";
                                        return digito_verificador == digito_verificador_obtenido;
                                    }
                                }

                                if (digito_tres == tercer_digito_persona_juridica)
                                {
                                    digito_verificador = Convert.ToInt32(cedula_ruc[9] + string.Empty);
                                    int[] coeficientes = { 4, 3, 2, 7, 6, 5, 4, 3, 2 };

                                    for (var a = 0; a < coeficientes.Length; a++)
                                    {
                                        digito_coeficiente = coeficientes[a];
                                        digito_ruc = Convert.ToInt32(cedula_ruc[a] + string.Empty);
                                        valor = digito_coeficiente * digito_ruc;
                                        total = total + valor;
                                    }
                                    var digito_verificador_obtenido = (total % modulo) == 0 ? 0 : modulo - (total % modulo);

                                    return_naturaleza = "JURI";
                                    return digito_verificador == digito_verificador_obtenido;
                                }
                            }
                            else
                            {
                                if (cedula_ruc == "9999999999999")
                                {
                                    return_naturaleza = "JURI";
                                    return true;
                                }
                            }
                        }
                        return false;
                    }
                    return false;
                }
                else
                {
                    return_naturaleza = naturaleza;
                    return true;
                }             
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
