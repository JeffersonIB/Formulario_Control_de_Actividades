﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="CentroAnalisis.aspx.cs" Inherits="IT_Ubicacion.Pages.Admin.CentroAnalisis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Centro de análisis
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Administración de centro de análisis
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Admin.js") %>"> </script>
    </head>
    <body>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container box">
            <center>
                <h1 class="title">Administración de centro de análisis
                </h1>
            </center>
        </div>
        <!-- Modal Agregar-->
        <div class="modal fade" id="Modal_Agregar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar un nuevo centro de análisis
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table align="center">
                                    <tr>
                                        <td>Empresa :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlEmpresas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" CssClass="form-control" Style="width: auto;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>Ubicacion :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlUbicaciones" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicaciones_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td>Lote :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotes_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td>Proceso :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlProcesos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProcesos_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>Centro de análisis :
                                    <br />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCentroAnalisis" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <h5 class="modal-title">Modificar datos del centro de análisis
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lbId_CentroAnalisis" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Empresa :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddEmpresas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddEmpresas_SelectedIndexChanged" CssClass="form-control" Style="width: auto;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td>Ubicacion :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddUbicaciones" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddUbicaciones_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td>Lote :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddLotes_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td>Proceso :
                                        </td>
                                        <td>
                                            <div class="control">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddProcesos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddProcesos_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>Centro de análisis :
                                    <br />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txCentroAnalisis" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                    <asp:Label runat="server" ID="lId_CentroAnalisis" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Está seguro de eliminar : 
                                       <strong>
                                           <asp:Label runat="server" ID="lCentroAnalisis" Visible="true"></asp:Label>
                                       </strong>
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
                        <asp:Button ID="Button1" runat="server" class="btn btn-round btn-primary" Text="Nuevo centro de análisis" OnClientClick="ShowModalAg();return false;" Style="width: auto;"/>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtBuscarCentroAnalisis" runat="server" placeholder="Buscar por centro de análisis" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" Style="width: auto;"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvCentroAnalisis" runat="server"
                            DataKeyNames="Id_CentroAnalisis"
                            PageSize="17"
                            CssClass="mydatagrid"
                            PagerStyle-CssClass="pager"
                            HeaderStyle-CssClass="header"
                            RowStyle-CssClass="rows"
                            GridLines="both"
                            AllowPaging="true"
                            GroupingEnabled="true"
                            HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False"
                            EmptyDataText="Sin registros"
                            EmptyDataRowStyle-ForeColor="Red"
                            OnRowCommand="gvCentroAnalisis_OnRowCommand"
                            OnPageIndexChanging="gvCentroAnalisis_PageIndexChanging">
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
                               <%-- <asp:TemplateField HeaderText="Id_Ubicacion" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Ubicacion" runat="server" Text='<%#Eval("Id_Ubicacion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Ubicación" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvUbicacion" runat="server" Text='<%#Eval("Ubicacion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Id_Lote" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Lote" runat="server" Text='<%#Eval("Id_Lote") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Lote" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvLote" runat="server" Text='<%#Eval("Lote") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="Id_Proceso" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Proceso" runat="server" Text='<%#Eval("Id_Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                              <%--  <asp:TemplateField HeaderText="Proceso" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvProceso" runat="server" Text='<%#Eval("Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Id_CentroAnalisis" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_CentroAnalisis" runat="server" Text='<%#Eval("Id_CentroAnalisis") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cesntro de análisis" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvCentroAnalisis" runat="server" Text='<%#Eval("CentroAnalisis") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Pages/Img/Editar.png" CommandName="ShowModalAc" CommandArgument='<%#Eval("Id_CentroAnalisis") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Pages/Img/Eliminar.png" CommandName="ShowModalEl" CommandArgument='<%#Eval("Id_CentroAnalisis") %>' Style="display: block; margin: 0 auto;" />
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
