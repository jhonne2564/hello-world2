using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583.Entidades
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Fedex 20170608</remarks>
    public class RespuestaValidacion
    {

        public ValidacionCampoDatos Respuesta { get; set; }
        public string ValorEnvio { get; set; }

    }
}
