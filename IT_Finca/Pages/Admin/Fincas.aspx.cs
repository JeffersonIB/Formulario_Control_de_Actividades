using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Admin
{
    public partial class Fincas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    TB_Fincas();
                    DDLCargarEmpresas();
                    DDCargarEmpresas();
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
                string finca = txtBuscarFinca.Text.Trim();
                DataTable dt = GetFilteredData(finca);
                gvFincas.DataSource = dt;
                gvFincas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private DataTable GetFilteredData(string finca)
        {
            SqlCommand cmd = new SqlCommand("SP_TB_FNC00100", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Id_Empresa", 1);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (!string.IsNullOrEmpty(finca))
            {
                // Filtrar los datos en la columna "Lote"
                dt.DefaultView.RowFilter = string.Format("Finca LIKE '%{0}%'", finca);
                dt = dt.DefaultView.ToTable();
            }
            con.Close();
            return dt;
        }
        //Cargar tabla con listado de Fincas
        protected void TB_Fincas()
        {
            try
            {
                DataTable dt = GetFilteredData("");
                gvFincas.DataSource = dt;
                gvFincas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reducir listado de GridView despues de 17 lineas
        protected void gvFincas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Fincas();
        }
        //Cargar Listado de Empresas en DropDownList para modal Agregar
        public void DDLCargarEmpresas()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_FNC00101";
                cmd.Connection = con;
                con.Open();
                ddlEmpresas.DataSource = cmd.ExecuteReader();
                ddlEmpresas.DataTextField = "Empresa";
                ddlEmpresas.DataValueField = "Id_Empresa";
                ddlEmpresas.DataBind();
            }
        }
        //Agrega nuevo registro dentro del Modal_Agregar
        protected void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddlEmpresas.Text;
                cmd.Parameters.Add("@Finca", System.Data.SqlDbType.VarChar).Value = txtFinca.Text;
                cmd.Parameters.Add("@Pais", System.Data.SqlDbType.VarChar).Value = txtPais.Text;
                cmd.Parameters.Add("@Ciudad", System.Data.SqlDbType.VarChar).Value = txtCiudad.Text;
                cmd.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = txtDireccion.Text;
                cmd.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = txtTelefono.Text;
                cmd.Parameters.Add("@Id_Usuario", System.Data.SqlDbType.VarChar).Value = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/Pages/Admin/Fincas.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Empresas en DropDownList para Modal_Actualizar
        public void DDCargarEmpresas()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_FNC00101";
                cmd.Connection = con;
                con.Open();
                ddEmpresas.DataSource = cmd.ExecuteReader();
                ddEmpresas.DataTextField = "Empresa";
                ddEmpresas.DataValueField = "Id_Empresa";
                ddEmpresas.DataBind();
            }
        }
        //Actualizar registro dentro del Modal_Actualizar
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Finca", lbId_Finca.Text);
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.VarChar).Value = ddEmpresas.Text;
                cmd.Parameters.Add("@Finca", System.Data.SqlDbType.VarChar).Value = txFinca.Text;
                cmd.Parameters.Add("@Pais", System.Data.SqlDbType.VarChar).Value = txPais.Text;
                cmd.Parameters.Add("@Ciudad", System.Data.SqlDbType.VarChar).Value = txCiudad.Text;
                cmd.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = txDireccion.Text;
                cmd.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = txTelefono.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/Admin/Fincas.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Eliminar registro dentro del Modal_Eliminar
        protected void Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_EL_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Finca", lId_Finca.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalEl(false);
                Response.Redirect("~/Pages/Admin/Fincas.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Retraer Modal_Actualizar y Modal_Eliminar detro del GridView por Id_
        protected void gvFincas_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Finca.Text = gvFincas.DataKeys[gvrow.RowIndex].Value.ToString(); ;
                ddEmpresas.Text = (gvrow.FindControl("gvId_Empresa") as Label).Text;
                txFinca.Text = (gvrow.FindControl("gvFinca") as Label).Text;
                txPais.Text = (gvrow.FindControl("gvPais") as Label).Text;
                txCiudad.Text = (gvrow.FindControl("gvCiudad") as Label).Text;
                txDireccion.Text = (gvrow.FindControl("gvDireccion") as Label).Text;
                txTelefono.Text = (gvrow.FindControl("gvTelefono") as Label).Text;
                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lId_Finca.Text = gvFincas.DataKeys[gvrow.RowIndex].Value.ToString();
                lFinca.Text = (gvrow.FindControl("gvFinca") as Label).Text;
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