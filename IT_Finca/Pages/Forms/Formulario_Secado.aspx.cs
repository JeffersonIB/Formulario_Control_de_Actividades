using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration.Provider;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace IT_Finca.Pages.Forms
{
    public partial class Formualario_Secado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    GVTiposCafe();
                    DDLTipoSecado();
                    DDLPartidas();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar GridView para selecciónar tipo de café
        private void GVTiposCafe()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_FNC00405", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GridViewTiposCafe.DataSource = dt;
                        GridViewTiposCafe.DataBind();
                    }
                }
            }
        }
        // Cargar tabla según elección en GridView tipo de café
        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            Tipo_Secado.Visible =true;
            Agregar.Visible = true;
            //Confirmar.Visible = true;
            bool algunSeleccionado = false;
            List<int> tiposCafeSeleccionados = new List<int>();
            foreach (GridViewRow row in GridViewTiposCafe.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    algunSeleccionado = true;
                    int idTipoCafe = Convert.ToInt32(GridViewTiposCafe.DataKeys[row.RowIndex].Value);
                    tiposCafeSeleccionados.Add(idTipoCafe);
                }
            }
            if (algunSeleccionado)
            {
                MostrarDatosPorTipoCafe(tiposCafeSeleccionados);
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona al menos un tipo de café.";
            }
        }
        private void MostrarDatosPorTipoCafe(List<int> tiposCafeSeleccionados)
        {
            Session["tiposCafeSeleccionados"] = tiposCafeSeleccionados;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_FNC00602_5", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    string tiposCafe = string.Join(",", tiposCafeSeleccionados);
                    cmd.Parameters.Add("@Id_Tipo_Cafe", SqlDbType.NVarChar).Value = tiposCafe;
                    con.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GridViewResultados.DataSource = dt;
                        GridViewResultados.DataBind();
                    }
                }
            }
        }
        protected void GridViewResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewResultados.PageIndex = e.NewPageIndex;
            List<int> tiposCafeSeleccionados = Session["tiposCafeSeleccionados"] as List<int>;
            if (tiposCafeSeleccionados != null)
            {
                MostrarDatosPorTipoCafe(tiposCafeSeleccionados);
            }
            else
            {

            }
        }
        //Cargar DropDowList de Tipo secado
        void DDLTipoSecado()
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
                //ddlTipo_Secado.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar DropDowList de partidas
        void DDLPartidas()
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
                //ddlPartida.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Confirmar checkbox selecccionados en GridView
        protected void BTNAgregar_Click(object sender, EventArgs e)
        {
            // Obtén los valores seleccionados de los DropDownLists
            int idTipoSecado = Convert.ToInt32(ddlTipo_Secado.SelectedValue);
            int idPartida = Convert.ToInt32(ddlPartida.SelectedValue);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                foreach (GridViewRow row in GridViewResultados.Rows)
                {
                    // Verifica si el checkbox está seleccionado
                    CheckBox chkInsertar = (CheckBox)row.FindControl("chkInsertar");
                    if (chkInsertar != null && chkInsertar.Checked)
                    {
                        // Obtén los valores de cada columna del GridView
                        int idBeneficio = Convert.ToInt32(((Label)row.FindControl("lbl_Id_Beneficio")).Text);
                        int idEmpresa = Convert.ToInt32(((Label)row.FindControl("lbl_Id_Empresa")).Text);
                        int idFinca = Convert.ToInt32(((Label)row.FindControl("lbl_Id_Finca")).Text);
                        int idLote = Convert.ToInt32(((Label)row.FindControl("lbl_Id_Lote")).Text);
                        int idProceso = Convert.ToInt32(((Label)row.FindControl("lbl_Id_Proceso")).Text);
                        decimal cantidad = Convert.ToDecimal(((Label)row.FindControl("lbl_Cantidad")).Text);
                        DateTime fecha = Convert.ToDateTime(((Label)row.FindControl("lbl_Fecha_Crea")).Text);
                        // Llama al procedimiento almacenado para insertar los datos
                        using (SqlCommand cmd = new SqlCommand("SP_AG_FNC00606", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id_Beneficio", idBeneficio);
                            cmd.Parameters.AddWithValue("@Id_Empresa", idEmpresa);
                            cmd.Parameters.AddWithValue("@Id_Finca", idFinca);
                            cmd.Parameters.AddWithValue("@Id_Lote", idLote);
                            cmd.Parameters.AddWithValue("@Id_Proceso", idProceso);
                            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmd.Parameters.AddWithValue("@Fecha_Crea_R", fecha);
                            cmd.Parameters.AddWithValue("@Id_Tipo_Secado", idTipoSecado);                            
                            cmd.Parameters.AddWithValue("@Id_Partida", idPartida);
                            cmd.Parameters.AddWithValue("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                con.Close();
                Response.Redirect("~/Pages/Forms/Formulario_Secado.aspx");
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