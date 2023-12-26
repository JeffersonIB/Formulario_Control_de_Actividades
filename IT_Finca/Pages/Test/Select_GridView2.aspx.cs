using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Test
{
    public partial class Select_GridView2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    TB_Beneficio();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        private DataTable GetFilteredData(string fecha)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM VW_FNC00602_2 ORDER BY Fecha_Crea,Finca,Lote ASC", con);
            cmd.CommandType = System.Data.CommandType.Text;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (!string.IsNullOrEmpty(fecha))
            {
                string formattedFecha = DateTime.Parse(fecha).ToString("yyyy-MM-dd");
                dt.DefaultView.RowFilter = $"Fecha_Crea = #{formattedFecha}#";
                dt = dt.DefaultView.ToTable();
            }
            con.Close();
            return dt;
        }
        void TB_Beneficio()
        {
            try
            {
                DataTable dt = GetFilteredData("");
                gvBeneficio.DataSource = dt;
                gvBeneficio.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void GetSelectedRecords(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Finca"), new DataColumn("Maduro") });

            foreach (GridViewRow row in gvBeneficio.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control CheckBox utilizando el atributo 'type' y 'class'
                    HtmlInputCheckBox chkRow = (row.Cells[0].FindControl("chkRow") as HtmlInputCheckBox);

                    if (chkRow != null && chkRow.Checked)
                    {
                        string finca = (row.Cells[1].FindControl("lbl_Finca") as Label).Text;
                        string maduro = (row.Cells[2].FindControl("lbl_Maduro") as Label).Text;
                        dt.Rows.Add(finca, maduro);
                    }
                }
            }
            gvSelected.DataSource = dt;
            gvSelected.DataBind();
        }

    }
}