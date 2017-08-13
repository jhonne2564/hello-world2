using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimuladorISO8583.GRL
{
    public partial class Inicio : CLAS.PaginaBaseMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyMaster.Titulo = "Inicio";
            }
        }
    }
}