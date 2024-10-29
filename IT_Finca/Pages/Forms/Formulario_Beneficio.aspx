<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_Beneficio.aspx.cs" Inherits="IT_Finca.Pages.Forms.Formulario_Beneficio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formulario de beneficio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Formulario de beneficio
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Admin.js") %>"> </script>
        <script>
            $(function () {
                $("#Calendario").datepicker({
                    dateFormat: 'dd-mm-yy',
                    showButtonPanel: true,
                    changeMonth: true,
                    changeYear: true
                });
            });
        </script>
    </head>
    <body>
        <div class="container box">
            <center>
                <h1 class="title">Formulario de beneficio
                </h1>
            </center>
        </div>
        <br />
        <!-- Modal Actualizar -->
        <div class="modal fade" id="Modal_Actualizar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Modificar registros
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbId_Beneficio" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="Center">Verde :
                                </td>
                                <td align="Center">Maduro :
                                </td>
                            </tr>
                            <br />
                            <tr>
                                <td align="Center">
                                    <asp:TextBox ID="txVerde" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 50%;"></asp:TextBox>
                                </td>
                                <td align="Center">
                                    <asp:TextBox ID="txMaduro" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 50%;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbId_Finca" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbId_Lote" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbId_Proceso" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbId_Actividad" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbFecha_Crea_V" Visible="false"></asp:Label>
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
        <!-- Tabla de Fincas -->
        <center>
            <table>
                <tr>
                    <td>
                        <div class="row">
                            <div class="item form-group">
                                <label class="col-form-label col-md-3 col-sm-3 label-align">
                                    <h5>Fecha
                                    </h5>
                                </label>
                            </div>
                            <div class="item form-group">
                                <input id="Calendario" runat="server" class="date-picker form-control" placeholder="dd-mm-yyyy" type="text" onfocus="this.type='date'" onmouseover="this.type='date'" onclick="this.type='date'" onblur="this.type='text'" onmouseout="timeFunctionLong(this)">
                                <script>
                                    function timeFunctionLong(input) {
                                        setTimeout(function () {
                                            input.type = 'text';
                                        }, 60000);
                                    }
                                </script>
                            </div>
                            <div class="ml-auto">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvBeneficio" runat="server"
                            DataKeyNames="Id_Beneficio"
                            PageSize="17"
                            CssClass="mydatagrid"
                            GridLines="both"
                            GroupingEnabled="true"
                            AllowPaging="true"
                            HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False"
                            EmptyDataText="Sin registros"
                            EmptyDataRowStyle-ForeColor="Red"
                            RowStyle-CssClass="rows"
                            PagerStyle-CssClass="pager"
                            HeaderStyle-CssClass="header"
                            OnRowCommand="gvBeneficio_OnRowCommand"
                            OnPageIndexChanging="gvBeneficio_OnPageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Id Beneficio" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Id_Beneficio" runat="server" Text='<%#Eval("Id_Beneficio") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Finca" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Id_Finca" runat="server" Text='<%#Eval("Id_Finca") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Finca" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Finca" runat="server" Text='<%#Eval("Finca") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Lote" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Id_Lote" runat="server" Text='<%#Eval("Id_Lote") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lote" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Lote" runat="server" Text='<%#Eval("Lote") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Proceso" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Id_Proceso" runat="server" Text='<%#Eval("Id_Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proceso" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Proceso" runat="server" Text='<%#Eval("Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Actividad" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Id_Actividad" runat="server" Text='<%#Eval("Id_Actividad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actividad" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Actividad" runat="server" Text='<%#Eval("Actividad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Verde" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Verde" runat="server" align="center" Text='<%# String.Format("{0:N}", Eval("Verde") )%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maduro" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gv_Maduro" runat="server" align="center" Text='<%# String.Format("{0:N}", Eval("Maduro") )%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvFecha_Crea" runat="server" Text='<%#Eval("Fecha_Crea") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Pages/Img/Editar.png" CommandName="ShowModalAc" CommandArgument='<%#Eval("Id_Beneficio") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Confirmar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btn_Confir" runat="server" ImageUrl="~/Pages/Img/Calificado.gif" OnClick="btn_Confir_Click" CommandArgument='<%#Eval("Id_Beneficio") %>' Style="display: block; margin: 0 auto;" />
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
