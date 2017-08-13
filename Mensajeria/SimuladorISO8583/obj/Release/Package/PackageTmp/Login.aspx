<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SimuladorISO8583.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="2">Autenticación
                    </td>
                </tr>
                <tr>
                    <td>Tipo de Identificación:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server">
                            <asp:ListItem>Cédula de Ciudadania</asp:ListItem>
                            <asp:ListItem>Nit</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Número de Identificación:</td>
                    <td>
                        <asp:TextBox ID="tbNumeroIdentificacion" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Clave:</td>
                    <td>
                        <asp:TextBox ID="tbClave" runat="server" TextMode="Password" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btIngresar" runat="server" Text="Ingresar" OnClick="btIngresar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
