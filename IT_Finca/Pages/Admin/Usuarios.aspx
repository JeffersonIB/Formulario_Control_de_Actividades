<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="IT_Usuario.Pages.Admin.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Administración de usuarios
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

        <div class="container box">
            <center>
                <h1 class="title">Administración de usuarios
                </h1>
            </center>
        </div>

        <!-- Modal Agregar-->
        <div class="modal fade" id="Modal_Agregar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar nuevo usuario
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <td>Empresa :
                                </td>
                                <td>
                                    <div class="control">
                                        <div class="select">
                                            <asp:DropDownList ID="ddlEmpresas" runat="server" AutoPosBack="true" CssClass="form-control" Style="width: auto;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Nombre :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Apellido :
                                     <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Usuario :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Contraseña :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtClave" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <table align="center">
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                    <asp:Button runat="server" ID="btnAgregar" class="btn btn-round btn-success" Text="Guardar" OnClick="Agregar_Click" />
                                    <asp:Button runat="server" ID="btnCancelar" class="btn btn-round btn-danger" Text="Cancelar" OnClientClick="CloseModalAg();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Agregar-->

        <!-- Modal Finca -->
        <div class="modal fade" id="Modal_Finca" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Asignar fincas
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="fnId_Usuario" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvUsuariosFincas" runat="server"
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
                                                    <asp:TemplateField HeaderText="Id_Finca" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvId_Finca" runat="server" Text='<%#Eval("Id_Finca") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Finca" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvFinca" runat="server" Text='<%#Eval("Finca") %>'></asp:Label>
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
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table align="center">
                            <tr>
                                <td align="center">
                                    <br />
                                    <asp:Button runat="server" ID="btnGuardarCambios" class="btn btn-round btn-success" Text="Guardar" OnClick="btnGuardarCambios_Click" />
                                    <asp:Button runat="server" ID="Cancelar" class="btn btn-round btn-danger" Text="Cancelar" OnClientClick="CloseModalFn();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Finca -->

        <!-- Modal Actualizar -->
        <div class="modal fade" id="Modal_Actualizar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Modificar datos de usuario
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lbId_Usuario" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Empresa :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddEmpresas" runat="server" AutoPosBack="true" CssClass="form-control" Style="width: auto;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nombre :
                                    <br />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txNombre" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Apellido :
                                    <br />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txApellido" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Usuario :
                                    <br />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txUsuario" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Contraseña :
                                    <br />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txClave" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table align="center">
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                    <asp:Button runat="server" ID="btActualizar" class="btn btn-round btn-success" Text="Guardar" OnClick="Actualizar_Click" />
                                    <asp:Button runat="server" ID="btCancelar" class="btn btn-round btn-danger" Text="Cancelar" OnClientClick="CloseModalAc();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Actualizar -->

        <!-- Modal Eliminar -->
        <div class="modal fade" id="Modal_Eliminar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">
                            <strong>Advertencia!
                            </strong>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lId_Usuario" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Está seguro de eliminar : 
                                       <strong>
                                           <asp:Label runat="server" ID="lUsuario" Visible="true"></asp:Label></strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <asp:Button runat="server" ID="bEliminar" class="btn btn-round btn-success" Text="Eliminar" OnClick="Eliminar_Click" />
                                    <asp:Button runat="server" ID="bCancelar" class="btn btn-round btn-danger" Text="Cancelar" OnClientClick="CloseModalEl();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Eliminar -->
        <br />
        <!-- Tabla de Usuarios -->
        <center>
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="Agregar" runat="server" class="btn btn-round btn-primary" Text="Nuevo usuario" OnClientClick="ShowModalAg();return false;" Style="width: auto;" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtBuscarUsuario" runat="server" placeholder="Buscar por usuario" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" Style="width: auto;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
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
                            EmptyDataRowStyle-ForeColor="Red"
                            OnRowCommand="gvUsuarios_OnRowCommand"
                            OnPageIndexChanging="gvUsuarios_PageIndexChanging">
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
                                <asp:TemplateField HeaderText="Nombre" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvNombre" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apellido" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvApellido" runat="server" Text='<%#Eval("Apellido") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Usuario" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Usuario" runat="server" Text='<%#Eval("Id_Usuario") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Usuario" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvUsuario" runat="server" Text='<%#Eval("Usuario") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clave" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvClave" runat="server" Text='<%#Eval("Clave") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fincas">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnFinca" runat="server" ImageUrl="~/Pages/Img/Fincas.png" CommandName='ShowModalFn' CommandArgument='<%#Eval("Id_Usuario") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Pages/Img/Editar.png" CommandName='ShowModalAc' CommandArgument='<%#Eval("Id_Usuario") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Pages/Img/Eliminar.png" CommandName='ShowModalEl' CommandArgument='<%#Eval("Id_Usuario") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </center>
    </body>
    </html>
</asp:Content>
