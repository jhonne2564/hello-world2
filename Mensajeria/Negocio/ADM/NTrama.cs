using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio.ADM
{
    public class NTrama : GRL.NBaseNegocio
    {

        public GRL.NPaginaBD BuscarTrama(string sesID,
                                         EEncabezadoTrama pi_cEncabezadoTrama,
                                         string pi_sCampoOrden,
                                         string pi_sOrdenamiento,
                                         int Pagina,
                                         int RegistrosPorPagina,
                                         ref string po_sMensajeError)
        {
            //var vl_cErroresCatch = new GRL.NLog();
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            var Retorno = new GRL.NPaginaBD();
            try
            {
                DataSet objDS;
                Param.Limpiar();
                Param.AdicionarNodo("Tipo", "BuscarTrama");
                Param.AdicionarNodo("sesID", sesID);
                Param.AdicionarNodo("enc_mti", pi_cEncabezadoTrama.enc_mti);
                Param.AdicionarNodo("CampoOrden", pi_sCampoOrden);
                Param.AdicionarNodo("Ordenamiento", pi_sOrdenamiento);
                Param.AdicionarNodo("Pag", Pagina.ToString());
                Param.AdicionarNodo("RegPag", RegistrosPorPagina.ToString());
                Console.WriteLine("Conexión:"+ConString);
                objDS = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Tramas", Param.Generar(), ConString);
                if (ValidarSesion(objDS))
                {
                    if (objDS.Tables[1].Rows.Count == 0)
                        po_sMensajeError = "No se encontraron datos asociados a la búsqueda.";
                    else
                    {
                        Retorno.Pagina = Pagina;
                        Retorno.RegistrosPorPagina = RegistrosPorPagina;
                        Retorno.DataSource = objDS.Tables[1];
                        Retorno.CantidadRegistros = int.Parse(objDS.Tables[2].Rows[0]["CanReg"].ToString());
                        Retorno.CantidadPaginas = int.Parse(objDS.Tables[2].Rows[0]["CanPag"].ToString());
                    }
                }
                else
                    po_sMensajeError = "No es posible realizar la búsqueda en este momento, intente nuevamente.";
            }
            catch (Exception vl_exException)
            {
                //vl_cErroresCatch.GrabaErrorCatch(vl_exException, sesID, "NInscripcion.cs", "Buscar");
                po_sMensajeError = "No es posible realizar la búsqueda en este momento, intente nuevamente.";
            }
            return Retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesID"></param>
        /// <param name="pi_sGuidDetalleTramaTemp"></param>
        /// <param name="po_sMensajeError"></param>
        /// <returns></returns>
        public GRL.NPaginaBD BuscarDetalleTablaTemporal(string sesID,
                                                        string pi_sGuidDetalleTramaTemp,
                                                        ref string po_sMensajeError)
        {
            //var vl_cErroresCatch = new GRL.NLog();
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            var Retorno = new GRL.NPaginaBD();
            try
            {
                DataSet objDS;
                Param.Limpiar();
                Param.AdicionarNodo("Tipo", "BuscarDetalleTablaTemporal");
                Param.AdicionarNodo("sesID", sesID);
                Param.AdicionarNodo("dtt_guid", pi_sGuidDetalleTramaTemp);
                objDS = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Tramas", Param.Generar(), ConString);
                if (ValidarSesion(objDS))
                {
                    Retorno.DataSource = objDS.Tables[1];
                    if (Retorno.DataSource.Rows.Count == 0)
                        po_sMensajeError = "No se encontraron datos asociados a la búsqueda.";
                }
                else
                    po_sMensajeError = "No es posible realizar la búsqueda en este momento, intente nuevamente.";
            }
            catch (Exception vl_exException)
            {
                //vl_cErroresCatch.GrabaErrorCatch(vl_exException, sesID, "NInscripcion.cs", "Buscar");
               po_sMensajeError="No es posible realizar la búsqueda en este momento, intente nuevamente.";
            }
            return Retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sSesID"></param>
        /// <param name="pi_cUsuario"></param>
        /// <param name="po_sMensajeError"></param>
        /// <returns></returns>
        public string CrearDetalleTramaTemporal(string pi_sSesID,
                                                EDetalleTrama pi_cUsuario,
                                                ref string po_sMensajeError)
        {
            //var vl_cLog = new GRL.NLog();
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            DataSet vl_dsRespuesta = new DataSet();
            var vl_cFunciones = new GRL.NFunciones();
            string vl_sDttID = string.Empty;
            try
            {
                Param.Limpiar();
                Param.AdicionarNodo("Tipo", "CrearDetalleTramaTemporal");
                Param.AdicionarNodo("sesID", pi_sSesID);
                Param.AdicionarNodo("dtt_guid", pi_cUsuario.det_guid);
                Param.AdicionarNodo("dtt_campo", pi_cUsuario.det_campo);
                Param.AdicionarNodo("dtt_nombre", pi_cUsuario.det_nombre);
                Param.AdicionarNodo("dtt_descripcion", pi_cUsuario.det_descripcion);
                Param.AdicionarNodo("ddt_estatico", pi_cUsuario.det_estatico);
                Param.AdicionarNodo("ddt_estatico_informacion", pi_cUsuario.det_estatico_informacion);
                vl_dsRespuesta = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Tramas", Param.Generar(), ConString);
                if (ValidarSesion(vl_dsRespuesta))
                {
                    if (vl_cFunciones.RevisaErrores(vl_dsRespuesta, ref po_sMensajeError) == "0")
                    {
                        if (vl_dsRespuesta.Tables.Count > 1)
                        {
                            vl_sDttID = vl_dsRespuesta.Tables[1].Rows[0]["dttID"].ToString();
                        }
                        else
                            po_sMensajeError = "No es posible adicionar el campo en este momento, intente nuevamente.";
                    }
                }
                else
                    po_sMensajeError = "Sesión inválida";
            }
            catch (Exception vl_exException)
            {
                //vl_cLog.GrabaErrorCatch(vl_exException, pi_sSesID, "NUsuarioEmpresa.cs", "CrearUsuarioEmpresa");
                po_sMensajeError = "No es posible crear el campo en este momento";
            }
            return vl_sDttID;
        }

        public String EliminarTabla(string v, string id, ref string po_sMensajeError)
        {
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            DataSet vl_dsRespuesta = new DataSet();
            var vl_cFunciones = new GRL.NFunciones();
            string vl_sEncID = string.Empty;
            try
            {
                Param.Limpiar();
                Param.AdicionarNodo("Tipo", "EliminarTrama");
                Param.AdicionarNodo("sesID", v);
                Param.AdicionarNodo("encID", id);                
                vl_dsRespuesta = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Tramas", Param.Generar(), ConString);
                if (ValidarSesion(vl_dsRespuesta))
                {
                    if (vl_cFunciones.RevisaErrores(vl_dsRespuesta, ref po_sMensajeError) == "0")
                    {
                        if (vl_dsRespuesta.Tables.Count > 1)
                        {
                            vl_sEncID = vl_dsRespuesta.Tables[1].Rows[0]["encID"].ToString();
                        }
                        else
                            po_sMensajeError = "No es posible crear la trama este momento, intente nuevamente.";
                    }
                }
                else
                    po_sMensajeError = "Sesión inválida";
            }
            catch (Exception vl_exException)
            {
                //vl_cLog.GrabaErrorCatch(vl_exException, pi_sSesID, "NUsuarioEmpresa.cs", "CrearUsuarioEmpresa");
                po_sMensajeError = "No es posible crear la en este momento";
            }
            return vl_sEncID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sSesID"></param>
        /// <param name="pi_sMTI"></param>
        /// <param name="pi_sDescripcion"></param>
        /// <param name="pi_sGUID"></param>
        /// <returns></returns>
        public string CrearTrama(string pi_sSesID,
                                 string pi_sMTI,
                                 string pi_sDescripcion,
                                 string pi_sGUID,
                                 ref string po_sMensajeError)
        {
            //var vl_cLog = new GRL.NLog();
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            DataSet vl_dsRespuesta = new DataSet();
            var vl_cFunciones = new GRL.NFunciones();
            string vl_sEncID = string.Empty;
            try
            {
                Param.Limpiar();
                Param.AdicionarNodo("Tipo", "CrearTrama");
                Param.AdicionarNodo("sesID", pi_sSesID);
                Param.AdicionarNodo("enc_mti", pi_sMTI);
                Param.AdicionarNodo("enc_descripcion", pi_sDescripcion);
                Param.AdicionarNodo("dtt_guid", pi_sGUID);
                vl_dsRespuesta = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Tramas", Param.Generar(), ConString);
                if (ValidarSesion(vl_dsRespuesta))
                {
                    if (vl_cFunciones.RevisaErrores(vl_dsRespuesta, ref po_sMensajeError) == "0")
                    {
                        if (vl_dsRespuesta.Tables.Count > 1)
                        {
                            vl_sEncID = vl_dsRespuesta.Tables[1].Rows[0]["encID"].ToString();
                        }
                        else
                            po_sMensajeError = "No es posible crear la trama este momento, intente nuevamente.";
                    }
                }
                else
                    po_sMensajeError = "Sesión inválida";
            }
            catch (Exception vl_exException)
            {
                //vl_cLog.GrabaErrorCatch(vl_exException, pi_sSesID, "NUsuarioEmpresa.cs", "CrearUsuarioEmpresa");
                po_sMensajeError = "No es posible crear la en este momento";
            }
            return vl_sEncID;
        }

        public DataTable DetalleTrama(string pi_sSesID,
                                      string pi_sEncID,
                                      ref string po_sMensajeError)
        {
            //var vl_cLog = new GRL.NLog();
            var vl_cAccesoDatos = new AccesoDatos.EjecutaBaseDatos();
            DataSet vl_dsRespuesta = new DataSet();
            var vl_cFunciones = new GRL.NFunciones();
            DataTable vl_dtDetalleTrama = new DataTable();
            try
            {
                Param.Limpiar();
                Param.AdicionarNodo("Tipo", "DetalleTrama");
                Param.AdicionarNodo("sesID", pi_sSesID);
                Param.AdicionarNodo("encID", pi_sEncID);
                vl_dsRespuesta = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Tramas", Param.Generar(), ConString);
                if (ValidarSesion(vl_dsRespuesta))
                {
                    if (vl_cFunciones.RevisaErrores(vl_dsRespuesta, ref po_sMensajeError) == "0")
                    {
                        if (vl_dsRespuesta.Tables.Count > 1)
                        {
                            vl_dtDetalleTrama = vl_dsRespuesta.Tables[1];
                        }
                        else
                            po_sMensajeError = "No es posible consultar el detalle en este momento, intente nuevamente.";
                    }
                }
                else
                    po_sMensajeError = "Sesión inválida";
            }
            catch (Exception vl_exException)
            {
                //vl_cLog.GrabaErrorCatch(vl_exException, pi_sSesID, "NUsuarioEmpresa.cs", "CrearUsuarioEmpresa");
                po_sMensajeError = "No es posible consultar el detalle en este momento";
            }
            return vl_dtDetalleTrama;
        }

    }
}
