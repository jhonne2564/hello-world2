using System;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace AccesoDatos
{
    public class BaseDatos
    {

        /// <summary>
        /// Ejecuta un Stored Procedure con parametro de entrada XML
        /// </summary>
        /// <param name="pi_sTabla">Nombre de las tablas a devolver en el DataSet</param>
        /// <param name="pi_sNombreSP">Nombre del Stored Procedure a ejecutar</param>
        /// <param name="pi_xdDatos">XML de datos de entrada en el Stored Procedure</param>
        /// <param name="pi_sCadenaConexion">Cadena de conexión a la base de datos</param>
        /// <returns>DataSet con la información ejecutada por el Stored Procedure</returns>
        /// <remarks>Fedex 20130212</remarks>
        public DataSet EjecutaSPXml(string pi_sTabla, string pi_sNombreSP, XDocument pi_xdDatos, string pi_sCadenaConexion)
        {
            var topics = new DataSet();
            var objCommand = new System.Data.SqlClient.SqlCommand(pi_sNombreSP);
            var adapter = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@XMLDatos", SqlDbType.Xml);
                if (pi_xdDatos.Declaration == null)
                    objCommand.Parameters["@XMLDatos"].Value = pi_xdDatos.ToString();
                else
                    objCommand.Parameters["@XMLDatos"].Value = pi_xdDatos.Declaration.ToString() + pi_xdDatos.ToString();
                //objCommand.CommandTimeout = _timeout;
                using (objCommand.Connection = new System.Data.SqlClient.SqlConnection(pi_sCadenaConexion))
                {
                    objCommand.Connection.Open();
                    adapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
                    adapter.Fill(topics, pi_sTabla);
                    objCommand.Connection.Close();
                    objCommand.Parameters.Clear();
                    return topics;
                }
            }
            catch
            {
                return topics;
            }
            finally
            {
                adapter.Dispose();
                topics.Dispose();
                objCommand.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta un Stored Procedure con parametro de entrada VARCHAR(MAX)
        /// </summary>
        /// <param name="pi_sTabla">Nombre de las tablas a devolver en el DataSet</param>
        /// <param name="pi_sNombreSP">Nombre del Stored Procedure a ejecutar</param>
        /// <param name="pi_xdDatos">VARCHAR(MAX) de datos de entrada en el Stored Procedure</param>
        /// <param name="pi_sCadenaConexion">Cadena de conexión a la base de datos</param>
        /// <returns>DataSet con la información ejecutada por el Stored Procedure</returns>
        /// <remarks>Fedex 20130212</remarks>
        public DataSet EjecutaSPVarchar(string pi_sTabla, string pi_sNombreSP, XDocument pi_xdDatos, string pi_sCadenaConexion)
        {
            var topics = new DataSet();
            var objCommand = new System.Data.SqlClient.SqlCommand(pi_sNombreSP);
            var adapter = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@StrXMLDatos", SqlDbType.VarChar);
                if (pi_xdDatos.Declaration == null)
                    objCommand.Parameters["@StrXMLDatos"].Value = pi_xdDatos.ToString();
                else
                    objCommand.Parameters["@StrXMLDatos"].Value = pi_xdDatos.Declaration.ToString() + pi_xdDatos.ToString();
                //objCommand.CommandTimeout = _timeout;
                using (objCommand.Connection = new System.Data.SqlClient.SqlConnection(pi_sCadenaConexion))
                {
                    objCommand.Connection.Open();
                    adapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
                    adapter.Fill(topics, pi_sTabla);
                    objCommand.Connection.Close();
                    objCommand.Parameters.Clear();
                    return topics;
                }
            }
            catch(Exception vl_exExceptio)
            {
                //Console.WriteLine(vl_exExceptio.GetBaseException);
                return topics;
            }
            finally
            {
                adapter.Dispose();
                topics.Dispose();
                objCommand.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta un Stored Procedure con parametro de entrada XML
        /// </summary>
        /// <param name="pi_sTabla">Nombre de las tablas a devolver en el DataSet</param>
        /// <param name="pi_sNombreSP">Nombre del Stored Procedure a ejecutar</param>
        /// <param name="pi_xdDatos">Parametros en formato '<Datos><Tipo></Tipo><Nombre></Nombre><Valor></Valor></Datos>'</param>
        /// <param name="pi_sCadenaConexion">Cadena de conexión a la base de datos</param>
        /// <returns>DataSet con la información ejecutada por el Stored Procedure</returns>
        /// <remarks>Fedex 20130212</remarks>
        public DataSet EjecutarDataSetSPParametros(string pi_sTabla,
                                                   string pi_sNombreSP, XmlDocument pi_xdDatos, string pi_sCadenaConexion)
        {
            var objCommand = new System.Data.SqlClient.SqlCommand(pi_sNombreSP);
            var topics = new DataSet();
            var adapter = new System.Data.SqlClient.SqlDataAdapter();
            string vl_sTipo;
            string vl_sNombre;
            string vl_sValor;
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                //objCommand.CommandTimeout = _timeout;
                try
                {
                    foreach (XmlNode vl_xNode in pi_xdDatos.DocumentElement)
                    {
                        vl_sTipo = vl_xNode.SelectSingleNode("Tipo").InnerText;
                        vl_sNombre = vl_xNode.SelectSingleNode("Nombre").InnerText;
                        vl_sValor = vl_xNode.SelectSingleNode("Valor").InnerText;
                        if (vl_sTipo == "VARCHAR")
                        {
                            objCommand.Parameters.Add(vl_sNombre, SqlDbType.VarChar);
                            objCommand.Parameters[vl_sNombre].Value = vl_sValor;
                        }
                        else if (vl_sTipo == "INT")
                        {
                            objCommand.Parameters.Add(vl_sNombre, SqlDbType.Int);
                            objCommand.Parameters[vl_sNombre].Value = vl_sValor;
                        }
                        else if (vl_sTipo == "DECIMAL")
                        {
                            objCommand.Parameters.Add(vl_sNombre, SqlDbType.Decimal);
                            objCommand.Parameters[vl_sNombre].Value = vl_sValor;
                        }
                        else if (vl_sTipo == "TEXT")
                        {
                            objCommand.Parameters.Add(vl_sNombre, SqlDbType.Text);
                            objCommand.Parameters[vl_sNombre].Value = vl_sValor;
                        }
                        else if (vl_sTipo == "XML")
                        {
                            objCommand.Parameters.Add(vl_sNombre, SqlDbType.Xml);
                            objCommand.Parameters[vl_sNombre].Value = vl_sValor;
                        }
                    }
                    using (objCommand.Connection = new System.Data.SqlClient.SqlConnection(pi_sCadenaConexion))
                    {
                        objCommand.Connection.Open();
                        adapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
                        adapter.Fill(topics, pi_sTabla);
                        objCommand.Connection.Close();
                        objCommand.Parameters.Clear();
                        return topics;
                    }
                }
                catch
                {
                    return topics;
                }
            }
            catch
            {
                return topics;
            }
            finally
            {
                objCommand.Dispose();
                adapter.Dispose();
                topics.Dispose();
            }
        }



        /// <summary>
        /// Ejecuta Bulk Insert
        /// </summary>
        /// <param name="DTOrigen">Datatable: DataTable donde se tiene la informacion</param>
        /// <param name="TablaDestino">String: Nombre de la tabal de destino donde va insertar la informacion</param>
        /// <param name="pi_sLLaveConexion">String: cadena de conexion</param>
        /// <returns>String: con informacion de carga</returns>
        /// <remarks>Ivan Garcia Avila</remarks>
        public string SubirDataTable(DataTable DTOrigen, string TablaDestino, string ConnectionString)
        {
            try
            {
                using (System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(ConnectionString, System.Data.SqlClient.SqlBulkCopyOptions.KeepNulls))
                {
                    bulkCopy.DestinationTableName = TablaDestino;
                    bulkCopy.BulkCopyTimeout = 0;
                    try
                    {
                        bulkCopy.WriteToServer(DTOrigen);
                        return "OK";
                    }
                    catch (Exception ex)
                    {
                        return "NOK:" + ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "NOK:" + ex.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sNombreSP"></param>
        /// <param name="pi_sTipo"></param>
        /// <param name="pi_sIdTabla"></param>
        /// <param name="pi_bArchivo"></param>
        /// <param name="pi_sCadenaConexion"></param>
        /// <returns>Fedex 20130903</returns>
        public bool GrabaArchivo(string pi_sSesID, string pi_sNombreSP, string pi_sTipo, string pi_sIdTabla, byte[] pi_bArchivo, string pi_sCadenaConexion)
        {
            bool vl_bRespuesta = false;
            var objCommand = new System.Data.SqlClient.SqlCommand(pi_sNombreSP);
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@sesID", SqlDbType.VarChar);
                objCommand.Parameters["@sesID"].Value = pi_sSesID;
                objCommand.Parameters.Add("@Tipo", SqlDbType.VarChar);
                objCommand.Parameters["@Tipo"].Value = pi_sTipo;
                objCommand.Parameters.Add("@Archivo", SqlDbType.VarBinary);
                objCommand.Parameters["@Archivo"].Value = pi_bArchivo; //System.Text.Encoding.Default.GetString(pi_bArchivo);//pi_bArchivo.ToString();
                //string a = System.Text.Encoding.Default.GetString(pi_bArchivo);
                //byte[] bytes = new byte[a.Length * sizeof(char)];
                //System.Buffer.BlockCopy(a.ToCharArray(), 0, bytes, 0, bytes.Length);
                //System.IO.File.WriteAllBytes("C:\\Fedex007.pdf", bytes);
                //objCommand.CommandTimeout = _timeout;
                using (objCommand.Connection = new System.Data.SqlClient.SqlConnection(pi_sCadenaConexion))
                {
                    objCommand.Connection.Open();
                    objCommand.ExecuteNonQuery();
                    objCommand.Connection.Close();
                    objCommand.Parameters.Clear();
                    vl_bRespuesta = true;
                }
            }
            catch
            {

            }
            finally
            {
                objCommand.Dispose();
            }
            return vl_bRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_sNombreSP"></param>
        /// <param name="pi_sTipo"></param>
        /// <param name="pi_sIdTabla"></param>
        /// <param name="pi_bArchivo"></param>
        /// <param name="pi_sCadenaConexion"></param>
        /// <returns>Fedex 20130903</returns>
        public DataSet GrabaArchivoTicket(string pi_sNombreSP, string pi_sSesID, string pi_sNombre, string pi_sCapa, string pi_sTipo, byte[] pi_bArchivo, string pi_sCadenaConexion)
        {
            DataSet vl_dsRespuesta = new DataSet();
            var adapter = new System.Data.SqlClient.SqlDataAdapter();
            var objCommand = new System.Data.SqlClient.SqlCommand(pi_sNombreSP);
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@sesID", SqlDbType.VarChar);
                objCommand.Parameters["@sesID"].Value = pi_sSesID;
                objCommand.Parameters.Add("@TipoArchivo", SqlDbType.VarChar);
                objCommand.Parameters["@TipoArchivo"].Value = pi_sTipo;
                objCommand.Parameters.Add("@NombreArchivo", SqlDbType.VarChar);
                objCommand.Parameters["@NombreArchivo"].Value = pi_sNombre;
                objCommand.Parameters.Add("@Archivo", SqlDbType.VarBinary);
                objCommand.Parameters["@Archivo"].Value = pi_bArchivo;
                objCommand.Parameters.Add("@Capa", SqlDbType.VarChar);
                objCommand.Parameters["@Capa"].Value = pi_sCapa;
                using (objCommand.Connection = new System.Data.SqlClient.SqlConnection(pi_sCadenaConexion))
                {
                    objCommand.Connection.Open();
                    adapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
                    adapter.Fill(vl_dsRespuesta, "trx");
                    objCommand.Connection.Close();
                    objCommand.Parameters.Clear();
                }
            }
            catch
            {

            }
            finally
            {
                adapter.Dispose();
                objCommand.Dispose();
            }
            return vl_dsRespuesta;
        }

    }
}
