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
using IT_Ubicacion.Pages.AdminCombustible;
using System.Text;

namespace IT_Finca.Pages.Forms
{
    public partial class Formulario_Beneficio : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM VW_FNC00602 ORDER BY Fecha_Crea DESC", con);
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
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_FNC00602_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Beneficio", System.Data.SqlDbType.Int).Value = Convert.ToInt32(lbId_Beneficio.Text);
                cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Empresa"]);
                cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = Convert.ToInt32(lbId_Finca.Text);
                cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = Convert.ToInt32(lbId_Lote.Text);
                cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = Convert.ToInt32(lbId_Proceso.Text);
                cmd.Parameters.AddWithValue("@Id_Actividad", System.Data.SqlDbType.Int).Value = Convert.ToInt32(lbId_Actividad.Text);
                cmd.Parameters.AddWithValue("@Fecha_Crea_V", System.Data.SqlDbType.Date).Value = Convert.ToDateTime(lbFecha_Crea_V.Text);
                cmd.Parameters.AddWithValue("@Verde_R", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(txVerde.Text);
                cmd.Parameters.AddWithValue("@Maduro_R", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(txMaduro.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/Forms/Formulario_Beneficio.aspx");
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
        protected void gvBeneficio_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Beneficio.Text = gvBeneficio.DataKeys[gvrow.RowIndex].Value.ToString();
                lbId_Finca.Text = (gvrow.FindControl("gv_Id_Finca") as Label).Text;
                lbId_Lote.Text = (gvrow.FindControl("gv_Id_Lote") as Label).Text;
                lbId_Proceso.Text = (gvrow.FindControl("gv_Id_Proceso") as Label).Text;
                lbId_Actividad.Text = (gvrow.FindControl("gv_Id_Actividad") as Label).Text;
                lbFecha_Crea_V.Text = (gvrow.FindControl("gvFecha_Crea") as Label).Text;
                txVerde.Text = (gvrow.FindControl("gv_Verde") as Label).Text;
                txMaduro.Text = (gvrow.FindControl("gv_Maduro") as Label).Text;
                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Beneficio.Text = gvBeneficio.DataKeys[gvrow.RowIndex].Value.ToString();
                txVerde.Text = (gvrow.FindControl("gv_Verde") as Label).Text;
                ModalEl(true);
            }
        }
        //Traer Modal_Actualizar
        void ModalAc(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                builder.Append("<script language=JavaScript> ShowModalAc(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModalAc", builder.ToString());
            }
            else
            {
                builder.Append("<script language=JavaScript> CloseModalAc(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseModalAc", builder.ToString());
            }
        }
        //Traer Modal_Eliminar
        void ModalEl(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                builder.Append("<script language=JavaScript> ShowModalEl(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModalAc", builder.ToString());
            }
            else
            {
                builder.Append("<script language=JavaScript> CloseModalEl(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseModalAc", builder.ToString());
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
                Label IdBeneficio = row.FindControl("gv_Id_Beneficio") as Label;
                Label IdFinca = row.FindControl("gv_Id_Finca") as Label;
                Label IdLote = row.FindControl("gv_Id_Lote") as Label;
                Label IdProceso = row.FindControl("gv_Id_Proceso") as Label;
                Label IdActividad = row.FindControl("gv_Id_Actividad") as Label;
                Label FechaCrea = row.FindControl("gvFecha_Crea") as Label;
                Label VerdeR = row.FindControl("gv_Verde") as Label;
                Label MaduroR = row.FindControl("gv_Maduro") as Label;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString()))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_AG_FNC00604_1", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Beneficio", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdBeneficio.Text);
                        cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Empresa"]);
                        cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdFinca.Text);
                        cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdLote.Text);
                        cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdProceso.Text);
                        cmd.Parameters.AddWithValue("@Id_Actividad", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdActividad.Text);
                        cmd.Parameters.AddWithValue("@Fecha_Crea_V", System.Data.SqlDbType.Date).Value = Convert.ToDateTime(FechaCrea.Text);
                        cmd.Parameters.AddWithValue("@Verde_R", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(VerdeR.Text);
                        cmd.Parameters.AddWithValue("@Maduro_R", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(MaduroR.Text);
                        cmd.Parameters.AddWithValue("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Usuario"]);
                        cmd.ExecuteNonQuery();
                    }
                }
                gvBeneficio.EditIndex = -1;
                TB_Beneficio();
            }
            catch (Exception)
            {
                throw;
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