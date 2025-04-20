using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huellitas.Forms
{
    public partial class FormGestionCitas : System.Web.UI.Page
    {
        // Reemplazar con tu propia cadena de conexión
        string connectionString = "Data Source=.;Initial Catalog=HuellitasDB;Integrated Security=True";



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCitas();
            }
        }


        private void CargarCitas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, NombreMascota, Servicio, Fecha, Hora, Telefono, Correo FROM Citas";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptCitas.DataSource = dt;
                rptCitas.DataBind();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string citaId = btn.CommandArgument;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Citas SET Confirmada = 1 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", citaId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Refresca la lista después de confirmar
            CargarCitas();
        }

        protected void btnBuscarFecha_Click(object sender, EventArgs e)
        {
            string fechaSeleccionada = txtBuscarFecha.Text;

            if (!string.IsNullOrEmpty(fechaSeleccionada))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, NombreMascota, Servicio, Fecha, Hora, Telefono, Correo FROM Citas WHERE CONVERT(date, Fecha) = @Fecha";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(fechaSeleccionada));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptCitas.DataSource = dt;
                    rptCitas.DataBind();
                }
            }
            else
            {
                // Si no hay fecha, recargar todas las citas
                CargarCitas();
            }


        }
    }
}