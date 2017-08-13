using System;
using System.Web;

namespace SimuladorISO8583.CLAS
{
    public class PaginaBaseMasterPage : System.Web.UI.Page
    {

        //private Negocio.GEN.NSesion objSesion = new Negocio.GEN.NSesion();
        protected GRL.Principal MyMaster;
        protected string LLaveConexion = "ISO8583";
        //protected Entidades.ESesion objSes = new Entidades.ESesion();

        public PaginaBaseMasterPage()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["sesID"] == null)
            //    Response.Redirect("~/Login.aspx");
            MyMaster = (GRL.Principal)Master;
            //string[] Pagina;
            //string sesID = "";
            //try
            //{
            //    sesID = Session["sesID"].ToString();
            //}
            //catch (Exception)
            //{
            //    sesID = "";
            //}
            //Pagina = Page.Request.RawUrl.ToString().Split(new Char[] { '?' });
            //objSes = objSesion.Validar(sesID);
            //if (objSes.CambiaClave.ToUpper() == "N")
            //{
            //    if (!Pagina[0].Contains("USU/Datos.aspx"))
            //    {
            //        Response.Redirect("~/USU/Datos.aspx?clave=S");
            //        return;
            //    }
            //}
            //if (objSes == null)
            //    Response.Redirect("~/Login.aspx");
            //else
            //{
            //    CargaDatos();
            //}
        }

        private void CargaDatos()
        {
            //LinkButton lbtMasterUsuario = (LinkButton)MyMaster.FindControl("lbtNombreUsuario");
            //lbtMasterUsuario.Text = objSes.Nombres + " " + objSes.Apellidos;
        }

    }
}