﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Ubicaciones.aspx.cs" Inherits="IT_Ubicacion.Pages.AdminCombustible.Ubicaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Ubicaciones
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Administración de ubicaciones
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Admin.js") %>"> </script>
    </head>
    <body>
        <div class="container box">
            <center>
                <h1 class="title">Administración de ubicaciones
                </h1>
            </center>
        </div>
        <!-- Modal Agregar-->
        <div class="modal fade" id="Modal_Agregar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar una nueva ubicación
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
                                <td>Ubicación :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtUbicacion" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>País :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPais" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Ciudad :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCiudad" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Dirección :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Teléfono :
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" MaxLength="20" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
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

        <!-- Modal Actualizar -->
        <div class="modal fade" id="Modal_Actualizar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Modificar datos de la ubicación
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbId_Ubicacion" Visible="false"></asp:Label>
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
                                <td>Ubicación :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txUbicacion" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>País :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txPais" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Ciudad :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txCiudad" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Dirección :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txDireccion" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Teléfono :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txTelefono" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                </td>
                            </tr>
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
                                    <asp:Label runat="server" ID="lId_Ubicacion" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Está seguro de eliminar : 
                                       <strong>
                                           <asp:Label runat="server" ID="lUbicacion" Visible="true"></asp:Label></strong>
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
        <!-- Tabla de Ubicaciones -->
        <center>
            <table>
                <tr>
                    <td align="center">
                        <asp:Button ID="Button1" runat="server" class="btn btn-round btn-primary" Text="Nueva ubicación" OnClientClick="ShowModalAg();return false;" Style="width: auto;"/>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtBuscarUbicacion" runat="server" placeholder="Buscar por Ubicacion" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" Style="width: auto;"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvUbicaciones" runat="server"
                            DataKeyNames="Id_Ubicacion"
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
                            EmptyDataText="Sin registros"
                            EmptyDataRowStyle-ForeColor="Red"
                            OnRowCommand="gvUbicaciones_OnRowCommand"
                            OnPageIndexChanging="gvUbicaciones_PageIndexChanging">
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
                                <asp:TemplateField HeaderText="Id_Ubicacion" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Ubicacion" runat="server" Text='<%#Eval("Id_Ubicacion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ubicación" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvUbicacion" runat="server" Text='<%#Eval("Ubicacion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="País" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvPais" runat="server" Text='<%#Eval("Pais") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ciudad" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvCiudad" runat="server" Text='<%#Eval("Ciudad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dirección" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvDireccion" runat="server" Text='<%#Eval("Direccion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teléfono" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="gvTelefono" runat="server" Text='<%#Eval("Telefono") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Usr_Crea" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Usr_Crea" runat="server" Text='<%#Eval("Id_Usr_Crea") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha_Crea" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvFecha_Crea" runat="server" Text='<%#Eval("Fecha_Crea") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha_Modifica" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvFecha_Modifica" runat="server" Text='<%#Eval("Fecha_Modifica") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha_Elimina" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvFecha_Elimina" runat="server" Text='<%#Eval("Fecha_Elimina") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvIsActive" runat="server" Text='<%#Eval("IsACtive") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Pages/Img/Editar.png" CommandName='ShowModalAc' CommandArgument='<%#Eval("Id_Ubicacion") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Pages/Img/Eliminar.png" CommandName='ShowModalEl' CommandArgument='<%#Eval("Id_Ubicacion") %>' Style="display: block; margin: 0 auto;" />
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
