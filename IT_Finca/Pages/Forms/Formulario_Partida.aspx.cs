using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT_Finca.Pages.Admin;
using Newtonsoft.Json.Linq;

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
                    DDLPartidas();
                    DDLAlmacenaje();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void DDLPartidas()
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
        void DDLAlmacenaje()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00406", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlAlmacenaje.Items.Clear();
                con.Open();
                ddlAlmacenaje.DataSource = cmd.ExecuteReader();
                ddlAlmacenaje.DataTextField = "Tipo_Almacenaje";
                ddlAlmacenaje.DataValueField = "Id_Tipo_Almacenaje";
                ddlAlmacenaje.DataBind();
                ddlAlmacenaje.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void Agregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_AG_FNC00608", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Partida", Convert.ToInt32(ddlPartida.SelectedValue));
                    cmd.Parameters.AddWithValue("@Humedad", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantHumedad.Text);
                    cmd.Parameters.AddWithValue("@Pergamino", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantPergamino.Text);
                    cmd.Parameters.AddWithValue("@Chibolita_N", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantChibolita.Text);
                    cmd.Parameters.AddWithValue("@Segunda", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantSegunda.Text);
                    cmd.Parameters.AddWithValue("@Natas", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantNatas.Text);
                    cmd.Parameters.AddWithValue("@Flotes", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantFlotes.Text);
                    cmd.Parameters.AddWithValue("@Chibolita_S_V", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantAlmacenaje.Text);
                    cmd.Parameters.AddWithValue("@Id_Tipo_Almacenaje", Convert.ToInt32(ddlAlmacenaje.SelectedValue));
                    cmd.Parameters.AddWithValue("@Almacenaje", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(CantAlmacenaje.Text);
                    cmd.Parameters.AddWithValue("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("~/Pages/Forms/Formulario_Partida.aspx");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
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