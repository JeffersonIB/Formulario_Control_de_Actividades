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
    public partial class Formulario_Combustible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Usuario"] != null)
            {
                DataTable dt = new DataTable();
                Session["GridViewData"] = dt;
                BindGridView();
                //DDLCargarFincas();
                DDLCargarLotes();
            }
            else
            {

            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar Listado de Fincas en DropDownList
        //void DDLCargarFincas()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Empresa"]);
        //        ddlFincas.Items.Clear();
        //        con.Open();
        //        ddlFincas.DataSource = cmd.ExecuteReader();
        //        ddlFincas.DataTextField = "Finca";
        //        ddlFincas.DataValueField = "Id_Finca";
        //        ddlFincas.DataBind();
        //        ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
        //        con.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //protected void ddlFincas_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlLotes.ClearSelection();
        //    DDLCargarLotes(int.Parse(ddlFincas.SelectedValue));
        //}
        //Cargar Listado de Lotes en DropDownList
        void DDLCargarLotes()
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
            DDLCargarProcesos(int.Parse(ddlLotes.SelectedValue));
            ddlClasificacion.ClearSelection();
            DDLCargarClasificacion(int.Parse(ddlLotes.SelectedValue));
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
            ddlCentroAnalisis.ClearSelection();
            DDLCargarCentroAnalisis(int.Parse(ddlProcesos.SelectedValue));
            ddlCentroGasto.ClearSelection();
            DDLCargarCentroGasto(int.Parse(ddlProcesos.SelectedValue));
        }
        void DDLCargarCentroAnalisis(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00409", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlCentroAnalisis.Items.Clear();
                con.Open();
                ddlCentroAnalisis.DataSource = cmd.ExecuteReader();
                ddlCentroAnalisis.DataTextField = "CentroAnalisis";
                ddlCentroAnalisis.DataValueField = "Id_CentroAnalisis";
                ddlCentroAnalisis.DataBind();
                //ddlCentroAnalisis.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlCentroAnalisis_OnSelectedIndexChanged(object sender, EventArgs e)
        {

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

        }
        void DDLCargarClasificacion(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00408", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
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
        //void DDLCargarTipo(long IdProceso)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FNC00407", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
        //        ddlCentroGasto.Items.Clear();
        //        con.Open();
        //        ddlCentroGasto.DataSource = cmd.ExecuteReader();
        //        ddlCentroGasto.DataTextField = "CentroGasto";
        //        ddlCentroGasto.DataValueField = "Id_CentroGasto";
        //        ddlCentroGasto.DataBind();
        //        //ddlCentroGasto.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
        //        con.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
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
                dt.Columns.Add("Id_Finca", typeof(int));
                dt.Columns.Add("Id_Lote", typeof(int));
                dt.Columns.Add("Id_Proceso", typeof(int));
                dt.Columns.Add("Id_CentroAnalisis", typeof(int));
                dt.Columns.Add("Id_CentroGasto", typeof(int));
                dt.Columns.Add("Id_Clasificacion", typeof(int));
                dt.Columns.Add("Fecha", typeof(DateTime));
                dt.Columns.Add("NoVale", typeof(int));
                dt.Columns.Add("Kilometraje", typeof(decimal));
                dt.Columns.Add("Cantidad", typeof(decimal));
                dt.Columns.Add("Comentario", typeof(string));
                ViewState["RegistrosDataTable"] = dt;
            }

            int idLote = Convert.ToInt32(ddlLotes.SelectedValue);
            int idProceso = Convert.ToInt32(ddlProcesos.SelectedValue);
            int idCentroAnalisis = Convert.ToInt32(ddlCentroAnalisis.SelectedValue);
            int idCentroGasto = Convert.ToInt32(ddlCentroGasto.SelectedValue);
            int idClasificacion = Convert.ToInt32(ddlClasificacion.SelectedValue);
            DateTime fecha = DateTime.Parse(DateFecha.Value);
            int noVale = Convert.ToInt32(NumVale.Text);
            decimal kilometraje = Convert.ToDecimal(NumKilometraje.Text);
            decimal cantidad = Convert.ToDecimal(NumCantidad.Text);
            string comentario = txtComentario.Text;

            // Agregar una nueva fila al DataTable
            DataRow newRow = dt.NewRow();
            newRow["Id_Lote"] = idLote;
            newRow["Id_Proceso"] = idProceso;
            newRow["Id_CentroAnalisis"] = idCentroAnalisis;
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
        protected void GridViewRegistros_RowCreated(object sender, GridViewRowEventArgs e)
        {
            
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
                    //int idEmpresa = Convert.ToInt32(Session["Id_Empresa"]);
                    //int idFinca = Convert.ToInt32(Session["Id_Finca"]);
                    int idLote = Convert.ToInt32((row.FindControl("lblId_Lote") as Label)?.Text);
                    //int idProceso = Convert.ToInt32((row.FindControl("lblId_Proceso") as Label)?.Text);                    
                    //int idCentroGasto= Convert.ToInt32((row.FindControl("lblId_CentroGasto") as Label)?.Text);
                    //int idClasificacion = Convert.ToInt32((row.FindControl("lblId_Clasificacion") as Label)?.Text);
                    //TextBox dateFecha = (TextBox)row.FindControl("DateFecha");
                    //TextBox numVale = (TextBox)row.FindControl("NumNoVale");
                    //TextBox kilometraje = (TextBox)row.FindControl("Kilometraje");
                    //TextBox numCantidad = (TextBox)row.FindControl("NumCantidad");
                    TextBox txtComentario = (TextBox)row.FindControl("TxtComentario");
                    //cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = idEmpresa;
                    //cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = idFinca;
                    cmd.Parameters.Add("@Id_Lote", System.Data.SqlDbType.Int).Value = idLote;
                    //cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = idProceso;
                    //cmd.Parameters.Add("@Id_CentroGasto", System.Data.SqlDbType.Int).Value = idCentroGasto;
                    //cmd.Parameters.Add("@Id_Clasificacion", System.Data.SqlDbType.Int).Value = idClasificacion;
                    //cmd.Parameters.Add("@Fecha", System.Data.SqlDbType.Date).Value = dateFecha;
                    //cmd.Parameters.Add("@NoVale", System.Data.SqlDbType.Int).Value = numVale;
                    //cmd.Parameters.Add("@Kilometraje", System.Data.SqlDbType.Decimal).Value = kilometraje;
                    //cmd.Parameters.Add("@Cantidad", System.Data.SqlDbType.Decimal).Value = numCantidad;
                    cmd.Parameters.Add("@Comentario", System.Data.SqlDbType.Decimal).Value = txtComentario;
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