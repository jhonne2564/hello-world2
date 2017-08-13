<%@ Page Title="" Language="C#" MasterPageFile="~/GRL/Principal.Master" AutoEventWireup="true" CodeBehind="IngenieriaInversa.aspx.cs" Inherits="SimuladorISO8583.OPE.IngenieriaInversa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upPagina" runat="server">
        <ContentTemplate>
            <table style="width: 100%" class="Tabla">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5px;" rowspan="3"></td>
                                <td style="font-weight: bold;">Trama</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="tbTrama" runat="server" TextMode="MultiLine" Width="100%" Height="89px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btIngenieriaInversa" runat="server" Text="Ingenieria Inversa" OnClick="btIngenieriaInversa_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Table ID="tlInversa" runat="server" Visible="false">
                            <asp:TableRow>
                                <asp:TableCell>Indicador de Tipo de Mensaje (MTI):</asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lbMTI" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
