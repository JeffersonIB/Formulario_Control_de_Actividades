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
    public partial class Test : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                refreshdata();
            }
        }



        public void refreshdata()
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("select * from FNC00401 Where IsActive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                con.Open();
                DropDownList DropDownList1 = (e.Row.FindControl("DropDownList1") as DropDownList);


                SqlCommand cmd = new SqlCommand("select * from FNC00401", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                DropDownList1.DataSource = dt;

                DropDownList1.DataTextField = "Tipo_Actividad";
                DropDownList1.DataValueField = "Id_Tipo_Actividad";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--Select Qualification--", "0"));


            }

        }
    }
}