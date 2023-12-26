<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_Partida.aspx.cs" Inherits="IT_Finca.Pages.Forms.Formulario_Partida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formuario partidas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Beneficio
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Cale.js") %>"> </script>
    </head>
    <body>
        <div class="container box">
            <center>
                <h1 class="title">Partidas
                </h1>
            </center>
        </div>
        <br />
        <!-- Tabla de Fincas -->
        <center>
            <table>
                <tr>
                    <td>
                        <div class="row">
                            <div class="item form-group">
                                <h5>No. partida</h5>
                            </div>
                            <div class="item form-group">
                                <asp:DropDownList ID="ddlPartida" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                            </div>
                            <%--<div class="ml-auto">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" />
                            </div>--%>
                        </div>
                    </td>
                </tr>
            </table>
            <table class="table table-striped jambo_table bulk_action">
                <thead>
                    <tr class="headings">
                        <th class="column-title">Descripción </th>
                        <th class="column-title">Cantidad </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="even pointer">
                        <td class="a-center ">Humedad</td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantHumedad" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Tipo de café </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantTipoCafe" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Pergamino (QQ) </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantPergamino" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Chibolita natural (QQ) </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantChibolita" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Segunda (QQ) </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantSegunda" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Natas (QQ) </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantNatas" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Flotes (QQ) </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantFlotes" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">Chibolita Seco / Verde (QQ) </td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantiChibolitaSecoVerde" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                    <tr class="even pointer">
                        <td class="a-center ">
                            <asp:DropDownList ID="ddlSacoJumbo" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList></td>
                        <td class="a-center ">
                            <asp:TextBox ID="CantSacoJumbo" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>
            <asp:Button Text="Confirmar" runat="server" OnClientClick="btn_Confir_Click" />
        </center>
    </body>
    </html>
</asp:Content>
