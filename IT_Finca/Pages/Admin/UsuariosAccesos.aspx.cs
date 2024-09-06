using IT_Usuario.Pages.Admin;
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
    public partial class UsuariosAccesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    CargarUsuarios();
                    //TB_Usuarios();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //DDL USUARIOS
        void CargarUsuarios()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00202", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlUsuarios.Items.Clear();
                con.Open();
                ddlUsuarios.DataSource = cmd.ExecuteReader();
                ddlUsuarios.DataTextField = "Nombre";
                ddlUsuarios.DataValueField = "Id_Usuario";
                ddlUsuarios.DataBind();
                ddlUsuarios.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        // Evento del botón buscar
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = ddlUsuarios.SelectedValue;
                DataTable dt = GetFilteredData(usuario);
                gvAccesosUsuarios.DataSource = dt;
                gvAccesosUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Método para obtener datos filtrados por usuario
        private DataTable GetFilteredData(string usuario)
        {
            using (SqlCommand cmd = new SqlCommand("SP_TB_FNC00209", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (string.IsNullOrEmpty(usuario) || usuario == "0")
                {
                    cmd.Parameters.AddWithValue("@Id_Usuario", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id_Usuario", usuario);
                }
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }
        }

        // Método para cargar la tabla con el listado de usuarios
        protected void TB_Usuarios()
        {
            try
            {
                DataTable dt = GetFilteredData("");
                gvAccesosUsuarios.DataSource = dt;
                gvAccesosUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        // boton
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvAccesosUsuarios.Rows)
                {
                    int idUsuario = Convert.ToInt32(gvAccesosUsuarios.DataKeys[row.RowIndex].Values["Id_Usuario"]);
                    int idPermiso = Convert.ToInt32((row.FindControl("gvId_Permiso") as Label).Text);
                    // Obtener el estado actual del checkbox
                    bool nuevoEstado = (row.FindControl("gvEstado") as CheckBox).Checked;
                    int Estado = nuevoEstado ? 1 : 0;

                    // Actualizar la base de datos con el nuevo estado
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_AC_FNC00209", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Estado", Estado);
                            cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                            cmd.Parameters.AddWithValue("@Id_Permiso", idPermiso);

                            try
                            {
                                con.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();
                                con.Close();

                                if (rowsAffected == 0)
                                {
                                    // Si no se afectaron filas, puedes manejarlo aquí si es necesario
                                    Console.WriteLine($"No se actualizó el usuario con Id_Usuario: {idUsuario}");
                                }
                            }
                            catch (Exception ex)
                            {
                                // Manejo de errores específicos para la base de datos
                                Console.WriteLine("Error al actualizar la base de datos: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                Console.WriteLine("Error en la actualización de datos: " + ex.Message);
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