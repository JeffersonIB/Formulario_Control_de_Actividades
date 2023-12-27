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

namespace IT_Finca.Pages.Test
{
    public partial class DDL_GW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTipoCafe();
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Id"), new DataColumn("Name"), new DataColumn("Country1"), new DataColumn("Country2") });
                    dt.Rows.Add(1, "John Hammond", "United States", "United States 2");
                    dt.Rows.Add(2, "Mudassar Khan", "India", "India 2");
                    dt.Rows.Add(3, "Suzanne Mathews", "France", "France 2");
                    dt.Rows.Add(4, "Robert Schidner", "Russia", "Russia 2");
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt32(ddlCafe.SelectedValue);

            // Configurar visibilidad de las columnas basado en la selección del DropDownList
            GridView1.Columns[2].Visible = selectedValue == 1; // Country1
            GridView1.Columns[3].Visible = selectedValue == 2; // Country2
        }

        protected void CargarTipoCafe()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00405", con);
                cmd.CommandType = CommandType.StoredProcedure;
                ddlCafe.Items.Clear();
                con.Open();
                ddlCafe.DataSource = cmd.ExecuteReader();
                ddlCafe.DataTextField = "Id_Tipo_Cafe";
                ddlCafe.DataValueField = "Id_Tipo_Cafe";
                ddlCafe.DataBind();
                ddlCafe.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
        }

    }
}