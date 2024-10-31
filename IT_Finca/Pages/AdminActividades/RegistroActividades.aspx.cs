using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Admin
{
    public partial class Registros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    TB_Registros();
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
            SqlCommand cmd = new SqlCommand("SP_TB_FNC00600", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Id_Empresa", 1);
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
        protected void TB_Registros()
        {
            try
            {
                DataTable dt = GetFilteredData(0);
                gvRegistros.DataSource = dt;
                gvRegistros.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reducir listado de GridView despues de 17 lineas
        protected void gvRegistros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Registros();
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
                gvRegistros.DataSource = dt;
                gvRegistros.DataBind();
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
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
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
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
        }
        //Cargar Listado de Lotes en DropDownList para Modal_Actualizar
        void DDCargarLotes(long IdFinca)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
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
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
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
            DDCargarActividad1(int.Parse(ddProcesos.SelectedValue));
            DDCargarActividad2(int.Parse(ddProcesos.SelectedValue));
            DDCargarActividad3(int.Parse(ddProcesos.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
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
        void DDCargarActividad1(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddActividad1.Items.Clear();
                con.Open();
                ddActividad1.DataSource = cmd.ExecuteReader();
                ddActividad1.DataTextField = "Actividad";
                ddActividad1.DataValueField = "Id_Actividad";
                ddActividad1.DataBind();
                //ddActividad1.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void DDCargarActividad2(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddActividad2.Items.Clear();
                con.Open();
                ddActividad2.DataSource = cmd.ExecuteReader();
                ddActividad2.DataTextField = "Actividad";
                ddActividad2.DataValueField = "Id_Actividad";
                ddActividad2.DataBind();
                ddActividad2.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void DDCargarActividad3(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddActividad3.Items.Clear();
                con.Open();
                ddActividad3.DataSource = cmd.ExecuteReader();
                ddActividad3.DataTextField = "Actividad";
                ddActividad3.DataValueField = "Id_Actividad";
                ddActividad3.DataBind();
                ddActividad3.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
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
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddTipo_Actividad1.Items.Clear();
                con.Open();
                ddTipo_Actividad1.DataSource = cmd.ExecuteReader();
                ddTipo_Actividad1.DataTextField = "Tipo_Actividad";
                ddTipo_Actividad1.DataValueField = "Id_Tipo_Actividad";
                ddTipo_Actividad1.DataBind();
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
                SqlCommand cmd = new SqlCommand("SP_AC_FNC00600", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Registro", lbId_Registro.Text);
                cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddEmpresas.Text;
                cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = ddFincas.Text;
                cmd.Parameters.AddWithValue("@Id_Empleado", System.Data.SqlDbType.Int).Value = ddProveedores.Text;
                cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = ddLotes.Text;
                cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddProcesos.Text;
                cmd.Parameters.AddWithValue("@Id_Actividad1", System.Data.SqlDbType.Int).Value = ddActividad1.Text;
                cmd.Parameters.AddWithValue("@Id_Actividad2", System.Data.SqlDbType.Int).Value = ddActividad2.Text;
                cmd.Parameters.AddWithValue("@Id_Actividad3", System.Data.SqlDbType.Int).Value = ddActividad3.Text;
                cmd.Parameters.AddWithValue("@Id_Tipo_Actividad1", System.Data.SqlDbType.Int).Value = ddTipo_Actividad1.Text;
                cmd.Parameters.AddWithValue("@Cantidad1", System.Data.SqlDbType.Decimal).Value = txCantidad1.Text;
                cmd.Parameters.AddWithValue("@Cantidad2", System.Data.SqlDbType.Decimal).Value = txCantidad2.Text;
                cmd.Parameters.AddWithValue("@Cantidad3", System.Data.SqlDbType.Decimal).Value = txCantidad3.Text;              
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/AdminActividades/RegistroActividades.aspx");
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
                SqlCommand cmd = new SqlCommand("SP_EL_FNC00600", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Registro", lId_Registro.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalEl(false);
                Response.Redirect("~/Pages/AdminActividades/Registros.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Retraer Modal_Actualizar y Modal_Eliminar detro del GridView por Id_
        protected void gvRegistros_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Registro.Text = gvRegistros.DataKeys[gvrow.RowIndex].Value.ToString();

                long idEmpresa = Convert.ToInt32((gvrow.FindControl("gvId_Empresa") as Label).Text);
                DDCargarFincas(idEmpresa);

                long idFinca = Convert.ToInt32((gvrow.FindControl("gvId_Finca") as Label).Text);
                DDCargarLotes(idFinca);

                long idLote = Convert.ToInt32((gvrow.FindControl("gvId_Lote") as Label).Text);
                DDCargarProcesos(idLote);

                long idProceso = Convert.ToInt32((gvrow.FindControl("gvId_Proceso") as Label).Text);
                DDCargarActividad1(idProceso);
                DDCargarActividad2(idProceso);
                DDCargarActividad3(idProceso);
                DDCargarTipoActividad();
                DDCargarProveedores(idFinca);

                ddEmpresas.Text = (gvrow.FindControl("gvId_Empresa") as Label).Text;
                ddFincas.Text = (gvrow.FindControl("gvId_Finca") as Label).Text;
                ddLotes.Text = (gvrow.FindControl("gvId_Lote") as Label).Text;
                ddProcesos.Text = (gvrow.FindControl("gvId_Proceso") as Label).Text;
                ddProveedores.Text = (gvrow.FindControl("gvId_Proveedor") as Label).Text;

                string actividad1 = (gvrow.FindControl("gvId_Actividad1") as Label)?.Text;
                if (!string.IsNullOrEmpty(actividad1) && actividad1 != "0")
                {
                    ddActividad1.Text = actividad1;
                }

                string actividad2 = (gvrow.FindControl("gvId_Actividad2") as Label)?.Text;
                if (!string.IsNullOrEmpty(actividad2) && actividad2 != "0")
                {
                    ddActividad2.Text = actividad2;
                }

                string actividad3 = (gvrow.FindControl("gvId_Actividad3") as Label)?.Text;
                if (!string.IsNullOrEmpty(actividad3) && actividad3 != "0")
                {
                    ddActividad3.Text = actividad3;
                }

                ddTipo_Actividad1.Text = (gvrow.FindControl("gvId_Tipo_Actividad1") as Label).Text;
                txCantidad1.Text = (gvrow.FindControl("gvCantidad1") as Label).Text;
                txCantidad2.Text = (gvrow.FindControl("gvCantidad2") as Label).Text;
                txCantidad3.Text = (gvrow.FindControl("gvCantidad3") as Label).Text;

                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lId_Registro.Text = gvRegistros.DataKeys[gvrow.RowIndex].Value.ToString();
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