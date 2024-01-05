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
                    DDLTipoCafe();
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
        //Cargar DropDownList para selecciónar tipo de café
        void DDLTipoCafe()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00405", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlCafe.Items.Clear();
                con.Open();
                ddlCafe.DataSource = cmd.ExecuteReader();
                ddlCafe.DataTextField = "Tipo_Cafe";
                ddlCafe.DataValueField = "Id_Tipo_Cafe";
                ddlCafe.DataBind();
                ddlCafe.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Cargar tabla según elección en DropDownList Tipo Café
        protected void BTNBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCafe.SelectedIndex <= 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Debe seleccionar el tipo de café !', 'error')", true);
                }
                if (ddlCafe.SelectedIndex > 0 && ddlCafe.SelectedIndex < 2)
                {
                    TB_Secado1();
                    GVSecado.Columns[6].Visible = true;
                    GVSecado.Columns[7].Visible = false;
                    Tipo_Secado.Visible = true;
                    Agregar.Visible = true;
                    Confirmar.Visible = true;
                }
                else if (ddlCafe.SelectedIndex > 1)
                {
                    TB_Secado2();
                    GVSecado.Columns[6].Visible = false;
                    GVSecado.Columns[7].Visible = true;
                    Tipo_Secado.Visible = true;
                    Agregar.Visible = true;
                    Confirmar.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GridView Tipo café Verde
        void TB_Secado1()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_TB_FNC00602_3", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GVSecado.DataSource = dt;
                GVSecado.DataBind();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GridView Tipo café Maduro
        void TB_Secado2()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_TB_FNC00602_4", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GVSecado.DataSource = dt;
                GVSecado.DataBind();
                con.Close();
            }
            catch (Exception)
            {
                throw;
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
                ddlTipo_Secado.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
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
                ddlPartida.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
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
            try
            {
                if (ddlCafe.SelectedIndex <= 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Debe seleccionar el tipo de café !', 'error')", true);
                }
                if (ddlCafe.SelectedIndex > 0 && ddlCafe.SelectedIndex < 2)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Id_Beneficio_R"), new DataColumn("Fecha_Crea"), new DataColumn("Id_Finca"), new DataColumn("Finca"), new DataColumn("Id_Lote"), new DataColumn("Lote"), new DataColumn("Libras") });
                    foreach (GridViewRow row in GVSecado.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            HtmlInputCheckBox chkRow = (row.Cells[0].FindControl("chkRow") as HtmlInputCheckBox);
                            if (chkRow != null && chkRow.Checked)
                            {
                                string id = (row.Cells[1].FindControl("lbl_Id_Beneficio_R") as Label).Text;
                                string fecha = (row.Cells[2].FindControl("lbl_Fecha_Crea") as Label).Text;
                                string idfinca = (row.Cells[3].FindControl("lbl_Id_Finca") as Label).Text;
                                string finca = (row.Cells[4].FindControl("lbl_Finca") as Label).Text;
                                string idlote = (row.Cells[5].FindControl("lbl_Id_Lote") as Label).Text;
                                string lote = (row.Cells[6].FindControl("lbl_Lote") as Label).Text;
                                string verde = (row.Cells[7].FindControl("lbl_Verde") as Label).Text;
                                dt.Rows.Add(id, fecha, idfinca, finca, idlote, lote, verde);
                            }
                        }
                    }
                    GVSecado2.DataSource = dt;
                    GVSecado2.DataBind();
                }
                else if (ddlCafe.SelectedIndex > 1)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Id_Beneficio_R"), new DataColumn("Fecha_Crea"), new DataColumn("Id_Finca"), new DataColumn("Finca"), new DataColumn("Id_Lote"), new DataColumn("Lote"), new DataColumn("Libras") });
                    foreach (GridViewRow row in GVSecado.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            HtmlInputCheckBox chkRow = (row.Cells[0].FindControl("chkRow") as HtmlInputCheckBox);
                            if (chkRow != null && chkRow.Checked)
                            {
                                string id = (row.Cells[1].FindControl("lbl_Id_Beneficio_R") as Label).Text;
                                string fecha = (row.Cells[2].FindControl("lbl_Fecha_Crea") as Label).Text;
                                string idfinca = (row.Cells[3].FindControl("lbl_Id_Finca") as Label).Text;
                                string finca = (row.Cells[4].FindControl("lbl_Finca") as Label).Text;
                                string idlote = (row.Cells[5].FindControl("lbl_Id_Lote") as Label).Text;
                                string lote = (row.Cells[6].FindControl("lbl_Lote") as Label).Text;
                                string maduro = (row.Cells[8].FindControl("lbl_Maduro") as Label).Text;
                                dt.Rows.Add(id, fecha, idfinca, finca, idlote, lote, maduro);
                            }
                        }
                    }
                    GVSecado2.DataSource = dt;
                    GVSecado2.DataBind();
                }
                BTNInsertar_Click(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Recorre GVSecado2 para trasladar los registros
        protected void BTNInsertar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GVSecado2.Rows)
            {
                string id = (row.FindControl("lbl_Id_Beneficio_R") as Label).Text;
                //string fecha = (row.FindControl("lbl_Fecha_Crea") as Label).Text;
                string fechaStr = (row.FindControl("lbl_Fecha_Crea") as Label).Text;
                DateTime fecha = DateTime.Parse(fechaStr);
                int idfinca = Convert.ToInt32((row.FindControl("lbl_Id_Finca") as Label).Text);
                string finca = (row.FindControl("lbl_Finca") as Label).Text;
                int idlote = Convert.ToInt32((row.FindControl("lbl_Id_Lote") as Label).Text);
                string lote = (row.FindControl("lbl_Lote") as Label).Text;
                decimal libras = Convert.ToDecimal((row.FindControl("lbl_Libras") as Label).Text);
                InsertarDatos(id, fecha, idfinca, finca, idlote, lote, libras);
            }
            GVSecado2.DataBind();
            Response.Redirect("~/Pages/Test/Formulario_Secado.aspx");
        }
        // Resive los registros e inserta en procedimiento
        private void InsertarDatos(string id, DateTime fecha, int idfinca, string finca, int idlote, string lote, decimal libras)
        {
            try
            {
                //using (SqlConnection con = new SqlConnection("tu_cadena_de_conexion"))
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_AG_FNC00606", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Beneficio_R", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.AddWithValue("@Fecha_Crea_R", SqlDbType.DateTime).Value = fecha;
                    cmd.Parameters.AddWithValue("@Id_Finca", SqlDbType.Int).Value = idfinca;
                    cmd.Parameters.AddWithValue("@Finca", SqlDbType.VarChar).Value = finca;
                    cmd.Parameters.AddWithValue("@Id_Lote", SqlDbType.Int).Value = idlote;
                    cmd.Parameters.AddWithValue("@Lote", SqlDbType.VarChar).Value = lote;
                    cmd.Parameters.AddWithValue("@Libras", SqlDbType.Decimal).Value = libras;
                    cmd.Parameters.AddWithValue("@Id_Tipo_Secado", System.Data.SqlDbType.Int).Value = Convert.ToInt32(ddlTipo_Secado.Text);
                    cmd.Parameters.AddWithValue("@Id_Partida", System.Data.SqlDbType.Int).Value = Convert.ToInt32(ddlPartida.Text);
                    cmd.Parameters.AddWithValue("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Usuario"]);
                    cmd.ExecuteNonQuery();
                }
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