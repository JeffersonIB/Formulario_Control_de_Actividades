using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT_Finca.Pages.Extras
{
    public partial class Contrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    lblNombre.Text = Session["Nombre"].ToString();
                    lblApellido.Text = Session["Apellido"].ToString();
                    Password.Text = Session["Clave"].ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string password = txtPassword.Text;

                // Realiza validaciones adicionales si es necesario
                bool cumpleOtrasValidaciones = ValidarOtrasCondiciones(password);

                if (cumpleOtrasValidaciones)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("SP_AC_FNC00202", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Usuario", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                        cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = txtPassword2.Text;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Actualizado con Exito!', 'Se ha actualoizado su contraseña correctamente!', 'success')", true);
                        Response.Redirect("~/Pages/Extras/Contrasena.aspx");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    // La contraseña no cumple con las validaciones adicionales
                    // Muestra un mensaje de error o toma la acción adecuada
                }
            }
        }

        private bool ValidarOtrasCondiciones(string password)
        {
            // Agrega aquí tus propias validaciones adicionales, si es necesario
            // Por ejemplo, longitud mínima, longitud máxima, etc.

            return true; // Devuelve true si todas las validaciones adicionales son exitosas
        }

    }
}