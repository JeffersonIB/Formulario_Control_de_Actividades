﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace IT_Finca
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        //protected void btnTestDb_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        //        connection.Open();
        //        if ((connection.State & ConnectionState.Open) > 0)
        //        {
        //            Response.Write("Conexión OK!");
        //            connection.Close();
        //        }
        //        else
        //        {
        //            Response.Write("Conexión els falló");
        //        }
        //    }
        //    catch
        //    {
        //        Response.Write("Conexión chatch falló");
        //    }
        //}
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //string Patron = "InfoToolsSV";
        protected void Ingresar_Click(object sender, EventArgs e)
        {
            string patron = "InfoToolsSV";
            try
            {
                string usuarioValue = Usuario.Value; // Obtener el valor del input
                string claveValue = Clave.Value; // Obtener el valor del input
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00200", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuarioValue;
                cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = claveValue;
                cmd.Parameters.Add("@Patron", SqlDbType.VarChar).Value = patron;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Session["Id_Empresa"] = rd[0].ToString();
                    Session["Empresa"] = rd[1].ToString();
                    //Session["Id_Finca"] = rd[2].ToString();
                    //Session["Finca"] = rd[3].ToString();
                    Session["Id_Usuario"] = rd[4].ToString();
                    Session["Nombre"] = rd[5].ToString();
                    Session["Apellido"] = rd[6].ToString();
                    Session["Usuario"] = rd[7].ToString();
                    Session["Clave"] = rd[8].ToString();
                    Session["Id_Rol"] = rd[9].ToString();
                    Response.Redirect("Index1.aspx");
                }
                con.Open();
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Usuario y contraseña no coinciden!', 'error')", true);
            }
        }
        protected void Salir()
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
