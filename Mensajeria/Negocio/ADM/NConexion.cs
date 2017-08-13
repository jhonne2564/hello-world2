using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio.ADM
{
    public class NConexion : GRL.NBaseNegocio
    {

        public GRL.NPaginaBD BuscarConexion(string sesID,
                                            EConexion pi_eConexion,
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
                Param.AdicionarNodo("Tipo", "BuscarConexion");
                Param.AdicionarNodo("sesID", sesID);
                Param.AdicionarNodo("con_descripcion", pi_eConexion.con_descripcion);
                Param.AdicionarNodo("CampoOrden", pi_sCampoOrden);
                Param.AdicionarNodo("Ordenamiento", pi_sOrdenamiento);
                Param.AdicionarNodo("Pag", Pagina.ToString());
                Param.AdicionarNodo("RegPag", RegistrosPorPagina.ToString());
                objDS = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Conexion", Param.Generar(), ConString);
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

        public string CrearConexion(string pi_sSesID,
                                    EConexion pi_eConexion,
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
                Param.AdicionarNodo("Tipo", "CrearConexion");
                Param.AdicionarNodo("sesID", pi_sSesID);
                Param.AdicionarNodo("con_descripcion", pi_eConexion.con_descripcion);
                Param.AdicionarNodo("con_ip", pi_eConexion.con_ip);
                Param.AdicionarNodo("con_puerto", pi_eConexion.con_puerto);
                Param.AdicionarNodo("con_time_out_envio", pi_eConexion.con_time_out_envio);
                Param.AdicionarNodo("con_time_out_recepcion", pi_eConexion.con_time_out_recepcion);
                vl_dsRespuesta = vl_cAccesoDatos.EjecutaSPVarchar("trx", "sp_Conexion", Param.Generar(), ConString);
                if (ValidarSesion(vl_dsRespuesta))
                {
                    if (vl_cFunciones.RevisaErrores(vl_dsRespuesta, ref po_sMensajeError) == "0")
                    {
                        if (vl_dsRespuesta.Tables.Count > 1)
                        {
                            vl_sDttID = vl_dsRespuesta.Tables[1].Rows[0]["conID"].ToString();
                        }
                        else
                            po_sMensajeError = "No es posible crear la conexión en este momento, intente nuevamente.";
                    }
                }
                else
                    po_sMensajeError = "Sesión inválida";
            }
            catch (Exception vl_exException)
            {
                //vl_cLog.GrabaErrorCatch(vl_exException, pi_sSesID, "NUsuarioEmpresa.cs", "CrearUsuarioEmpresa");
                po_sMensajeError = "No es posible crear la conexión en este momento";
            }
            return vl_sDttID;
        }


    }
}
