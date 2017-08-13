using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace EstaSiEs
{
    public class Formulario
    {

        public LinkButton CrearLink(string pi_sNombre)
        {
            LinkButton vl_lnkHola = new LinkButton();
            vl_lnkHola.Text = pi_sNombre;
            return vl_lnkHola;
        }

    }
}
