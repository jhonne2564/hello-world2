using System.Data;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace AccesoDatos
{
    public class EjecutaBaseDatos
    {

        /// <summary>
        /// Ejecuta un Stored Procedure con parametro de entrada XML
        /// </summary>
        /// <param name="pi_sTabla">Nombre de las tablas a devolver en el DataSet</param>
        /// <param name="pi_sNombreSP">Nombre del Stored Procedure a ejecutar</param>
        /// <param name="pi_sDatos">XML de datos de entrada en el Stored Procedure</param>
        /// <param name="pi_sLLaveConexion">LLave del web.config del la cadena de conexion</param>
        /// <returns>DataSet con la información ejecutada por el Stored Procedure</returns>
        /// <remarks>Fedex 20130212</remarks>
        public DataSet EjecutaSPXml(string pi_sTabla, string pi_sNombreSP, string pi_sDatos, string pi_sLLaveConexion)
        {
            var vl_cBasedatos = new BaseDatos();
            DataSet vl_dsRespuesta;
            string vl_sCadenaConexion = ConfigurationManager.ConnectionStrings[pi_sLLaveConexion].ConnectionString;
            XDocument vl_xdDatos = XDocument.Load(new StringReader(pi_sDatos));
            vl_dsRespuesta = vl_cBasedatos.EjecutaSPXml(pi_sTabla, pi_sNombreSP, vl_xdDatos, vl_sCadenaConexion);
            return vl_dsRespuesta;
        }

        /// <summary>
        /// Ejecuta un Stored Procedure con parametro de entrada VARCHAR(MAX)
        /// </summary>
        /// <param name="pi_sTabla">Nombre de las tablas a devolver en el DataSet</param>
        /// <param name="pi_sNombreSP">Nombre del Stored Procedure a ejecutar</param>
        /// <param name="pi_sDatos">VARCHAR(MAX) de datos de entrada en el Stored Procedure</param>
        /// <param name="pi_sLLaveConexion">LLave del web.config del la cadena de conexion</param>
        /// <returns>DataSet con la información ejecutada por el Stored Procedure</returns>
        /// <remarks>Fedex 20130212</remarks>
        public DataSet EjecutaSPVarchar(string pi_sTabla, string pi_sNombreSP, string pi_sDatos, string pi_sLLaveConexion)
        {
            var vl_cBasedatos = new BaseDatos();
            DataSet vl_dsRespuesta;
            string vl_sCadenaConexion = ConfigurationManager.ConnectionStrings[pi_sLLaveConexion].ConnectionString;
            var a=""; System.Console.WriteLine("CONNECTION"+ vl_sCadenaConexion);
            XDocument vl_xdDatos = XDocument.Load(new StringReader(pi_sDatos));
            vl_dsRespuesta = vl_cBasedatos.EjecutaSPVarchar(pi_sTabla, pi_sNombreSP, vl_xdDatos, vl_sCadenaConexion);
            return vl_dsRespuesta;
        }

        /// <summary>
        /// Ejecuta un Stored Procedure con parametro de entrada XML (tipo,nombre y valor)
        /// </summary>
        /// <param name="pi_sTabla">Nombre de las tablas a devolver en el DataSet</param>
        /// <param name="pi_sNombreSP">Nombre del Stored Procedure a ejecutar</param>
        /// <param name="pi_sDatos">XML con los nodos contenidos de tipo,nombre y valor</param>
        /// <param name="pi_sLLaveConexion">LLave del web.config del la cadena de conexion</param>
        /// <returns>DataSet con la información ejecutada por el Stored Procedure</returns>
        /// <remarks>Fedex 20130212</remarks>
        public DataSet EjecutarDataSetSPParametros(string pi_sTabla, string pi_sNombreSP, string pi_sDatos, string pi_sLLaveConexion)
        {
            var vl_cBasedatos = new BaseDatos();
            DataSet vl_dsRespuesta;
            string vl_sCadenaConexion = ConfigurationManager.ConnectionStrings[pi_sLLaveConexion].ConnectionString;
            var vl_xDatos = new XmlDocument();
            vl_xDatos.LoadXml(pi_sDatos);
            vl_dsRespuesta = vl_cBasedatos.EjecutarDataSetSPParametros(pi_sTabla, pi_sNombreSP, vl_xDatos, vl_sCadenaConexion);
            return vl_dsRespuesta;
        }


        /// <summary>
        /// Ejecuta Bulk Insert
        /// </summary>
        /// <param name="DTOrigen">Datatable: DataTable donde se tiene la informacion</param>
        /// <param name="TablaDestino">String: Nombre de la tabal de destino donde va insertar la informacion</param>
        /// <param name="pi_sLLaveConexion">String: cadena de conexion</param>
        /// <returns>String: con informacion de carga</returns>
        /// <remarks>Ivan Garcia Avila</remarks>
        public string SubirDataTable(DataTable DTOrigen, string TablaDestino, string pi_sLLaveConexion)
        {
            var vl_cBasedatos = new BaseDatos();
            string vl_dsRespuesta;
            string vl_sCadenaConexion = ConfigurationManager.ConnectionStrings[pi_sLLaveConexion].ConnectionString;
            vl_dsRespuesta = vl_cBasedatos.SubirDataTable(DTOrigen, TablaDestino, vl_sCadenaConexion);
            return vl_dsRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sNombreSP"></param>
        /// <param name="pi_sTipo"></param>
        /// <param name="pi_sIdTabla"></param>
        /// <param name="pi_bArchivo"></param>
        /// <param name="pi_sLLaveConexion"></param>
        /// <returns>Fedex 20130903</returns>
        public bool GrabaArchivo(string pi_sSesID, string pi_sNombreSP, string pi_sTipo, string pi_sIdTabla, byte[] pi_bArchivo, string pi_sLLaveConexion)
        {
            var vl_cBasedatos = new BaseDatos();
            string vl_sCadenaConexion = ConfigurationManager.ConnectionStrings[pi_sLLaveConexion].ConnectionString;
            return vl_cBasedatos.GrabaArchivo(pi_sSesID, pi_sNombreSP, pi_sTipo, pi_sIdTabla, pi_bArchivo, vl_sCadenaConexion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sNombreSP"></param>
        /// <param name="pi_sTipo"></param>
        /// <param name="pi_sIdTabla"></param>
        /// <param name="pi_bArchivo"></param>
        /// <param name="pi_sLLaveConexion"></param>
        /// <returns>Fedex 20130903</returns>
        public DataSet GrabaArchivoTicket(string pi_sNombreSP, string pi_sSesID, string pi_sNombre, string pi_sCapa, string pi_sTipo, byte[] pi_bArchivo, string pi_sLLaveConexion)
        {
            var vl_cBasedatos = new BaseDatos();
            string vl_sCadenaConexion = ConfigurationManager.ConnectionStrings[pi_sLLaveConexion].ConnectionString;
            return vl_cBasedatos.GrabaArchivoTicket(pi_sNombreSP, pi_sSesID, pi_sNombre, pi_sCapa, pi_sTipo, pi_bArchivo, vl_sCadenaConexion);
        }

    }
}
