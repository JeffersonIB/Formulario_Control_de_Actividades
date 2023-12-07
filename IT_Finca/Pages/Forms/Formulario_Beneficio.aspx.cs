using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IT_Finca.Pages.Admin;
using System.Globalization;

namespace IT_Finca.Pages.Forms
{
    public partial class Formulario_Beneficio : System.Web.UI.Page
    {
        //string cs = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //SqlConnection comn;
        //SqlDataAdapter adapt;
        //DataTable dt;
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string fecha = Calendario.Value;
                DataTable dt = GetFilteredData(fecha);
                gvBeneficio.DataSource = dt;
                gvBeneficio.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private DataTable GetFilteredData(string fecha)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM VW_FNC00602 ORDER BY Fecha_Crea,Id_Finca,Lote ASC", con);
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
        protected void gvBeneficio_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Beneficio();
        }
        protected void gvBeneficio_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvBeneficio.EditIndex = e.NewEditIndex;
            TB_Beneficio();
        }
        protected void gvBeneficio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Obtener los nuevos valores de los TextBox
                TextBox VerdeR = gvBeneficio.Rows[e.RowIndex].FindControl("txt_Verde") as TextBox;
                TextBox MaduroR = gvBeneficio.Rows[e.RowIndex].FindControl("txt_Maduro") as TextBox;
                // Obtener el resto de los valores necesarios
                Label IdBeneficio = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Id_Beneficio") as Label;
                Label IdFinca = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Id_Finca") as Label;
                Label Finca = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Finca") as Label;
                Label IdLote = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Id_Lote") as Label;
                Label Lote = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Lote") as Label;
                //Label VerdeV = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Verde") as Label;
                //Label MaduroV = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Maduro") as Label;
                Label FechaCrea = gvBeneficio.Rows[e.RowIndex].FindControl("lbl_Fecha_Crea") as Label;
                // Abrir la conexión
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString()))
                {
                    con.Open();
                    // Llamar al Stored Procedure
                    using (SqlCommand cmd = new SqlCommand("SP_AG_FNC00604", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Beneficio", Convert.ToInt32(IdBeneficio.Text));
                        cmd.Parameters.AddWithValue("@Id_Finca", Convert.ToInt32(IdFinca.Text));
                        cmd.Parameters.AddWithValue("@Finca", Finca.Text);
                        cmd.Parameters.AddWithValue("@Id_Lote", Convert.ToInt32(IdLote.Text));
                        cmd.Parameters.AddWithValue("@Lote", Lote.Text);
                        //cmd.Parameters.AddWithValue("@Verde_V", Convert.ToDecimal(VerdeV.Text));
                        //cmd.Parameters.AddWithValue("@Maduro_V", Convert.ToDecimal(MaduroV.Text));
                        cmd.Parameters.AddWithValue("@Fecha_Crea_V", Convert.ToDateTime(FechaCrea.Text));
                        cmd.Parameters.AddWithValue("@Verde_R", Convert.ToDecimal(VerdeR.Text));
                        cmd.Parameters.AddWithValue("@Maduro_R", Convert.ToDecimal(MaduroR.Text));
                        cmd.Parameters.AddWithValue("@Id_Usr_Crea", Convert.ToInt32(Session["Id_Usuario"]));
                        cmd.ExecuteNonQuery();
                    }
                }
                // Finalizar la edición
                gvBeneficio.EditIndex = -1;
                TB_Beneficio();
            }
            catch (Exception)
            {
                // Manejar la excepción, por ejemplo, mostrar un mensaje o registrarla
            }
        }

        protected void gvBeneficio_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview
            gvBeneficio.EditIndex = -1;
            TB_Beneficio();
        }
        protected void btn_Confir_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btn_Confir = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn_Confir.NamingContainer;
                Label IdBeneficio = row.FindControl("lbl_Id_Beneficio") as Label;
                Label IdFinca = row.FindControl("lbl_Id_Finca") as Label;
                Label Finca = row.FindControl("lbl_Finca") as Label;
                Label IdLote = row.FindControl("lbl_Id_Lote") as Label;
                Label Lote = row.FindControl("lbl_Lote") as Label;
                Label FechaCrea = row.FindControl("lbl_Fecha_Crea") as Label;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString()))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_AG_FNC00604_1", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Beneficio", Convert.ToInt32(IdBeneficio.Text));
                        cmd.Parameters.AddWithValue("@Id_Finca", Convert.ToInt32(IdFinca.Text));
                        cmd.Parameters.AddWithValue("@Finca", Finca.Text);
                        cmd.Parameters.AddWithValue("@Id_Lote", Convert.ToInt32(IdLote.Text));
                        cmd.Parameters.AddWithValue("@Lote", Lote.Text);
                        cmd.Parameters.AddWithValue("@Fecha_Crea_V", Convert.ToDateTime(FechaCrea.Text));
                        cmd.Parameters.AddWithValue("@Id_Usr_Crea", Convert.ToInt32(Session["Id_Usuario"]));
                        cmd.ExecuteNonQuery();
                    }
                }
                gvBeneficio.EditIndex = -1;
                TB_Beneficio();
            }
            catch (Exception)
            {
                // Manejar la excepción, por ejemplo, mostrar un mensaje o registrarla
            }
        }
        //Error con texto en mayuscula
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-1.8.3.min.js",
                DebugPath = "~/scripts/jquery-1.8.3.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.js"
            });
        }
    }
}