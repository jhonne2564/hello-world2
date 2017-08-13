using System;
using System.Xml;

namespace Negocio.GRL
{
    public class NSPXml
    {

        XmlDocument vg_xParametros;

        /// <summary>
        /// Crea un XML
        /// </summary>
        /// <param name="pi_sTagPrincipal">Nombre del tag principal</param>
        /// <remarks>Fedex 20130213</remarks>
        public NSPXml(string pi_sTagPrincipal)
        {
            pi_sTagPrincipal = pi_sTagPrincipal.Replace(" ", "");
            vg_xParametros = new XmlDocument();
            vg_xParametros.LoadXml("<" + pi_sTagPrincipal + "></" + pi_sTagPrincipal + ">");
        }

        /// <summary>
        /// Adiciona o actualiza un nodo al tag principal
        /// </summary>
        /// <param name="pi_sRutaNodo">Ruta del nuevo nodo</param>
        /// <param name="pi_sValorNodo">Valor del nodo</param>
        /// <param name="pi_bActualizaNodo">Si es true actualiza el nodo en caso que exista de lo contrario crea uno nuevo</param>
        /// <remarks>Fedex 20130213</remarks>
        public void AdicionarNodo(string pi_sRutaNodo, string pi_sValorNodo, bool pi_bActualizaNodo = true)
        {
            pi_sRutaNodo = pi_sRutaNodo.Replace(" ", "");
            if ((vg_xParametros.DocumentElement.SelectNodes(pi_sRutaNodo).Count == 1) && (pi_bActualizaNodo))
            {
                vg_xParametros.DocumentElement.SelectSingleNode(pi_sRutaNodo).InnerText = pi_sValorNodo;
                return;
            }
            string[] vl_sNodos;
            vl_sNodos = pi_sRutaNodo.Split('/');
            if (vl_sNodos.Length == 1)
            {
                XmlNode vl_xnNode = vg_xParametros.DocumentElement;
                XmlElement vl_xeNodo = vg_xParametros.CreateElement(pi_sRutaNodo);
                vl_xeNodo.InnerText = pi_sValorNodo;
                vl_xnNode.AppendChild(vl_xeNodo);
            }
            else
            {
                AdicionaSubNodos(vl_sNodos);
                vg_xParametros.DocumentElement.SelectSingleNode(pi_sRutaNodo).InnerText = pi_sValorNodo;
            }
        }

        /// <summary>
        /// Adiciona sub nodo al tag principal
        /// </summary>
        /// <param name="pi_sSubNodos">Sub nodos a adicionar</param>
        /// <remarks>Fedex 20130213</remarks>
        private void AdicionaSubNodos(string[] pi_sSubNodos)
        {
            string vl_sRutaNodo = string.Empty;
            string vl_sNodoAnterior = string.Empty;
            for (int vl_iCantidadNodos = 0; vl_iCantidadNodos <= pi_sSubNodos.Length - 1; vl_iCantidadNodos++)
            {
                if (vl_iCantidadNodos == 0)
                    vl_sRutaNodo = pi_sSubNodos[vl_iCantidadNodos];
                else
                    vl_sRutaNodo += "/" + pi_sSubNodos[vl_iCantidadNodos];
                if (vg_xParametros.DocumentElement.SelectNodes(vl_sRutaNodo).Count == 0)
                {
                    XmlNode vl_xnNode;
                    XmlElement vl_xeNodo;
                    if (vl_iCantidadNodos == 0)
                    {
                        vl_xnNode = vg_xParametros.DocumentElement;
                        vl_xeNodo = vg_xParametros.CreateElement(vl_sRutaNodo);
                    }
                    else
                    {
                        vl_xnNode = vg_xParametros.DocumentElement.SelectSingleNode(vl_sNodoAnterior);
                        vl_xeNodo = vg_xParametros.CreateElement(pi_sSubNodos[vl_iCantidadNodos]);
                    }
                    vl_xnNode.AppendChild(vl_xeNodo);
                }
                if (vl_iCantidadNodos == 0)
                    vl_sNodoAnterior = pi_sSubNodos[vl_iCantidadNodos];
                else
                    vl_sNodoAnterior += "/" + pi_sSubNodos[vl_iCantidadNodos];
            }
        }

        /// <summary>
        /// Elimina nodo dentro del tag principal
        /// </summary>
        /// <param name="pi_sRutaNodo">Ruta del nodo a eliminar</param>
        /// <remarks>Fedex 20130213</remarks>
        public void EliminaNodo(string pi_sRutaNodo)
        {
            pi_sRutaNodo = pi_sRutaNodo.Replace(" ", "");
            if (vg_xParametros.DocumentElement.SelectNodes(pi_sRutaNodo).Count == 1)
            {
                XmlNode vl_xnNode;
                vl_xnNode = vg_xParametros.DocumentElement.SelectSingleNode(pi_sRutaNodo);
                vg_xParametros.DocumentElement.RemoveChild(vl_xnNode);
            }
        }

        /// <summary>
        /// Obtiene el valor de un nodo específico dentro del tag principal
        /// </summary>
        /// <param name="pi_sRutaNodo">Ruta del nodo a obtener información</param>
        /// <returns>Si el nodo existe retorna el valor del nodo de lo contrario retorna vacio</returns>
        /// <remarks>Fedex 20130213</remarks>
        public string ObtieneValorNodo(string pi_sRutaNodo)
        {
            string vl_sValorNodo = string.Empty;
            if (vg_xParametros.DocumentElement.SelectNodes(pi_sRutaNodo).Count == 1)
            {
                vl_sValorNodo = vg_xParametros.DocumentElement.SelectSingleNode(pi_sRutaNodo).InnerText;
            }
            return vl_sValorNodo;
        }

        /// <summary>
        /// Elimina todos los nodos dentro del tag principal
        /// </summary>
        /// <remarks>Fedex 20130213</remarks>
        public void Limpiar()
        {
            string vl_sTagPrincipal;
            vl_sTagPrincipal = vg_xParametros.DocumentElement.Name;
            vg_xParametros.LoadXml("<" + vl_sTagPrincipal + "></" + vl_sTagPrincipal + ">");
        }

        /// <summary>
        /// Genera el XML
        /// </summary>
        /// <returns>Devuelve el XML</returns>
        /// <remarks>Fedex 20130213</remarks>
        public string Generar()
        {
            return vg_xParametros.OuterXml;
        }

    }
}
