<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="FormsV2_1.aspx.cs" Inherits="IT_Finca.Pages.Test.FormsV2_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formuario V2_2
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Formualario Versión 2
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Admin.js") %>"> </script>
        <script type="text/javascript">
            var color = 'White';
            // Este método devuelve la fila padre del objeto
            function getParentRow(obj) {
                do {
                    obj = obj.parentElement;
                } while (obj.tagName !== "TR");
                return obj;
            }
        </script>
        <style type="text/css">
            .scroll_checkboxes {
                height: 120px;
                width: 100%;
                padding: 5px;
                overflow: auto;
                border: 1px solid #ccc;
                display: block;
                padding: .375rem .75rem;
                font-size: 1rem;
                line-height: 1.5;
                color: #495057;
                background-color: #fff;
                background-clip: padding-box;
                border: 1px solid #ced4da;
                border-radius: .25rem;
                transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out
            }
            .scroll_checkboxess {
                height: 120px;
                width: 200px;
                padding: 5px;
                overflow: auto;
                border: 1px solid #ccc;
            }
            .FormText {
                FONT-SIZE: 11px;
                FONT-FAMILY: tahoma,sans-serif
            }
        </style>
    </head>
    <body>
        <%--<div class="modal-dialog modal-xl" role="document">--%>
            <%--<div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar actividades V2_1 <strong>"<asp:Label CssClass="navbar-link" runat="server" ID="lblFinca"> </asp:Label>"</strong>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">--%>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table align="center" Style="width: 100%;">
                                <tr>
                                    <td colspan="3">Lote
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotes_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">Proceso
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlProcesos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProcesos_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">Actividad 1
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlActividad1" runat="server" AutoPosBack="true" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Id proveedor
                                       <asp:TextBox ID="txtIdProveedor" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                    <td>Tipo de pago
                                        <asp:DropDownList ID="ddlTipo_Actividad" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                                    </td>
                                    <td>Cantidad
                                        <asp:TextBox ID="txtCantidad1" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="ml-auto">
                            <asp:Button ID="AgregarActividad1" runat="server" ValidationGroup="Validate" class="btn btn-round btn-primary" Text="Agregar" OnClick="AgregarActividad1_Click" />
                        </div>
                    </div>
                    <table Style="width: 100%;">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView
                                            ID="GridViewCalificaciones"
                                            runat="server"
                                            CssClass="mydatagrid"
                                            GridLines="both"
                                            GroupingEnabled="true"
                                            AllowPaging="true"
                                            HorizontalAlign="Center"
                                            ShowHeaderWhenEmpty="True"
                                            EmptyDataText="Sin registros"
                                            EmptyDataRowStyle-ForeColor="Red"
                                            RowStyle-CssClass="rows"
                                            PagerStyle-CssClass="pager"
                                            HeaderStyle-CssClass="header"
                                            AutoGenerateColumns="False"
                                            AutoGenerateEditButton="False"
                                            AutoGenerateDeleteButton="False"
                                            OnRowEditing="GridViewCalificaciones_RowEditing"
                                            OnRowDeleting="GridViewCalificaciones_RowDeleting"
                                            OnRowUpdating="GridViewCalificaciones_RowUpdating"
                                            OnRowCancelingEdit="GridViewCalificaciones_RowCancelingEdit">
                                            <Columns>
                                                <asp:BoundField DataField="GV_ddlLotes" HeaderText="GV ddlLotes" Visible="false" />
                                                <asp:BoundField DataField="GV_ddlProcesos" HeaderText="GV ddlProcesos" Visible="false" />
                                                <asp:BoundField DataField="GV_ddlActividad1" HeaderText="GV ddlActividad1" Visible="false" />
                                                <asp:BoundField DataField="GV_txtIdProveedor" HeaderText="Id proveedor" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="GV_ddlTipo_Actividad" HeaderText="Tipo actividad" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="GV_txtCantidad1" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                <asp:CommandField ShowEditButton="True" ButtonType="Button" HeaderText="Editar" EditText="Editar" ControlStyle-CssClass="btn btn-primary" />
                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Eliminar" DeleteText="Eliminar" ControlStyle-CssClass="btn btn-danger" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="ml-auto">
                                    <asp:Button ID="Insertar" runat="server" class="btn btn-round btn-success" Text="Insertar" ValidationGroup="Validate" OnClick="Insertar_Click" Visible="false" />
                                </div>
                            </td>
                        </tr>
                    </table>
                <%--</div>
            </div>--%>
        <%--</div>--%>
    </body>
    </html>
</asp:Content>
