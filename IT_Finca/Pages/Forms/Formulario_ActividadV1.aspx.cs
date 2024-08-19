using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Services;

namespace IT_Finca.Pages.Test
{
    public partial class FromV1 : System.Web.UI.Page
    {
        int IdEmpresa = 0;
        int IdFinca = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            if (Session["Usuario"] != null)
            {
                IdFinca = Convert.ToInt32(Session["Id_Finca"].ToString());
                IdEmpresa = Convert.ToInt32(Session["Id_Empresa"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            // Actualiza el tiempo de última actividad de la sesión
            Session["LastActivity"] = DateTime.Now;
            DateTime lastActivity = (DateTime)Session["LastActivity"];
            TimeSpan timeSinceLastActivity = DateTime.Now - lastActivity;
            if (timeSinceLastActivity.TotalMinutes > Session.Timeout)
            {
                Response.Redirect("~/Default.aspx");
            }
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack)
                {
                    //CargarFincas();
                    CargarDropDowns();
                    //CargarEmpleados();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //void CargarFincas()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = 1;
        //        con.Open();
        //        ddlFinca.DataSource = cmd.ExecuteReader();
        //        ddlFinca.DataTextField = "Finca";
        //        ddlFinca.DataValueField = "Id_Finca";
        //        ddlFinca.DataBind();
        //        con.Close();
        //        ddlFinca.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        void CargarEmpleados()
        {
            string idFinca = Request.QueryString["Id_Finca"];
            int idFincaInt = Convert.ToInt32(idFinca);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00202", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                ddlEmpleados.Items.Clear();
                con.Open();
                ddlEmpleados.DataSource = cmd.ExecuteReader();
                ddlEmpleados.DataTextField = "Nom_Ape";
                ddlEmpleados.DataValueField = "Id_Empleado";
                ddlEmpleados.DataBind();
                ddlEmpleados.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //void CargarCodigos(long IdEmpleado)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_FNC00201", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Empleado", SqlDbType.Int).Value = IdEmpleado;
        //        ddlCodigo_Empleado.Items.Clear();
        //        con.Open();
        //        ddlCodigo_Empleado.DataSource = cmd.ExecuteReader();
        //        ddlCodigo_Empleado.DataTextField = "Codigo_Empleado";
        //        ddlCodigo_Empleado.DataValueField = "Id_Empleado";
        //        ddlCodigo_Empleado.DataBind();
        //        ////ddlCodigo_Empleado.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
        //        con.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        void CargarLotes()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                ddlLotes.Items.Clear();
                con.Open();
                ddlLotes.DataSource = cmd.ExecuteReader();
                ddlLotes.DataTextField = "Lote";
                ddlLotes.DataValueField = "Id_Lote";
                ddlLotes.DataBind();
                ddlLotes.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
                ddlProcesos.Items.Clear();
                con.Open();
                ddlProcesos.DataSource = cmd.ExecuteReader();
                ddlProcesos.DataTextField = "Proceso";
                ddlProcesos.DataValueField = "Id_Proceso";
                ddlProcesos.DataBind();
                ddlProcesos.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarActividad1(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad1.Items.Clear();
                con.Open();
                ddlActividad1.DataSource = cmd.ExecuteReader();
                ddlActividad1.DataTextField = "Actividad";
                ddlActividad1.DataValueField = "Id_Actividad";
                ddlActividad1.DataBind();
                ddlActividad1.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarActividad2(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad2.Items.Clear();
                con.Open();
                ddlActividad2.DataSource = cmd.ExecuteReader();
                ddlActividad2.DataTextField = "Actividad";
                ddlActividad2.DataValueField = "Id_Actividad";
                ddlActividad2.DataBind();
                ddlActividad2.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarActividad3(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad3.Items.Clear();
                con.Open();
                ddlActividad3.DataSource = cmd.ExecuteReader();
                ddlActividad3.DataTextField = "Actividad";
                ddlActividad3.DataValueField = "Id_Actividad";
                ddlActividad3.DataBind();
                ddlActividad3.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarTipoTrabajo1()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = 1;
                ddlTipoTrabajo1.Items.Clear();
                con.Open();
                ddlTipoTrabajo1.DataSource = cmd.ExecuteReader();
                ddlTipoTrabajo1.DataTextField = "Tipo_Actividad";
                ddlTipoTrabajo1.DataValueField = "Id_Tipo_Actividad";
                ddlTipoTrabajo1.DataBind();
                //ddlTipoTrabajo1.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarTipoTrabajo2()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = 1;
                ddlTipoTrabajo2.Items.Clear();
                con.Open();
                ddlTipoTrabajo2.DataSource = cmd.ExecuteReader();
                ddlTipoTrabajo2.DataTextField = "Tipo_Actividad";
                ddlTipoTrabajo2.DataValueField = "Id_Tipo_Actividad";
                ddlTipoTrabajo2.DataBind();
                ddlTipoTrabajo2.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarTipoTrabajo3()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = 1;
                ddlTipoTrabajo3.Items.Clear();
                con.Open();
                ddlTipoTrabajo3.DataSource = cmd.ExecuteReader();
                ddlTipoTrabajo3.DataTextField = "Tipo_Actividad";
                ddlTipoTrabajo3.DataValueField = "Id_Tipo_Actividad";
                ddlTipoTrabajo3.DataBind();
                ddlTipoTrabajo3.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CargarDropDowns()
        {
            //CargarFincas();
            CargarEmpleados();
            CargarLotes();
            //CargarCodigos(long.Parse(ddlEmpleados.SelectedValue));
            CargarProcesos(int.Parse(ddlLotes.SelectedValue));
            CargarActividad1(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad2(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad3(long.Parse(ddlProcesos.SelectedValue));
            CargarTipoTrabajo1();
            CargarTipoTrabajo2();
            CargarTipoTrabajo3();
            txtCantidad1.Text = "0";
            txtCantidad2.Text = "0";
            txtCantidad3.Text = "0";
        }
        protected void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_FNC00600_1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                cmd.Parameters.Add("@Id_Empleado", System.Data.SqlDbType.Int).Value = ddlEmpleados.Text;
                cmd.Parameters.Add("@Id_Lote", System.Data.SqlDbType.Int).Value = ddlLotes.Text;
                cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddlProcesos.Text;
                cmd.Parameters.Add("@Id_Actividad1", System.Data.SqlDbType.Int).Value = ddlActividad1.Text;
                cmd.Parameters.Add("@Id_Actividad2", System.Data.SqlDbType.Int).Value = ddlActividad2.Text;
                cmd.Parameters.Add("@Id_Actividad3", System.Data.SqlDbType.Int).Value = ddlActividad3.Text;
                cmd.Parameters.Add("@Id_Tipo_Actividad1", System.Data.SqlDbType.Int).Value = ddlTipoTrabajo1.Text;
                cmd.Parameters.Add("@Id_Tipo_Actividad2", System.Data.SqlDbType.Int).Value = ddlTipoTrabajo2.Text;
                cmd.Parameters.Add("@Id_Tipo_Actividad3", System.Data.SqlDbType.Int).Value = ddlTipoTrabajo3.Text;
                cmd.Parameters.Add("@Cantidad1", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(txtCantidad1.Text);
                cmd.Parameters.Add("@Cantidad2", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(txtCantidad2.Text);
                cmd.Parameters.Add("@Cantidad3", System.Data.SqlDbType.Decimal).Value = Decimal.Parse(txtCantidad3.Text);
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha guardado los registros!', 'success')", true);
                //Response.Redirect("Pages/Forms/FormularioV1.aspx");
                //CargarFincas();
                CargarDropDowns();
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        //void Limpiar()
        //{
        //    txtObs_Califica.Text = string.Empty;
        //    txtCalificado.Text = string.Empty;
        //    CargarMeses();
        //}
        //Evento al seleccionar Empresa en DropDownList
        //protected void ddlFinca_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ddlEmpleados.DataSource = null;
        //    //ddlEmpleados.DataBind();
        //    CargarEmpleados(int.Parse(ddlFinca.SelectedValue));
        //    CargarLotes(int.Parse(ddlFinca.SelectedValue));
        //}
        protected void ddlEmpleados_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
            //CargarCodigos(long.Parse(ddlEmpleados.SelectedValue));
        }
        protected void ddlCodigo_Empleado_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlLotes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProcesos(int.Parse(ddlLotes.SelectedValue));
        }
        protected void ddlProcesos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarActividad1(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad2(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad3(long.Parse(ddlProcesos.SelectedValue));
            CargarTipoTrabajo1();
            CargarTipoTrabajo2();
            CargarTipoTrabajo3();
        }
        protected void ddlActividad1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlActividad2_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlActividad3_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlTipoTrabajo1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlTipoTrabajo2_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlTipoTrabajo3_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index2.aspx");
        }
        protected void Salir_Click(object sender, EventArgs e)
        {
            Session["Id_Empresa"] = null;
            Session["Empresa"] = null;
            Session["Id_Usuario"] = null;
            Session["Nombre"] = null;
            Session["Apellido"] = null;
            Session["Usuario"] = null;
            Session["Clave"] = null;
            Session["Id_Rol"] = null;
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

    }
}