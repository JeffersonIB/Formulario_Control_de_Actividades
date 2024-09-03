<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Usuarios_Fincas.aspx.cs" Inherits="IT_Finca.Pages.Admin.Usuarios_Fincas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <br />
    <br />
    <br />
    <asp:DropDownList ID="ddlFincas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:GridView ID="gvUsuariosFincas" runat="server"
        DataKeyNames="Id_Permiso"
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
        EmptyDataRowStyle-ForeColor="Red"
        OnRowCommand="gvUsuariosFincas_OnRowCommand"
        OnPageIndexChanging="gvUsuariosFincas_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="Id_Empresa" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="gvId_Empresa" runat="server" Text='<%#Eval("Id_Empresa") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Empresa" Visible="true">
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
            <asp:TemplateField HeaderText="Id_Permiso" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="gvId_Permiso" runat="server" Text='<%#Eval("Id_Permiso") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descripcion" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="gvDescripcion" runat="server" Text='<%#Eval("Descripcion") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Estado" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="gvEstado" runat="server" Text='<%#Eval("Estado") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- <asp:TemplateField HeaderText="Tipo Pago">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlTipo_Actividad" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
</asp:Content>
