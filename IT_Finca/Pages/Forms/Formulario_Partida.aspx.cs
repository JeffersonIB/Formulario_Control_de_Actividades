using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Forms
{
    public partial class Formulario_Partida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    CargarPartidas();
                    CargarAlmacenaje();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void CargarPartidas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00404_1", con);
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
        void CargarAlmacenaje()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00404_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlSacoJumbo.Items.Clear();
                con.Open();
                ddlSacoJumbo.DataSource = cmd.ExecuteReader();
                ddlSacoJumbo.DataTextField = "Partida";
                ddlSacoJumbo.DataValueField = "Id_Partida";
                ddlSacoJumbo.DataBind();
                //ddlSacoJumbo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
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