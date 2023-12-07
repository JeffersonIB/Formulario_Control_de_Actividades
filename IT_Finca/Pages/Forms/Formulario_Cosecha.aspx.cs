using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Forms
{
    public partial class Formulario_Cosecha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack && Session["Usuario"] != null)
            {
                ViewState["CalificacionesDataTable"] = CreateDataTable();
                if (CheckBoxListEmpleados.Items.Count == 0)
                {
                    CargarLotes();
                    CargarEmpleados();
                    CheckBoxListEmpleados.DataBind();
                    CargarTipoActividad();
                }
            }
            else
            {
            }
            Session["LastActivity"] = DateTime.Now;
            DateTime lastActivity = (DateTime)Session["LastActivity"];
            TimeSpan timeSinceLastActivity = DateTime.Now - lastActivity;
            if (timeSinceLastActivity.TotalMinutes > Session.Timeout)
            {
                Response.Redirect("~/Default.aspx");
            }
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void CargarLotes()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
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
        protected void ddlLotes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesos.ClearSelection();
            CargarProcesos(int.Parse(ddlLotes.SelectedValue));
            dllActividad.ClearSelection();
            CargarActividad(int.Parse(ddlLotes.SelectedValue));

        }
        void CargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00301", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
                ddlProcesos.Items.Clear();
                con.Open();
                ddlProcesos.DataSource = cmd.ExecuteReader();
                ddlProcesos.DataTextField = "Proceso";
                ddlProcesos.DataValueField = "Id_Proceso";
                ddlProcesos.DataBind();
                //ddlProcesos.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlProcesos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        void CargarTipoActividad()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dllTipoActividad.Items.Clear();
                con.Open();
                dllTipoActividad.DataSource = cmd.ExecuteReader();
                dllTipoActividad.DataTextField = "Tipo_Actividad";
                dllTipoActividad.DataValueField = "Id_Tipo_Actividad";
                dllTipoActividad.DataBind();
                //dllTipoActividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarActividad(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00402", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
                dllActividad.Items.Clear();
                con.Open();
                dllActividad.DataSource = cmd.ExecuteReader();
                dllActividad.DataTextField = "Actividad";
                dllActividad.DataValueField = "Id_Actividad";
                dllActividad.DataBind();
                //dllActividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00202", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
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
        protected void CustomValidatorEmpleados_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = CheckBoxListEmpleados.SelectedIndex != -1;
        }

        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id_Empleado", typeof(int));
            dataTable.Columns.Add("Nom_Ape", typeof(string));
            return dataTable;
        }
        protected void AgregarEmpleados_Click(object sender, EventArgs e)
        {
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
            //foreach (GridViewRow row in GridViewCalificaciones.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        DropDownList ddlTipoActividad = (DropDownList)row.FindControl("ddlTipoActividad");
            //        //CargarTipoActividad(ddlTipoActividad);

            //        if (ddlTipoActividad.ClientID.EndsWith("ddlActividad2") && ddlTipoActividad.SelectedValue != "")
            //        {
            //            TemplateField campoCantidad2 = (TemplateField)GridViewCalificaciones.Columns[4];
            //            campoCantidad2.Visible = true;
            //        }
            //        else if (ddlTipoActividad.ClientID.EndsWith("ddlActividad3") && ddlTipoActividad.SelectedValue != "")
            //        {
            //            TemplateField campoCantidad3 = (TemplateField)GridViewCalificaciones.Columns[5];
            //            campoCantidad3.Visible = true;
            //        }
            //    }
            //}
            Insertar.Visible = GridViewCalificaciones.Rows.Count > 0;
        }

        protected void Insertar_Click(object sender, EventArgs e)
        {
            Insertar.Visible = false;
            DataTable dataTable = (DataTable)ViewState["CalificacionesDataTable"];
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                foreach (DataRow row in dataTable.Rows)
                {
                    int IdEmpleado = Convert.ToInt32(row["Id_Empleado"]);
                    //DropDownList ddlTipo_Actividad = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlTipo_Actividad") as DropDownList;
                    string Verde = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtVerde")).Text;
                    string Maduro = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtMaduro")).Text;
                    if (ddlLotes != null)
                    {
                        SqlCommand cmd = new SqlCommand("SP_AG_FNC00602", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                        cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                        cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = ddlLotes.Text;
                        cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddlProcesos.Text;
                        cmd.Parameters.AddWithValue("@Id_Tipo_Actividad", System.Data.SqlDbType.Int).Value = dllTipoActividad.Text;
                        cmd.Parameters.AddWithValue("@Id_Actividad", System.Data.SqlDbType.Int).Value = dllActividad.Text;
                        cmd.Parameters.AddWithValue("@Id_Empleado", IdEmpleado);                        
                        cmd.Parameters.AddWithValue("@Verde", Verde);
                        cmd.Parameters.AddWithValue("@Maduro", Maduro);
                        cmd.Parameters.AddWithValue("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            GridViewCalificaciones.DataSource = null;
            GridViewCalificaciones.DataBind();
            ViewState["CalificacionesDataTable"] = CreateDataTable();
            ddlLotes.ClearSelection();
            ddlProcesos.ClearSelection();
            //ddlActividad.ClearSelection();
            CreateDataTable();
            //TemplateField campoCantidad2 = (TemplateField)GridViewCalificaciones.Columns[4];
            //campoCantidad2.Visible = false;
            //TemplateField campoCantidad3 = (TemplateField)GridViewCalificaciones.Columns[5];
            //campoCantidad3.Visible = false;
        }

    }
}