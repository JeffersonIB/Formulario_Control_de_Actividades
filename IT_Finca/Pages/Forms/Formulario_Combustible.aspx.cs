using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Ubicacion.Pages.Forms
{
    public partial class Formulario_Combustible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Usuario"] != null)
            {
                DDLCargarTipoCombustible();
                DDLCargarCentroAnalisis();
                DataTable dt = new DataTable();
                Session["GridViewData"] = dt;
                BindGridView();
            }
            else
            {

            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar listado de tipos de combustible en DropDownList
        void DDLCargarTipoCombustible()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00410", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlId_TipoCombustible.Items.Clear();
                con.Open();
                ddlId_TipoCombustible.DataSource = cmd.ExecuteReader();
                ddlId_TipoCombustible.DataTextField = "TipoCombustible";
                ddlId_TipoCombustible.DataValueField = "Id_TipoCombustible";
                ddlId_TipoCombustible.DataBind();
                ddlId_TipoCombustible.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar listado de Centro de análisis en DropDownList
        void DDLCargarCentroAnalisis()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00409", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlCentroAnalisis.Items.Clear();
                con.Open();
                ddlCentroAnalisis.DataSource = cmd.ExecuteReader();
                ddlCentroAnalisis.DataTextField = "CentroAnalisis";
                ddlCentroAnalisis.DataValueField = "Id_CentroAnalisis";
                ddlCentroAnalisis.DataBind();
                ddlCentroAnalisis.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlCentroAnalisis_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCentroAnalisis.SelectedValue != "0")
            {
                int idCentroAnalisis = int.Parse(ddlCentroAnalisis.SelectedValue);
                DDLCargarUbicaciones(idCentroAnalisis);
                if (ddlUbicacion.Items.Count >= 1 && ddlUbicacion.SelectedValue != "0")
                {
                    int idUbicacion = int.Parse(ddlUbicacion.SelectedValue);
                    DDLCargarProcesos(idUbicacion);
                    //DDLCargarProcesos(int.Parse(ddlUbicacion.SelectedValue));
                    if (ddlProcesos.Items.Count >= 1 && ddlProcesos.SelectedValue != "0")
                    {
                        int idUbicacion2 = int.Parse(ddlUbicacion.SelectedValue);
                        DDLCargarCentroGasto(idUbicacion2);
                        //DDLCargarCentroGasto(int.Parse(ddlUbicacion.SelectedValue));
                        if (ddlCentroGasto.Items.Count >= 1 && ddlCentroGasto.SelectedValue != "0")
                        {
                            int idCentroGasto = int.Parse(ddlCentroGasto.SelectedValue);
                            DDLCargarClasificacion(idCentroGasto);
                        }
                    }
                }
            }
            else
            {
                ddlUbicacion.Items.Clear();
                ddlProcesos.Items.Clear();
                ddlCentroGasto.Items.Clear();
                ddlClasificacion.Items.Clear();
            }
        }
        //Cargar Listado de Ubicaciones en DropDownList
        void DDLCargarUbicaciones(long IdCentroAnalisis)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00411", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_CentroAnalisis", SqlDbType.Int).Value = IdCentroAnalisis;
                ddlUbicacion.Items.Clear();
                con.Open();
                ddlUbicacion.DataSource = cmd.ExecuteReader();
                ddlUbicacion.DataTextField = "Ubicacion";
                ddlUbicacion.DataValueField = "Id_Ubicacion";
                ddlUbicacion.DataBind();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlUbicacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarProcesos(int.Parse(ddlUbicacion.SelectedValue));
            DDLCargarCentroGasto(int.Parse(ddlUbicacion.SelectedValue));
        }
        void DDLCargarProcesos(long IdUbicacion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Ubicacion", SqlDbType.Int).Value = IdUbicacion;
                ddlProcesos.Items.Clear();
                con.Open();
                ddlProcesos.DataSource = cmd.ExecuteReader();
                ddlProcesos.DataTextField = "Proceso";
                ddlProcesos.DataValueField = "Id_Proceso";
                ddlProcesos.DataBind();
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
        void DDLCargarCentroGasto(long IdUbicacion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00407", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Ubicacion", SqlDbType.Int).Value = IdUbicacion;
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
        protected void AgregarRegistro_Click(object sender, EventArgs e)
        {
            GridViewRegistros.Visible = true;
            Insertar.Visible = true;
            DataTable dt;

            if (ViewState["RegistrosDataTable"] != null)
            {
                dt = (DataTable)ViewState["RegistrosDataTable"];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Id_TipoCombustible", typeof(int));
                dt.Columns.Add("Id_CentroAnalisis", typeof(int));
                dt.Columns.Add("Id_Ubicacion", typeof(int));
                dt.Columns.Add("Id_Proceso", typeof(int));
                dt.Columns.Add("Id_CentroGasto", typeof(int));
                dt.Columns.Add("Id_Clasificacion", typeof(int));
                dt.Columns.Add("Fecha", typeof(DateTime));
                dt.Columns.Add("NoVale", typeof(int));
                dt.Columns.Add("Kilometraje", typeof(decimal));
                dt.Columns.Add("Cantidad", typeof(decimal));
                dt.Columns.Add("Comentario", typeof(string));
                ViewState["RegistrosDataTable"] = dt;
            }
            int idtipoCombustible = Convert.ToInt32(ddlId_TipoCombustible.SelectedValue);
            int idCentroAnalisis = Convert.ToInt32(ddlCentroAnalisis.SelectedValue);
            int idUbicacion = Convert.ToInt32(ddlUbicacion.SelectedValue);
            int idProceso = Convert.ToInt32(ddlProcesos.SelectedValue);
            int idCentroGasto = Convert.ToInt32(ddlCentroGasto.SelectedValue);
            int idClasificacion = Convert.ToInt32(ddlClasificacion.SelectedValue);
            DateTime fecha = DateTime.Parse(DateFecha.Value);
            int noVale = Convert.ToInt32(NumVale.Text);
            decimal kilometraje = Convert.ToDecimal(NumKilometraje.Text);
            decimal cantidad = Convert.ToDecimal(NumCantidad.Text);
            string comentario = txtComentario.Text;

            // Agregar una nueva fila al DataTable
            DataRow newRow = dt.NewRow();
            newRow["Id_TipoCombustible"] = idtipoCombustible;
            newRow["Id_CentroAnalisis"] = idCentroAnalisis;
            newRow["Id_Ubicacion"] = idUbicacion;
            newRow["Id_Proceso"] = idProceso;
            newRow["Id_CentroGasto"] = idCentroGasto;
            newRow["Id_Clasificacion"] = idClasificacion;
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
            // Guardar el DataTable actualizado en el ViewState y Session
            ViewState["RegistrosDataTable"] = dt;
            Session["GridViewData"] = dt;
        }
        protected void GridViewRegistros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = (DataTable)ViewState["RegistrosDataTable"];
            if (dt != null)
            {
                dt.Rows.RemoveAt(rowIndex);
                ViewState["RegistrosDataTable"] = dt;
                Session["GridViewData"] = dt;
            }
            GridViewRegistros.EditIndex = -1;
            BindGridView();
        }
        private void BindGridView()
        {
            DataTable dt = (DataTable)ViewState["RegistrosDataTable"];
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

                    Label idTipoCombustible = (Label)row.FindControl("lblId_TipoCombustible");
                    Label idCentroAnalisis = (Label)row.FindControl("lblId_CentroAnalisis");
                    Label idUbicacion = (Label)row.FindControl("lblId_Ubicacion");
                    Label idProceso = (Label)row.FindControl("lblId_Proceso");
                    Label idCentroGasto = (Label)row.FindControl("lblId_CentroGasto");
                    Label idClasificacion = (Label)row.FindControl("lblId_Clasificacion");
                    Label dateFecha = (Label)row.FindControl("DateFecha");
                    Label numNoVale = (Label)row.FindControl("lblNumNoVale");
                    Label numKilometraje = (Label)row.FindControl("lblKilometraje");
                    Label numCantidad = (Label)row.FindControl("lblCantidad");
                    Label comentario = (Label)row.FindControl("lblComentario");

                    cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Decimal).Value = Session["Id_Empresa"];
                    cmd.Parameters.Add("@Id_TipoCombustible", System.Data.SqlDbType.Int).Value = Convert.ToInt32(idTipoCombustible.Text);
                    cmd.Parameters.Add("@Id_CentroAnalisis", System.Data.SqlDbType.Int).Value = Convert.ToInt32(idCentroAnalisis.Text);
                    cmd.Parameters.Add("@Id_Ubicacion", System.Data.SqlDbType.Decimal).Value = Convert.ToInt32(idUbicacion.Text);
                    cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = Convert.ToInt32(idProceso.Text);
                    cmd.Parameters.Add("@Id_CentroGasto", System.Data.SqlDbType.Int).Value = Convert.ToInt32(idCentroGasto.Text);
                    cmd.Parameters.Add("@Id_Clasificacion", System.Data.SqlDbType.Int).Value = Convert.ToInt32(idClasificacion.Text);
                    cmd.Parameters.Add("@Fecha", System.Data.SqlDbType.Date).Value = Convert.ToDateTime(dateFecha.Text);
                    cmd.Parameters.Add("@NoVale", System.Data.SqlDbType.Int).Value = Convert.ToInt32(numNoVale.Text);
                    cmd.Parameters.Add("@Kilometraje", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(numKilometraje.Text);
                    cmd.Parameters.Add("@Cantidad", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(numCantidad.Text);
                    cmd.Parameters.Add("@Comentario", System.Data.SqlDbType.NVarChar).Value = comentario.Text;
                    cmd.Parameters.Add("@Id_Usr_Crea", System.Data.SqlDbType.Decimal).Value = Session["Id_Usuario"];

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                Insertar.Visible = false;
                Response.Redirect("~/Pages/Forms/Formulario_Combustible.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }

    }
}