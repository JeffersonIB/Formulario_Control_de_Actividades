<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="CentroGastos.aspx.cs" Inherits="IT_Finca.Pages.Test.CentroGastos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
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
                <td colspan="2">Finca
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
        <table>
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
