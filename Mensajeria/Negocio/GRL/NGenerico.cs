using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

namespace Negocio.GRL
{
    public class NGenerico : NBaseNegocio
    {

        /// <summary>
        /// Carga un drop con datos de la base de datos
        /// </summary>
        /// <param name="pi_sSesID">GUID de la sesión</param>
        /// <param name="pi_sTipo">Tipo de la consulta en el stored procedure</param>
        /// <param name="pi_sPrimerItem">Primer item en el drop</param>
        /// <param name="po_ddlDrop">Drop donde se va a almacenar la información</param>
        /// <remarks>Fedex 20130215</remarks>
        public void CargarDrop(string pi_sSesID,
                               string pi_sTipo,
                               string pi_sPrimerItem,
                               DropDownList po_ddlDrop,
                               string pi_sValor = "",
                               string pi_sValor2 = "")
        {
            po_ddlDrop.Items.Clear();
            Param.Limpiar();
            Param.AdicionarNodo("sesID", pi_sSesID);
            Param.AdicionarNodo("Tipo", pi_sTipo);
            if (pi_sValor != "")
            {
                Param.AdicionarNodo("Valor", pi_sValor);
            }
            if (pi_sValor2 != "")
            {
                Param.AdicionarNodo("Valor2", pi_sValor2);
            }
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            DataSet vl_dsRespuesta = new DataSet();
            try
            {
                vl_dsRespuesta = vl_cAccesoDatos.EjecutaSPVarchar("drop", "sp_Drop", Param.Generar(), ConString);
                if (ValidarSesion(vl_dsRespuesta))
                {
                    if (vl_dsRespuesta.Tables[1].Rows.Count > 0)
                    {
                        po_ddlDrop.DataSource = vl_dsRespuesta.Tables[1];
                        po_ddlDrop.DataValueField = "Value";
                        po_ddlDrop.DataTextField = "Texto";
                        po_ddlDrop.DataBind();
                    }
                    if (pi_sPrimerItem != "")
                        po_ddlDrop.Items.Insert(0, new ListItem(pi_sPrimerItem, ""));
                }
                else
                    po_ddlDrop.Items.Add(new ListItem("--Sesión Inválida", ""));
            }
            catch (Exception vl_exException)
            {
                //var vl_cLog = new NLog();
                //vl_cLog.GrabaErrorCatch(vl_exException, "", "NGenerico.cs", "CargarDrop");
                po_ddlDrop.Items.Add(new ListItem("--Error--"));
            }
        }

    }
}
