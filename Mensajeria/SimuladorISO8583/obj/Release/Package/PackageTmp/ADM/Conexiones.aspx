<%@ Page Title="" Language="C#" MasterPageFile="~/GRL/Principal.Master" AutoEventWireup="true" CodeBehind="Conexiones.aspx.cs" Inherits="SimuladorISO8583.ADM.Conexiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upPagina" runat="server">
        <ContentTemplate>
            <table style="width: 100%" class="Tabla">
                <tr>
                    <td>
                        <asp:Panel ID="pnFiltroBusqueda" runat="server">
                            <table>
                                <tr>
                                    <td colspan="3">Filtro de b&uacute;squeda</td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;" rowspan="2"></td>
                                    <td>Descripción:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbDescripcion" runat="server" MaxLength="100" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btBuscar" runat="server" Text="Buscar" OnClick="btBuscar_Click" />
                                        <asp:Button ID="btCrearConexion" runat="server" Text="Nueva Conexión" OnClick="btCrearConexion_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnResultadoBusqueda" runat="server" Visible="false">
                            <table class="Tabla" style="width: 100%; margin: auto;">
                                <tr>
                                    <td style="height: 20px" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Registro
                                        <asp:Label ID="lbRegistroInicial" runat="server" />
                                        al
                                        <asp:Label ID="lbRegistroFinal" runat="server" />
                                    </td>
                                    <td style="text-align: right;">Total Registros
                                        <asp:Label ID="lblTotalRegistros" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="gvConexion" runat="server" OnRowCommand="gvConexion_RowCommand" Width="100%"
                                            AutoGenerateColumns="False" OnSorting="gvConexion_Sorting" AllowSorting="True" CssClass="TablaGrilla">
                                            <RowStyle CssClass="CeldaGrilla"></RowStyle>
                                            <HeaderStyle CssClass="TituloGrilla"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundField HeaderText="Descripción" DataField="con_descripcion" SortExpression="con_descripcion" />
                                                <asp:BoundField HeaderText="IP" DataField="con_ip" SortExpression="con_ip" />
                                                <asp:BoundField HeaderText="Puerto" DataField="con_puerto" SortExpression="con_puerto" />
                                                <asp:BoundField HeaderText="Time Out Envio" DataField="con_time_out_envio" SortExpression="con_time_out_envio" />
                                                <asp:BoundField HeaderText="Time Out Recepción" DataField="con_time_out_recepcion" SortExpression="con_time_out_recepcion" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="2">
                                        <asp:LinkButton runat="server" ID="lnkPrimero" Text="Primero" OnClick="lnkBusqueda_Primero"
                                            CausesValidation="false" />
                                        <asp:LinkButton runat="server" ID="lnkAtras" Text="Atrás" OnClick="lnkBusqueda_Atras"
                                            CausesValidation="false" />
                                        <asp:DropDownList runat="server" ID="ddlPaginas" AutoPostBack="true" OnSelectedIndexChanged="ddlPaginas_SelectedIndexChanged" />
                                        <asp:LinkButton runat="server" ID="lnkSiguiente" Text="Siguiente" OnClick="lnkBusqueda_Siguiente"
                                            CausesValidation="false" />
                                        <asp:LinkButton runat="server" ID="lnkUltimo" Text="Último" OnClick="lnkBusqueda_Ultimo"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnCrearConexion" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td colspan="3">Nueva Conexión
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;" rowspan="7"></td>
                                    <td>Descripción:</td>
                                    <td>
                                        <asp:TextBox ID="tbDescripcionNueva" runat="server" MaxLength="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>IP:</td>
                                    <td>
                                        <asp:TextBox ID="tbIPNueva" runat="server" MaxLength="15" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Puerto:</td>
                                    <td>
                                        <asp:TextBox ID="tbPuerto" runat="server" MaxLength="5" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Time Out Envio (Sec):</td>
                                    <td>
                                        <asp:TextBox ID="tbTimeOutEnvioSec" runat="server" MaxLength="18" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Time Out Recepción (Sec):</td>
                                    <td>
                                        <asp:TextBox ID="tbTimeOutRecepcionSec" runat="server" MaxLength="18" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btCrearTrama" runat="server" Text="Crear" OnClick="btCrearTrama_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btRegresarNueva" runat="server" Text="Regresar" OnClick="btRegresarNueva_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbCampoOrden" Text="conID" runat="server" Visible="false" />
            <asp:Label ID="lbOrdenamiento" Text="DESC" runat="server" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
