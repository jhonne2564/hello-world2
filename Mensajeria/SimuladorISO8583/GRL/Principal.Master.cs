using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimuladorISO8583.GRL
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AlertaOcultar();
        }

        public string Titulo
        {
            get
            {
                return this.lblMasterTitulo.Text;
            }
            set
            {
                this.lblMasterTitulo.Text = value;
            }
        }

        public void AlertaMostrar(string pi_sMensaje, string pi_sTipoMensaje = "NOK")
        {
            if (pi_sTipoMensaje.ToUpper() == "NOK")
            {
                this.imgMensaje.ImageUrl = "~/IMG/imgAlerta.png";
            }
            else if (pi_sTipoMensaje.ToUpper() == "OK")
            {
                this.imgMensaje.ImageUrl = "~/IMG/ok.png";
            }
            lblMasterAlerta.Text = pi_sMensaje.Replace(". ", ".<br/>");
            panMasterAlerta.Visible = true;
        }

        public void AlertaOcultar()
        {
            lblMasterAlerta.Text = "";
            panMasterAlerta.Visible = false;
        }

        protected void lbCerrarSesion_Click(object sender, EventArgs e)
        {
            //var vl_cSesion = new NSesion();
            //try
            //{
            //    vl_cSesion.CerrarSecion(Session["sesID"].ToString());
            //    Session.RemoveAll();
            //    Session.Abandon();
            //    Response.Clear();
            //    Response.Redirect("~/Login.aspx", false);
            //}
            //catch
            //{
            //}
        }

        protected void lbtNombreUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Datos.aspx");
        }
    }
}