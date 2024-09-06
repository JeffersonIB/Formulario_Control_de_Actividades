using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Forms
{
    public partial class FormsV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFinca.Text = Session["Finca"].ToString();
                CargarLotes();
                CargarEmpleados();
            }
            else
            {

            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void CargarLotes()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Finca"]);
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
            ddlActividad1.ClearSelection();
        }
        void CargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
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
        protected void ddlProcesos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActividad1.ClearSelection();
            ddlActividad2.ClearSelection();
            ddlActividad3.ClearSelection();
            CargarActividad1(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad2(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad3(long.Parse(ddlProcesos.SelectedValue));
        }
        void CargarActividad1(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad1.Items.Clear();
                con.Open();
                ddlActividad1.DataSource = cmd.ExecuteReader();
                ddlActividad1.DataTextField = "Actividad";
                ddlActividad1.DataValueField = "Id_Actividad";
                ddlActividad1.DataBind();
                ddlActividad1.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlActividad1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void CargarActividad2(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad2.Items.Clear();
                con.Open();
                ddlActividad2.DataSource = cmd.ExecuteReader();
                ddlActividad2.DataTextField = "Actividad";
                ddlActividad2.DataValueField = "Id_Actividad";
                ddlActividad2.DataBind();
                ddlActividad2.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlActividad2_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void CargarActividad3(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad3.Items.Clear();
                con.Open();
                ddlActividad3.DataSource = cmd.ExecuteReader();
                ddlActividad3.DataTextField = "Actividad";
                ddlActividad3.DataValueField = "Id_Actividad";
                ddlActividad3.DataBind();
                ddlActividad3.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlActividad3_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public class Employee
        {
            public int Id_Empleado { get; set; }
            public string Nom_Ape { get; set; }
        }
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00201", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Finca"]);
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

                // Generar el código JavaScript para cargar los nombres de empleados en el cliente
                List<string> empleadosNombres = new List<string>();
                foreach (Employee empleado in empleados)
                {
                    empleadosNombres.Add(empleado.Nom_Ape);
                }
                string empleadosNombresJson = string.Join(",", empleadosNombres.Select(name => "\"" + name + "\""));
                Page.ClientScript.RegisterStartupScript(GetType(), "LoadEmployees", $"var empleados = [{empleadosNombresJson}];", true);
            }
        }
        protected void AgregarEmpleados_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_Empleado", typeof(int));
            dt.Columns.Add("Nom_Ape", typeof(string));
            foreach (ListItem item in CheckBoxListEmpleados.Items)
            {
                if (item.Selected)
                {
                    // Agregar empleado al DataTable
                    DataRow dr = dt.NewRow();
                    dr["Id_Empleado"] = Convert.ToInt32(item.Value);
                    dr["Nom_Ape"] = item.Text;
                    dt.Rows.Add(dr);
                }
            }
            // Enlazar el DataTable al GridView
            GridViewCalificaciones.DataSource = dt;
            GridViewCalificaciones.DataBind();
            // Evaluar si ddlActividad2 o ddlActividad3 fueron seleccionados
            bool ddlActividad1Selected = ddlActividad1.SelectedIndex > 0;
            bool ddlActividad2Selected = ddlActividad2.SelectedIndex > 0;
            bool ddlActividad3Selected = ddlActividad3.SelectedIndex > 0;
            // Mostrar u ocultar las columnas según las selecciones
            GridViewCalificaciones.Columns[4].Visible = ddlActividad1Selected; // Índice de la columna "Manzanas"
            GridViewCalificaciones.Columns[5].Visible = ddlActividad1Selected; // Índice de la columna "Cantidad1"
            GridViewCalificaciones.Columns[6].Visible = ddlActividad2Selected; // Índice de la columna "Cantidad2"
            GridViewCalificaciones.Columns[7].Visible = ddlActividad3Selected; // Índice de la columna "Cantidad3"            
            ViewState["EmpleadosDataTable"] = dt; // Guardar DataTable en ViewState
            Insertar.Visible = true;
        }
        protected void GridViewCalificaciones_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlTipo_Actividad = (DropDownList)e.Row.FindControl("ddlTipo_Actividad");
                if (ddlTipo_Actividad != null)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Close();
                    ddlTipo_Actividad.DataSource = dt;
                    ddlTipo_Actividad.DataTextField = "Tipo_Actividad";
                    ddlTipo_Actividad.DataValueField = "Id_Tipo_Actividad";
                    ddlTipo_Actividad.DataBind();
                    //ddlTipo_Actividad.Items.Insert(0, new ListItem("--Select Qualification--", "0"));
                }
            }
        }
        protected void Insertar_Click(object sender, EventArgs e)
        {
            try
            {
                List<SqlParameter[]> parametrosList = new List<SqlParameter[]>();

                foreach (GridViewRow row in GridViewCalificaciones.Rows)
                {
                    int idEmpleado = Convert.ToInt32((row.FindControl("lblIdEmpleado") as Label)?.Text);
                    string nomApe = row.Cells[1].Text;
                    DropDownList ddlTipo_Actividad = (DropDownList)row.FindControl("ddlTipo_Actividad");
                    TextBox txtManzanas = (TextBox)row.FindControl("txtManzanas");
                    TextBox txtCantidad1 = (TextBox)row.FindControl("txtCantidad1");
                    TextBox txtCantidad2 = (TextBox)row.FindControl("txtCantidad2");
                    TextBox txtCantidad3 = (TextBox)row.FindControl("txtCantidad3");

                    SqlParameter[] parametros = new SqlParameter[]
                    {
                new SqlParameter("@Id_Finca", SqlDbType.Int) { Value = Convert.ToInt32(Session["Id_Finca"]) },
                new SqlParameter("@Id_Empleado", SqlDbType.Int) { Value = idEmpleado },
                new SqlParameter("@Id_Lote", SqlDbType.Int) { Value = Convert.ToInt32(ddlLotes.Text) },
                new SqlParameter("@Id_Proceso", SqlDbType.Int) { Value = Convert.ToInt32(ddlProcesos.Text) },
                new SqlParameter("@Id_Actividad1", SqlDbType.Int) { Value = Convert.ToInt32(ddlActividad1.Text) },
                new SqlParameter("@Id_Actividad2", SqlDbType.Int) { Value = Convert.ToInt32(ddlActividad2.Text) },
                new SqlParameter("@Id_Actividad3", SqlDbType.Int) { Value = Convert.ToInt32(ddlActividad3.Text) },
                new SqlParameter("@Id_Tipo_Actividad1", SqlDbType.Int) { Value = Convert.ToInt32(ddlTipo_Actividad.SelectedValue) },
                new SqlParameter("@Manzanas", SqlDbType.Decimal) { Value = Decimal.Parse(txtManzanas.Text) },
                new SqlParameter("@Cantidad1", SqlDbType.Decimal) { Value = Decimal.Parse(txtCantidad1.Text) },
                new SqlParameter("@Cantidad2", SqlDbType.Decimal) { Value = Decimal.Parse(txtCantidad2.Text) },
                new SqlParameter("@Cantidad3", SqlDbType.Decimal) { Value = Decimal.Parse(txtCantidad3.Text) },
                new SqlParameter("@Id_Empresa", SqlDbType.Int) { Value = Convert.ToInt32(Session["Id_Empresa"]) },
                new SqlParameter("@Id_Usr_Crea", SqlDbType.Int) { Value = Convert.ToInt32(Session["Id_Usuario"]) }
                    };

                    parametrosList.Add(parametros);
                }
                // Realizar la inserción fuera del bucle
                SqlCommand cmd = new SqlCommand("SP_AG_FNC00600_2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                foreach (SqlParameter[] parametros in parametrosList)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parametros);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                Insertar.Visible = false;
                Response.Redirect("~/Pages/Forms/Formulario_ActividadV2.aspx");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    $"swal('Error!', 'Error en validación de datos: {ex.Message}', 'error')", true);
            }
        }

    }
}