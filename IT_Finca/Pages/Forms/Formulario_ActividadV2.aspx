<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Formulario_ActividadV2.aspx.cs" Inherits="IT_Finca.Pages.Forms.FormsV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formuario de actividades
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
            function changeColor(obj) {
                var rowObject = getParentRow(obj);
                var parentTable = document.getElementById("<%=CheckBoxListEmpleados.ClientID%>");
                if (color === '') {
                    color = getRowColor();
                }
                if (obj.checked) {
                    rowObject.style.backgroundColor = '#A3B1D8';
                } else {
                    rowObject.style.backgroundColor = color;
                    color = 'White';
                }
            }
            // Este método devuelve la fila padre del objeto
            function getParentRow(obj) {
                do {
                    obj = obj.parentElement;
                } while (obj.tagName !== "TR");
                return obj;
            }
            function TurnCheckBoixGridView(id) {
                var checkBoxList = document.getElementById("<%= CheckBoxListEmpleados.ClientID %>");
                var checkboxes = checkBoxList.getElementsByTagName("input");

                for (var i = 0; i < checkboxes.length; i++) {
                    if (checkboxes[i].type === "checkbox") {
                        checkboxes[i].checked = document.getElementById(id).checked;
                        changeColor(checkboxes[i]);
                    }
                }
            }
            function filterEmployees() {
                var searchText = document.getElementById('txtSearch').value.toLowerCase();
                var checkboxes = document.getElementById('<%= CheckBoxListEmpleados.ClientID %>').getElementsByTagName('input');

                for (var i = 0; i < checkboxes.length; i++) {
                    var checkbox = checkboxes[i];
                    var employeeName = checkbox.parentNode.innerText.toLowerCase();
                    if (employeeName.indexOf(searchText) > -1) {
                        checkbox.parentNode.style.display = 'block';
                    } else {
                        checkbox.parentNode.style.display = 'none';
                    }
                }
            }
            function SelectAll(id) {
                var parentTable = document.getElementById("<%=CheckBoxListEmpleados.ClientID%>");
                var color = document.getElementById(id).checked ? '#A3B1D8' : 'White';

                for (var i = 0; i < parentTable.rows.length; i++) {
                    var checkbox = parentTable.rows[i].getElementsByTagName("input")[0];
                    checkbox.checked = document.getElementById(id).checked;
                    changeColor(checkbox);
                }

                TurnCheckBoixGridView(id);
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
                <h1 class="title">Formulario de actividades
                </h1>
            </center>
        </div>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                    <h5 class="modal-title">Agregar actividades 
                    <strong>"<asp:Label CssClass="navbar-link" runat="server" ID="lblFinca"> </asp:Label>"</strong>
                    </h5>
                </center>
                <table align="center" style="width: 100%;">
                    <tr>
                        <td>
                            <h5>Lote</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotes_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlLotes"
                                ErrorMessage="Campo obligatorio"
                                ForeColor="Red"
                                ValidationGroup="Validate"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                        <td>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlProcesos"
                                ErrorMessage="Campo obligatorio"
                                ForeColor="Red"
                                ValidationGroup="Validate"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>Actividad 1</h5>
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlActividad1" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlActividad1_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="ddlActividad1"
                                ErrorMessage="Campo obligatorio"
                                ForeColor="Red"
                                ValidationGroup="Validate"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a data-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <h5>Actividad 2 ↓ </h5>
                                </a>
                            </p>
                            <div class="collapse" id="collapseExample2">
                                <asp:DropDownList ID="ddlActividad2" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlActividad2_OnSelectedIndexChanged" Style="width: 100%;" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a data-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <h5>Actividad 3 ↓</h5>
                                </a>
                            </p>
                            <div class="collapse" id="collapseExample3">
                                <asp:DropDownList ID="ddlActividad3" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlActividad3_OnSelectedIndexChanged" Style="width: 100%;" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>Proveedores</h5>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="scroll_checkboxes" cssclass="form-control">
                                <input type="text" id="txtSearch" oninput="filterEmployees()" placeholder="Buscar por código o nombre" style="width: 100%;" />
                                <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server" CssClass="FormText" DataTextField="Nom_Ape" DataValueField="Id_Empleado"></asp:CheckBoxList>
                            </div>
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
                <asp:Button ID="ButtonAgregarEmpleados" runat="server" ValidationGroup="Validate" class="btn btn-round btn-primary" Text="Agregar" OnClick="AgregarEmpleados_Click" />
            </div>
        </div>
        <table>
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
                                OnRowDeleting="GridViewCalificaciones_RowDeleting"
                                Visible="false">
                                <Columns>
                                    <asp:BoundField DataField="Id_Empleado" HeaderText="ID Proveedor" Visible="false" />
                                    <asp:TemplateField HeaderText="Id_Empleado" ItemStyle-CssClass="hidden-column" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdEmpleado" runat="server" Text='<%# Eval("Id_Empleado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Nom_Ape" HeaderText="Nombre proveedor" />
                                    <asp:TemplateField HeaderText="Tipo Pago">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTipo_Actividad" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manzanas" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtManzanas" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rendimiento Actividad 1">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCantidad1" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="txtCantidad1"
                                                ErrorMessage="!=0"
                                                ForeColor="Red"
                                                ValidationGroup="Validate2"
                                                Display="Dynamic"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rendimiento Actividad 2" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCantidad2" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rendimiento Actividad 3" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCantidad3" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
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
