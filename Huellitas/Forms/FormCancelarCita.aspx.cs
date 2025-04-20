using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                if (string.IsNullOrEmpty(id))
                {
                    // Redirigir si no hay ID
                    Response.Redirect("FormGestionCitas.aspx");
                }
            }
        }

        protected void btnCancelarCita_Click(object sender, EventArgs e)
        {
            string citaId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(citaId))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Citas WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", citaId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Redirigir al formulario de gestión de citas
            Response.Redirect("FormGestionCitas.aspx");
        }
    }
}