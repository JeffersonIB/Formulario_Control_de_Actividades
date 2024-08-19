<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="ControlDieselGasolina.aspx.cs" Inherits="IT_Finca.Pages.Forms.ControlDieselGasolina" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Control de Diesel y Gasolina
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table align="center" style="width: 100%;">
                    <tr>
                        <td colspan="2">Finca
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlFincas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFincas_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlFincas"
                                ErrorMessage="Campo obligatorio"
                                ForeColor="Red"
                                ValidationGroup="Validate"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Lote
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
                        <td colspan="3">Centro de gasto
                        <div class="control">
                            <div class="select">
                                <asp:DropDownList ID="ddlCentroGasto" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlCentroGasto_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">Clasificación
                        <div class="control">
                            <div class="select">
                                <asp:DropDownList ID="ddlClasificacion" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlClasificacion_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">Tipo
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlTipo" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlTipo_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>Fecha : 
                        </td>
                        <td>No. Vale : 
                        </td>
                        <td>Kilometraje : 
                        </td>
                        <td>Cantidad consumo :
                        </td>
                        <td>Comentario :
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
                        <td>
                            <asp:TextBox ID="NumKilometraje" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="NumCantidad" runat="server" type="number" CssClass="form-control" Text="0" min="0" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtComentario" runat="server" type="text" CssClass="form-control" placeholder=" " Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="ml-auto">
                <asp:Button ID="ButtonAgregarRegistros" runat="server" ValidationGroup="Validate" class="btn btn-round btn-primary" Text="Agregar" OnClick="AgregarRegistro_Click" />
            </div>
        </div>
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
                                OnRowCreated="GridViewRegistros_RowCreated"
                                OnRowDeleting="GridViewRegistros_RowDeleting"
                                Visible="false">
                                <Columns>
                                    <asp:BoundField DataField="Id_Finca" HeaderText="Id_Finca" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Id_Finca" ItemStyle-CssClass="hidden-column" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Finca" runat="server" Text='<%# Eval("Id_Finca") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id_Lote" HeaderText="Id_Lote" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Id_Lote" ItemStyle-CssClass="hidden-column" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Lote" runat="server" Text='<%# Eval("Id_Lote") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id_Proceso" HeaderText="Id_Proceso" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Id_Proceso" ItemStyle-CssClass="hidden-column" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Proceso" runat="server" Text='<%# Eval("Id_Proceso") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id_CentroGasto" HeaderText="Id_CentroGasto" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Id_CentroGasto" ItemStyle-CssClass="hidden-column" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="ddlId_CentroGasto" runat="server" Text='<%# Eval("Id_CentroGasto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id_Clasificacion" HeaderText="Id_Proveedor" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Id_Clasificacion" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Clasificacion" runat="server" Text='<%# Eval("Id_Clasificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id_Tipo" HeaderText="Id_Tipo" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Id_Tipo" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Tipo" runat="server" Text='<%# Eval("Id_Tipo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Fecha" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="DateFecha" runat="server" Text='<%# Eval("Fecha") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NoVale" HeaderText="NoVale" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="NoVale" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="NumNoVale" runat="server" Text='<%# Eval("NoVale") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Kilometraje" HeaderText="Kilometraje" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Kilometraje" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="NumKilometraje" runat="server" Text='<%# Eval("Kilometraje") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad1" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="NumCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Comentario" ItemStyle-CssClass="hidden-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtComentario" runat="server" Text='<%# Eval("Comentario") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        <asp:Button ID="Insertar" runat="server" class="btn btn-round btn-success" Text="Insertar" ValidationGroup="Validate2" OnClick="Insertar_Click" Visible="false" />
                    </div>
                </td>
            </tr>
        </table>
    </body>
    </html>
</asp:Content>
