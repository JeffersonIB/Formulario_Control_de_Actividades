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
    public partial class Formulario_Secado : System.Web.UI.Page
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
                    CargarTipoSecado();
                    CargarPartidas();
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
        //Cargar DropDowList de Tipo secado
        void CargarTipoSecado()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00403", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlTipo_Secado.Items.Clear();
                con.Open();
                ddlTipo_Secado.DataSource = cmd.ExecuteReader();
                ddlTipo_Secado.DataTextField = "Tipo_Secado";
                ddlTipo_Secado.DataValueField = "Id_Tipo_Secado";
                ddlTipo_Secado.DataBind();
                ddlTipo_Secado.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar DropDowList de Lotes
        void CargarPartidas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00404", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlPartida.Items.Clear();
                con.Open();
                ddlPartida.DataSource = cmd.ExecuteReader();
                ddlPartida.DataTextField = "Partida";
                ddlPartida.DataValueField = "Id_Partida";
                ddlPartida.DataBind();
                ddlPartida.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //protected void gvBeneficio_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlTipo_Secado = (DropDownList)e.Row.FindControl("ddlTipo_Secado");
        //        DropDownList ddlPartida = (DropDownList)e.Row.FindControl("ddlPartida");
        //        if (ddlTipo_Secado != null)
        //        {
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("SP_FNC00403", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            sda.Fill(dt);
        //            con.Close();
        //            ddlTipo_Secado.DataSource = dt;
        //            ddlTipo_Secado.DataTextField = "Tipo_Secado";
        //            ddlTipo_Secado.DataValueField = "Id_Tipo_Secado";
        //            ddlTipo_Secado.DataBind();
        //            //ddlTipo_Actividad.Items.Insert(0, new ListItem("--Select Qualification--", "0"));
        //        }
        //        if (ddlPartida != null)
        //        {
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("SP_FNC00404", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            sda.Fill(dt);
        //            con.Close();
        //            ddlPartida.DataSource = dt;
        //            ddlPartida.DataTextField = "Partida";
        //            ddlPartida.DataValueField = "Id_Partida";
        //            ddlPartida.DataBind();
        //            //ddlTipo_Actividad.Items.Insert(0, new ListItem("--Select Qualification--", "0"));
        //        }
        //    }
        //}
        protected void gvBeneficio_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Beneficio();
        }
        protected void btn_Confir_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btn_Confir = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn_Confir.NamingContainer;

                Label IdBeneficio = row.FindControl("lbl_Id_Beneficio_R") as Label;
                Label FechaCrea = row.FindControl("lbl_Fecha_Crea") as Label;
                Label IdFinca = row.FindControl("lbl_Id_Finca") as Label;
                Label Finca = row.FindControl("lbl_Finca") as Label;
                Label IdLote = row.FindControl("lbl_Id_Lote") as Label;
                Label Lote = row.FindControl("lbl_Lote") as Label;
                Label Verde = row.FindControl("lbl_Verde") as Label;
                Label Maduro = row.FindControl("lbl_Maduro") as Label;
                DropDownList ddlTipo_Secado = (DropDownList)row.FindControl("ddlTipo_Secado");
                DropDownList ddlPartida = (DropDownList)row.FindControl("ddlPartida");

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString()))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_AG_FNC00605", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Beneficio_R", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdBeneficio.Text);
                        //cmd.Parameters.AddWithValue("@Fecha_Crea_R", System.Data.SqlDbType.Int).Value = 1;
                        cmd.Parameters.AddWithValue("@Fecha_Crea_R", Convert.ToDateTime(FechaCrea.Text));
                        cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdFinca.Text);
                        cmd.Parameters.AddWithValue("@Finca", System.Data.SqlDbType.VarChar).Value = Finca.Text;
                        cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = Convert.ToInt32(IdLote.Text);
                        cmd.Parameters.AddWithValue("@Lote", System.Data.SqlDbType.VarChar).Value = Lote.Text;
                        cmd.Parameters.AddWithValue("@Verde_R", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(Verde.Text);
                        cmd.Parameters.AddWithValue("@Maduro_R", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(Maduro.Text);
                        cmd.Parameters.AddWithValue("@Id_Tipo_Secado", System.Data.SqlDbType.Int).Value = Convert.ToInt32(ddlTipo_Secado.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_Partida", System.Data.SqlDbType.Int).Value = Convert.ToInt32(ddlPartida.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Usuario"]);
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