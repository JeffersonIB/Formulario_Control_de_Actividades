<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="GRIDVIEW_CON_SELECCIONABLE.aspx.cs" Inherits="IT_Finca.Pages.Test.GRIDVIEW_CON_SELECCIONABLE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Administración de usuarios
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Admin.js") %>"> </script>
    </head>
    <body>
        <asp:GridView ID="gvUsuarios" runat="server"
            DataKeyNames="Id_Usuario"
            PageSize="17"
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
            EmptyDataText="No Records Found"
            EmptyDataRowStyle-ForeColor="Red">
            <Columns>
                <asp:TemplateField HeaderText="Id_Usuario" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="gvId_Usuario" runat="server" Text='<%#Eval("Id_Usuario") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Usuario" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="gvUsuario" runat="server" Text='<%#Eval("Usuario") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id_Finca" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="gvId_Finca" runat="server" Text='<%#Eval("Id_Finca") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Finca" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="gvFinca" runat="server" Text='<%#Eval("Finca") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="gvEstado" runat="server" Text='<%#Eval("Estado") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="checkbox">
            <label>
                <input type="checkbox" class="flat" checked="checked">
                Checked
            </label>
        </div>
    </body>
    </html>
</asp:Content>
