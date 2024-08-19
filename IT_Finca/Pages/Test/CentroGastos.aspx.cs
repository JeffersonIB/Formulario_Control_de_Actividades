using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Test
{
    public partial class CentroGastos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Usuario"] != null)
            {
                DataTable dt = new DataTable();
                Session["GridViewData"] = dt;
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
        }//Cargar Listado de Lotes en DropDownList para Modal
        protected void ddlLotes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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
        protected void Insertar_Click(object sender, EventArgs e)
        {
            try
            {
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