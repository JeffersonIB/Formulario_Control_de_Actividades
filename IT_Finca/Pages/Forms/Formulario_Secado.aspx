<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_Secado.aspx.cs" Inherits="IT_Finca.Pages.Forms.Formualario_Secado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formulario de secado
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
    </head>
    <body>
        <div class="container box">
            <center>
                <h1 class="title">Formulario de secado
                </h1>
            </center>
        </div>
        <br />
        <center>
            <%--<asp:ScriptManager ID="ScriptManager" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:GridView
                            ID="GridViewTiposCafe"
                            runat="server"
                            DataKeyNames="Id_Tipo_Cafe"
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
                            HeaderStyle-CssClass="header">
                            <Columns>
                                <asp:TemplateField HeaderText="Id_Tipo_Cafe" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Id_Tipo_Cafe" runat="server" Text='<%#Eval("Id_Tipo_Cafe") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de café" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="Tipo_Cafe" runat="server" Text='<%#Eval("Tipo_Cafe") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check" Visible="true">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                        <asp:Button ID="btnCargarDatos" runat="server" class="btn btn-round btn-primary" Text="Cargar datos" OnClick="btnCargarDatos_Click" />
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="2">
                        <asp:GridView
                            ID="GridViewResultados"
                            runat="server"
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
                            OnPageIndexChanging="GridViewResultados_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Id Beneficio" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id_Beneficio" runat="server" Text='<%#Eval("Id_Beneficio") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha beneficio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Fecha_Crea" runat="server" Text='<%# Eval("Fecha_Crea", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id_Empresa" runat="server" Text='<%#Eval("Id_Empresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Empresa" runat="server" Text='<%#Eval("Empresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Finca" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id_Finca" runat="server" Text='<%#Eval("Id_Finca") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Finca" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Finca" runat="server" Text='<%#Eval("Finca") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Lote" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id_Lote" runat="server" Text='<%#Eval("Id_Lote") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lote" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Lote" runat="server" Text='<%#Eval("Lote") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Proceso" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id_Proceso" runat="server" Text='<%#Eval("Id_Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proceso" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Proceso" runat="server" Text='<%#Eval("Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Actividad" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Id_Actividad" runat="server" Text='<%#Eval("Id_Actividad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actividad" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Actividad" runat="server" Text='<%#Eval("Actividad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lbl_Cantidad" runat="server" align="center" Text='<%# String.Format("{0:N}", Eval("Cantidad") )%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Check">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkInsertar" runat="server" />
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
                    <div id="Tipo_Secado" runat="server" visible="false">
                        <td align="center"><strong>Tipo secado</strong>
                            <asp:DropDownList ID="ddlTipo_Secado" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                        </td>
                        <td align="center"><strong>No partida</strong>
                            <asp:DropDownList ID="ddlPartida" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                        </td>
                    </div>
                </tr>
            </table>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <table align="center">
                <tr>
                    <div id="Agregar" runat="server" visible="false">
                        <td colspan="2" align="center">
                            <asp:Button ID="BTNAgregar" runat="server" ValidationGroup="Validate" Text="Agregar" class="btn btn-round btn-info" OnClick="BTNAgregar_Click" />
                        </td>
                    </div>
                </tr>
            </table>
            <br />
        </center>
    </body>
    </html>
</asp:Content>
