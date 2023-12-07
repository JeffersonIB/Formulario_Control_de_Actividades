using IT_Finca.Pages.Admin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca
{
    public partial class Index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack && Session["Usuario"] != null)
            {
                Cargar_Fincas();
            }
            else
            {
                //Response.Redirect("~/Default.aspx");
            }
            //Actualiza el tiempo de última actividad de la sesión
            Session["LastActivity"] = DateTime.Now;
            DateTime lastActivity = (DateTime)Session["LastActivity"];
            TimeSpan timeSinceLastActivity = DateTime.Now - lastActivity;
            if (timeSinceLastActivity.TotalMinutes > Session.Timeout)
            {
                Response.Redirect("~/Default.aspx");
            }
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void Cargar_Fincas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00207", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Usuario", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"];
                ddlFincas.Items.Clear();

                con.Open();
                ddlFincas.DataSource = cmd.ExecuteReader();
                ddlFincas.DataTextField = "Finca";
                ddlFincas.DataValueField = "Id_Finca";
                ddlFincas.DataBind();

                con.Close();
            }
            catch (Exception)
            {
                // Manejar la excepción según sea necesario
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error cargando fincas', 'error')", true);
            }
        }
        protected void IngresarClick(object sender, EventArgs e)
        {
            try
            {
                // Obtener el Id_Finca seleccionado del DropDownList
                int idFincaSeleccionado = Convert.ToInt32(ddlFincas.SelectedValue);
                // Almacenar ID_Finca en la sesión
                Session["Id_Finca"] = idFincaSeleccionado;
                string consulta = "SELECT Finca FROM FNC00100 WHERE Id_Finca = @IdFinca";
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdFinca", idFincaSeleccionado);
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            // Almacenar Finca en la sesión
                            Session["Finca"] = resultado.ToString();
                        }
                        else
                        {
                            Session["Finca"] = "Finca no encontrada";
                        }
                    }
                }
                Response.Redirect("~/Index2.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error al seleccionar finca', 'error')", true);
            }
        }

    }
}