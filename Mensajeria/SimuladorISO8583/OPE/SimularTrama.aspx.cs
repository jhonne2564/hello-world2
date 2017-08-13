using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace SimuladorISO8583.OPE
{
    public partial class SimularTrama : CLAS.PaginaBaseMasterPage
    {

        private int vg_iCantidadPaginasGrilla = int.Parse(ConfigurationManager.AppSettings["PAGS-GRILLA"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyMaster.Titulo = "Simular Trama";
            }
        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(1);
        }

        protected void gvTrama_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (this.lbCampoOrden.Text == e.SortExpression)
            {
                if (this.lbOrdenamiento.Text == "ASC")
                    this.lbOrdenamiento.Text = "DESC";
                else
                    this.lbOrdenamiento.Text = "ASC";
            }
            else
            {
                this.lbOrdenamiento.Text = "ASC";
                this.lbCampoOrden.Text = e.SortExpression;
            }
            CargarGrillaBusqueda(1);
        }

        public void CargarGrillaBusqueda(int pi_iPagina)
        {
            string vl_sMensajeError = string.Empty;
            var vl_cNegocio = new Negocio.ADM.NTrama();
            Negocio.GRL.NPaginaBD Datos;
            var vl_cDatos = new Entidades.EEncabezadoTrama();
            Datos = vl_cNegocio.BuscarTrama("123456789",
                                            vl_cDatos,
                                            this.lbCampoOrden.Text,
                                            this.lbOrdenamiento.Text,
                                            pi_iPagina,
                                            vg_iCantidadPaginasGrilla,
                                            ref vl_sMensajeError);
            if (vl_sMensajeError == string.Empty)
            {
                gvTrama.DataSource = Datos.DataSource;
                gvTrama.DataBind();
                // Carga Informacion de la consulta
                this.lbRegistroInicial.Text = ((vg_iCantidadPaginasGrilla * (pi_iPagina - 1)) + 1).ToString();
                this.lbRegistroFinal.Text = ((vg_iCantidadPaginasGrilla * (pi_iPagina - 1)) + Datos.DataSource.Rows.Count).ToString();
                lblTotalRegistros.Text = Datos.CantidadRegistros.ToString();
                // Llena Drop de cantidad de Pags
                Datos.ComboPagina(ddlPaginas);
                Datos.VerificarLink(lnkPrimero
                                    , lnkAtras
                                    , lnkSiguiente
                                    , lnkUltimo);
                pnResultadoBusqueda.Visible = true;
            }
            else
            {
                MyMaster.AlertaMostrar(vl_sMensajeError);
                pnResultadoBusqueda.Visible = false;
            }
        }

        #region Botones Paginación Búsqueda

        protected void lnkBusqueda_Primero(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(1);
        }


        protected void lnkBusqueda_Siguiente(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(Convert.ToInt32(ddlPaginas.SelectedValue) + 1);

        }

        protected void lnkBusqueda_Atras(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(Convert.ToInt32(ddlPaginas.SelectedValue) - 1);
        }


        protected void lnkBusqueda_Ultimo(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(int.Parse(ddlPaginas.Items[ddlPaginas.Items.Count - 1].Value));
        }

        protected void ddlPaginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vl_iPagina;
            try
            {
                vl_iPagina = Convert.ToInt32(ddlPaginas.SelectedValue);
            }
            catch
            {
                vl_iPagina = 1;
            }
            CargarGrillaBusqueda(vl_iPagina);
        }

        #endregion

        protected void btSimular_Click(object sender, EventArgs e)
        {
            bool vl_bSeleccionTrama = false;
            foreach (GridViewRow vl_gvRow in gvTrama.Rows)
            {
                RadioButton vl_rbSeleccion = (RadioButton)vl_gvRow.FindControl("rbSeleccion");
                if (vl_rbSeleccion.Checked)
                {
                    vl_bSeleccionTrama = true;
                    Label vl_lbEncID = (Label)vl_gvRow.FindControl("lbEncID");
                    Label vl_lbEncMTI = (Label)vl_gvRow.FindControl("lbEncMTI");
                    this.lbEncID.Text = vl_lbEncID.Text;
                    this.lbMTI.Text = vl_lbEncMTI.Text;
                    break;
                }
            }
            if (vl_bSeleccionTrama)
            {
                this.pnFiltroBusqueda.Visible = false;
                this.pnResultadoBusqueda.Visible = false;
                this.pnSimular.Visible = true;
                LLenarCampos();
                LLenarDropConexion();
            }
            else
            {
                MyMaster.AlertaMostrar("Debe Seleccionar una Trama.");
            }
        }

        private void LLenarCampos()
        {
            string vl_sMensajeError = string.Empty;
            var vl_cNegocio = new Negocio.ADM.NTrama();
            var vl_dtDetalleTrama = vl_cNegocio.DetalleTrama("1234567890", this.lbEncID.Text, ref vl_sMensajeError);
            if (vl_sMensajeError == string.Empty)
            {
                foreach (DataRow vl_drRow in vl_dtDetalleTrama.Rows)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow vl_trCampo = (System.Web.UI.HtmlControls.HtmlTableRow)pnSimular.FindControl("trCampo" + vl_drRow["det_campo"].ToString());
                    vl_trCampo.Visible = true;
                    Label vl_lbNombreCampo = (Label)pnSimular.FindControl("lbNombreCampo" + vl_drRow["det_campo"].ToString());
                    vl_lbNombreCampo.Text = vl_drRow["det_nombre"].ToString();
                    TextBox vl_tbCampo = (TextBox)pnSimular.FindControl("tbCampo" + vl_drRow["det_campo"].ToString());
                    Label vl_lbDescripcion = (Label)pnSimular.FindControl("lbDescripcion" + vl_drRow["det_campo"].ToString());
                    vl_lbDescripcion.Text = vl_drRow["det_descripcion"].ToString();
                    if (vl_drRow["det_estatico"].ToString().ToUpper() == "S")
                    {
                        vl_tbCampo.Text = vl_drRow["det_estatico_informacion"].ToString();
                        vl_tbCampo.Enabled = false;
                    }
                    else
                    {
                        vl_tbCampo.Text = "";
                        vl_tbCampo.Enabled = true;
                    }
                }
            }
            else
                MyMaster.AlertaMostrar(vl_sMensajeError);
        }

        private void LLenarDropConexion()
        {
            var vl_cGenerico = new Negocio.GRL.NGenerico();
            vl_cGenerico.CargarDrop("123456789", "CONEXION", "-- Seleccione --", this.ddlConexion);
        }

        protected void btEnviar_Click(object sender, EventArgs e)
        {
            if (this.ddlConexion.SelectedItem.Value == "")
            {
                MyMaster.AlertaMostrar("Seleccione la conexión");
                return;
            }
            var vl_isoTrama = new ISO8583.Iso8583(this.lbMTI.Text);
            for (int vl_iContador = 2; vl_iContador <= 128; vl_iContador++)
            {
                System.Web.UI.HtmlControls.HtmlTableRow vl_trCampo = (System.Web.UI.HtmlControls.HtmlTableRow)pnSimular.FindControl("trCampo" + vl_iContador.ToString());
                if (vl_trCampo.Visible)
                {
                    TextBox vl_tbCampo = (TextBox)pnSimular.FindControl("tbCampo" + vl_iContador.ToString());
                    var vl_cValidaCampo = vl_isoTrama.adicionarCampoDatos(vl_iContador, vl_tbCampo.Text);
                }
            }
            DateTime vl_dtInicio, vl_dtFin;
            int m_iBytes = 0;
            byte[] m_aBuffer = new byte[8000];
            byte[] newArray;
            string[] vl_sDetalleConexion = this.ddlConexion.SelectedItem.Value.Split('|');
            byte[] vl_baTrama = vl_isoTrama.obtieneTramaBytes();
            this.tbTrama.Text = System.Text.Encoding.ASCII.GetString(vl_baTrama, 0, vl_baTrama.Length);
            var endPoint = new IPEndPoint(IPAddress.Parse(vl_sDetalleConexion[0]), int.Parse(vl_sDetalleConexion[1]));
            var vl_tcpSocket = new TcpClient();
            vl_tcpSocket.SendTimeout = int.Parse(vl_sDetalleConexion[2]) * 1000;
            vl_tcpSocket.ReceiveTimeout = int.Parse(vl_sDetalleConexion[3]) * 1000;
            vl_dtInicio = DateTime.Now;
            try
            {
                vl_tcpSocket.Connect(endPoint);
            }
            catch
            {
                MyMaster.AlertaMostrar("No se pudo establecer la conexión.");
                return;
            }
            System.IO.Stream nsEscritura = vl_tcpSocket.GetStream();
            nsEscritura.Write(vl_baTrama, 0, vl_baTrama.Length);
            try
            {
                m_iBytes = nsEscritura.Read(m_aBuffer, 0, m_aBuffer.Length);

                newArray = new byte[m_iBytes];
                Array.Copy(m_aBuffer, 0, newArray, 0, m_iBytes);
                //Buffer.BlockCopy(m_aBuffer, 16, newArray, 0, m_iBytes);
            }
            catch
            {
                MyMaster.AlertaMostrar("Se agotó el tiempo de espera para la recepción del mensaje.");
                vl_tcpSocket.Close();
                return;
            }
            vl_tcpSocket.Close();
            vl_dtFin = DateTime.Now;
            this.tbTrama.Text = vl_isoTrama.generarMensaje();
            this.tlEnvio.Visible = true;
            vl_isoTrama = new ISO8583.Iso8583(newArray);
            this.tbTramaRespuesta.Text = System.Text.Encoding.ASCII.GetString(newArray);
            this.lbTiempoRespuesta.Text = (vl_dtFin - vl_dtInicio).TotalSeconds.ToString();
            this.tlRespuesta.Visible = true;
        }

    }
}