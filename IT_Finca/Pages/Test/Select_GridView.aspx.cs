using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Test
{
    public partial class Select_GridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Country") });
                dt.Rows.Add("John Hammond", "Canada");
                dt.Rows.Add("Rick Stewards", "United States");
                dt.Rows.Add("Huang He", "China");
                dt.Rows.Add("Mudassar Khan", "India");
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        protected void GetSelectedRecords(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Country") });
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string name = row.Cells[1].Text;
                        string country = (row.Cells[2].FindControl("lblCountry") as Label).Text;
                        dt.Rows.Add(name, country);
                    }
                }
            }
            gvSelected.DataSource = dt;
            gvSelected.DataBind();
        }
    }
}