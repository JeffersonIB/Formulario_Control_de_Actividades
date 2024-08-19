using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;

namespace IT_Finca.Pages.Forms
{
    public partial class ControlDieselGasolina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Usuario"] != null)
            {
                DataTable dt = new DataTable();
                Session["GridViewData"] = dt;
                BindGridView();
                DDLCargarFincas();                
            }
            else
            {

            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar Listado de Fincas en DropDownList
        void DDLCargarFincas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Empresa"]);
                ddlFincas.Items.Clear();
                con.Open();
                ddlFincas.DataSource = cmd.ExecuteReader();
                ddlFincas.DataTextField = "Finca";
                ddlFincas.DataValueField = "Id_Finca";
                ddlFincas.DataBind();
                ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlFincas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLotes.ClearSelection();
            DDLCargarLotes(int.Parse(ddlFincas.SelectedValue));            
        }
        //Cargar Listado de Lotes en DropDownList
        void DDLCargarLotes(long IdFinca)
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
        protected void ddlLotes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesos.ClearSelection();
            DDLCargarProcesos(int.Parse(ddlLotes.SelectedValue));
        }
        void DDLCargarProcesos(long IdLote)
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
            ddlCentroGasto.ClearSelection();
            DDLCargarCentroGasto(int.Parse(ddlProcesos.SelectedValue));
        }
        void DDLCargarCentroGasto(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00407", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlCentroGasto.Items.Clear();
                con.Open();
                ddlCentroGasto.DataSource = cmd.ExecuteReader();
                ddlCentroGasto.DataTextField = "CentroGasto";
                ddlCentroGasto.DataValueField = "Id_CentroGasto";
                ddlCentroGasto.DataBind();
                //ddlCentroGasto.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlCentroGasto_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCentroGasto.ClearSelection();
            DDLCargarClasificacion(int.Parse(ddlProcesos.SelectedValue));
        }
        void DDLCargarClasificacion(long IdCentroGasto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00408", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_CentroGasto", SqlDbType.Int).Value = IdCentroGasto;
                ddlClasificacion.Items.Clear();
                con.Open();
                ddlClasificacion.DataSource = cmd.ExecuteReader();
                ddlClasificacion.DataTextField = "Clasificacion";
                ddlClasificacion.DataValueField = "Id_Clasificacion";
                ddlClasificacion.DataBind();
                //ddlClasificacion.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlClasificacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void DDLCargarTipo(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00407", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlCentroGasto.Items.Clear();
                con.Open();
                ddlCentroGasto.DataSource = cmd.ExecuteReader();
                ddlCentroGasto.DataTextField = "CentroGasto";
                ddlCentroGasto.DataValueField = "Id_CentroGasto";
                ddlCentroGasto.DataBind();
                //ddlCentroGasto.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void AgregarRegistro_Click(object sender, EventArgs e)
        {
            GridViewRegistros.Visible = true;
            // Obtener el DataTable desde el ViewState
            DataTable dt;
            if (ViewState["RegistrosDataTable"] != null)
            {
                dt = (DataTable)ViewState["RegistrosDataTable"];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Id_Finca", typeof(int));
                dt.Columns.Add("Id_Lote", typeof(int));
                dt.Columns.Add("Id_Proceso", typeof(int));
                dt.Columns.Add("Id_CentroGasto", typeof(int));
                dt.Columns.Add("Id_Clasificacion", typeof(int));
                dt.Columns.Add("Id_Tipo", typeof(int));
                dt.Columns.Add("Fecha", typeof(DateTime));
                dt.Columns.Add("NoVale", typeof(int));
                dt.Columns.Add("Kilometraje", typeof(decimal));
                dt.Columns.Add("Cantidad", typeof(decimal));
                dt.Columns.Add("Comentario", typeof(string));
                ViewState["RegistrosDataTable"] = dt;
            }

            // Obtener los valores de los controles
            int idFinca = Convert.ToInt32(ddlFincas.SelectedValue);
            int idLote = Convert.ToInt32(ddlLotes.SelectedValue);
            int idProceso = Convert.ToInt32(ddlProcesos.SelectedValue);
            int idCentroGasto = Convert.ToInt32(ddlCentroGasto.SelectedValue);
            int idClasificacion = Convert.ToInt32(ddlClasificacion.SelectedValue);
            int idTipo = Convert.ToInt32(ddlTipo.Text);
            //DateTime fecha =  Convert.ToDateTime(DateFecha.SelectedValue);
            string fecha = string.Empty;
            int noVale = Convert.ToInt32(NumVale.Text);
            decimal kilometraje = Convert.ToInt32(NumKilometraje.Text);
            decimal cantidad = Convert.ToInt32(NumCantidad.Text);
            string comentario = (txtComentario.Text);

            // Agregar una nueva fila al DataTable
            DataRow newRow = dt.NewRow();
            newRow["Id_Finca"] = idFinca;
            newRow["Id_Lote"] = idLote;
            newRow["Id_Proceso"] = idProceso;
            newRow["Id_CentroGasto"] = idCentroGasto;
            newRow["Id_Clasificacion"] = idClasificacion;
            newRow["Id_Tipo"] = idTipo;
            newRow["Fecha"] = fecha;
            newRow["NoVale"] = noVale;
            newRow["Kilometraje"] = kilometraje;
            newRow["Cantidad"] = cantidad;
            newRow["Comentario"] = comentario;

            // Agregar la nueva fila al DataTable
            dt.Rows.Add(newRow);
            // Enlazar el DataTable al GridView
            GridViewRegistros.DataSource = dt;
            GridViewRegistros.DataBind();

            // Guardar el DataTable actualizado en el ViewState
            ViewState["RegistrosDataTable"] = dt;
            Insertar.Visible = true;
            Session["GridViewData"] = dt;
        }

        protected void GridViewRegistros_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void GridViewRegistros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = (DataTable)Session["GridViewData"];
            dt.Rows.RemoveAt(rowIndex);
            GridViewRegistros.EditIndex = -1;
            BindGridView();
        }
        private void BindGridView()
        {
            DataTable dt = (DataTable)Session["GridViewData"];
            GridViewRegistros.DataSource = dt;
            GridViewRegistros.DataBind();
        }
        protected void Insertar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridViewRegistros.Rows)
                {
                    SqlCommand cmd = new SqlCommand("SP_AG_FNC00610", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    int idFinca = Convert.ToInt32(Session["Id_Finca"]);
                    int idEmpleado = Convert.ToInt32((row.FindControl("lblId_Proveedor") as Label)?.Text);
                    int idLote = Convert.ToInt32((row.FindControl("lblId_Lote") as Label)?.Text);
                    int idProceso = Convert.ToInt32((row.FindControl("lblId_Proceso") as Label)?.Text);
                    int idActividad1 = Convert.ToInt32((row.FindControl("ddlActividad1") as Label)?.Text);
                    int idActividad2 = Convert.ToInt32((row.FindControl("ddlActividad2") as Label)?.Text);
                    int idActividad3 = Convert.ToInt32((row.FindControl("ddlActividad3") as Label)?.Text);
                    DropDownList ddlTipo_Actividad = (DropDownList)row.FindControl("ddlTipo_Actividad");
                    TextBox txtCantidad1 = (TextBox)row.FindControl("txtCantidad1");
                    TextBox txtCantidad2 = (TextBox)row.FindControl("txtCantidad1");
                    TextBox txtCantidad3 = (TextBox)row.FindControl("txtCantidad1");
                    int idEmpresa = Convert.ToInt32(Session["Id_Empresa"]);


                    cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = idFinca;
                    cmd.Parameters.Add("@Id_Empleado", System.Data.SqlDbType.Int).Value = idEmpleado;
                    cmd.Parameters.Add("@Id_Lote", System.Data.SqlDbType.Int).Value = idLote;
                    cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = idProceso;
                    cmd.Parameters.Add("@Id_Actividad1", System.Data.SqlDbType.Int).Value = idActividad1;
                    cmd.Parameters.Add("@Id_Actividad2", System.Data.SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Id_Actividad3", System.Data.SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Id_Tipo_Actividad1", System.Data.SqlDbType.Int).Value = Convert.ToInt32(ddlTipo_Actividad.SelectedValue);
                    cmd.Parameters.Add("@Cantidad1", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(txtCantidad1.Text);
                    cmd.Parameters.Add("@Cantidad2", System.Data.SqlDbType.Decimal).Value = 0;
                    cmd.Parameters.Add("@Cantidad3", System.Data.SqlDbType.Decimal).Value = 0;
                    cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = idEmpresa;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                Insertar.Visible = false;
                Response.Redirect("~/Pages/Forms/ControlDieselGasolina.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }

    }
}