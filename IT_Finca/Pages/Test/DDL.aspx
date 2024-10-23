<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="DDL.aspx.cs" Inherits="IT_Finca.Pages.Test.DDL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <br />
    <br />
    <br />
    <div class="control">
        <div class="select">
            <asp:DropDownList ID="ddlCentroAnalisis" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlCentroAnalisis_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
            </asp:DropDownList>
        </div>
    </div>
    <br />
    <br />
    <br />
    <div class="control">
        <div class="select">
            <asp:DropDownList ID="ddlUbicacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
            </asp:DropDownList>
        </div>
    </div>
</asp:Content>
