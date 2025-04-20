using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huellitas.Forms
{
    public partial class FormReagendarCita : System.Web.UI.Page
    {
        // Reemplaza con tu cadena de conexión real
        private string cadenaConexion = "TuCadenaDeConexion";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID de la cita desde la QueryString (ej: ReagendarCita.aspx?id=123)
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int idCita))
                {
                    hdnIdCita.Value = idCita.ToString();
                    CargarDatosCita(idCita);
                    CargarServicios();
                }
                else
                {
                    // Manejar el caso en que no se proporciona un ID de cita válido
                    lblMensaje.Text = "Error: No se proporcionó un ID de cita válido.";
                    btnReagendar.Enabled = false;
                }
            }
        }

        private void CargarDatosCita(int idCita)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string consulta = "SELECT c.Fecha, c.Hora, s.Nombre AS Servicio, m.Nombre AS MascotaNombre, c.IdServicio " +
                                      "FROM Citas c " +
                                      "INNER JOIN Servicios s ON c.IdServicio = s.IdServicio " +
                                      "INNER JOIN Mascotas m ON c.IdMascota = m.IdMascota " +
                                      "WHERE c.IdCita = @IdCita";
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdCita", idCita);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                txtFecha.Text = ((DateTime)lector["Fecha"]).ToString("yyyy-MM-dd");
                                txtHora.Text = ((TimeSpan)lector["Hora"]).ToString(@"hh\:mm");
                                lblNombreMascota.Text = lector["MascotaNombre"].ToString();

                                // Seleccionar el servicio actual en el DropDownList
                                string servicioActual = lector["Servicio"].ToString();
                                if (!string.IsNullOrEmpty(servicioActual))
                                {
                                    ddlServicio.Items.FindByText(servicioActual)?.Selected = true;
                                }
                            }
                            else
                            {
                                lblMensaje.Text = "Error: No se encontró la cita con el ID proporcionado.";
                                btnReagendar.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los datos de la cita: " + ex.Message;
                btnReagendar.Enabled = false;
            }
        }

        private void CargarServicios()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string consulta = "SELECT IdServicio, Nombre FROM Servicios";
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                ddlServicio.Items.Add(new ListItem(lector["Nombre"].ToString(), lector["IdServicio"].ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los servicios: " + ex.Message;
                btnReagendar.Enabled = false;
            }
        }

        protected void btnReagendar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (int.TryParse(hdnIdCita.Value, out int idCita))
                {
                    DateTime nuevaFecha;
                    TimeSpan nuevaHora;
                    int nuevoIdServicio;

                    if (DateTime.TryParse(txtFecha.Text, out nuevaFecha) &&
                        TimeSpan.TryParse(txtHora.Text, out nuevaHora) &&
                        int.TryParse(ddlServicio.SelectedValue, out nuevoIdServicio))
                    {
                        try
                        {
                            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                            {
                                conexion.Open();
                                string consulta = "UPDATE Citas SET IdServicio = @IdServicio, Fecha = @Fecha, Hora = @Hora WHERE IdCita = @IdCita";
                                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                                {
                                    comando.Parameters.AddWithValue("@IdServicio", nuevoIdServicio);
                                    comando.Parameters.AddWithValue("@Fecha", nuevaFecha.Date);
                                    comando.Parameters.AddWithValue("@Hora", nuevaHora);
                                    comando.Parameters.AddWithValue("@IdCita", idCita);

                                    int filasAfectadas = comando.ExecuteNonQuery();
                                    if (filasAfectadas > 0)
                                    {
                                        lblMensaje.Text = "La cita ha sido reagendada con éxito.";
                                        // Opcional: Redirigir a otra página o limpiar el formulario
                                    }
                                    else
                                    {
                                        lblMensaje.Text = "Error al reagendar la cita. Inténtelo de nuevo.";
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMensaje.Text = "Error al actualizar la base de datos: " + ex.Message;
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Por favor, ingrese una fecha y hora válidas, y seleccione un servicio.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Error: El ID de la cita no es válido.";
                }
            }
        }
    }
}