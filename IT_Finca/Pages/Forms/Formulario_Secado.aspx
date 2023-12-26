<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_Secado.aspx.cs" Inherits="IT_Finca.Pages.Forms.Formualario_Secado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formuario Secado
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Beneficio
        </title>
        <link href="<%= ResolveClientUrl("~/CSS/Admin.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Cale.js") %>"> </script>
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
                <h1 class="title">Secado
                </h1>
            </center>
        </div>
        <br />
        <!-- Tabla de Fincas -->
        <center>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center">
                                <div class="row" align="center">
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
                                </div>
                            </td>
                            <td align="center">

                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-round btn-success" OnClick="btnBuscar_Click" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvBeneficio" runat="server"
                                    DataKeyNames="Id_Beneficio_R"
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
                                    OnPageIndexChanging="gvBeneficio_OnPageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id_Beneficio_R" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Id_Beneficio_R" runat="server" Text='<%#Eval("Id_Beneficio_R") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha beneficio">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Fecha_Crea" runat="server" Text='<%#Eval("Fecha_Crea") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id_Finca" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Id_Finca" runat="server" Text='<%#Eval("Id_Finca") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Finca">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Finca" runat="server" Text='<%#Eval("Finca") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id_Lote" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Id_Lote" runat="server" Text='<%#Eval("Id_Lote") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lote">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Lote" runat="server" Text='<%#Eval("Lote") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Verde">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lbl_Verde" runat="server" align="center" Text='<%# String.Format("{0:N}", Eval("Verde") )%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maduro">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lbl_Maduro" runat="server" align="center" Text='<%# String.Format("{0:N}", Eval("Maduro") )%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <input ID="chkRow" runat="server" type="checkbox" class="flat">
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <bh></bh>
                            </td>
                        </tr>
                        <tr>
                            <td align="center"><strong>Tipo secado</strong>
                                <asp:DropDownList ID="ddlTipo_Secado" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                            </td>
                            <td align="center"><strong>No partida</strong>
                                <asp:DropDownList ID="ddlPartida" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </body>
    </html>
</asp:Content>
