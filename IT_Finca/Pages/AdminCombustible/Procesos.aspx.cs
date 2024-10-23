using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Ubicacion.Pages.AdminCombustible
{
    public partial class Procesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    TB_Procesos();
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
        //Cargar tabla con listado de Ubicaciones
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string lote = txtBuscarProceso.Text.Trim();
                DataTable dt = GetFilteredData(lote);
                gvProcesos.DataSource = dt;
                gvProcesos.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DataTable GetFilteredData(string lote)
        {
            SqlCommand cmd = new SqlCommand("SP_TB_FNC00300_1", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (!string.IsNullOrEmpty(lote))
            {
                // Filtrar los datos en la columna "Lote"
                dt.DefaultView.RowFilter = string.Format("Proceso LIKE '%{0}%'", lote);
                dt = dt.DefaultView.ToTable();
            }

            con.Close();
            return dt;
        }

        protected void TB_Procesos()
        {
            try
            {
                DataTable dt = GetFilteredData("");
                gvProcesos.DataSource = dt;
                gvProcesos.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reducir listado de GridView despues de 17 lineas
        protected void gvProcesos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Procesos();
        }
        //Cargar Listado de Empresas en DropDownList para modal Agregar
        void DDLCargarEmpresas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00101", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                ddlEmpresas.DataSource = cmd.ExecuteReader();
                ddlEmpresas.DataTextField = "Empresa";
                ddlEmpresas.DataValueField = "Id_Empresa";
                ddlEmpresas.DataBind();
                con.Close();
                ddlEmpresas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Ubicaciones en DropDownList para modal Agregar
        void DDLCargarUbicaciones(long IdEmpresa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = IdEmpresa;
                ddlUbicaciones.Items.Clear();
                con.Open();
                ddlUbicaciones.DataSource = cmd.ExecuteReader();
                ddlUbicaciones.DataTextField = "Ubicacion";
                ddlUbicaciones.DataValueField = "Id_Ubicacion";
                ddlUbicaciones.DataBind();
                ddlUbicaciones.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Agrega nuevo registro dentro del Modal_Agregar
        protected void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_FNC00300_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddlEmpresas.Text;
                cmd.Parameters.Add("@Id_Ubicacion", System.Data.SqlDbType.Int).Value = ddlUbicaciones.Text;
                cmd.Parameters.Add("@Proceso", System.Data.SqlDbType.VarChar).Value = txtProceso.Text;
                cmd.Parameters.Add("@Id_Usuario", System.Data.SqlDbType.VarChar).Value = Session["Id_Usuario"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/Pages/AdminCombustible/Procesos.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Empresas en DropDownList para Modal_Actualizar
        void DDCargarEmpresas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00101", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                ddEmpresas.DataSource = cmd.ExecuteReader();
                ddEmpresas.DataTextField = "Empresa";
                ddEmpresas.DataValueField = "Id_Empresa";
                ddEmpresas.DataBind();
                con.Close();
                //ddEmpresas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Ubicaciones en DropDownList para Modal_Actualizar
        void DDCargarUbicaciones(long IdEmpresa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = IdEmpresa;
                ddlUbicaciones.Items.Clear();
                con.Open();
                ddUbicaciones.DataSource = cmd.ExecuteReader();
                ddUbicaciones.DataTextField = "Ubicacion";
                ddUbicaciones.DataValueField = "Id_Ubicacion";
                ddUbicaciones.DataBind();
                //ddlUbicaciones.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Actualizar registro dentro del Modal_Actualizar
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_FNC00300_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Proceso", lbId_Proceso.Text);
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddEmpresas.Text;
                cmd.Parameters.Add("@Id_Ubicacion", System.Data.SqlDbType.Int).Value = ddUbicaciones.Text;
                cmd.Parameters.Add("@Proceso", System.Data.SqlDbType.VarChar).Value = txProceso.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/AdminCombustible/Procesos.aspx");
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
                SqlCommand cmd = new SqlCommand("SP_EL_FNC00300_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Proceso", lId_Proceso.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalEl(false);
                Response.Redirect("~/Pages/AdminCombustible/Procesos.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Retraer Modal_Actualizar y Modal_Eliminar detro del GridView por Id_
        protected void gvProcesos_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Proceso.Text = gvProcesos.DataKeys[gvrow.RowIndex].Value.ToString();
                ddEmpresas.Text = (gvrow.FindControl("gvId_Empresa") as Label).Text;
                ddUbicaciones.Text = (gvrow.FindControl("gvId_Ubicacion") as Label).Text;
                long idEmpresa = Convert.ToInt64((gvrow.FindControl("gvId_Empresa") as Label).Text);
                DDCargarUbicaciones(idEmpresa);
                long idUbicacion = Convert.ToInt64((gvrow.FindControl("gvId_Ubicacion") as Label).Text);
                txProceso.Text = (gvrow.FindControl("gvProceso") as Label).Text;
                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lId_Proceso.Text = gvProcesos.DataKeys[gvrow.RowIndex].Value.ToString();
                lProceso.Text = (gvrow.FindControl("gvProceso") as Label).Text;
                ModalEl(true);
            }
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarUbicaciones(int.Parse(ddlEmpresas.SelectedValue));
        }
        protected void ddlUbicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void ddlLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
        }
        protected void ddEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarUbicaciones(int.Parse(ddEmpresas.SelectedValue));
        }
        protected void ddUbicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void ddLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Actualizar", "$('#Modal_Actualizar').modal('show')", true);
        }
        protected void ddProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Actualizar", "$('#Modal_Actualizar').modal('show')", true);
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