using ISO8583.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Fedex 20170608</remarks>
    public class Validaciones
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sCampo">Posición del campo de datos</param>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        public RespuestaValidacion FormatoCampo(int pi_sCampo, string pi_sValor)
        {
            string vl_sValorEnvio = string.Empty;
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sCampo == 2)
            {
                //Primary account number (PAN)
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 19);
            }
            else if (pi_sCampo == 3)
            {
                //Processing code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 6);
            }
            else if (pi_sCampo == 4)
            {
                //Transaction Amount
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 12);
            }
            else if (pi_sCampo == 5)
            {
                //Amount, Settlement
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 12);
            }
            else if (pi_sCampo == 6)
            {
                //Amount, cardholder billing
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 12);
            }
            else if (pi_sCampo == 7)
            {
                //Transmission date & time
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 10);
            }
            else if (pi_sCampo == 8)
            {
                //Amount, Cardholder billing fee
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 9)
            {
                //Conversion rate, Settlement
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 10)
            {
                //Conversion rate, cardholder billing
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 11)
            {
                //Systems trace audit number
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 6);
            }
            else if (pi_sCampo == 12)
            {
                // Local Transaction Time
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 6);
            }
            else if (pi_sCampo == 13)
            {
                //Date, Local transaction
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 4);
            }
            else if (pi_sCampo == 14)
            {
                //Date, Expiration
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 4);
            }
            else if (pi_sCampo == 15)
            {
                //Settlement Date
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 4);
            }
            else if (pi_sCampo == 16)
            {
                //Date, conversion
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 4);
            }
            else if (pi_sCampo == 17)
            {
                //Date, capture
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 4);
            }
            else if (pi_sCampo == 18)
            {
                //Merchant type
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 4);
            }
            else if (pi_sCampo == 19)
            {
                //Acquiring institution country code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 20)
            {
                //PAN Extended, country code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 21)
            {
                //Forwarding institution. country code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 22)
            {
                //Point of service entry mode
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 23)
            {
                //Application PAN number
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 24)
            {
                //Function code(ISO 8583:1993)/Network International identifier
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 25)
            {
                //Point of service condition code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 2);
            }
            else if (pi_sCampo == 26)
            {
                //Point of service capture code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 2);
            }
            else if (pi_sCampo == 27)
            {
                //Authorizing identification response length
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 1);
            }
            else if (pi_sCampo == 28)
            {
                //Amount, transaction fee
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 29)
            {
                //Amount. settlement fee
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 30)
            {
                //Amount, transaction processing fee
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 31)
            {
                //Amount, settlement processing fee
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 32)
            {
                //Acquiring institution identification code
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 12);
            }
            else if (pi_sCampo == 33)
            {
                //Forwarding institution identification code
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 11);
            }
            else if (pi_sCampo == 34)
            {
                //Primary account number, extended
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 28);
            }
            else if (pi_sCampo == 35)
            {
                //Track 2 Data
                vl_oRespuesta = ValidaLLVARTEXT(pi_sValor, 37);
            }
            else if (pi_sCampo == 36)
            {

            }
            else if (pi_sCampo == 37)
            {
                //Retrieval Reference Number
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 12);
            }
            else if (pi_sCampo == 38)
            {
                // Authorization Identification Response
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 6);
            }
            else if (pi_sCampo == 39)
            {
                //Response code
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 2);
            }
            else if (pi_sCampo == 40)
            {

            }
            else if (pi_sCampo == 41)
            {
                //Card acceptor terminal identification
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 8);
            }
            else if (pi_sCampo == 42)
            {
                //Card Acceptor Identification Code
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 15);
            }
            else if (pi_sCampo == 43)
            {
                //Card acceptor name/location
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 40);
            }
            else if (pi_sCampo == 44)
            {

            }
            else if (pi_sCampo == 45)
            {

            }
            else if (pi_sCampo == 46)
            {
                //Additional data - ISO
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor, 999);
            }
            else if (pi_sCampo == 47)
            {

            }
            else if (pi_sCampo == 48)
            {
                //Additional data -Private
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 49)
            {
                //Transaction Currency Code
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 50)
            {

            }
            else if (pi_sCampo == 51)
            {

            }
            else if (pi_sCampo == 52)
            {
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 16);
            }
            else if (pi_sCampo == 53)
            {
                //Security Control Information
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 16);
            }
            else if (pi_sCampo == 54)
            {
                //Additional Amount 
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 12);
            }
            else if (pi_sCampo == 55)
            {

            }
            else if (pi_sCampo == 56)
            {

            }
            else if (pi_sCampo == 57)
            {

            }
            else if (pi_sCampo == 58)
            {
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 59)
            {

            }
            else if (pi_sCampo == 60)
            {
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 61)
            {
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 62)
            {

            }
            else if (pi_sCampo == 63)
            {
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 64)
            {
                //Primary MAC
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 16);

            }
            else if (pi_sCampo == 65)
            {

            }
            else if (pi_sCampo == 66)
            {

            }
            else if (pi_sCampo == 67)
            {

            }
            else if (pi_sCampo == 68)
            {

            }
            else if (pi_sCampo == 69)
            {

            }
            else if (pi_sCampo == 70)
            {
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 3);
            }
            else if (pi_sCampo == 71)
            {

            }
            else if (pi_sCampo == 72)
            {

            }
            else if (pi_sCampo == 73)
            {

            }
            else if (pi_sCampo == 74)
            {

            }
            else if (pi_sCampo == 75)
            {

            }
            else if (pi_sCampo == 76)
            {

            }
            else if (pi_sCampo == 77)
            {

            }
            else if (pi_sCampo == 78)
            {

            }
            else if (pi_sCampo == 79)
            {

            }
            else if (pi_sCampo == 80)
            {

            }
            else if (pi_sCampo == 81)
            {

            }
            else if (pi_sCampo == 82)
            {

            }
            else if (pi_sCampo == 83)
            {

            }
            else if (pi_sCampo == 84)
            {

            }
            else if (pi_sCampo == 85)
            {

            }
            else if (pi_sCampo == 86)
            {

            }
            else if (pi_sCampo == 87)
            {

            }
            else if (pi_sCampo == 88)
            {

            }
            else if (pi_sCampo == 89)
            {

            }
            else if (pi_sCampo == 90)
            {
                //Original data elements
                vl_oRespuesta = Valida_NUMBER_Exacto(pi_sValor, 42);
            }
            else if (pi_sCampo == 91)
            {

            }
            else if (pi_sCampo == 92)
            {

            }
            else if (pi_sCampo == 93)
            {

            }
            else if (pi_sCampo == 94)
            {

            }
            else if (pi_sCampo == 95)
            {
                //Replacement amounts
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 42);
            }
            else if (pi_sCampo == 96)
            {

            }
            else if (pi_sCampo == 97)
            {

            }
            else if (pi_sCampo == 98)
            {

            }
            else if (pi_sCampo == 99)
            {

            }
            else if (pi_sCampo == 100)
            {
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 11);
            }
            else if (pi_sCampo == 101)
            {

            }
            else if (pi_sCampo == 102)
            {
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 28);
            }
            else if (pi_sCampo == 103)
            {
                vl_oRespuesta = Valida_NUMBER_Hasta(pi_sValor, 28);
            }
            else if (pi_sCampo == 104)
            {

            }
            else if (pi_sCampo == 105)
            {

            }
            else if (pi_sCampo == 106)
            {

            }
            else if (pi_sCampo == 107)
            {

            }
            else if (pi_sCampo == 108)
            {

            }
            else if (pi_sCampo == 109)
            {

            }
            else if (pi_sCampo == 110)
            {

            }
            else if (pi_sCampo == 111)
            {

            }
            else if (pi_sCampo == 112)
            {

            }
            else if (pi_sCampo == 113)
            {

            }
            else if (pi_sCampo == 114)
            {

            }
            else if (pi_sCampo == 115)
            {

            }
            else if (pi_sCampo == 116)
            {

            }
            else if (pi_sCampo == 117)
            {

            }
            else if (pi_sCampo == 118)
            {

            }
            else if (pi_sCampo == 119)
            {

            }
            else if (pi_sCampo == 120)
            {
                //Key Management
                vl_oRespuesta=Valida_TEXT_Exacto(pi_sValor, 9);
            }
            else if (pi_sCampo == 121)
            {

            }
            else if (pi_sCampo == 122)
            {

            }
            else if (pi_sCampo == 123)
            {
                //Cryptographic Service Message(Network Management)
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 124)
            {

            }
            else if (pi_sCampo == 125)
            {

            }
            else if (pi_sCampo == 126)
            {
                //Issuer Trace Id
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 127)
            {
                vl_oRespuesta = ValidaLLLVARTEXT(pi_sValor);
            }
            else if (pi_sCampo == 128)
            {
                //Secondary MAC
                vl_oRespuesta = Valida_TEXT_Exacto(pi_sValor, 16);
            }
            return vl_oRespuesta;
        }

        public string obtieneValor(int pi_iCampo, string pi_sTrama, ref int pi_iUbicacion)
        {
            string vl_sValor = string.Empty;
            if (pi_iCampo == 3)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 6);
                pi_iUbicacion += 6;
            }
            else if (pi_iCampo == 7)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 10);
                pi_iUbicacion += 10;
            }
            else if (pi_iCampo == 11)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 6);
                pi_iUbicacion += 6;
            }
            else if (pi_iCampo == 12)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 6);
                pi_iUbicacion += 6;
            }
            else if (pi_iCampo == 13)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 4);
                pi_iUbicacion += 4;
            }
            else if (pi_iCampo == 22)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 3);
                pi_iUbicacion += 3;
            }
            else if (pi_iCampo == 25)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 2);
                pi_iUbicacion += 2;
            }
            else if (pi_iCampo == 38)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 6);
                pi_iUbicacion += 6;
            }
            else if (pi_iCampo == 39)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 2);
                pi_iUbicacion += 2;
            }
            else if (pi_iCampo == 41)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 8);
                pi_iUbicacion += 8;
            }
            else if (pi_iCampo == 43)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 40);
                pi_iUbicacion += 40;
            }
            else if (pi_iCampo == 46)
            {
                int vl_ilongitud = int.Parse(pi_sTrama.Substring(pi_iUbicacion, 3));
                pi_iUbicacion += 3;
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, vl_ilongitud);
                pi_iUbicacion += vl_ilongitud;
            }
            else if (pi_iCampo == 47)
            {
                int vl_ilongitud = int.Parse(pi_sTrama.Substring(pi_iUbicacion, 3));
                pi_iUbicacion += 3;
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, vl_ilongitud);
                pi_iUbicacion += vl_ilongitud;
            }
            else if (pi_iCampo == 48)
            {
                int vl_ilongitud = int.Parse(pi_sTrama.Substring(pi_iUbicacion, 3));
                pi_iUbicacion += 3;
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, vl_ilongitud);
                pi_iUbicacion += vl_ilongitud;
            }
            else if (pi_iCampo == 49)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 3);
                pi_iUbicacion += 3;
            }
            else if (pi_iCampo == 70)
            {
                vl_sValor = pi_sTrama.Substring(pi_iUbicacion, 3);
                pi_iUbicacion += 3;
            }
            return vl_sValor;
        }


        /// <summary>
        /// Valida que el numero sea de pi_iLongitudMaxima. 
        /// Rellena de ceros a la izquierda. 
        /// </summary>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <param name="pi_iLongutudMaxima">Longitud máxima aceptada por el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        private RespuestaValidacion Valida_NUMBER_Exacto(string pi_sValor, int pi_iLongitudMaxima)
        {
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sValor.Length <= pi_iLongitudMaxima)
            {
                string vl_sPattern = string.Concat("^\\d{", pi_sValor.Length, "}$");
                if (System.Text.RegularExpressions.Regex.IsMatch(pi_sValor, vl_sPattern))
                {
                    vl_oRespuesta.ValorEnvio = pi_sValor.PadLeft(pi_iLongitudMaxima, '0');
                    vl_oRespuesta.Respuesta = ValidacionCampoDatos.Exitoso;
                }
                else
                {
                    vl_oRespuesta.Respuesta = ValidacionCampoDatos.FormatoInvalido;
                }
            }
            else
            {
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.LongitudInvalida;
            }
            return vl_oRespuesta;
        }


        /// <summary>
        /// Valida los numeros hasta pi_iLongitudMaxima. 
        /// Concatena el tama;o del numero rellenando de cero a la izquierda.        
        /// </summary>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <param name="pi_iLongutudMaxima">Longitud máxima aceptada por el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        private RespuestaValidacion Valida_NUMBER_Hasta(string pi_sValor, int pi_iLongitudMaxima)
        {
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sValor.Length <= pi_iLongitudMaxima)
            {
                string vl_sPattern = string.Concat("^\\d{", pi_sValor.Length, "}$");
                if (System.Text.RegularExpressions.Regex.IsMatch(pi_sValor, vl_sPattern))
                {
                    string vl_sHeader = string.Empty;
                    if (pi_iLongitudMaxima <= 99)
                        vl_sHeader = pi_sValor.Length.ToString().PadLeft(2, '0');
                    else
                        vl_sHeader = pi_sValor.Length.ToString().PadLeft(3, '0');
                    vl_oRespuesta.ValorEnvio = string.Concat(vl_sHeader, pi_sValor);
                    vl_oRespuesta.Respuesta = ValidacionCampoDatos.Exitoso;
                }
                else
                {
                    vl_oRespuesta.Respuesta = ValidacionCampoDatos.FormatoInvalido;
                }

            }
            else
            {
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.LongitudInvalida;
            }
            return vl_oRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <param name="pi_iLongutudMaxima">Longitud máxima aceptada por el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        private RespuestaValidacion Valida_TEXT_Exacto(string pi_sValor, int pi_iLongutudMaxima)
        {
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sValor.Length <= pi_iLongutudMaxima)
            {
                vl_oRespuesta.ValorEnvio = pi_sValor.PadRight(pi_iLongutudMaxima, ' ');
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.Exitoso;
            }
            else
            {
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.LongitudInvalida;
            }
            return vl_oRespuesta;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <param name="pi_iLongutudMaxima">Longitud máxima aceptada por el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        private RespuestaValidacion ValidaLLVARTEXT(string pi_sValor, int pi_iLongutudMaxima)
        {
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sValor.Length <= pi_iLongutudMaxima)
            {
                string vl_sHeader = pi_sValor.Length.ToString().PadLeft(2, '0');
                vl_oRespuesta.ValorEnvio = string.Concat(vl_sHeader, pi_sValor);
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.Exitoso;
            }
            else
            {
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.LongitudInvalida;
            }
            return vl_oRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <param name="pi_iLongutudMaxima">Longitud máxima aceptada por el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        private RespuestaValidacion ValidaLLLVARTEXT(string pi_sValor, int pi_iLongutudMaxima)
        {
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sValor.Length <= pi_iLongutudMaxima)
            {
                string vl_sHeader = pi_sValor.Length.ToString().PadLeft(3, '0');
                vl_oRespuesta.ValorEnvio = string.Concat(vl_sHeader, pi_sValor);
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.Exitoso;
            }
            else
            {
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.LongitudInvalida;
            }
            return vl_oRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <param name="pi_iLongutudMaxima">Longitud máxima aceptada por el campo de datos</param>
        /// <returns></returns>
        /// <remarks>Fedex 20170608</remarks>
        private RespuestaValidacion ValidaLLLVARTEXT(string pi_sValor)
        {
            var vl_oRespuesta = new RespuestaValidacion();
            if (pi_sValor.Length <= 999)
            {
                string vl_sHeader = pi_sValor.Length.ToString().PadLeft(3, '0');
                vl_oRespuesta.ValorEnvio = string.Concat(vl_sHeader, pi_sValor);
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.Exitoso;
            }
            else
            {
                vl_oRespuesta.Respuesta = ValidacionCampoDatos.LongitudInvalida;
            }
            return vl_oRespuesta;
        }

    }
}
