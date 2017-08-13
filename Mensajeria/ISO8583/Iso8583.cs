using ISO8583.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{

    /// <summary>
    /// Objeto Iso8583 para versión 1987 - 0xxx
    /// </summary>
    /// <remarks></remarks>
    public class Iso8583
    {


        public string MTI { get; set; } //Message Type Indicator (MTI) 
        private Hashtable CamposDatos;
        private string BitmapPrimario = string.Empty;
        private string BitmapSecundario = string.Empty;
        private bool UsaBitmapSecundario = false;

        private const string EncabezadoTransaccionesFinancieras = "ISO016000050"; //for Financial Transactions.
        private const string EncabesajoMensajesAdministracionRed = "ISO006000040"; //for Network Management Messages.

        /// <summary>
        /// Inicializa un mensaje Iso8583
        /// </summary>
        /// <param name="pi_sMIT">Indicador de tipo de mensaje</param>
        /// <remarks>Fedex 20170608</remarks>
        public Iso8583(string pi_sMIT)
        {
            MTI = pi_sMIT;
            CamposDatos = new Hashtable();
        }

        /// <summary>
        /// Carga un mensaje de respuesta
        /// </summary>
        /// <param name="pi_aMensaje"></param>
        /// <remarks>Fedex 20170608</remarks>
        public Iso8583(byte[] pi_aMensaje, bool pi_bLongitud = true)
        {
            MTI = string.Empty;
            string vl_sTramaRespuesta = string.Empty;
            CamposDatos = new Hashtable();
            if (pi_bLongitud)
            {
                byte[] newArray = new byte[pi_aMensaje.Length - 2];
                Buffer.BlockCopy(pi_aMensaje, 2, newArray, 0, pi_aMensaje.Length - 2);
                vl_sTramaRespuesta = System.Text.Encoding.ASCII.GetString(newArray);
            }
            else
            {
                vl_sTramaRespuesta = System.Text.Encoding.ASCII.GetString(pi_aMensaje);
            }
            if (vl_sTramaRespuesta.StartsWith("ISO016000050") || vl_sTramaRespuesta.StartsWith("ISO006000040"))
            {
                vl_sTramaRespuesta = vl_sTramaRespuesta.Substring(12, vl_sTramaRespuesta.Length - 12);
            }
            unpack(vl_sTramaRespuesta);
        }

        /// <summary>
        /// Adiciona un campo de datos al mensaje Iso8583
        /// </summary>
        /// <param name="pi_sPosicion">Posición del campo de datos</param>
        /// <param name="pi_sValor">Valor a adicionar en el campo de datos</param>
        /// <returns>Retorna true si se adiciono el nuevo campo de datos o false si se genero un error y no se pudo agregar</returns>
        /// <remarks>Fedex 20170608</remarks>
        public ValidacionCampoDatos adicionarCampoDatos(int pi_sPosicion, string pi_sValor)
        {
            if (pi_sPosicion > 1 && pi_sPosicion <= 128)
            {
                var vl_cCampos = new Validaciones();
                var vl_cRespuesta = vl_cCampos.FormatoCampo(pi_sPosicion, pi_sValor);
                if (vl_cRespuesta.Respuesta == ValidacionCampoDatos.Exitoso)
                {
                    CampoDatos vl_sCampo = new CampoDatos();
                    vl_sCampo.ValorOriginal = pi_sValor;
                    vl_sCampo.ValorEnvio = vl_cRespuesta.ValorEnvio;
                    if (CamposDatos[pi_sPosicion] == null)
                    {
                        CamposDatos[pi_sPosicion] = vl_sCampo;
                        return ValidacionCampoDatos.Exitoso;
                    }
                    else
                    {
                        return ValidacionCampoDatos.CampoDatosExiste;
                    }
                }
                else
                {
                    return vl_cRespuesta.Respuesta;
                }
            }
            else
            {
                return ValidacionCampoDatos.PosicionInvalida;
            }
        }

        /// <summary>
        /// Adiciona un campo de datos al mensaje Iso8583 sin realizar validaciones
        /// </summary>
        /// <param name="pi_sPosition">Posición del campo de datos</param>
        /// <param name="pi_sValue">Valor a adicionar en el campo de datos</param>
        /// <remarks>Fedex 20170608</remarks>
        private void adicionarCampoDatosSinValidar(int pi_sPosition, string pi_sValue)
        {
            CampoDatos vl_sCampo = new CampoDatos();
            vl_sCampo.ValorOriginal = pi_sValue;
            vl_sCampo.ValorEnvio = pi_sValue;
            CamposDatos[pi_sPosition] = vl_sCampo;
        }

        /// <summary>
        /// Generar el mensaje sin los 2 byte de la longitud
        /// </summary>
        /// <returns>Retorna el mensaje</returns>
        /// <remarks>Fedex 20170608</remarks>
        private string generarMensajeOld()
        {
            StringBuilder pi_sbMensaje = new StringBuilder();
            if (MTI.StartsWith("02"))
            {
                pi_sbMensaje.Append(EncabezadoTransaccionesFinancieras);
            }
            else if (MTI.StartsWith("08"))
            {
                pi_sbMensaje.Append(EncabesajoMensajesAdministracionRed);
            }
            pi_sbMensaje.Append(MTI);
            pi_sbMensaje.Append(generarBitmaps());
            for (int vl_iContador = 2; vl_iContador <= 128; vl_iContador++)
            {
                if (CamposDatos[vl_iContador] != null)
                {
                    CampoDatos vl_sCampo = (CampoDatos)CamposDatos[vl_iContador];
                    pi_sbMensaje.Append(vl_sCampo.ValorEnvio);
                }
            }
            return pi_sbMensaje.ToString();
        }

        public string generarMensaje()
        {
            StringBuilder pi_sbMensaje = new StringBuilder();
            bool vl_bUsaBitman2 = false;
            string vl_sEncabezado = string.Empty;
            string vl_sBitmap = string.Empty;
            string vl_sBitmap1 = "0000000000000000000000000000000000000000000000000000000000000000";
            string vl_sBitmap2 = "0000000000000000000000000000000000000000000000000000000000000000";
            for (int vl_iPosicion = 2; vl_iPosicion <= 128; vl_iPosicion++)
            {
                if (CamposDatos[vl_iPosicion] != null)
                {
                    CampoDatos vl_sCampo = (CampoDatos)CamposDatos[vl_iPosicion];
                    pi_sbMensaje.Append(vl_sCampo.ValorEnvio);
                    if (vl_iPosicion <= 64)
                    {
                        vl_sBitmap1 = vl_sBitmap1.Insert(vl_iPosicion - 1, "1");
                        vl_sBitmap1 = vl_sBitmap1.Remove(vl_iPosicion, 1);
                    }
                    else
                    {
                        vl_sBitmap2 = vl_sBitmap2.Insert(vl_iPosicion - 1 - 64, "1");
                        vl_sBitmap2 = vl_sBitmap2.Remove(vl_iPosicion - 64, 1);
                        vl_bUsaBitman2 = true;
                    }
                }
            }
            if (vl_bUsaBitman2)
            {
                vl_sBitmap1 = vl_sBitmap1.Insert(0, "1");
                vl_sBitmap1 = vl_sBitmap1.Remove(1, 1);
                vl_sBitmap1 = vl_sBitmap1 + vl_sBitmap2;
            }
            int vl_iContador = 0;
            while (vl_iContador < vl_sBitmap1.Length)
            {
                vl_sBitmap += Convert.ToInt32(vl_sBitmap1.Substring(vl_iContador, 4), 2).ToString("X");
                vl_iContador += 4;
            }
            if (MTI.StartsWith("02"))
            {
                vl_sEncabezado = EncabezadoTransaccionesFinancieras;
            }
            else if (MTI.StartsWith("08"))
            {
                vl_sEncabezado = EncabesajoMensajesAdministracionRed;
            }
            return string.Concat(vl_sEncabezado, MTI, vl_sBitmap, pi_sbMensaje.ToString());
        }

        public byte[] obtieneTramaBytes()
        {
            string vl_sTrama = generarMensaje();
            var vl_baTrama = new byte[vl_sTrama.Length + 2];
            byte[] vl_baLongitud = longitudMensaje(vl_sTrama.Length);
            vl_baTrama[0] = vl_baLongitud[0];
            vl_baTrama[1] = vl_baLongitud[1];
            byte[] vl_baTramaString = new byte[vl_sTrama.Length];
            vl_baTramaString = System.Text.Encoding.ASCII.GetBytes(vl_sTrama);
            Array.Copy(vl_baTramaString, 0, vl_baTrama, 2, vl_baTramaString.Length);
            return vl_baTrama;
        }

        /// <summary>
        /// Obtiene la información de un campo de datos de una posición determinada
        /// </summary>
        /// <param name="pi_iPosicion">Posición del campo de datos</param>
        /// <returns>Fedex 20170608</returns>
        public CampoDatos obtenerCampoDatos(int pi_iPosicion)
        {
            CampoDatos vl_oCampoDatos = null;
            if (CamposDatos[pi_iPosicion] != null)
            {
                vl_oCampoDatos = (CampoDatos)CamposDatos[pi_iPosicion];
            }
            return vl_oCampoDatos;
        }

        /// <summary>
        /// Genera los Bitmaps
        /// </summary>
        /// <returns>Retorna los los Bitmaps primario y secundario concatenados</returns>
        /// <remarks>Fedex 20170690</remarks>
        private string generarBitmaps()
        {
            string vl_sResult = string.Empty;
            bool vl_bUsaBitman2 = false;
            string vl_sBitmap1 = "0000000000000000000000000000000000000000000000000000000000000000";
            string vl_sBitmap2 = "0000000000000000000000000000000000000000000000000000000000000000";
            foreach (DictionaryEntry vl_deCampo in CamposDatos)
            {
                int pi_iPosicion = (int)vl_deCampo.Key;
                if (pi_iPosicion <= 64)
                {
                    vl_sBitmap1 = vl_sBitmap1.Insert(pi_iPosicion - 1, "1");
                    vl_sBitmap1 = vl_sBitmap1.Remove(pi_iPosicion, 1);
                }
                else
                {
                    vl_sBitmap2 = vl_sBitmap2.Insert(pi_iPosicion - 1 - 64, "1");
                    vl_sBitmap2 = vl_sBitmap2.Remove(pi_iPosicion - 64, 1);
                    vl_bUsaBitman2 = true;
                }
            }
            if (vl_bUsaBitman2)
            {
                vl_sBitmap1 = vl_sBitmap1.Insert(0, "1");
                vl_sBitmap1 = vl_sBitmap1.Remove(1, 1);
                vl_sBitmap1 = string.Concat(vl_sBitmap1, vl_sBitmap2);
            }
            int vl_iContador = 0;
            while (vl_iContador < vl_sBitmap1.Length)
            {
                vl_sResult += Convert.ToInt32(vl_sBitmap1.Substring(vl_iContador, 4), 2).ToString("X");
                vl_iContador += 4;
            }
            return vl_sResult;
        }

        /// <summary>
        /// Calcula los 2 bytes iniciales con la longitud del mensaje
        /// </summary>
        /// <param name="pi_iMessageLength">Message length</param>
        /// <returns>Retorna el arreglo de bytes de la longitud del mensaje</returns>
        /// <remarks>Fedex 20170608</remarks>
        private byte[] longitudMensaje(int pi_iLongitudMensaje)
        {
            var vl_baLength = new byte[2];
            if (pi_iLongitudMensaje < 256)
            {
                vl_baLength[0] = (byte)(0);
                vl_baLength[1] = (byte)(pi_iLongitudMensaje);
            }
            else
            {
                vl_baLength[0] = (byte)(pi_iLongitudMensaje / 256);
                vl_baLength[1] = (byte)(pi_iLongitudMensaje % 256);
            }
            return vl_baLength;
        }

        private void unpack(string pi_sTrama)
        {
            var vl_cFunciones = new Funciones();
            var vl_cValidaciones = new Validaciones();
            try
            {
                int vl_iUbicacion = 0;
                //TODO falta validar si la respuesta de la trama es generada con encabezado
                MTI = pi_sTrama.Substring(0, 4);
                vl_iUbicacion += 4;
                string vl_sBitmap1Hexa = pi_sTrama.Substring(vl_iUbicacion, 16);
                vl_iUbicacion += 16;
                string vl_sBitmap1Bin = vl_cFunciones.HexaToBin(vl_sBitmap1Hexa);
                if (vl_sBitmap1Bin.StartsWith("1"))
                {
                    string vl_sBitmap2Hexa = pi_sTrama.Substring(vl_iUbicacion, 16);
                    vl_sBitmap1Bin = string.Concat(vl_sBitmap1Bin, vl_cFunciones.HexaToBin(vl_sBitmap2Hexa));
                    vl_iUbicacion += 16;
                }
                else
                {
                    if (pi_sTrama.Substring(vl_iUbicacion, 7) == "[.....]")
                        vl_iUbicacion += 7;
                }
                for (int vl_iContador = 1; vl_iContador < vl_sBitmap1Bin.Length; vl_iContador++)
                {
                    if (vl_sBitmap1Bin.Substring(vl_iContador, 1) == "1")
                    {
                        string vl_sValor = vl_cValidaciones.obtieneValor(vl_iContador + 1, pi_sTrama, ref vl_iUbicacion);
                        adicionarCampoDatosSinValidar(vl_iContador + 1, vl_sValor);
                    }
                }
            }
            catch
            {
            }
        }

    }
}
