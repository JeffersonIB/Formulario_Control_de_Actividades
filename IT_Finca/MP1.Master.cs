using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;

namespace IT_Finca
{
    public partial class MP1 : System.Web.UI.MasterPage
    {
        int Id_Rol = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Configura la respuesta HTTP para evitar que se almacene en caché
            Response.AppendHeader("Cache-Control", "no-store");
            // Verifica si el usuario está autenticado y configura los elementos de la página
            if (Session["Usuario"] != null)
            {
                Id_Rol = Convert.ToInt32(Session["Id_Rol"].ToString());
                lblFinca.Text = Session["Finca"].ToString();
                lblNombre.Text = Session["Nombre"].ToString();
                lblApellido.Text = Session["Apellido"].ToString();                
                divuser.Visible = true;
                Permisos(Id_Rol);

                // Actualiza el tiempo de última actividad de la sesión
                Session["LastActivity"] = DateTime.Now;
                DateTime lastActivity = (DateTime)Session["LastActivity"];
                TimeSpan timeSinceLastActivity = DateTime.Now - lastActivity;
                if (timeSinceLastActivity.TotalMinutes > Session.Timeout)
                {
                    // La sesión ha expirado
                    FormsAuthentication.SignOut();
                    HttpContext.Current.Session.Abandon();
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                // Si el usuario no está autenticado, oculta los elementos de la página y lo dirigien al Login
                divuser.Visible = false;
                lblNombre.Text = string.Empty;
                lblApellido.Text = string.Empty;
                Response.Redirect("~/Default.aspx");
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        //Roles Permisos
        void Permisos(int Id_Rol)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00209", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = Convert.ToInt32(Session["Id_Usuario"].ToString());
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                bool
                    PrimeraPag, SegundaPag, TercerPag, CuartaPag, QuintaPag, SextaPag, SeptimaPag, OctavaPag, NovenaPag, DecimaPag,
                    DecimaprimeraPag, DecimasegundaPag, DecimaterceraPag, DecimacuartaPag, DecimaquintaPag, DecimasextaPag, DecimaseptimaPag, DecimaoctavaPag, DecimanovenaPag, VigesimaPag,
                    VigesimaprimeraPag, VigesimasegundaPag, VigesimaterceraPag, VigesimacuartaPag, VigesimaquintaPag, VigesimasextaPag, VigesimaseptimaPag, VigesimaoctavaPag, VigesimanovenaPag, TrigesimaPag,
                    TrigesimaprimeraPag, TrigesimasegundaPag, TrigesimaterceraPag, TrigesimacuartaPag, TrigesimaquintaPag; /* TrigesimasextaPag, TrigesimaseptimaPag, TrigesimaoctavaPag, TrigesimanovenaPag, CuadragesimaPag,
                    CuadragesimaprimeraPag, CuadragesimasegundaPag, CuadragesimaterceraPg, CuadragesimacuartaPag, CuadragesimaquintaPag, CuadragesimasextaPag, CuadragesimaseptimaPag, CuadragesimaoctavaPag, CuadragesimanovenaPag, QuincuagesimaPag,
                    QuincuagesimaprimeraPag, QuincuagesimasegundaPag, QuincuagesimaterceraPag, QuincuagesimacuartaPag, QuincuagesimaquintaPag, QuincuagesimasextaPag, QuincuagesimaseptimaPag, QuincuagesimaoctavaPag, QuincuagesimanovenaPag, SexagesimaPag,
                    SexagesimaprimeraPag, SexagesimasegundaPag, SexagesimaterceraPag, SexagesimacuartaPag, SexagesimaquintaPag, SexagesimasextaPag, SexagesimaseptimaPag, SexagesimaoctavaPag, SexagesimanovenaPag, SeptuagesimaPag;*/

                while (reader.Read())
                {
                    switch (reader[0].ToString())
                    {
                        case "Primera_Pag":
                            PrimeraPag = Convert.ToBoolean(reader[1].ToString());
                            if (PrimeraPag)
                            {
                                Primer_Men.Visible = true;
                                Primera_Pag.Visible = true;
                            }
                            else
                            {
                                Primer_Men.Visible = false;
                                Primera_Pag.Visible = false;
                            }
                            break;
                        case "Segunda_Pag":
                            SegundaPag = Convert.ToBoolean(reader[1].ToString());
                            if (SegundaPag)
                            {
                                Primer_Men.Visible = true;
                                Segunda_Pag.Visible = true;
                            }
                            else
                            {
                                Primer_Men.Visible = false;
                                Segunda_Pag.Visible = false;
                            }
                            break;
                        case "Tercera_Pag":
                            TercerPag = Convert.ToBoolean(reader[1].ToString());
                            if (TercerPag)
                            {
                                Primer_Men.Visible = true;
                                Tercera_Pag.Visible = true;
                            }
                            else
                            {
                                Primer_Men.Visible = false;
                                Tercera_Pag.Visible = false;
                            }
                            break;
                        case "Cuarta_Pag":
                            CuartaPag = Convert.ToBoolean(reader[1].ToString());
                            if (CuartaPag)
                            {
                                Primer_Men.Visible = true;
                                Cuarta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Cuarta_Pag.Visible = false;
                            }
                            break;
                        case "Quinta_Pag":
                            QuintaPag = Convert.ToBoolean(reader[1].ToString());
                            if (QuintaPag)
                            {
                                Primer_Men.Visible = true;
                                Quinta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Quinta_Pag.Visible = false;
                            }
                            break;
                        case "Sexta_Pag":
                            SextaPag = Convert.ToBoolean(reader[1].ToString());
                            if (SextaPag)
                            {
                                Primer_Men.Visible = true;
                                Sexta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Sexta_Pag.Visible = false;
                            }
                            break;
                        case "Septima_Pag":
                            SeptimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (SeptimaPag)
                            {
                                Primer_Men.Visible = true;
                                Septima_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Septima_Pag.Visible = false;
                            }
                            break;
                        case "Octava_Pag":
                            OctavaPag = Convert.ToBoolean(reader[1].ToString());
                            if (OctavaPag)
                            {
                                Segundo_Men.Visible = true;
                                Octava_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Octava_Pag.Visible = false;
                            }
                            break;
                        case "Novena_Pag":
                            NovenaPag = Convert.ToBoolean(reader[1].ToString());
                            if (NovenaPag)
                            {
                                Segundo_Men.Visible = true;
                                Novena_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Novena_Pag.Visible = false;
                            }
                            break;
                        case "Decima_Pag":
                            DecimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaPag)
                            {
                                Segundo_Men.Visible = true;
                                Decima_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Decima_Pag.Visible = false;
                            }
                            break;
                        case "DecimaPrimera_Pag":
                            DecimaprimeraPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaprimeraPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaPrimera_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaPrimera_Pag.Visible = false;
                            }
                            break;
                        case "DecimaSegunda_Pag":
                            DecimasegundaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimasegundaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaSegunda_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaSegunda_Pag.Visible = false;
                            }
                            break;
                        case "DecimaTercera_Pag":
                            DecimaterceraPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaterceraPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaTercera_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaTercera_Pag.Visible = false;
                            }
                            break;
                        case "DecimaCuarta_Pag":
                            DecimacuartaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimacuartaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaCuarta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaCuarta_Pag.Visible = false;
                            }
                            break;
                        case "DecimaQuinta_Pag":
                            DecimaquintaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaquintaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaQuinta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaQuinta_Pag.Visible = false;
                            }
                            break;
                        case "DecimaSexta_Pag":
                            DecimasextaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimasextaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaSexta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaSexta_Pag.Visible = false;
                            }
                            break;
                        case "DecimaSeptima_Pag":
                            DecimaseptimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaseptimaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaSeptima_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaSeptima_Pag.Visible = false;
                            }
                            break;
                        case "DecimaOctava_Pag":
                            DecimaoctavaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimaoctavaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaOctava_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaOctava_Pag.Visible = false;
                            }
                            break;
                        case "DecimaNovena_Pag":
                            DecimanovenaPag = Convert.ToBoolean(reader[1].ToString());
                            if (DecimanovenaPag)
                            {
                                Segundo_Men.Visible = true;
                                DecimaNovena_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                DecimaNovena_Pag.Visible = false;
                            }
                            break;
                        case "Vigesima_Pag":
                            VigesimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimaPag)
                            {
                                Segundo_Men.Visible = true;
                                Vigesima_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                Vigesima_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaPrimera_Pag":
                            VigesimaprimeraPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimaprimeraPag)
                            {
                                Primer_Men.Visible = true;
                                VigesimaPrimera_Pag.Visible = true;
                            }
                            else
                            {
                                //Primer_Men.Visible = false;
                                VigesimaPrimera_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaSegunda_Pag":
                            VigesimasegundaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimasegundaPag)
                            {
                                Primer_Men.Visible = true;
                                VigesimaSegunda_Pag.Visible = true;
                            }
                            else
                            {
                                //Primer_Men.Visible = false;
                                VigesimaSegunda_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaTercera_Pag":
                            VigesimaterceraPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimaterceraPag)
                            {
                                Primer_Men.Visible = true;
                                VigesimaTercera_Pag.Visible = true;
                            }
                            else
                            {
                                //Primer_Men.Visible = false;
                                VigesimaTercera_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaCuarta_Pag":
                            VigesimacuartaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimacuartaPag)
                            {
                                Segundo_Men.Visible = true;
                                VigesimaCuarta_Pag.Visible = true;
                            }
                            else
                            {
                                //Segundo_Men.Visible = false;
                                VigesimaCuarta_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaQuinta_Pag":
                            VigesimaquintaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimaquintaPag)
                            {
                                Primer_Men.Visible = true;
                                VigesimaQuinta_Pag.Visible = true;
                            }
                            else
                            {
                                Primer_Men.Visible = false;
                                VigesimaQuinta_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaSexta_Pag":
                            VigesimasextaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimasextaPag)
                            {
                                Primer_Men.Visible = true;
                                VigesimaSexta_Pag.Visible = true;
                            }
                            else
                            {
                                Primer_Men.Visible = false;
                                VigesimaSexta_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaSeptima_Pag":
                            VigesimaseptimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimaseptimaPag)
                            {
                                Cuarto_Men.Visible = true;
                                VigesimaSeptima_Pag.Visible = true;
                            }
                            else
                            {
                                //Tercer_Men.Visible = false;
                                VigesimaSeptima_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaOctava_Pag":
                            VigesimaoctavaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimaoctavaPag)
                            {
                                Cuarto_Men.Visible = true;
                                VigesimaOctava_Pag.Visible = true;
                            }
                            else
                            {
                                //Tercer_Men.Visible = false;
                                VigesimaOctava_Pag.Visible = false;
                            }
                            break;
                        case "VigesimaNovena_Pag":
                            VigesimanovenaPag = Convert.ToBoolean(reader[1].ToString());
                            if (VigesimanovenaPag)
                            {
                                Quinto_Men.Visible = true;
                                VigesimaNovena_Pag.Visible = true;
                            }
                            else
                            {
                                //Tercer_Men.Visible = false;
                                VigesimaNovena_Pag.Visible = false;
                            }
                            break;
                        case "Trigesima_Pag":
                            TrigesimaPag = Convert.ToBoolean(reader[1].ToString());
                            if (TrigesimaPag)
                            {
                                Quinto_Men.Visible = true;
                                Trigesima_Pag.Visible = true;
                            }
                            else
                            {
                                //Tercer_Men.Visible = false;
                                Trigesima_Pag.Visible = false;
                            }
                            break;
                        case "TrigesimaPrimera_Pag":
                            TrigesimaprimeraPag = Convert.ToBoolean(reader[1].ToString());
                            if (TrigesimaprimeraPag)
                            {
                                Quinto_Men.Visible = true;
                                TrigesimaPrimera_Pag.Visible = true;
                            }
                            else
                            {
                                //Tercer_Men.Visible = false;
                                TrigesimaPrimera_Pag.Visible = false;
                            }
                            break;
                        case "TrigesimaSegunda_Pag":
                            TrigesimasegundaPag = Convert.ToBoolean(reader[1].ToString());
                            if (TrigesimasegundaPag)
                            {
                                Quinto_Men.Visible = true;
                                TrigesimaSegunda_Pag.Visible = true;
                            }
                            else
                            {
                                //Cuarto_Men.Visible = false;
                                TrigesimaSegunda_Pag.Visible = false;
                            }
                            break;
                        case "TrigesimaTercera_Pag":
                            TrigesimaterceraPag = Convert.ToBoolean(reader[1].ToString());
                            if (TrigesimaterceraPag)
                            {
                                Quinto_Men.Visible = true;
                                TrigesimaTercera_Pag.Visible = true;
                            }
                            else
                            {
                                //Cuarto_Men.Visible = false;
                                TrigesimaTercera_Pag.Visible = false;
                            }
                            break;
                        case "TrigesimaCuarta_Pag":
                            TrigesimacuartaPag = Convert.ToBoolean(reader[1].ToString());
                            if (TrigesimacuartaPag)
                            {
                                Sexto_Men.Visible = true;
                                TrigesimaCuarta_Pag.Visible = true;
                            }
                            else
                            {
                                //Cuarto_Men.Visible = false;
                                TrigesimaCuarta_Pag.Visible = false;
                            }
                            break;
                        case "TrigesimaQuinta_Pag":
                            TrigesimaquintaPag = Convert.ToBoolean(reader[1].ToString());
                            if (TrigesimaquintaPag)
                            {
                                Sexto_Men.Visible = true;
                                TrigesimaQuinta_Pag.Visible = true;
                            }
                            else
                            {
                                //Cuarto_Men.Visible = false;
                                TrigesimaQuinta_Pag.Visible = false;
                            }
                            break;
                    }
                }
                con.Close();
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Boton Salir
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