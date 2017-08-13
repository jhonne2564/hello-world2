using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimuladorISO8583.ADM
{
    public partial class Conexiones : CLAS.PaginaBaseMasterPage
    {

        private int vg_iCantidadPaginasGrilla = int.Parse(ConfigurationManager.AppSettings["PAGS-GRILLA"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyMaster.Titulo = "Administración de Conexiones";
            }
        }

        protected void btCrearConexion_Click(object sender, EventArgs e)
        {
            this.pnFiltroBusqueda.Visible = false;
            this.pnResultadoBusqueda.Visible = false;
            this.pnCrearConexion.Visible = true;
        }

        public void CargarGrillaBusqueda(int pi_iPagina)
        {
            string vl_sMensajeError = string.Empty;
            var vl_cNegocio = new Negocio.ADM.NConexion();
            Negocio.GRL.NPaginaBD Datos;
            var vl_cDatos = new Entidades.EConexion();
            vl_cDatos.con_descripcion = this.tbDescripcion.Text;
            Datos = vl_cNegocio.BuscarConexion("123456789",
                                               vl_cDatos,
                                               this.lbCampoOrden.Text,
                                               this.lbOrdenamiento.Text,
                                               pi_iPagina,
                                               vg_iCantidadPaginasGrilla,
                                               ref vl_sMensajeError);
            if (vl_sMensajeError == string.Empty)
            {
                gvConexion.DataSource = Datos.DataSource;
                gvConexion.DataBind();
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

        protected void gvConexion_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvConexion_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void btCrearTrama_Click(object sender, EventArgs e)
        {
            if (this.tbDescripcionNueva.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese la Descripción");
                return;
            }
            if (this.tbIPNueva.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese la IP");
                return;
            }
            if (this.tbPuerto.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese el Puerto");
                return;
            }
            if (this.tbTimeOutEnvioSec.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese el Time Out de Envio");
                return;
            }
            if (this.tbTimeOutRecepcionSec.Text == "")
            {
                MyMaster.AlertaMostrar("Ingrese el Time Out de Recepción");
                return;
            }
            string vl_sMensajeError = string.Empty;
            var vl_cNegocioConexion = new Negocio.ADM.NConexion();
            var vl_eConexion = new Entidades.EConexion();
            vl_eConexion.con_descripcion = this.tbDescripcionNueva.Text;
            vl_eConexion.con_ip = this.tbIPNueva.Text;
            vl_eConexion.con_puerto = this.tbPuerto.Text;
            vl_eConexion.con_time_out_envio = this.tbTimeOutEnvioSec.Text;
            vl_eConexion.con_time_out_recepcion = this.tbTimeOutRecepcionSec.Text;
            var vl_sDttID = vl_cNegocioConexion.CrearConexion("123456789", vl_eConexion, ref vl_sMensajeError);
            if (vl_sMensajeError == string.Empty)
            {
                RegresarNueva();
                MyMaster.AlertaMostrar("Conexión Creada Correctamente.", "OK");
            }
            else
                MyMaster.AlertaMostrar(vl_sMensajeError);
        }

        protected void btRegresarNueva_Click(object sender, EventArgs e)
        {
            RegresarNueva();
        }

        private void RegresarNueva()
        {
            this.pnFiltroBusqueda.Visible = true;
            if (this.gvConexion.Rows.Count > 0)
                this.pnResultadoBusqueda.Visible = true;
            else
                this.pnResultadoBusqueda.Visible = false;
            this.pnCrearConexion.Visible = false;
        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(1);
        }

    }
}