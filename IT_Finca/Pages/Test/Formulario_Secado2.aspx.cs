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
    public partial class Formulario_Secado2 : System.Web.UI.Page
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
                    CargarTipoCafe();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void CargarTipoCafe()
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
        protected void ddlCafeOnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt32(ddlCafe.SelectedValue);

            // Configurar visibilidad de las columnas basado en la selección del DropDownList
            gvBeneficio.Columns[6].Visible = selectedValue == 1; // Country1
            gvBeneficio.Columns[7].Visible = selectedValue == 2; // Country2
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCafe.SelectedIndex > 0 && 0 < 2)
                {
                    int idTipoCafe = Convert.ToInt32(ddlCafe.SelectedValue);

                    foreach (GridViewRow row in gvBeneficio.Rows)
                    {
                        Label lblVerde = (Label)row.FindControl("lbl_Verde");
                        Label lblMaduro = (Label)row.FindControl("lbl_Maduro");

                        // Restablecer la visibilidad
                        lblVerde.Visible = false;
                        lblMaduro.Visible = false;

                        // Mostrar la columna correspondiente según el tipo de café
                        switch (idTipoCafe)
                        {
                            case 1:
                                lblVerde.Visible = true;
                                break;
                            case 2:
                                lblMaduro.Visible = true;
                                break;
                                // Agrega más casos según sea necesario
                        }
                    }

                    // Cargar los datos en el GridView
                    DataTable dt = GetFilteredData("");
                    gvBeneficio.DataSource = dt;
                    gvBeneficio.DataBind();
                }
                else if (ddlCafe.SelectedIndex > 1)
                {
                    int idTipoCafe = Convert.ToInt32(ddlCafe.SelectedValue);

                    foreach (GridViewRow row in gvBeneficio.Rows)
                    {
                        Label lblVerde = (Label)row.FindControl("lbl_Verde");
                        Label lblMaduro = (Label)row.FindControl("lbl_Maduro");

                        // Restablecer la visibilidad
                        lblVerde.Visible = false;
                        lblMaduro.Visible = false;

                        // Mostrar la columna correspondiente según el tipo de café
                        switch (idTipoCafe)
                        {
                            case 1:
                                lblVerde.Visible = true;
                                break;
                            case 2:
                                lblMaduro.Visible = true;
                                break;
                                // Agrega más casos según sea necesario
                        }
                    }

                    // Cargar los datos en el GridView
                    DataTable dt = GetFilteredData2("");
                    gvBeneficio.DataSource = dt;
                    gvBeneficio.DataBind();
                }
            }
            catch (Exception)
            {
                // Manejar la excepción de manera apropiada
            }
            Tipo_Secado.Visible = true;
            Confirmar.Visible = true;
        }

        private DataTable GetFilteredData(string fecha)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM VW_FNC00602_3 ORDER BY Fecha_Crea,Finca,Lote ASC", con);
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
        private DataTable GetFilteredData2(string fecha)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM VW_FNC00602_4 ORDER BY Fecha_Crea,Finca,Lote ASC", con);
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
                if (ddlCafe.SelectedIndex > 0 && 0 < 2)
                {
                    int idTipoCafe = Convert.ToInt32(ddlCafe.SelectedValue);

                    // Recorrer las filas del GridView y establecer visibilidad según el tipo de café
                    foreach (GridViewRow row in gvBeneficio.Rows)
                    {
                        Label lblVerde = (Label)row.FindControl("lbl_Verde");
                        Label lblMaduro = (Label)row.FindControl("lbl_Maduro");

                        // Restablecer la visibilidad
                        lblVerde.Visible = false;
                        lblMaduro.Visible = false;

                        // Mostrar la columna correspondiente según el tipo de café
                        switch (idTipoCafe)
                        {
                            case 1:
                                lblVerde.Visible = true;
                                break;
                            case 2:
                                lblMaduro.Visible = true;
                                break;
                                // Agrega más casos según sea necesario
                        }
                    }

                    // Cargar los datos en el GridView
                    DataTable dt = GetFilteredData("");
                    gvBeneficio.DataSource = dt;
                    gvBeneficio.DataBind();
                }
                else
                {
                    int idTipoCafe = Convert.ToInt32(ddlCafe.SelectedValue);

                    foreach (GridViewRow row in gvBeneficio.Rows)
                    {
                        Label lblVerde = (Label)row.FindControl("lbl_Verde");
                        Label lblMaduro = (Label)row.FindControl("lbl_Maduro");

                        // Restablecer la visibilidad
                        lblVerde.Visible = false;
                        lblMaduro.Visible = false;

                        // Mostrar la columna correspondiente según el tipo de café
                        switch (idTipoCafe)
                        {
                            case 1:
                                lblVerde.Visible = true;
                                break;
                            case 2:
                                lblMaduro.Visible = true;
                                break;
                                // Agrega más casos según sea necesario
                        }
                    }
                    // Cargar los datos en el GridView
                    DataTable dt = GetFilteredData2("");
                    gvBeneficio.DataSource = dt;
                    gvBeneficio.DataBind();
                }
            }
            catch (Exception)
            {
                // Manejar la excepción de manera apropiada
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
        protected void gvBeneficio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener las etiquetas en cada fila
                Label lblVerde = (Label)e.Row.FindControl("lbl_Verde");
                Label lblMaduro = (Label)e.Row.FindControl("lbl_Maduro");

                // Establecer la visibilidad inicial como false
                lblVerde.Visible = false;
                lblMaduro.Visible = false;
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
        protected void btn_Confir_Click(object sender, EventArgs e)
        {
            try
            {

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