<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="UsuariosAccesos.aspx.cs" Inherits="IT_Finca.Pages.Admin.UsuariosAccesos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Accesos a usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Accesos a usuarios
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Admin.js") %>"> </script>
    </head>
    <body>
        <div class="container box">
            <center>
                <h1 class="title">Accesos a usuarios
                </h1>
            </center>
        </div>
        <!-- Modal Acceso -->
        <center>
            <br />
            <br />
            <table>
                <tr>
                    <td><h5>Seleccionar usuario</h5></td>
                    <td align="center">
                        <asp:DropDownList ID="ddlUsuarios" runat="server" AutoPostBack="false" CssClass="form-control" Style="width: 100%;">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" Style="width: auto;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvAccesosUsuarios"
                            runat="server"
                            DataKeyNames="Id_Usuario"
                            PageSize="47"
                            GridLines="both"
                            AllowPaging="true"
                            CssClass="mydatagrid"
                            PagerStyle-CssClass="pager"
                            HeaderStyle-CssClass="header"
                            RowStyle-CssClass="rows"
                            GroupingEnabled="true"
                            HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False"
                            EmptyDataText="Sin datos"
                            EmptyDataRowStyle-ForeColor="Red">
                            <Columns>
                                <asp:TemplateField HeaderText="Id_Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Empresa" runat="server" Text='<%#Eval("Id_Empresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvEmpresa" runat="server" Text='<%#Eval("Empresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Usuario" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Usuario" runat="server" Text='<%#Eval("Id_Usuario") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvNombre" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Usuario" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvUsuario" runat="server" Text='<%#Eval("Usuario") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Permiso" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Permiso" runat="server" Text='<%#Eval("Id_Permiso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NombrePag" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvNombrePag" runat="server" Text='<%#Eval("NombrePag") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Url" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvUrl" runat="server" Text='<%#Eval("Url") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvMenu" runat="server" Text='<%#Eval("Menu") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Página" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvPagina" runat="server" Text='<%#Eval("Pagina") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado" Visible="true">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="gvEstado" runat="server" Checked='<%# Eval("Estado").ToString() == "True" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <br />
                        <asp:Button runat="server" ID="Guardar" class="btn btn-round btn-success" Text="Guardar" OnClick="btnGuardarCambios_Click" Visible="false" />
                        <asp:Button runat="server" ID="Cancelar" class="btn btn-round btn-danger" Text="Cancelar" OnClientClick="CloseModalAcc();return false;" Visible="false" />
                    </td>
                </tr>
            </table>
    </body>
    </html>
</asp:Content>
