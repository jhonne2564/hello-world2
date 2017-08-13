using System;
using System.Data;
using System.Configuration;

namespace Negocio.GRL
{
    public class NBaseNegocio
    {

        protected NSPXml Param = new NSPXml("parametros");
        protected string ConString = ConfigurationManager.AppSettings["BD"].ToString();
      
        protected bool ValidarSesion(DataSet pi_dsDatos)
        {
            bool vl_bRespuesta = false;
            try
            {
                if (pi_dsDatos.Tables[0].Rows[0]["RET"].ToString() == "OK")
                    vl_bRespuesta = true;
                else
                    vl_bRespuesta = false;
            }
            catch (Exception vl_exException)
            {
                //var vl_cLog = new NLog();
                //vl_cLog.GrabaErrorCatch(vl_exException, "", "BaseNegocio.cs", "ValidarRespuesta");
                //vl_bRespuesta = false;
            }
            return vl_bRespuesta;
        }

    }
}
