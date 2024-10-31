<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_Combustible.aspx.cs" Inherits="IT_Ubicacion.Pages.Forms.Formulario_Combustible" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Control de diesel y gasolina
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Cale.js") %>"> </script>
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
        <div class="container box">
            <center>
                <h1 class="title">Control de diesel y gasolina
                </h1>
            </center>
        </div>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table align="center" style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <h5>Tipo de combustible</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlId_TipoCombustible" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlId_TipoCombustible"
                                ErrorMessage="Campo obligatorio"
                                ForeColor="Red"
                                ValidationGroup="Validate"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h5>Centro de analisis</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlCentroAnalisis" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCentroAnalisis_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h5>Ubicación</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlUbicacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h5>Proceso</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlProcesos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProcesos_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h5>Centro de gasto</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlCentroGasto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCentroGasto_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h5>Clasificación</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlClasificacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClasificacion_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <table align="center" style="width: 100%;">
                    <tr>
                        <td>
                            <h5>Fecha : </h5>
                        </td>
                        <td>
                            <h5>No. Vale : </h5>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <input id="DateFecha" runat="server" class="date-picker form-control" placeholder="dd-mm-yyyy" type="text" required="required" onfocus="this.type='date'" onmouseover="this.type='date'" onclick="this.type='date'" onblur="this.type='text'" onmouseout="timeFunctionLong(this)">
                                <script>
                                    function timeFunctionLong(input) {
                                        setTimeout(function () {
                                            input.type = 'text';
                                        }, 60000);
                                    }
                                </script>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="NumVale" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>Kilometraje : </h5>
                        </td>
                        <td>
                            <h5>Cantidad consumo :</h5>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="NumKilometraje" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0" Style="width: 100%;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="NumCantidad" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>Comentario :</h5>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtComentario" runat="server" type="text" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="center" style="margin: 0px auto;">
            <tr>
                <td colspan="2" align="center">
                    <div class="row">
                        <div class="ml-auto">
                            <asp:Button ID="ButtonAgregarRegistros" runat="server" ValidationGroup="Validate" class="btn btn-round btn-primary" Text="Agregar" OnClick="AgregarRegistro_Click" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView
                                ID="GridViewRegistros"
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
                                OnRowDeleting="GridViewRegistros_RowDeleting"
                                Visible="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id_TipoCombustible" ItemStyle-CssClass="hidden-column" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_TipoCombustible" runat="server" Text='<%# Eval("Id_TipoCombustible") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Combustible" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lblTipoCombustible" runat="server" Text='<%# Eval("Id_TipoCombustible").ToString() == "1" ? "Gasolina" : Eval("Id_TipoCombustible").ToString() == "2" ? "Diesel" : "Otro" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id_CentroAnalisis" ItemStyle-CssClass="hidden-column" Visible="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_CentroAnalisis" runat="server" Text='<%# Eval("Id_CentroAnalisis") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id_Ubicacion" ItemStyle-CssClass="hidden-column" Visible="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Ubicacion" runat="server" Text='<%# Eval("Id_Ubicacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id_Proceso" ItemStyle-CssClass="hidden-column" Visible="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Proceso" runat="server" Text='<%# Eval("Id_Proceso") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id_CentroGasto" ItemStyle-CssClass="hidden-column" Visible="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_CentroGasto" runat="server" Text='<%# Eval("Id_CentroGasto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id_Clasificacion" ItemStyle-CssClass="hidden-column" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Clasificacion" runat="server" Text='<%# Eval("Id_Clasificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Fecha" runat="server" Text='<%# Eval("Fecha", "{0:dd/MM/yyyy}") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NoVale" ItemStyle-CssClass="hidden-column" Visible="True" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumNoVale" runat="server" Text='<%# Eval("NoVale") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kilometraje" ItemStyle-CssClass="hidden-column" Visible="True" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKilometraje" runat="server" Text='<%# Eval("Kilometraje") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-CssClass="hidden-column" Visible="True" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comentario" ItemStyle-CssClass="hidden-column" Visible="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComentario" runat="server" Text='<%# Eval("Comentario") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Eliminar" DeleteText="Eliminar" ControlStyle-CssClass="btn btn-danger" />
                                </Columns>
                            </asp:GridView>
                            </td>
                        </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
        </table>
        <table>
            <tr>
                <td>
                    <div class="ml-auto">
                        <asp:Button ID="Insertar" runat="server" class="btn btn-round btn-success" Text="Insertar" OnClick="Insertar_Click" Visible="false" />
                    </div>
                </td>
            </tr>
        </table>
    </body>
    </html>
</asp:Content>
