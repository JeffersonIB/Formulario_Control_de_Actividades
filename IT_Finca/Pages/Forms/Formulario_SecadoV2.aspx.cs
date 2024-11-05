using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace IT_Finca.Pages.Forms
{
    public partial class Formulario_SecadoV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposCafe();
            }
        }
        private void CargarTiposCafe()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_FNC00405", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    GridViewTiposCafe.DataSource = reader;
                    GridViewTiposCafe.DataBind();
                }
            }
        }
        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            bool algunSeleccionado = false;
            List<int> tiposCafeSeleccionados = new List<int>();
            foreach (GridViewRow row in GridViewTiposCafe.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    algunSeleccionado = true;
                    int idTipoCafe = Convert.ToInt32(GridViewTiposCafe.DataKeys[row.RowIndex].Value);
                    tiposCafeSeleccionados.Add(idTipoCafe);
                }
            }
            if (algunSeleccionado)
            {
                MostrarDatosPorTipoCafe(tiposCafeSeleccionados);
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona al menos un tipo de café.";
            }
        }
        private void MostrarDatosPorTipoCafe(List<int> tiposCafeSeleccionados)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_FNC00602_5", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    string tiposCafe = string.Join(",", tiposCafeSeleccionados);
                    cmd.Parameters.Add("@Id_Tipo_Cafe", SqlDbType.NVarChar).Value = tiposCafe;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        GridViewResultados.DataSource = reader;
                        GridViewResultados.DataBind();
                    }

                    reader.Close();
                }
            }
        }
        protected void btnInsertarSeleccionados_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                foreach (GridViewRow row in GridViewResultados.Rows)
                {
                    HtmlInputCheckBox chkBox = (HtmlInputCheckBox)row.FindControl("chkInsertar");
                    if (chkBox != null && chkBox.Checked)
                    {
                        int idBeneficio = Convert.ToInt32(row.Cells[0].Text);
                        int idEmpresa = Convert.ToInt32(row.Cells[1].Text);
                        int idFinca = Convert.ToInt32(row.Cells[2].Text);
                        int idLote = Convert.ToInt32(row.Cells[3].Text);
                        int idProceso = Convert.ToInt32(row.Cells[4].Text);
                        int idActividad = Convert.ToInt32(row.Cells[5].Text);
                        int resultado = Convert.ToInt32(row.Cells[6].Text);

                        InsertarRegistro(con, idBeneficio, idEmpresa, idFinca, idLote, idProceso, idActividad, resultado);
                    }
                }
            }
        }
        private void InsertarRegistro(SqlConnection con, int idBeneficio, int idEmpresa, int idFinca, int idLote, int idProceso, int idActividad, decimal resultado)
        {
            using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_DATOS", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Beneficio", idBeneficio);
                cmd.Parameters.AddWithValue("@Id_Empresa", idEmpresa);
                cmd.Parameters.AddWithValue("@Id_Finca", idFinca);
                cmd.Parameters.AddWithValue("@Id_Lote", idLote);
                cmd.Parameters.AddWithValue("@Id_Proceso", idProceso);
                cmd.Parameters.AddWithValue("@Id_Actividad", idActividad);
                cmd.Parameters.AddWithValue("@Resultado", resultado);
                cmd.ExecuteNonQuery();
            }
        }
    }
}