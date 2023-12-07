using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using IT_Finca.Pages.Admin;

namespace IT_Finca.Pages.Forms
{
    public partial class LasMinas : System.Web.UI.Page
    {
        int IdFinca = 3;
        int IdEmpresa = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CalificacionesDataTable"] = CreateDataTable();
                if (CheckBoxListEmpleados.Items.Count == 0)
                {
                    CargarEmpleados();
                    CheckBoxListEmpleados.DataBind();
                }
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        protected void AgregarEmpleados_Click(object sender, EventArgs e)
        {
            //Abrir Modal_Agregar con listado de Empleados seleccionados
            ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
            DataTable dataTable = (DataTable)ViewState["CalificacionesDataTable"];
            bool empleadoRepetido = false;
            foreach (ListItem item in CheckBoxListEmpleados.Items)
            {
                if (item.Selected)
                {
                    string idEmpleado = item.Value;
                    string nombreEmpleado = item.Text;
                    // Validar si el empleado ya está cargado en el GridView
                    bool empleadoExistente = false;
                    foreach (GridViewRow row in GridViewCalificaciones.Rows)
                    {
                        Label labelIdEmpleado = (Label)row.FindControl("LabelIdEmpleado");
                        if (labelIdEmpleado != null && labelIdEmpleado.Text == idEmpleado)
                        {
                            empleadoExistente = true;
                            empleadoRepetido = true;
                            break;
                        }
                    }
                    if (!empleadoExistente)
                    {
                        DataRow newRow = dataTable.NewRow();
                        newRow["Id_Empleado"] = idEmpleado;
                        newRow["Nom_Ape"] = nombreEmpleado;
                        dataTable.Rows.Add(newRow);
                    }
                }
            }
            GridViewCalificaciones.DataSource = dataTable;
            GridViewCalificaciones.DataBind();
            // Limpiar la selección de empleados
            foreach (ListItem item in CheckBoxListEmpleados.Items)
            {
                item.Selected = false;
            }
            if (empleadoRepetido)
            {
                LabelError.Text = "No se pueden agregar empleados duplicados.";
            }
            else
            {
                LabelError.Text = "";
            }
            // Cargar los DropDownList en el GridView por cada Empleado
            foreach (GridViewRow row in GridViewCalificaciones.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlLotes = (DropDownList)row.FindControl("ddlLotes");
                    DropDownList ddlProcesos = (DropDownList)row.FindControl("ddlProcesos");
                    DropDownList ddlActividades = (DropDownList)row.FindControl("ddlActividades");
                    DropDownList ddlTipoActividad = (DropDownList)row.FindControl("ddlTipoActividad");
                    // Configurar los eventos y cargar los datos en los DropDownList
                    ddlLotes.SelectedIndexChanged += ddlLotes_OnSelectedIndexChanged;
                    ddlProcesos.SelectedIndexChanged += ddlProcesos_OnSelectedIndexChanged;
                    ddlActividades.SelectedIndexChanged += ddlActividades_OnSelectedIndexChanged;
                    ddlTipoActividad.SelectedIndexChanged += ddlTipoActividad_OnSelectedIndexChanged;
                    // Cargar los datos en los DropDownList
                    CargarLotes(ddlLotes);
                    CargarProcesos(int.Parse(ddlLotes.SelectedValue), ddlProcesos);
                    CargarActividades(int.Parse(ddlProcesos.SelectedValue), ddlActividades);
                    CargarTipoActividad(ddlTipoActividad);
                }
            }
            ButtonInsertar.Visible = GridViewCalificaciones.Rows.Count > 0;
        }
        //Cargar DropDowList de Lotes
        void CargarLotes(DropDownList ddlLotes)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                ddlLotes.Items.Clear();
                con.Open();
                ddlLotes.DataSource = cmd.ExecuteReader();
                ddlLotes.DataTextField = "Lote";
                ddlLotes.DataValueField = "Id_Lote";
                ddlLotes.DataBind();
                ddlLotes.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar DropDowList de Procesos
        void CargarProcesos(int IdLote, DropDownList ddlProcesos)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Lote", IdLote);
                ddlProcesos.Items.Clear();
                con.Open();
                ddlProcesos.DataSource = cmd.ExecuteReader();
                ddlProcesos.DataTextField = "Proceso";
                ddlProcesos.DataValueField = "Id_Proceso";
                ddlProcesos.DataBind();
                ddlProcesos.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar DropDowList de Actividades
        void CargarActividades(int IdProceso, DropDownList ddlActividades)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividades.Items.Clear();
                con.Open();
                ddlActividades.DataSource = cmd.ExecuteReader();
                ddlActividades.DataTextField = "Actividad";
                ddlActividades.DataValueField = "Id_Actividad";
                ddlActividades.DataBind();
                ddlActividades.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar DropDowList de Tipo_Actividad
        void CargarTipoActividad(DropDownList ddlTipoActividad)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlTipoActividad.Items.Clear();
                con.Open();
                ddlTipoActividad.DataSource = cmd.ExecuteReader();
                ddlTipoActividad.DataTextField = "Tipo_Actividad";
                ddlTipoActividad.DataValueField = "Id_Tipo_Actividad";
                ddlTipoActividad.DataBind();
                //ddlTipoActividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Función al seleccionar un Lote del DropDowList
        protected void ddlLotes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            DropDownList ddlProcesos = (DropDownList)row.FindControl("ddlProcesos");
            int IdLote = int.Parse(((DropDownList)sender).SelectedValue);
            CargarProcesos(IdLote, ddlProcesos);
            RequiredFieldValidator Validator1 = (RequiredFieldValidator)row.FindControl("Validator1");
            Validator1.Validate();
        }
        // Función al seleccionar un Proceso del DropDowList
        protected void ddlProcesos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProcesos = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlProcesos.NamingContainer;
            DropDownList ddlActividades = (DropDownList)row.FindControl("ddlActividades");
            int IdProceso = int.Parse(ddlProcesos.SelectedValue);
            CargarActividades(IdProceso, ddlActividades);
            RequiredFieldValidator Validator2 = (RequiredFieldValidator)row.FindControl("Validator2");
            Validator2.Validate();
        }
        // Función al seleccionar una Actividad del DropDowList
        protected void ddlActividades_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlActividades = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlActividades.NamingContainer;
            RequiredFieldValidator Validator3 = (RequiredFieldValidator)row.FindControl("Validator3");
            Validator3.Validate();
        }
        // Función al seleccionar un Tipo_Actividad del DropDowList
        protected void ddlTipoActividad_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTipoActividad = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlTipoActividad.NamingContainer;
        }
        //Insertar directo a base desde fronent
        //protected void ButtonInsertar_Click(object sender, EventArgs e)
        //{
        //    DataTable dataTable = (DataTable)ViewState["CalificacionesDataTable"];
        //    string insertQuery = "INSERT INTO FNC00600 (Id_Empresa, Id_Finca, Id_Empleado, Id_Lote, Id_Proceso, Id_Actividad1, Id_Tipo_Actividad1, Cantidad1, IsActive, Fecha_Crea) VALUES (1, 1, @Id_Empleado, @Id_Lote, @Id_Proceso, @Id_Actividad, @Id_TipoActividad, @Cantidad, 1, GETDATE())";
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
        //    {
        //        con.Open();
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            int IdEmpleado = Convert.ToInt32(row["Id_Empleado"]);
        //            DropDownList ddlLotes = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlLotes") as DropDownList;
        //            DropDownList ddlProcesos = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlProcesos") as DropDownList;
        //            DropDownList ddlActividades = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlActividades") as DropDownList;
        //            DropDownList ddlTipoActividad = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlTipoActividad") as DropDownList;
        //            string Cantidad = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtCantidad")).Text;
        //            if (ddlLotes != null)
        //            {
        //                SqlCommand cmd = new SqlCommand(insertQuery, con);
        //                cmd.Parameters.AddWithValue("@Id_Empleado", IdEmpleado);
        //                cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = ddlLotes.SelectedValue;
        //                cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddlProcesos.SelectedValue;
        //                cmd.Parameters.AddWithValue("@Id_Actividad", System.Data.SqlDbType.Int).Value = ddlActividades.SelectedValue;
        //                cmd.Parameters.AddWithValue("@Id_TipoActividad", System.Data.SqlDbType.Int).Value = ddlTipoActividad.SelectedValue;
        //                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    GridViewCalificaciones.DataSource = null;
        //    GridViewCalificaciones.DataBind();
        //    ViewState["CalificacionesDataTable"] = CreateDataTable();
        //}
        //Insertar a base mediante un Stored Procedure
        protected void ButtonInsertar_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["CalificacionesDataTable"];
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                foreach (DataRow row in dataTable.Rows)
                {
                    int IdEmpleado = Convert.ToInt32(row["Id_Empleado"]);
                    DropDownList ddlLotes = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlLotes") as DropDownList;
                    DropDownList ddlProcesos = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlProcesos") as DropDownList;
                    DropDownList ddlActividades = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlActividades") as DropDownList;
                    DropDownList ddlTipoActividad = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlTipoActividad") as DropDownList;
                    string Cantidad = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtCantidad")).Text;
                    if (ddlLotes != null)
                    {
                        SqlCommand cmd = new SqlCommand("SP_AG_FNC00601", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = IdFinca;
                        cmd.Parameters.AddWithValue("@Id_Empleado", IdEmpleado);
                        cmd.Parameters.AddWithValue("@Id_Lote", ddlLotes.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_Proceso", ddlProcesos.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_Actividad1", ddlActividades.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_Tipo_Actividad1", ddlTipoActividad.SelectedValue);
                        cmd.Parameters.AddWithValue("@Cantidad1", Cantidad);
                        cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = IdEmpresa;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            GridViewCalificaciones.DataSource = null;
            GridViewCalificaciones.DataBind();
            ViewState["CalificacionesDataTable"] = CreateDataTable();
        }
        //Cargar listado de Empleados desde froned
        //private void CargarEmpleados()
        //{
        //    string selectQuery = "SELECT Id_Empleado, Nom_Ape FROM FNC00200";
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(selectQuery, con);
        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        List<Employee> empleados = new List<Employee>();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                int idEmpleado = reader.GetInt32(0);
        //                string nombreEmpleado = reader.GetString(1);
        //                empleados.Add(new Employee { Id = idEmpleado, Nombre = nombreEmpleado });
        //            }
        //        }
        //        reader.Close();
        //        CheckBoxListEmpleados.DataSource = empleados;
        //        CheckBoxListEmpleados.DataTextField = "Nombre";
        //        CheckBoxListEmpleados.DataValueField = "Id";
        //        CheckBoxListEmpleados.DataBind();
        //    }
        //}
        //Cargar listado de Empleados desde un Stored Procedure
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00202", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> empleados = new List<Employee>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int idEmpleado = reader.GetInt32(0);
                        string nombreEmpleado = reader.GetString(1);
                        empleados.Add(new Employee { Id_Empleado = idEmpleado, Nom_Ape = nombreEmpleado });
                    }
                }
                reader.Close();
                CheckBoxListEmpleados.DataSource = empleados;
                CheckBoxListEmpleados.DataTextField = "Nom_Ape";
                CheckBoxListEmpleados.DataValueField = "Id_Empleado";
                CheckBoxListEmpleados.DataBind();
            }
        }
        public class Employee
        {
            public int Id_Empleado { get; set; }
            public string Nom_Ape { get; set; }
        }
        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id_Empleado", typeof(int));
            dataTable.Columns.Add("Nom_Ape", typeof(string));
            return dataTable;
        }
    }
}