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

namespace IT_Finca.Pages.Admin
{
    public partial class Cosecha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    TB_Cosecha();
                    DDCargarEmpresas();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        private DataTable GetFilteredData(int idRegistro)
        {
            SqlCommand cmd = new SqlCommand("SP_TB_FNC00602", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (idRegistro != 0)
            {
                dt.DefaultView.RowFilter = string.Format("Id_Registro = {0}", idRegistro);
                dt = dt.DefaultView.ToTable();
            }
            con.Close();
            return dt;
        }
        //Cargar tabla con listado de Fincas
        protected void TB_Cosecha()
        {
            try
            {
                DataTable dt = GetFilteredData(0);
                gvCosecha.DataSource = dt;
                gvCosecha.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reducir listado de GridView despues de 17 lineas
        protected void gvCosecha_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Cosecha();
        }
        //Boton para buscar registros
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idRegistro = 0;
                if (!string.IsNullOrEmpty(txtBuscarRegistro.Text.Trim()))
                {
                    idRegistro = int.Parse(txtBuscarRegistro.Text.Trim());
                }

                DataTable dt = GetFilteredData(idRegistro);
                gvCosecha.DataSource = dt;
                gvCosecha.DataBind();
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
        protected void ddEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarFincas(int.Parse(ddEmpresas.SelectedValue));
        }
        //Cargar Listado de Fincas en DropDownList para Modal_Actualizar
        void DDCargarFincas(long IdEmpresa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = IdEmpresa;
                ddFincas.Items.Clear();
                con.Open();
                ddFincas.DataSource = cmd.ExecuteReader();
                ddFincas.DataTextField = "Finca";
                ddFincas.DataValueField = "Id_Finca";
                ddFincas.DataBind();
                //ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddFincas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarLotes(int.Parse(ddFincas.SelectedValue));
        }
        //Cargar Listado de Lotes en DropDownList para Modal_Actualizar
        void DDCargarLotes(long IdFinca)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                ddLotes.Items.Clear();
                con.Open();
                ddLotes.DataSource = cmd.ExecuteReader();
                ddLotes.DataTextField = "Lote";
                ddLotes.DataValueField = "Id_Lote";
                ddLotes.DataBind();
                //ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarProcesos(int.Parse(ddLotes.SelectedValue));
        }
        //Cargar Listado de Procesos en DropDownList para Modal_Actualizar
        void DDCargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
                ddProcesos.Items.Clear();
                con.Open();
                ddProcesos.DataSource = cmd.ExecuteReader();
                ddProcesos.DataTextField = "Proceso";
                ddProcesos.DataValueField = "Id_Proceso";
                ddProcesos.DataBind();
                //ddProcesos.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarActividad(int.Parse(ddProcesos.SelectedValue));
        }
        void DDCargarProveedores(long IdFinca)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00202", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                ddProveedores.Items.Clear();
                con.Open();
                ddProveedores.DataSource = cmd.ExecuteReader();
                ddProveedores.DataTextField = "Nom_Ape";
                ddProveedores.DataValueField = "Id_Empleado";
                ddProveedores.DataBind();
                //ddProveedores.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void DDCargarActividad(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddActividad.Items.Clear();
                con.Open();
                ddActividad.DataSource = cmd.ExecuteReader();
                ddActividad.DataTextField = "Actividad";
                ddActividad.DataValueField = "Id_Actividad";
                ddActividad.DataBind();
                //ddActividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void DDCargarTipoActividad()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddTipo_Actividad.Items.Clear();
                con.Open();
                ddTipo_Actividad.DataSource = cmd.ExecuteReader();
                ddTipo_Actividad.DataTextField = "Tipo_Actividad";
                ddTipo_Actividad.DataValueField = "Id_Tipo_Actividad";
                ddTipo_Actividad.DataBind();
                //ddlTipoActividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
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
                SqlCommand cmd = new SqlCommand("SP_AC_FNC00602", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Registro", lbId_Registro.Text);
                cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddEmpresas.Text;
                cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = ddFincas.Text;
                cmd.Parameters.AddWithValue("@Id_Empleado", System.Data.SqlDbType.Int).Value = ddProveedores.Text;
                cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = ddLotes.Text;
                cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddProcesos.Text;
                cmd.Parameters.AddWithValue("@Id_Actividad", System.Data.SqlDbType.Int).Value = ddActividad.Text;
                cmd.Parameters.AddWithValue("@Id_Tipo_Actividad", System.Data.SqlDbType.Int).Value = ddTipo_Actividad.Text;
                cmd.Parameters.AddWithValue("@Verde", System.Data.SqlDbType.Decimal).Value = txVerde.Text;
                cmd.Parameters.AddWithValue("@Maduro", System.Data.SqlDbType.Decimal).Value = txMaduro.Text;
                cmd.Parameters.AddWithValue("@Id_Usuario", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/AdminActividades/Cosecha.aspx");
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
                SqlCommand cmd = new SqlCommand("SP_EL_FNC00602", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Registro", lId_Registro.Text);
                cmd.Parameters.AddWithValue("@Id_Usuario", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalEl(false);
                Response.Redirect("~/Pages/AdminActividades/Cosecha.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Retraer Modal_Actualizar y Modal_Eliminar detro del GridView por Id_
        protected void gvCosecha_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Registro.Text = gvCosecha.DataKeys[gvrow.RowIndex].Value.ToString();

                long idEmpresa = Convert.ToInt32((gvrow.FindControl("gvId_Empresa") as Label).Text);
                DDCargarFincas(idEmpresa);

                long idFinca = Convert.ToInt32((gvrow.FindControl("gvId_Finca") as Label).Text);
                DDCargarLotes(idFinca);

                long idLote = Convert.ToInt32((gvrow.FindControl("gvId_Lote") as Label).Text);
                DDCargarProcesos(idLote);

                long idProceso = Convert.ToInt32((gvrow.FindControl("gvId_Proceso") as Label).Text);
                DDCargarActividad(idProceso);
                DDCargarTipoActividad();
                DDCargarProveedores(idFinca);

                ddEmpresas.Text = (gvrow.FindControl("gvId_Empresa") as Label).Text;
                ddFincas.Text = (gvrow.FindControl("gvId_Finca") as Label).Text;
                ddLotes.Text = (gvrow.FindControl("gvId_Lote") as Label).Text;
                ddProcesos.Text = (gvrow.FindControl("gvId_Proceso") as Label).Text;
                ddProveedores.Text = (gvrow.FindControl("gvId_Proveedor") as Label).Text;

                string actividad = (gvrow.FindControl("gvId_Actividad") as Label)?.Text;
                if (!string.IsNullOrEmpty(actividad) && actividad != "0")
                {
                    ddActividad.Text = actividad;
                }

                ddTipo_Actividad.Text = (gvrow.FindControl("gvId_Tipo_Actividad") as Label).Text;
                txVerde.Text = (gvrow.FindControl("gvVerde") as Label).Text;
                txMaduro.Text = (gvrow.FindControl("gvMaduro") as Label).Text;
                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lId_Registro.Text = gvCosecha.DataKeys[gvrow.RowIndex].Value.ToString();
                lFinca.Text = (gvrow.FindControl("gvId_Registro") as Label).Text;
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