using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace IT_Finca.Pages.Test
{
    public partial class FormsV2_1 : System.Web.UI.Page
    {
        private DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = GetDataTable();
                Session["GridViewData"] = dt;
                CargarLotes();
                CargarTipo_Actividad();
            }
            else
            {
                dt = (DataTable)Session["GridViewData"];
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
            CargarActividad1(long.Parse(ddlProcesos.SelectedValue));
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
        void CargarTipo_Actividad()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlTipo_Actividad.Items.Clear();
                con.Open();
                ddlTipo_Actividad.DataSource = cmd.ExecuteReader();
                ddlTipo_Actividad.DataTextField = "Tipo_Actividad";
                ddlTipo_Actividad.DataValueField = "Id_Tipo_Actividad";
                ddlTipo_Actividad.DataBind();
                //ddlTipo_Actividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void AgregarActividad1_Click(object sender, EventArgs e)
        {
            string ddlLotesValue = ddlLotes.SelectedValue;
            string ddlProcesosValue = ddlProcesos.SelectedValue;
            string ddlActividad1Value = ddlActividad1.SelectedValue;
            string txtIdProveedorValue = txtIdProveedor.Text;
            string ddlTipoActividadValue = ddlTipo_Actividad.SelectedItem.Text;
            string txtCantidad1Value = txtCantidad1.Text;
            
            DataRow dr = dt.NewRow();
            dr["GV_ddlLotes"] = ddlLotesValue;
            dr["GV_ddlProcesos"] = ddlProcesosValue;
            dr["GV_ddlActividad1"] = ddlActividad1Value;
            dr["GV_txtIdProveedor"] = txtIdProveedorValue;
            dr["GV_ddlTipo_Actividad"] = ddlTipoActividadValue;
            dr["GV_txtCantidad1"] = txtCantidad1Value;

            dt.Rows.Add(dr);
            GridViewCalificaciones.DataSource = dt;
            GridViewCalificaciones.DataBind();
            Session["GridViewData"] = dt;
            Insertar.Visible = GridViewCalificaciones.Rows.Count > 0;
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GV_ddlLotes", typeof(string));
            dt.Columns.Add("GV_ddlProcesos", typeof(string));
            dt.Columns.Add("GV_ddlActividad1", typeof(string));
            dt.Columns.Add("GV_txtIdProveedor", typeof(string));
            dt.Columns.Add("GV_ddlTipo_Actividad", typeof(string));
            dt.Columns.Add("GV_txtCantidad1", typeof(string));
            return dt;
        }
        protected void GridViewCalificaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewCalificaciones.EditIndex = e.NewEditIndex;
            BindGridView();
        }
        protected void GridViewCalificaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = (DataTable)Session["GridViewData"];
            GridViewRow editedRow = GridViewCalificaciones.Rows[rowIndex];
            //string ddlLotes = e.NewValues["GV_ddlLotes"].ToString();
            //string ddlProcesos = e.NewValues["GV_ddlProcesos"].ToString();
            //string GV_ddlActividad1 = e.NewValues["GV_ddlActividad1"].ToString();
            string txtIdProveedor = e.NewValues["GV_txtIdProveedor"].ToString();
            string ddlTipo_Actividad = e.NewValues["GV_ddlTipo_Actividad"].ToString();
            string txtCantidad1 = e.NewValues["GV_txtCantidad1"].ToString();
            //dt.Rows[rowIndex]["GV_ddlLotes"] = ddlLotes;
            //dt.Rows[rowIndex]["GV_ddlProcesos"] = ddlProcesos;
            //dt.Rows[rowIndex]["GV_ddlActividad1"] = GV_ddlActividad1;
            dt.Rows[rowIndex]["GV_txtIdProveedor"] = txtIdProveedor;
            dt.Rows[rowIndex]["GV_ddlTipo_Actividad"] = ddlTipo_Actividad;
            dt.Rows[rowIndex]["GV_txtCantidad1"] = txtCantidad1;
            GridViewCalificaciones.EditIndex = -1;
            BindGridView();
        }
        protected void GridViewCalificaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewCalificaciones.EditIndex = -1;
            BindGridView();
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
            Insertar.Visible = false;
            DataTable dataTable = (DataTable)Session["GridViewData"];
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                foreach (DataRow row in dataTable.Rows)
                {
                    int IdProveedor = Convert.ToInt32(row["GV_txtIdProveedor"]);
                    if (ddlLotes != null)
                    {
                        SqlCommand cmd = new SqlCommand("SP_AG_FNC00600_3", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = ddlLotes.Text;
                        cmd.Parameters.AddWithValue("@Id_Proveedor", IdProveedor);
                        cmd.Parameters.AddWithValue("@Id_Tipo_Actividad", System.Data.SqlDbType.Int).Value = ddlTipo_Actividad.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            GridViewCalificaciones.DataSource = null;
            GridViewCalificaciones.DataBind();
            ViewState["GridViewData"] = GetDataTable();
            ddlLotes.ClearSelection();
            ddlProcesos.ClearSelection();
            ddlActividad1.ClearSelection();
            GetDataTable();
            dt.Rows.Clear();
        }
        //protected void EliminarTodasLasFilas_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["GridViewData"];

        //    // Eliminar todas las filas
        //    dt.Rows.Clear();

        //    // Actualizar el GridView
        //    BindGridView();
        //}

    }
}