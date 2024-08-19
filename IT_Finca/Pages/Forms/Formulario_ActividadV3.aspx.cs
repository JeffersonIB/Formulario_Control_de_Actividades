using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Forms
{
    public partial class FormsV3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Usuario"] != null)
            {
                DataTable dt = new DataTable();
                Session["GridViewData"] = dt;
                BindGridView();
                //lblFinca.Text = Session["Finca"].ToString();
                CargarLotes();
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
            //ddlActividad2.ClearSelection();
            //ddlActividad3.ClearSelection();
            CargarActividad1(long.Parse(ddlProcesos.SelectedValue));
            //CargarActividad2(long.Parse(ddlProcesos.SelectedValue));
            //CargarActividad3(long.Parse(ddlProcesos.SelectedValue));
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
        //void CargarActividad2(long IdProceso)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
        //        ddlActividad2.Items.Clear();
        //        con.Open();
        //        ddlActividad2.DataSource = cmd.ExecuteReader();
        //        ddlActividad2.DataTextField = "Actividad";
        //        ddlActividad2.DataValueField = "Id_Actividad";
        //        ddlActividad2.DataBind();
        //        ddlActividad2.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
        //        con.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //protected void ddlActividad2_OnSelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        //void CargarActividad3(long IdProceso)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
        //        ddlActividad3.Items.Clear();
        //        con.Open();
        //        ddlActividad3.DataSource = cmd.ExecuteReader();
        //        ddlActividad3.DataTextField = "Actividad";
        //        ddlActividad3.DataValueField = "Id_Actividad";
        //        ddlActividad3.DataBind();
        //        ddlActividad3.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
        //        con.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //protected void ddlActividad3_OnSelectedIndexChanged(object sender, EventArgs e)
        //{

        //}      
        protected void AgregarEmpleados_Click(object sender, EventArgs e)
        {
            GridViewCalificaciones.Visible = true;
            // Obtener el DataTable desde el ViewState
            DataTable dt;
            if (ViewState["EmpleadosDataTable"] != null)
            {
                dt = (DataTable)ViewState["EmpleadosDataTable"];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Id_Lote", typeof(int));
                dt.Columns.Add("Id_Proceso", typeof(int));
                dt.Columns.Add("Actividad1", typeof(int));
                dt.Columns.Add("Id_Proveedor", typeof(int));
                dt.Columns.Add("Tipo_Pago", typeof(int));
                dt.Columns.Add("Cantidad1", typeof(decimal));
                ViewState["EmpleadosDataTable"] = dt;
            }

            // Obtener los valores de los controles
            int idLote = Convert.ToInt32(ddlLotes.SelectedValue);
            int idProceso = Convert.ToInt32(ddlProcesos.SelectedValue);
            int idActividad = Convert.ToInt32(ddlActividad1.SelectedValue);
            int idProveedor = Convert.ToInt32(txtIdProveedor.Text);
            decimal cantidad1 = Convert.ToDecimal(txtCantidad1.Text);
            // Agrega aquí las líneas para obtener los valores de los otros controles

            // Agregar una nueva fila al DataTable
            DataRow newRow = dt.NewRow();
            newRow["Id_Lote"] = idLote;
            newRow["Id_Proceso"] = idProceso;
            newRow["Actividad1"] = idActividad;
            newRow["Id_Proveedor"] = idProveedor;
            newRow["Cantidad1"] = cantidad1;
            // Agrega aquí las líneas para asignar valores a otras columnas

            // Agregar la nueva fila al DataTable
            dt.Rows.Add(newRow);

            // Enlazar el DataTable al GridView
            GridViewCalificaciones.DataSource = dt;
            GridViewCalificaciones.DataBind();

            // Guardar el DataTable actualizado en el ViewState
            ViewState["EmpleadosDataTable"] = dt;
            Insertar.Visible = true;
            Session["GridViewData"] = dt;
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
        protected void GridViewCalificaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = (DataTable)Session["GridViewData"];
            dt.Rows.RemoveAt(rowIndex);
            GridViewCalificaciones.EditIndex = -1;
            BindGridView();
        }
        private void BindGridView()
        {
            DataTable dt = (DataTable)Session["GridViewData"];
            GridViewCalificaciones.DataSource = dt;
            GridViewCalificaciones.DataBind();
        }

        protected void Insertar_Click(object sender, EventArgs e)
        {
            
            try
            {
                foreach (GridViewRow row in GridViewCalificaciones.Rows)
                {
                    SqlCommand cmd = new SqlCommand("SP_AG_FNC00600_3", con);
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
                Response.Redirect("~/Pages/Forms/FormsV3.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }

    }
}