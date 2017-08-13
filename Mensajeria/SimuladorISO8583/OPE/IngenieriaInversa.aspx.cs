using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimuladorISO8583.OPE
{
    public partial class IngenieriaInversa : CLAS.PaginaBaseMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyMaster.Titulo = "Ingenieria Inversa";
            }
        }

        protected void btIngenieriaInversa_Click(object sender, EventArgs e)
        {
            if (this.tbTrama.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese la Trama");
                return;
            }
            byte[] vl_baTramaString = new byte[this.tbTrama.Text.Length];
            vl_baTramaString = System.Text.Encoding.ASCII.GetBytes(this.tbTrama.Text);
            var vl_cUnpackIso8583 = new ISO8583.Iso8583(vl_baTramaString, this.cbLongitud.Checked);
            this.lbMTI.Text = vl_cUnpackIso8583.MTI;
            for (int vl_iContador = 2; vl_iContador <= 128; vl_iContador++)
            {
                var vl_cCampoDato = vl_cUnpackIso8583.obtenerCampoDatos(vl_iContador);
                if (vl_cCampoDato != null)
                {
                    TableRow vl_trRow = new TableRow();
                    TableCell vl_tcBit = new TableCell();
                    vl_tcBit.Text = "Bit " + vl_iContador.ToString();
                    TableCell vl_tcValor = new TableCell();
                    vl_tcValor.Text = vl_cCampoDato.ValorEnvio;
                    vl_trRow.Cells.Add(vl_tcBit);
                    vl_trRow.Cells.Add(vl_tcValor);
                    tlInversa.Rows.Add(vl_trRow);
                }
            }
            this.tlInversa.Visible = true;
        }
    }
}