using System;
using System.Data.SqlClient;

namespace Huellitas.Forms
{
    public partial class FormCancelarCita : System.Web.UI.Page
    {
        string connectionString = "Data Source=.;Initial Catalog=HuellitasDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    CargarDatosCita(id);
                }
            }
        }

        private void CargarDatosCita(string citaId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT NombreMascota, Servicio, Fecha, Hora FROM Citas WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", citaId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nombreMascota.Text = reader["NombreMascota"].ToString();
                    servicio.Text = reader["Servicio"].ToString();
                    fecha.Text = Convert.ToDateTime(reader["Fecha"]).ToString("yyyy-MM-dd");
                    hora.Text = reader["Hora"].ToString();
                }
                conn.Close();
            }
        }

        protected void btnCancelarCita_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Citas WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Redirige después de eliminar
                Response.Redirect("FormGestionCitas.aspx");
            }
        }
    }
}