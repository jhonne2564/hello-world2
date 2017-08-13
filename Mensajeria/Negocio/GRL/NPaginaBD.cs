using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Negocio.GRL
{
    public class NPaginaBD
    {

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int CantidadPaginas { get; set; }
        public int CantidadRegistros { get; set; }
        public int valorTotal { get; set; }
        public DataTable DataSource { get; set; }
        public DataTable DataSource1 { get; set; }

        public void ComboPagina(DropDownList drop)
        {
            int Pag = Pagina;
            int CanPag = CantidadPaginas;
            Paginador(Pag, CanPag, drop);
        }

        public void VerificarLink(LinkButton Primero,
                                  LinkButton Anterior,
                                  LinkButton Siguiente,
                                  LinkButton Ultimo)
        {
            if (Pagina == 1)
            {
                if (Primero != null) Primero.Enabled = false;
                if (Anterior != null) Anterior.Enabled = false;
                if (Siguiente != null) Siguiente.Enabled = true;
                if (Ultimo != null) Ultimo.Enabled = true;
            }
            else if (Pagina == CantidadPaginas)
            {
                if (Primero != null) Primero.Enabled = true;
                if (Anterior != null) Anterior.Enabled = true;
                if (Siguiente != null) Siguiente.Enabled = false;
                if (Ultimo != null) Ultimo.Enabled = false;
            }
            else
            {
                if (Primero != null) Primero.Enabled = true;
                if (Anterior != null) Anterior.Enabled = true;
                if (Siguiente != null) Siguiente.Enabled = true;
                if (Ultimo != null) Ultimo.Enabled = true;
            }
            if (CantidadPaginas == 1)
            {
                if (Primero != null) Primero.Enabled = false;
                if (Anterior != null) Anterior.Enabled = false;
                if (Siguiente != null) Siguiente.Enabled = false;
                if (Ultimo != null) Ultimo.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pag"></param>
        /// <param name="Final"></param>
        /// <param name="drop"></param>
        /// <returns></returns>
        public DropDownList Paginador(int Pag, int Final, DropDownList drop)
        {
            drop.Items.Clear();
            if (Final > 50)
            {
                int iRangoAnterior, iRangoPosterior, zDivision, zAvance = 0, zPagina = 0, zAnterior = 0, ultimoInsert = 0, intermedio = 0;
                iRangoAnterior = Pag - 1;
                if (iRangoAnterior < 0)
                    iRangoAnterior = 0;
                iRangoPosterior = Pag + 1;
                if (iRangoPosterior > Final)
                    iRangoPosterior = 0;
                zDivision = QuitaDecimal(Final / 4);
                for (int i = 1; i <= Final; i++)
                {
                    zAvance = zAvance + 1;
                    if (zAvance < 4)
                    {
                        drop.Items.Add(i.ToString() + " de " + Final.ToString());
                        drop.Items[drop.Items.Count - 1].Value = i.ToString();
                        ultimoInsert = i;
                    }
                    else if (zAvance == 4)
                    {
                        zAvance = 0;
                        zPagina = (i + zDivision) - (i) + i;
                        zAnterior = i;
                        i = zPagina;
                        if ((i >= iRangoAnterior && zAnterior <= iRangoAnterior) ||
                           (i >= Pag && zAnterior <= Pag) ||
                           (i >= iRangoPosterior && zAnterior <= iRangoPosterior))
                        {
                            if (zAnterior <= iRangoAnterior)
                            {
                                if (iRangoAnterior > 0)
                                {
                                    zPagina = zAnterior + QuitaDecimal(((iRangoAnterior - zAnterior) / 2));
                                    if (iRangoAnterior + 1 > 8)
                                    {
                                        if (zPagina != zAnterior)
                                        {
                                            drop.Items.Add("...." + (QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert).ToString() + "....");
                                            drop.Items[drop.Items.Count - 1].Value = (QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert).ToString();
                                        }
                                    }
                                    else
                                    {
                                        for (int o = 0; o < ultimoInsert + 1; o++)
                                        {
                                            drop.Items.Add(o.ToString() + " de " + Final.ToString());
                                            drop.Items[drop.Items.Count - 1].Value = o.ToString();
                                        }
                                    }
                                    drop.Items.Add(iRangoAnterior.ToString() + " de " + Final.ToString());
                                    drop.Items[drop.Items.Count - 1].Value = iRangoAnterior.ToString();
                                    ultimoInsert = iRangoAnterior;
                                    i = iRangoAnterior;
                                }
                            }
                            if (zAnterior <= Pag)
                            {
                                drop.Items.Add(Pag.ToString() + " de " + Final.ToString());
                                drop.Items[drop.Items.Count - 1].Value = Pag.ToString();
                                ultimoInsert = iRangoAnterior;
                                i = Pag;
                            }
                            if (zAnterior <= iRangoPosterior)
                            {
                                if (iRangoPosterior > 0)
                                {
                                    drop.Items.Add(iRangoPosterior.ToString() + " de " + Final.ToString());
                                    drop.Items[drop.Items.Count - 1].Value = iRangoPosterior.ToString();
                                    ultimoInsert = iRangoPosterior;
                                    i = iRangoPosterior;
                                }
                            }
                            zAvance = 3;
                        }
                        else if (i < Final)
                        {
                            drop.Items.Add("...." + (QuitaDecimal((i - ultimoInsert) / 2) + ultimoInsert).ToString() + "....");
                            drop.Items[drop.Items.Count - 1].Value = (QuitaDecimal((i - ultimoInsert) / 2) + ultimoInsert).ToString();
                            zAvance = 0;
                        }
                        else if (i > Final)
                        {
                            zPagina = zAnterior + QuitaDecimal(((Final - zAnterior) / 2));
                            if (zPagina == Final)
                            {
                                drop.Items.Add(zPagina.ToString() + " de " + Final.ToString());
                                drop.Items[drop.Items.Count - 1].Value = zPagina.ToString();
                                ultimoInsert = zPagina;
                            }
                            else
                            {
                                intermedio = QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert;
                                if (intermedio != ultimoInsert)
                                {
                                    if (intermedio - ultimoInsert == 1)
                                    {
                                        drop.Items.Add((QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert).ToString() + " de " + Final);
                                        drop.Items[drop.Items.Count - 1].Value = (QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert).ToString();
                                        ultimoInsert = QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert;
                                    }
                                    else
                                    {
                                        drop.Items.Add("...." + (QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert).ToString() + "....");
                                        drop.Items[drop.Items.Count - 1].Value = (QuitaDecimal((zPagina - ultimoInsert) / 2) + ultimoInsert).ToString();
                                    }
                                }
                            }
                            i = zPagina;
                        }
                        else if (i == Final)
                        {
                            drop.Items.Add(i.ToString() + " de " + Final.ToString());
                            drop.Items[drop.Items.Count - 1].Value = Final.ToString();
                        }
                    }
                }
            }
            else
            {
                for (int Col = 1; Col <= Final; Col++)
                {
                    drop.Items.Add(Col.ToString() + " de " + Final.ToString());
                    drop.Items[drop.Items.Count - 1].Value = Col.ToString();
                }
            }
            drop.SelectedValue = Pag.ToString();
            return drop;
        }

        /// <summary>
        /// Quita los decimales de un double un número decimal
        /// </summary>
        /// <param name="dNumero">Número a convertir</param>
        /// <returns>Número entero</returns>
        public int QuitaDecimal(double dNumero)
        {
            return int.Parse(Math.Round(dNumero, 0).ToString());
        }

    }
}
