using System;
using System.Data;

namespace Negocio.GRL
{
    public class NFunciones
    {


        /// <summary>
        /// Revisa si hay errores en un DataSet
        /// </summary>
        /// <param name="pi_dsDatos">DataSet con las tablas a revisar</param>
        /// <param name="po_sGlsError">Mensaje de error</param>
        /// <returns>Fedex 20130228</returns>
        public string RevisaErrores(DataSet pi_dsDatos,
                                    ref string po_sGlsError)
        {
            if (pi_dsDatos == null)
                return "0";
            //Se busca en las tablas un campo "cod_error"
            foreach (DataTable vl_dtTable in pi_dsDatos.Tables)
            {
                if ((vl_dtTable.Rows.Count > 0) && (vl_dtTable.Columns.Count > 0))
                {
                    if (vl_dtTable.Columns[0].ColumnName == "cod_error")
                    {
                        po_sGlsError = vl_dtTable.Rows[0]["gls_error"].ToString();
                        return vl_dtTable.Rows[0]["cod_error"].ToString();
                    }
                }
            }
            return "0";
        }

    }
}
