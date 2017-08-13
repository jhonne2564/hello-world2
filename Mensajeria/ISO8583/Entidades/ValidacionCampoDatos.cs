using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583.Entidades
{

    /// <summary>
    /// Respuesta de la validación de campos
    /// </summary>
    /// <remarks>Fedex 20170608</remarks>
    public enum ValidacionCampoDatos
    {

        Exitoso,
        Error,
        LongitudInvalida,
        FormatoInvalido,
        PosicionInvalida,
        CampoDatosExiste

    }
}
