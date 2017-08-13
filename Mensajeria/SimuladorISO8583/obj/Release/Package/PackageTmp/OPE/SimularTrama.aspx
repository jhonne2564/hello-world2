<%@ Page Title="" Language="C#" MasterPageFile="~/GRL/Principal.Master" AutoEventWireup="true" CodeBehind="SimularTrama.aspx.cs" Inherits="SimuladorISO8583.OPE.SimularTrama" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvTrama.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
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
                                        <asp:GridView ID="gvTrama" runat="server" Width="100%" AutoGenerateColumns="False" OnSorting="gvTrama_Sorting"
                                            AllowSorting="True" CssClass="TablaGrilla">
                                            <RowStyle CssClass="CeldaGrilla"></RowStyle>
                                            <HeaderStyle CssClass="TituloGrilla"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbSeleccion" runat="server" onclick="RadioCheck(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID Trama" SortExpression="encID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEncID" runat="server" Text='<%# Bind("encID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Indicador de Tipo de Mensaje" SortExpression="enc_mti">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEncMTI" runat="server" Text='<%# Bind("enc_mti") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                <tr>
                                    <td style="height: 20px" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btSimular" runat="server" Text="Simular" OnClick="btSimular_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnSimular" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td style="width: 5px;" rowspan="129"></td>
                                    <td>Conexión:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlConexion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Indicador de Tipo de Mensaje (MTI):
                                    </td>
                                    <td>
                                        <asp:Label ID="lbMTI" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo2" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo2" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo2" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion2" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo3" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo3" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo3" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion3" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo4" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo4" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo4" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion4" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo5" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo5" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo5" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion5" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo6" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo6" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo6" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion6" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo7" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo7" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo7" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion7" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo8" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo8" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo8" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion8" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo9" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo9" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo9" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion9" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo10" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo10" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo10" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion10" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo11" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo11" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo11" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion11" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo12" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo12" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo12" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion12" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo13" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo13" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo13" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion13" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo14" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo14" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo14" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion14" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo15" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo15" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo15" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion15" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo16" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo16" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo16" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion16" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo17" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo17" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo17" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion17" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo18" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo18" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo18" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion18" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo19" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo19" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo19" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion19" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo20" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo20" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo20" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion20" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo21" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo21" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo21" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion21" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo22" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo22" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo22" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion22" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo23" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo23" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo23" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion23" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo24" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo24" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo24" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion24" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo25" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo25" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo25" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion25" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo26" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo26" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo26" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion26" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo27" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo27" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo27" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion27" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo28" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo28" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo28" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion28" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo29" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo29" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo29" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion29" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo30" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo30" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo30" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion30" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo31" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo31" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo31" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion31" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo32" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo32" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo32" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion32" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo33" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo33" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo33" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion33" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo34" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo34" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo34" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion34" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo35" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo35" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo35" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion35" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo36" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo36" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo36" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion36" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo37" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo37" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo37" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion37" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo38" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo38" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo38" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion38" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo39" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo39" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo39" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion39" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo40" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo40" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo40" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion40" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo41" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo41" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo41" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion41" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo42" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo42" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo42" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion42" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo43" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo43" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo43" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion43" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo44" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo44" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo44" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion44" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo45" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo45" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo45" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion45" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo46" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo46" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo46" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion46" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo47" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo47" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo47" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion47" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo48" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo48" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo48" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion48" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo49" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo49" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo49" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion49" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo50" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo50" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo50" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion50" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo51" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo51" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo51" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion51" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo52" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo52" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo52" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion52" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo53" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo53" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo53" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion53" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo54" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo54" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo54" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion54" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo55" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo55" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo55" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion55" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo56" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo56" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo56" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion56" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo57" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo57" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo57" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion57" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo58" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo58" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo58" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion58" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo59" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo59" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo59" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion59" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo60" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo60" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo60" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion60" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo61" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo61" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo61" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion61" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo62" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo62" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo62" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion62" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo63" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo63" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo63" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion63" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo64" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo64" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo64" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion64" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo65" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo65" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo65" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion65" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo66" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo66" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo66" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion66" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo67" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo67" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo67" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion67" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo68" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo68" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo68" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion68" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo69" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo69" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo69" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion69" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo70" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo70" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo70" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion70" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo71" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo71" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo71" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion71" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo72" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo72" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo72" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion72" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo73" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo73" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo73" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion73" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo74" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo74" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo74" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion74" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo75" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo75" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo75" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion75" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo76" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo76" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo76" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion76" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo77" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo77" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo77" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion77" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo78" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo78" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo78" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion78" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo79" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo79" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo79" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion79" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo80" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo80" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo80" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion80" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo81" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo81" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo81" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion81" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo82" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo82" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo82" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion82" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo83" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo83" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo83" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion83" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo84" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo84" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo84" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion84" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo85" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo85" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo85" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion85" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo86" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo86" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo86" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion86" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo87" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo87" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo87" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion87" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo88" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo88" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo88" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion88" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo89" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo89" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo89" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion89" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo90" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo90" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo90" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion90" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo91" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo91" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo91" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion91" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo92" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo92" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo92" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion92" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo93" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo93" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo93" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion93" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo94" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo94" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo94" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion94" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo95" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo95" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo95" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion95" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo96" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo96" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo96" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion96" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo97" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo97" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo97" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion97" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo98" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo98" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo98" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion98" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo99" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo99" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo99" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion99" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo100" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo100" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo100" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion100" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo101" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo101" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo101" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion101" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo102" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo102" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo102" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion102" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo103" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo103" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo103" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion103" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo104" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo104" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo104" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion104" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo105" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo105" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo105" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion105" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo106" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo106" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo106" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion106" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo107" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo107" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo107" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion107" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo108" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo108" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo108" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion108" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo109" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo109" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo109" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion109" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo110" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo110" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo110" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion110" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo111" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo111" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo111" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion111" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo112" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo112" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo112" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion112" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo113" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo113" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo113" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion113" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo114" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo114" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo114" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion114" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo115" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo115" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo115" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion115" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo116" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo116" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo116" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion116" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo117" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo117" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo117" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion117" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo118" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo118" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo118" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion118" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo119" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo119" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo119" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion119" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo120" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo120" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo120" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion120" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo121" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo121" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo121" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion121" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo122" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo122" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo122" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion122" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo123" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo123" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo123" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion123" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo124" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo124" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo124" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion124" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo125" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo125" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo125" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion125" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo126" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo126" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo126" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion126" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo127" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo127" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo127" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion127" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trCampo128" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lbNombreCampo128" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbCampo128" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDescripcion128" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="width: 5px;" rowspan="1"></td>
                                    <td>
                                        <asp:Button ID="btEnviar" runat="server" Text="Enviar" OnClick="btEnviar_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table id="tlEnvio" runat="server" visible="false" style="width: 100%">
                                <tr>
                                    <td style="width: 5px;" rowspan="3"></td>
                                    <td style="font-weight: bold;">Trama Envio</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="tbTrama" runat="server" TextMode="MultiLine" Width="100%" Height="89px" />
                                    </td>
                                </tr>
                            </table>
                            <table id="tlRespuesta" runat="server" visible="false" style="width: 100%">
                                <tr>
                                    <td style="width: 5px;" rowspan="3"></td>
                                    <td style="font-weight: bold;">Trama Respuesta</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="tbTramaRespuesta" runat="server" TextMode="MultiLine" Width="100%" Height="89px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tiempo de Respuesta: <asp:Label ID="lbTiempoRespuesta" runat="server" /> sec</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbEncID" runat="server" Visible="false" />
            <asp:Label ID="lbCampoOrden" Text="encID" runat="server" Visible="false" />
            <asp:Label ID="lbOrdenamiento" Text="DESC" runat="server" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
