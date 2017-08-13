<%@ Page Title="" Language="C#" MasterPageFile="~/GRL/Principal.Master" AutoEventWireup="true" CodeBehind="Tramas.aspx.cs" Inherits="SimuladorISO8583.ADM.Tramas" %>

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
                                    <td>Indicador de Tipo de Mensaje (MTI):
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbMTI" runat="server" MaxLength="4" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btBuscar" runat="server" Text="Buscar" OnClick="btBuscar_Click" />
                                        <asp:Button ID="btCrearTrama" runat="server" Text="Nueva Trama" OnClick="btCrearTrama_Click" />
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
                                        <asp:GridView ID="gvTrama" runat="server" OnRowCommand="gvTrama_RowCommand" Width="100%"
                                            AutoGenerateColumns="False" OnSorting="gvTrama_Sorting" AllowSorting="True" CssClass="TablaGrilla">
                                            <RowStyle CssClass="CeldaGrilla"></RowStyle>
                                            <HeaderStyle CssClass="TituloGrilla"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundField HeaderText="ID Trama" DataField="encID" SortExpression="encID" />
                                                <asp:BoundField HeaderText="Indicador de Tipo de Mensaje" DataField="enc_mti" SortExpression="enc_mti" />
                                                <asp:BoundField HeaderText="Descripción" DataField="enc_descripcion" SortExpression="enc_descripcion" />
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
                        <asp:Panel ID="pnCrearTrama" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td colspan="3">Nueva Trama
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;" rowspan="3"></td>
                                    <td>Indicador de Tipo de Mensaje (MTI):</td>
                                    <td>
                                        <asp:TextBox ID="tbTipoMensaje" runat="server" MaxLength="4" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Descripción:</td>
                                    <td>
                                        <asp:TextBox ID="tbDescripcionTrama" runat="server" MaxLength="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset>
                                            <legend>Adicionar Campo</legend>
                                            <table>
                                                <tr>
                                                    <td>Campo:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbCampo" runat="server" MaxLength="3" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbtbCampo" runat="server"
                                                            TargetControlID="tbCampo" FilterType="Numbers" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Nombre:</td>
                                                    <td>
                                                        <asp:TextBox ID="tbNombreCampo" runat="server" MaxLength="100" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="cbCampoEstatico" runat="server" AutoPostBack="true" Text="Valor Estático" OnCheckedChanged="cbCampoEstatico_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr id="trInformacion" runat="server" visible="false">
                                                    <td>Valor:</td>
                                                    <td>
                                                        <asp:TextBox ID="tbInformacionEstatica" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Descripción:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbDescripcionCampo" runat="server" TextMode="MultiLine" MaxLength="200" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btAdicionarBit" runat="server" Text="Adicionar Campo" OnClick="btAdicionarBit_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table id="tbCamposTemporal" runat="server" class="Tabla" style="width: 100%; margin: auto;">
                                <tr>
                                    <td style="height: 20px"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Total de Campos
                                        <asp:Label ID="lbTotalCampos" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvCampos" runat="server" OnRowCommand="gvCampos_RowCommand" Width="100%"
                                            AutoGenerateColumns="False" AllowSorting="False" CssClass="TablaGrilla">
                                            <RowStyle CssClass="CeldaGrilla"></RowStyle>
                                            <HeaderStyle CssClass="TituloGrilla"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundField HeaderText="Campo" DataField="dtt_campo" />
                                                <asp:BoundField HeaderText="Nombre" DataField="dtt_nombre" />
                                                <asp:BoundField HeaderText="Valor Estatico" DataField="ddt_estatico_informacion" />
                                                <asp:BoundField HeaderText="Descripción" DataField="dtt_descripcion" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="height: 20px" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;" rowspan="2"></td>
                                    <td>
                                        <asp:Button ID="brCrearTrama" runat="server" Text="Crear Trama" OnClick="brCrearTrama_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btRegresarCrear" runat="server" Text="Regresar" OnClick="btRegresarCrear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbIDTemporal" runat="server" Visible="false" />
            <asp:Label ID="lbCampoOrden" Text="encID" runat="server" Visible="false" />
            <asp:Label ID="lbOrdenamiento" Text="DESC" runat="server" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
