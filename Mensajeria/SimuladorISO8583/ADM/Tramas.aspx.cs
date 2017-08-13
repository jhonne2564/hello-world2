using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimuladorISO8583.ADM
{
    public partial class Tramas : CLAS.PaginaBaseMasterPage
    {

        private int vg_iCantidadPaginasGrilla = int.Parse(ConfigurationManager.AppSettings["PAGS-GRILLA"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyMaster.Titulo = "Administración de Tramas";
            }
        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(1);
        }

        protected void btCrearTrama_Click(object sender, EventArgs e)
        {
            Guid vl_gIDTemporal = Guid.NewGuid();
            this.lbIDTemporal.Text = vl_gIDTemporal.ToString();
            this.pnFiltroBusqueda.Visible = false;
            this.pnResultadoBusqueda.Visible = false;
            this.tbCamposTemporal.Visible = false;
            this.pnCrearTrama.Visible = true;
        }

        protected void gvTrama_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Comprobante")
            //{
            //    char[] vl_cSeparador = { '|' };
            //    string[] vl_sDatos = e.CommandArgument.ToString().Split(vl_cSeparador);
            //    this.pnFiltroBusqueda.Visible = false;
            //    this.pnlResultadoBusqueda.Visible = false;
            //    this.pnSubirComprobante.Visible = true;
            //    this.lbAdmID.Text = vl_sDatos[0];
            //    this.lbAnho.Text = vl_sDatos[1];
            //    this.lbMes.Text = vl_sDatos[2];
            //    this.lbEstado.Text = vl_sDatos[3];
            //    this.lbMonto.Text = double.Parse(vl_sDatos[4]).ToString("C");
            //    this.tbDescripcion.Text = vl_sDatos[5];
            //}
            //else if (e.CommandName == "CuentaDeCobro")
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
            //    Response.TransmitFile(Server.MapPath("~/PDF/CuentaDeCobro.pdf"));
            //    Response.End();
            //}
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
            else if (vl_sMensajeError.Equals("No se encontraron datos asociados a la búsqueda."))
            {            
                pnResultadoBusqueda.Visible = false;
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

        protected void btEliminarTrama_Click(object sender, EventArgs e)
        {
            var id = "";
            bool vl_bSeleccionTrama = false;
            foreach (GridViewRow vl_gvRow in gvTrama.Rows)
            {
                RadioButton vl_rbSeleccion = (RadioButton)vl_gvRow.FindControl("rbSeleccion");
                if (vl_rbSeleccion.Checked)
                {
                    vl_bSeleccionTrama = true;
                    Label vl_lbEncID = (Label)vl_gvRow.FindControl("lbEncID");
                    Label vl_lbEncMTI = (Label)vl_gvRow.FindControl("lbEncMTI");
                     id = vl_lbEncID.Text;
                    break;
                }
            }
            if (vl_bSeleccionTrama)
            {
                string vl_sMensajeError = string.Empty;
                var vl_cNegocioTramas = new Negocio.ADM.NTrama();
                var vl_sDttID = vl_cNegocioTramas.EliminarTabla("123456789", id, ref vl_sMensajeError);
                CargarGrillaBusqueda(1);
            }
            else
            {
                MyMaster.AlertaMostrar("Debe Seleccionar una Trama.");
            }
        }
        protected void btAdicionarBit_Click(object sender, EventArgs e)
        {
            var vl_xCampos = new System.Xml.XmlDocument();
            vl_xCampos.Load(Server.MapPath("~/XML/Campos.xml"));
            if (this.tbCampo.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese el Campo.");
                return;
            }
            else
            {
                int vl_iBit;
                if (int.TryParse(this.tbCampo.Text, out vl_iBit))
                {
                    if (vl_iBit > 128 || vl_iBit < 2)
                    {
                        MyMaster.AlertaMostrar("Valor del Campo Inválido se Espera un Entero Entre 2 y 128.");
                        return;
                    }
                    else
                    {
                        if (vl_xCampos.SelectNodes("Trama/Campos/Campo[@bit='" + this.tbCampo.Text  + "']").Count == 0)
                        {
                            MyMaster.AlertaMostrar("Campo no Configurado.");
                            return;
                        }
                        else
                        {
                            if (vl_xCampos.SelectNodes("Trama/Campos/Campo[@bit='" + this.tbCampo.Text + "' and @valido = 'N']").Count == 1)
                            {
                                MyMaster.AlertaMostrar(vl_xCampos.SelectSingleNode("Trama/Campos/Campo[@bit='" + this.tbCampo.Text + "' and @valido='N']").Attributes["mensaje"].Value);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MyMaster.AlertaMostrar("Formato de Campo Incorrecto se Espera un Entero.");
                    return;
                }
            }
            //if (this.tbNombreCampo.Text == "")
            //{
            //    MyMaster.AlertaMostrar("Ingrese el Nombre.");
            //    return;
            //}
            if (this.cbCampoEstatico.Checked && this.tbInformacionEstatica.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese la Información.");
                return;
            }
            string vl_sMensajeError = string.Empty;
            var vl_cNegocioTramas = new Negocio.ADM.NTrama();
            var vl_cDetalleTrama = new Entidades.EDetalleTrama();
            vl_cDetalleTrama.det_guid = this.lbIDTemporal.Text;
            vl_cDetalleTrama.det_campo = this.tbCampo.Text;
            vl_cDetalleTrama.det_nombre = this.tbNombreCampo.Text;
            vl_cDetalleTrama.det_estatico = (this.cbCampoEstatico.Checked ? "S" : "N");
            vl_cDetalleTrama.det_estatico_informacion = this.tbInformacionEstatica.Text;
            vl_cDetalleTrama.det_descripcion = this.tbDescripcionCampo.Text;
            var vl_sDttID = vl_cNegocioTramas.CrearDetalleTramaTemporal("123456789", vl_cDetalleTrama, ref vl_sMensajeError);
            if (vl_sMensajeError != string.Empty)
            {
                MyMaster.AlertaMostrar(vl_sMensajeError);
            }
            else
            {
                this.tbCampo.Text = "";
                this.tbNombreCampo.Text = "";
                this.cbCampoEstatico.Checked = false;
                this.tbInformacionEstatica.Text = "";
                this.trInformacion.Visible = false;
                this.tbDescripcionCampo.Text = "";
                CargarGrillaBusquedaDetalleTemporal();

                MyMaster.AlertaMostrar("Campo adicionado correctamente.", "OK");
            }
        }

        public void CargarGrillaBusquedaDetalleTemporal()
        {
            
            string vl_sMensajeError = string.Empty;
            var vl_cNegocio = new Negocio.ADM.NTrama();
            Negocio.GRL.NPaginaBD Datos;
            Datos = vl_cNegocio.BuscarDetalleTablaTemporal("123456789", this.lbIDTemporal.Text, ref vl_sMensajeError);
            if (Datos == null)
            {
                MyMaster.AlertaMostrar("No es posible realizar la búsqueda en este mometo, intente nuevamente.");
                return;
            }
            if (vl_sMensajeError == string.Empty)
            {
                this.lbTotalCampos.Text = Datos.DataSource.Rows.Count.ToString();
                gvCampos.DataSource = Datos.DataSource;
                gvCampos.DataBind();
                this.tbCamposTemporal.Visible = true;
            }
            else
                MyMaster.AlertaMostrar(vl_sMensajeError);
        }


        protected void gvCampos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Comprobante")
            //{
            //    char[] vl_cSeparador = { '|' };
            //    string[] vl_sDatos = e.CommandArgument.ToString().Split(vl_cSeparador);
            //    this.pnFiltroBusqueda.Visible = false;
            //    this.pnlResultadoBusqueda.Visible = false;
            //    this.pnSubirComprobante.Visible = true;
            //    this.lbAdmID.Text = vl_sDatos[0];
            //    this.lbAnho.Text = vl_sDatos[1];
            //    this.lbMes.Text = vl_sDatos[2];
            //    this.lbEstado.Text = vl_sDatos[3];
            //    this.lbMonto.Text = double.Parse(vl_sDatos[4]).ToString("C");
            //    this.tbDescripcion.Text = vl_sDatos[5];
            //}
            //else if (e.CommandName == "CuentaDeCobro")
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
            //    Response.TransmitFile(Server.MapPath("~/PDF/CuentaDeCobro.pdf"));
            //    Response.End();
            //}
        }

        protected void brCrearTrama_Click(object sender, EventArgs e)
        {
            if (this.tbTipoMensaje.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese el Indicador de Tipo de Mensaje (MTI).");
                return;
            }
            if (this.tbDescripcionTrama.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese la Descripción.");
                return;
            }
            if (this.gvCampos.Rows.Count == 0)
            {
                MyMaster.AlertaMostrar("La Trama no Tiene Campos Adicionados.");
                return;
            }
            string vl_sMensajeError = string.Empty;
            var vl_cNegocioTramas = new Negocio.ADM.NTrama();
            var vl_sDttID = vl_cNegocioTramas.CrearTrama("123456789", this.tbTipoMensaje.Text, this.tbDescripcionTrama.Text, this.lbIDTemporal.Text, ref vl_sMensajeError);
            if (vl_sMensajeError == string.Empty)
            {
                Regresar();
                CargarGrillaBusqueda(1);
                MyMaster.AlertaMostrar("Trama Creada Correctamente.", "OK");
            }
            else
                MyMaster.AlertaMostrar(vl_sMensajeError);
        }

        protected void btRegresarCrear_Click(object sender, EventArgs e)
        {
            Regresar();
        }

        private void Regresar()
        {
            this.pnFiltroBusqueda.Visible = true;
            if (this.gvTrama.Rows.Count > 0)
                this.pnResultadoBusqueda.Visible = true;
            else
                this.pnResultadoBusqueda.Visible = false;
            this.pnCrearTrama.Visible = false;
        }

        protected void cbCampoEstatico_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbCampoEstatico.Checked)
            {
                this.tbInformacionEstatica.Text = "";
                this.trInformacion.Visible = true;
            }
            else
            {
                this.trInformacion.Visible = false;
            }
        }

    }
}