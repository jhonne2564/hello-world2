using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583.Entidades
{
    public class Funciones
    {


        public string HexaToBin(string pi_sBitmapHexa)
        {
            StringBuilder vl_sbRespuesta = new StringBuilder();
            for (int vl_iContador = 0; vl_iContador < pi_sBitmapHexa.Length; vl_iContador++)
                vl_sbRespuesta.Append(Convert.ToString(Convert.ToInt32(pi_sBitmapHexa.Substring(vl_iContador, 1), 16), 2).PadLeft(4, '0'));
            return vl_sbRespuesta.ToString();
        }

    }
}
