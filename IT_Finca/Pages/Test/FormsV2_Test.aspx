<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="FormsV2_Test.aspx.cs" Inherits="IT_Finca.Pages.Test.FormuarioV2_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <asp:TextBox ID="txtInformacion" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlOpciones" runat="server">
        <asp:ListItem Text="Opción 1" Value="1"></asp:ListItem>
        <asp:ListItem Text="Opción 2" Value="2"></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="Agregar_Actividad1" runat="server" class="btn btn-round btn-primary" Text="Agregar" OnClick="Agregar_Actividad1_OnClick" />

    <asp:GridView
        ID="GV_Actividad1" 
        runat="server" 
        AutoGenerateColumns="False" 
        OnRowEditing="GV_Actividad1_RowEditing" 
        OnRowCancelingEdit="GV_Actividad1_RowCancelingEdit" 
        OnRowUpdating="GV_Actividad1_RowUpdating" 
        OnRowDeleting="GV_Actividad1_RowDeleting" 
        DataKeyNames="ID">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" Visible="false" />
        <asp:BoundField DataField="Informacion" HeaderText="Informacion" />
        <asp:BoundField DataField="Opcion" HeaderText="Opcion" />
        <asp:CommandField ShowEditButton="True" />
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
</asp:GridView>

</asp:Content>
