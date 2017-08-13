using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EstaSiEs;

namespace SimuladorISO8583.OPE
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var wepa = Request.QueryString["wepa"];
            string a;
            a = "";
            wepa = Context.Items["wepa"].ToString();
            //Context.Items.Add("wepa", "hola");
            //Server.Transfer("inicio.aspx");
        }

        protected void wepa0_Click(object sender, EventArgs e)
        {
            Formulario form = new Formulario();
            this.form1.Controls.Add(form.CrearLink("Dinamico"));
        }
    }
}