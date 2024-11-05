<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_SecadoV2.aspx.cs" Inherits="IT_Finca.Pages.Forms.Formulario_SecadoV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="GridViewTiposCafe" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_Tipo_Cafe">
                <Columns>
                    <asp:BoundField DataField="Tipo_Cafe" HeaderText="Tipo de Café" />
                    <asp:BoundField DataField="Id_Tipo_Cafe" HeaderText="ID" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnCargarDatos" runat="server" Text="Cargar Datos" OnClick="btnCargarDatos_Click" />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

    <%--<triggers>
        <asp:AsyncPostBackTrigger ControlID="btnCargarDatos" EventName="Click" />
    </triggers>--%>


    <br />
    <br />

    <asp:UpdatePanel ID="UpdatePanelResultados" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="GridViewResultados" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Id_Beneficio" HeaderText="Beneficio" />
                    <asp:BoundField DataField="Id_Empresa" HeaderText="Empresa" />
                    <asp:BoundField DataField="Id_Finca" HeaderText="Finca" />
                    <asp:BoundField DataField="Id_Lote" HeaderText="Lote" />
                    <asp:BoundField DataField="Id_Proceso" HeaderText="Proceso" />
                    <asp:BoundField DataField="Id_Actividad" HeaderText="Actividad" />
                    <asp:BoundField DataField="Resultado" HeaderText="Resultado" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" id="chkInsertar" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnInsertarSeleccionados" runat="server" Text="Insertar Seleccionados" OnClick="btnInsertarSeleccionados_Click" />
</asp:Content>
