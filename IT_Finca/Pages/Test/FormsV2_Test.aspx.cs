using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace IT_Finca.Pages.Test
{
    public partial class FormuarioV2_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosEnGridView();
            }
        }
        private DataTable ObtenerDataTableDesdeViewState()
        {
            return ViewState["DataTable"] as DataTable ?? new DataTable();
        }
        private void GuardarDataTableEnViewState(DataTable dt)
        {
            ViewState["DataTable"] = dt;
        }
        private void CargarDatosEnGridView()
        {
            DataTable dt = ObtenerDataTableDesdeViewState();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID", typeof(int));  // Asegúrate de agregar la columna 'ID'
            //dt.Columns.Add("Informacion", typeof(string));
            //dt.Columns.Add("Opcion", typeof(string));

            // Agrega algunas filas de ejemplo
            //dt.Rows.Add("Dato1", "Opcion1");
            //dt.Rows.Add("Dato2", "Opcion2");

            GV_Actividad1.DataSource = dt;
            GV_Actividad1.DataBind();

            GuardarDataTableEnViewState(dt);
        }
        protected void Agregar_Actividad1_OnClick(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox
            string informacion = txtInformacion.Text;

            // Obtener el valor seleccionado del DropDownList
            string opcionSeleccionada = ddlOpciones.SelectedValue;

            // Lógica adicional según tus necesidades

            // Luego, puedes agregar esta información al GridView
            AgregarFilaAlGridView(informacion, opcionSeleccionada);
        }
        private void AgregarFilaAlGridView(string informacion, string opcion)
        {
            DataTable dt = ObtenerDataTableDesdeViewState();
            //DataTable dt;

            // Verificar si el GridView tiene datos previos
            if (GV_Actividad1.DataSource == null)
            {
                dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Informacion", typeof(string));
                dt.Columns.Add("Opcion", typeof(string));
            }
            else
            {
                // Si el GridView ya tiene datos, obtener el DataTable existente
                dt = (DataTable)GV_Actividad1.DataSource;
            }

            // Crear una nueva fila y agregarla al DataTable
            DataRow newRow = dt.NewRow();
            newRow["ID"] = dt.Rows.Count + 1; // Asignar un ID único
            newRow["Informacion"] = informacion;
            newRow["Opcion"] = opcion;
            dt.Rows.Add(newRow);

            // Actualizar el DataSource del GridView y enlazar los datos
            GV_Actividad1.DataSource = dt;
            GV_Actividad1.DataBind();

            GuardarDataTableEnViewState(dt);
        }

        protected void GV_Actividad1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Habilitar el modo de edición
            GV_Actividad1.EditIndex = e.NewEditIndex;
            CargarDatosEnGridView(); // Utiliza el nombre correcto del método

        }
        protected void GV_Actividad1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancelar la edición y regresar al modo normal
            GV_Actividad1.EditIndex = -1;
            CargarDatosEnGridView(); // Asegúrate de usar el método correcto para cargar datos en el GridView
        }

        protected void GV_Actividad1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Actualizar los datos modificados
            int rowIndex = e.RowIndex;
            GridViewRow row = GV_Actividad1.Rows[rowIndex];
            // Obtener los nuevos valores editados
            string nuevaInformacion = e.NewValues["Informacion"].ToString();
            string nuevaOpcion = e.NewValues["Opcion"].ToString();
            // Actualizar el DataTable y el GridView
            ActualizarFilaEnGridView(rowIndex, nuevaInformacion, nuevaOpcion);
            GV_Actividad1.EditIndex = -1;
            CargarDatosEnGridView();
        }

        private void ActualizarFilaEnGridView(int rowIndex, string nuevaInformacion, string nuevaOpcion)
        {
            DataTable dt = ObtenerDataTableDesdeViewState();
            //DataTable dt = (DataTable)GV_Actividad1.DataSource;

            // Actualizar los valores en el DataTable
            dt.Rows[rowIndex]["Informacion"] = nuevaInformacion;
            dt.Rows[rowIndex]["Opcion"] = nuevaOpcion;

            // Actualizar el GridView
            //GV_Actividad1.EditIndex = -1;
            //GV_Actividad1.DataSource = dt;
            //GV_Actividad1.DataBind();

            GV_Actividad1.DataSource = dt;
            GV_Actividad1.DataBind();

            GuardarDataTableEnViewState(dt);
        }

        protected void GV_Actividad1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Eliminar la fila seleccionada
            int rowIndex = e.RowIndex;
            EliminarFilaEnGridView(rowIndex);
        }
        private void EliminarFilaEnGridView(int rowIndex)
        {
            DataTable dt = ObtenerDataTableDesdeViewState();
            //DataTable dt = (DataTable)GV_Actividad1.DataSource;

            // Eliminar la fila del DataTable
            dt.Rows.RemoveAt(rowIndex);

            // Actualizar el GridView
            //GV_Actividad1.EditIndex = -1;
            //GV_Actividad1.DataSource = dt;
            //GV_Actividad1.DataBind();

            GV_Actividad1.DataSource = dt;
            GV_Actividad1.DataBind();

            GuardarDataTableEnViewState(dt);
        }
    }
}