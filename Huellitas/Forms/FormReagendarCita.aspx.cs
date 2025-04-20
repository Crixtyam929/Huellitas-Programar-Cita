using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Huellitas.Forms
{
    public partial class FormReagendarCita : System.Web.UI.Page
    {
        string connectionString = "Data Source=.;Initial Catalog=HuellitasDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarServicios();

                // Obtener ID de cita (puede venir por querystring)
                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"];
                    CargarDatosCita(id);
                }
            }
        }

        private void CargarServicios()
        {
            ddlServicios.Items.Clear();
            ddlServicios.Items.Add("Seleccionar el servicio");
            ddlServicios.Items.Add("Chequeo");
            ddlServicios.Items.Add("Vacunación");
            ddlServicios.Items.Add("Desparasitación");
            ddlServicios.Items.Add("Cirugía");
            ddlServicios.Items.Add("Peluquería");
        }

        private void CargarDatosCita(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT NombreMascota, Servicio, Fecha, Hora FROM Citas WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblNombreMascota.Text = reader["NombreMascota"].ToString();
                    ddlServicios.SelectedValue = reader["Servicio"].ToString();
                    txtFecha.Text = Convert.ToDateTime(reader["Fecha"]).ToString("yyyy-MM-dd");
                    txtHora.Text = reader["Hora"].ToString();
                }
                conn.Close();
            }
        }

        protected void btnReagendar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null) return;

            string id = Request.QueryString["id"];
            string servicio = ddlServicios.SelectedValue;
            string fecha = txtFecha.Text;
            string hora = txtHora.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Citas SET Servicio = @Servicio, Fecha = @Fecha, Hora = @Hora WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Servicio", servicio);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(fecha));
                cmd.Parameters.AddWithValue("@Hora", hora);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Redireccionar después de actualizar
            Response.Redirect("FormGestionCitas.aspx");
        }
    }
}