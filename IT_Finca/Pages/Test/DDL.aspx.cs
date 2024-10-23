using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Test
{
    public partial class DDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DDLCargarCentroAnalisis();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
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
            int selectedValue;
            if (int.TryParse(ddlCentroAnalisis.SelectedValue, out selectedValue))
            {
                DDLCargarUbicaciones(selectedValue);
            }
            else
            {
                //ddlUbicacion.Items.Insert(0, new ListItem("No se encontraron ubicaciones", "0"));
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
                ddlUbicacion.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlUbicacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}